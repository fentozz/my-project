using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Management;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;
using System.IO.Compression;

namespace TaskCompress
{
    static class MonitorSys
    {
        /// <summary>
        /// Всего ОЗУ у компуктера (видимой) KiB
        /// </summary>
        public static double TotalRam { get; private set; }
        /// <summary>
        /// Свободная ОЗУ KiB
        /// </summary>
        public static double FreeRam { get; private set; }
        /// <summary>
        /// Сколько процесс использует ОЗУ Byte
        /// </summary>
        public static double CurrRam { get => Environment.WorkingSet; }
        /// <summary>
        /// Допустимое количество используемого ОЗУ KiB
        /// </summary>
        public static double PermRam { get => FreeRam * Procent; }
        /// <summary>
        /// Процент используемой ОЗУ ( от доступной ОЗУ )
        /// </summary>
        public static double Procent { get; set; } = 0.01;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="num">Сколько раз поделить на 1024</param>
        /// <returns></returns>
        public static ref double ConvertByte(ref double input, int num)
        {
            while (num-- != 0)
                input = input / 1024;
            return ref input;
        }
        public static double ConvertByte(double input, int num)
        {
            while (num-- != 0)
                input = input / 1024;
            return input;
        }
        private static ConnectionOptions connection = new ConnectionOptions();
        /// <summary>
        /// пространство имен
        /// </summary>
        private static ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
        /// <summary>
        /// запрос
        /// </summary>
        private static ObjectQuery query;
        static MonitorSys()
        {
            connection.Impersonation = ImpersonationLevel.Impersonate;//уровень
            scope.Connect();
        }
        /// <summary>
        /// Запрос текущих данных
        /// </summary>
        public static void CheckRam() 
        {
            query = new ObjectQuery("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            foreach (ManagementObject queryObj in searcher.Get())
            {
                TotalRam = double.Parse(queryObj["TotalVisibleMemorySize"].ToString());
                FreeRam = double.Parse(queryObj["FreePhysicalMemory"].ToString());
            }        
        }
        /// <summary>
        /// Количество ядер в процессоре
        /// </summary>
        public static int NumberCores { get; private set; }
        /// <summary>
        /// Запрос количества ядер
        /// </summary>
        public static void CheckCores()
        {
            query = new ObjectQuery("SELECT NumberOfEnabledCore FROM Win32_Processor");
            using ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            foreach (ManagementObject queryObj in searcher.Get())
                NumberCores = int.Parse(queryObj["NumberOfEnabledCore"].ToString());
            
        }
        /// <summary>
        /// Превышен ли лимит 
        /// </summary>
        /// <returns></returns>
        public static bool CheckLimit() => ConvertByte(CurrRam, 1) >= PermRam ? true : false;


    }

    class Program
    {
        static int Main(string[] args)
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                Console.WriteLine("Не реализовано в текущей системе");
                return 1;
            }

            if (args.Length == 1 && args[0] == "/?")
            {
                Console.WriteLine(" \n\rcompress/decompress [имя исходного файла] [имя результирующего файла] ");
                return 0;
            }
            else if (args.Length != 3)
            {
                Console.WriteLine("Неверное количество ключей");
                return 1;
            }

            try
            {
                Compress(in args[1], in args[2], SelOperation(in args[0]));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }

            return 0;//в багдаде всё спокойно
        }

        static bool SelOperation ( in string flagOperation) =>
            flagOperation.ToLower() switch
            {
                "compress" => true,
                "decompress" => false,
                _ => throw new Exception("Ошибка флага compress/decompress")
            };

        static object LockerCache = new object();
        static object LockerRead = new object();

        static SortedDictionary<int, byte[]> Cache = new SortedDictionary<int, byte[]>();

        static int ThreadCount;
        
        static long Sizeblock = 1024 * 1024;

        static AutoResetEvent ResetWrite = new AutoResetEvent(false);

