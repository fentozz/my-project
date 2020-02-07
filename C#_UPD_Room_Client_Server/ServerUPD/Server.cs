using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ClassUDP;

namespace ServerUPD
{
    /// <summary>
    /// Класс сервера
    /// </summary>
	class Server
	{
        /// <summary>
        /// флаг остановки прослушивания сообщений
        /// </summary>
        static bool stopped = false;
        public static void Main(string[] args)
		{
            UdpListenerHall HallServer = new UdpListenerHall(new IPEndPoint(IPAddress.Any, 1025));
            Console.WriteLine("Сервер холла запущен");
            Task.Factory.StartNew(async () => 
            {  
                while (!stopped)
                {
                    Received HallReceived = await HallServer.Receive();//Ждём ответа с сервера

                    _ = Task.Factory.StartNew(async () => //Создаем новый поток чтобы продолжить слушать
                    {
                        if (HallReceived is Received)
                            switch (HallReceived.Package.typeMessage)
                            {
                                case TypeMessage.Connect:
                                    HallServer.AddClient(HallReceived.Sender, HallReceived.Package.Name);
                                    break;
                                case TypeMessage.SupConnect:
                                    HallServer.SupClient(HallReceived.Sender, HallReceived.Package.Name);
                                    break;
                                case TypeMessage.Disconnect:
                                    HallServer.Disconnect(HallReceived.Sender, HallReceived.Package.Name);
                                    break;
                                case TypeMessage.HallAddRoom:
                                    HallServer.AddRoom(HallReceived);
                                    break;
                                case TypeMessage.HallDelRoom:
                                    HallServer.DeleteRoom(HallReceived);
                                    break;
                                case TypeMessage.ConnectToRoom:
                                    HallServer.ConnectedRoom(HallReceived);
                                    break;
                                case TypeMessage.CheckLisnting:
                                    HallServer.CheckList(HallReceived.Sender, HallReceived.Package.Name);
                                    break;
                            }
                    });

                }
            });
            //обрабатываем сообщения в консоли сервера
            while (true)
            {
                string contr = Console.ReadLine().ToLower();                
                switch (contr)
                {
                    case "stop":
                        Console.WriteLine("Остановка сервера");
                        stopped = true;
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
		}
	}
}
