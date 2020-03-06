using System;
using System.Management;

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

}