        static void WriteFile(object x)
        {
            string path = (string)x.GetType().GetField("Item1").GetValue(x);
            CompressionMode compression = (CompressionMode)x.GetType().GetField("Item2").GetValue(x);

            FileStream file0;
            GZipStream gZip;
            BinaryWriter writer0;

            while (!Complete)
            {
                ResetWrite.WaitOne();

                if (Cache.Count == 0) 
                    continue;
                lock (LockerCache)
                {
                    switch (compression)
                    {
                        case CompressionMode.Compress:
                            file0 = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write);
                            gZip = new GZipStream(file0, compression);
                            writer0 = new BinaryWriter(gZip);
                            break;
                        case CompressionMode.Decompress:
                            file0 = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write);
                            gZip = null;
                            writer0 = new BinaryWriter(file0);
                            break;
                        default:
                            throw new Exception("Неверное значение CompressionMode");                            
                    }
                    
                    try
                    {
                        foreach (var el in Cache)                        
                            writer0.Write(el.Value);
                        
                        writer0.Flush();
                        writer0.BaseStream.Flush();
                    }
                    finally
                    {
                        writer0?.Close();
                        gZip?.Close();
                        file0?.Close();                    
                    }
                    Cache.Clear();
                }               

            }
        }

        /// <summary>
        /// Текущий блок
        /// </summary>
        static ulong currentBlock = 0;

        static void ReadFile(object e)
        {
            ulong backblock = 0;
            ulong currblock = 0;

            byte[] buff = null;

            lock (LockerRead)
            {
                buff = FileReader.ReadBytes((int)Sizeblock);
                if (buff.Length == 0)
                {
                    ThreadCount--;
                    return;
                }
                else
                {
                    currentBlock++;
                    backblock = currblock = currentBlock;
                }
            }

            while (/*buff.Length == sizeblocks &&*/ buff.Length != 0)
            {
                lock (LockerCache)
                {
                    if (Cache.ContainsKey((int)backblock))
                    {
                        if (Cache.Count == ThreadCount)
                        {
                            ResetWrite.Set();//записать кеш 
                            continue;//вернемся чтобы освободить кеш и дождаться его записи
                        }
                    }                    
                   
                    Cache.Add((int)currblock, buff);                    
                }

                lock (LockerRead)
                {
                    buff = FileReader.ReadBytes((int)Sizeblock);

                    if (buff.Length == 0)
                    {
                        ThreadCount--;
                        return;
                    }
                    else if (buff.Length != 0)
                    {
                        currentBlock++;
                        backblock = currblock;
                        currblock = currentBlock;
                    }                   

                }

            }
        }

        static BinaryReader FileReader;

        static bool Complete = false;

        static bool Compress(in string inpf, in string outf , bool compress = true )
        {
            if (!File.Exists(inpf))            
                throw new Exception("Неверный входной файл");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();            

            if (File.Exists(outf))
                File.Delete(outf);

            FileStream fileinput = File.OpenRead(inpf);
            CompressionMode mode;

            if (compress)
            {
                Console.WriteLine("Сжатие данных");
                mode = CompressionMode.Compress;
                FileReader = new BinaryReader(fileinput);
            }
            else
            {
                Console.WriteLine("Восстановление данных");
                mode = CompressionMode.Decompress;
                FileReader = new BinaryReader(new GZipStream(fileinput, CompressionMode.Decompress));
            }

            List<Thread> threadsReads = new List<Thread>();

            MonitorSys.CheckRam();
            MonitorSys.CheckCores();

            FileInfo info = new FileInfo(inpf);
            int sumblocks = (int)Math.Round((double)info.Length / Sizeblock);

            int maxthread = MonitorSys.NumberCores;

            //if (sumblocks <= 1)
            //    maxthread = 1;
            //else if (sumblocks > 1 && sumblocks <= 50)
            //    maxthread = 5;
            //else if (sumblocks > 50 && sumblocks <= 1000)
            //    maxthread = 100;
            //else
            //    maxthread = 1000;
            

            int counter = 0;

            do
            {
                threadsReads.Add(new Thread(new ParameterizedThreadStart(ReadFile)) { Name = counter.ToString() });
                counter++;
            }
            while (!MonitorSys.CheckLimit() && counter < maxthread);

            Console.WriteLine("");

            ThreadCount = threadsReads.Count;
            Console.WriteLine("Создано потоков " + (ThreadCount + 1).ToString());

            Thread threadWrite = new Thread(new ParameterizedThreadStart(WriteFile));
            threadWrite.Start((outf, mode));

            threadsReads.ForEach(q => q.Start());            

            threadsReads.ForEach(q => q.Join());

            ResetWrite.Set();//отправить последние блоки

            Complete = true;

            threadWrite.Join();

            FileReader.Close();
            fileinput.Close();

            
            Console.WriteLine("Размер входного файла :" + (info.Length/ 1024) .ToString());
            info = new FileInfo(outf);
            Console.WriteLine("Размер выходного файла :" + (info.Length/1024).ToString());

            TimeSpan tSpan;
            stopwatch.Stop();
            tSpan = stopwatch.Elapsed;
            Console.WriteLine("Потрачено времени: " + tSpan.ToString());

            //Console.WriteLine("Остаток в кеше: " + cache.Count.ToString());
            //Console.WriteLine("Незавершённые потоки: " + CacheCount.ToString());

            GC.Collect();

            return true;
        }
    }
}
