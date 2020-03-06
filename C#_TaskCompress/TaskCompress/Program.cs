using System;

namespace TaskCompress
{
    
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

            Gzip gzip = new Gzip(1024 * 1024);
            return gzip.RunGzip(in args[1], in args[2], in args[0]) ? 0 : 1;
        }

        
    }
}
