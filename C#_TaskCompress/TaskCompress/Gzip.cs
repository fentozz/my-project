using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;

namespace TaskCompress
{
    public class Gzip
    {
        /// <summary>
        /// Синхронизация кэша
        /// </summary>
        private object LockerCache = new object();
        /// <summary>
        /// Синхронизация чтения
        /// </summary>
        private object LockerRead = new object();
        /// <summary>
        /// Кэш
        /// </summary>
        private SortedDictionary<int, byte[]> Cache = new SortedDictionary<int, byte[]>();
        /// <summary>
        /// Количество действующих потоков
        /// </summary>
        private int ThreadCount;       
        /// <summary>
        /// Синхронизация записи
        /// </summary>
        private AutoResetEvent ResetWrite = new AutoResetEvent(false);
        /// <summary>
        /// Текущий блок
        /// </summary>
        private ulong currentBlock = 0;
        /// <summary>
        /// Размер обрабатываемого блока
        /// </summary>
        public long Sizeblock { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizeblock">Размер блока</param>
        public Gzip(long sizeblock) => Sizeblock = sizeblock;
        /// <summary>
        /// Запуск операции
        /// </summary>
        /// <param name="compress">compress/decompress</param>
        /// <param name="inputFile">путь к входящему файлу</param>
        /// <param name="outFile">путь к конечному файлу(будет перезаписан)</param>
        /// <returns>успех миссии</returns>
        public bool RunGzip(in string inputFile, in string outFile, in string compress)
        {
            try
            {
               return Compress(in inputFile, in outFile, SelOperation(in compress));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Определение текущей операции
        /// </summary>
        /// <param name="flagOperation"></param>
        /// <returns>Сжатие или восстановление</returns>
        private bool SelOperation(in string flagOperation) =>
           flagOperation.ToLower() switch
           {
               "compress" => true,
               "decompress" => false,
               _ => throw new Exception("Ошибка флага compress/decompress")
           };
        /// <summary>
        /// Запись файла
        /// </summary>
        /// <param name="x"></param>
        private void WriteFile(object x)
        {
            string path = (string)x.GetType().GetField("Item1").GetValue(x);
            CompressionMode compression = (CompressionMode)x.GetType().GetField("Item2").GetValue(x);

            FileStream file;
            GZipStream gZip;
            BinaryWriter writer;

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
                            file = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write);
                            gZip = new GZipStream(file, compression);
                            writer = new BinaryWriter(gZip);
                            break;
                        case CompressionMode.Decompress:
                            file = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write);
                            gZip = null;
                            writer = new BinaryWriter(file);
                            break;
                        default:
                            throw new Exception("Неверное значение CompressionMode");
                    }

                    try
                    {
                        foreach (var el in Cache)
                            writer.Write(el.Value);

                        writer.Flush();
                        writer.BaseStream.Flush();
                    }
                    finally
                    {
                        writer?.Close();
                        gZip?.Close();
                        file?.Close();
                    }
                    Cache.Clear();
                }
            }
        }
        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="e"></param>
        private void ReadFile(object e)
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
                        if (Cache.Count >= ThreadCount)
                            ResetWrite.Set();//записать кеш 
                        continue;//вернемся чтобы освободить кеш и дождаться его записи
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
        /// <summary>
        /// Поток чтения
        /// </summary>
        BinaryReader FileReader;
        /// <summary>
        /// Завершение записи
        /// </summary>
        private bool Complete = false;
        /// <summary>
        /// Выполнение обработки файла
        /// </summary>
        /// <param name="inpf"></param>
        /// <param name="outf"></param>
        /// <param name="compress"></param>
        /// <returns></returns>
        private bool Compress(in string inpf, in string outf, bool compress = true)
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

            Console.WriteLine();

            while (true)
            {
                if (threadsReads.Select(q => q.Join(500)).All(q => q == true))
                    break;

                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                MonitorSys.CheckRam();
                Console.WriteLine("Расход ОЗУ : " + Math.Round(MonitorSys.ConvertByte(MonitorSys.CurrRam, 2), 1) + " MiB               ");
            }

            ResetWrite.Set();//отправить последние блоки

            Complete = true;

            threadWrite.Join();

            FileReader.Close();
            fileinput.Close();


            Console.WriteLine("Размер входного файла :" + (info.Length / 1024).ToString());
            info = new FileInfo(outf);
            Console.WriteLine("Размер выходного файла :" + (info.Length / 1024).ToString());

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
