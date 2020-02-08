using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ClassUDP
{
    #region Message
    /// <summary>
    /// Тип сообщения
    /// </summary>
    public enum TypeMessage
    {
        /// <summary>
        /// Добавление комнаты в холл
        /// </summary>
        HallAddRoom,
        /// <summary>
        /// Удаление комнаты из холла
        /// </summary>
        HallDelRoom,
        /// <summary>
        /// Подключение к серверу
        /// </summary>
        Connect,
        /// <summary>
        /// Запрос у сервера холла на подключение к комнате
        /// </summary>
        ConnectToRoom,
        /// <summary>
        /// Пингование сервера (холл/комната)
        /// </summary>
        SupConnect,
        /// <summary>
        /// Отключение от сервера
        /// </summary>
        Disconnect,
        /// <summary>
        /// Сообщение 
        /// </summary>
        Message,
        /// <summary>
        /// Лист с пользователями комнаты или комнатами в холле
        /// </summary>
        Listing,
        /// <summary>
        /// Запрос у сервера листа(пользователей/комнат)
        /// </summary>
        CheckLisnting,
        /// <summary>
        /// Ошибка
        /// </summary>
        Error
    }
    /// <summary>
    /// Структура сообщения
    /// </summary>
    [Serializable]
    public struct MessageObject
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public TypeMessage typeMessage;
        /// <summary>
        /// Имя отсылающего
        /// </summary>
        public string Name;
        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message;
        /// <summary>
        /// лист
        /// </summary>
        public List<string> listRoom;
        //нужно сделать через object

        /// <summary>
        /// не используется
        /// </summary>
        public void Clear()
        {
            typeMessage = TypeMessage.SupConnect;
            Message = "";
        }
    }
    /// <summary>
    /// Получаемое сообщение
    /// </summary>
    public struct Received
    {
        public IPEndPoint Sender;
        public MessageObject Package;

    }
    #endregion
    #region BaseUDP
    /// <summary>
    /// Базовый класс клиента и сервера
    /// </summary>
    public abstract class UdpBase
    {
        public UdpClient Client;
        public UdpBase()
        {
            Client = new UdpClient();
        }
        /// <summary>
        /// Прием сообщений
        /// Т.е получать обратно сообщения может и клиент
        /// </summary>
        /// <param name="time">true - Прервать ожидание после 3х секунд</param>
        /// <returns>Полученное сообщение</returns>
        public async Task<Received> Receive(bool time = false)
        {
            try
            {
                Task<UdpReceiveResult> result = Task.Run(() => Client.ReceiveAsync());
                if (time)
                {
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    while (!result.IsCompleted)
                        if (timer.Elapsed.TotalMilliseconds > 3000)
                            throw new Exception("Превышено время ожидания сервера");
                }
                else
                    await result;
                return new Received() { Package = HelpSerialize.DeSerialize(result.Result), Sender = result.Result.RemoteEndPoint };
            }
            catch (Exception ex)
            {
                return new Received() { Package = new MessageObject() { typeMessage = TypeMessage.Error, Message = ex.Message } };
            }
        }
    }
    #endregion
    #region ClientUDP
    /// <summary>
    /// Класс клиента
    /// </summary>
    public class UdpUser : UdpBase
    {
        private UdpUser() { }
        public static UdpUser ConnectTo(string hostname, int port)
        {
            UdpUser connection = new UdpUser();
            connection.Client.ExclusiveAddressUse = true;// одно подключение на один порт
            connection.Client.EnableBroadcast = true;//широковещательные пакеты
            ///Протокол(сокет) UDP только запоминает удалённый адрес, не производя с ним соединения
            connection.Client.Connect(hostname, port);
            return connection;
        }
        /// <summary>
        /// Отправка строки
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Количество отправленных байт</returns>
        public int Send(string message)
        {
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = message, typeMessage = TypeMessage.Message });
            return Client.Send(datagram, datagram.Length);
        }
        /// <summary>
        /// Отправка сериализированного обьекта(набора байт)
        /// </summary>
        /// <param name="datagram"></param>
        /// <returns>Количество отправленных байт</returns>
        public int Send(byte[] datagram) => Client.Send(datagram, datagram.Length);
    }
    #endregion
    #region ServerUDP
    /// <summary>
    /// Класс сервера холла
    /// </summary>
    public class UdpListenerHall : UdpBase
    {
        /// <summary>
        /// Клиенты в холле
        /// </summary>
        public struct ClientObj
        {
            public IPEndPoint iP;
            public string Name;
            public ClientObj(IPEndPoint ip, string name)
            {
                iP = ip;
                Name = name;
            }
        }
        /// <summary>
        /// Комнаты в холле
        /// </summary>
        public struct Room
        {
            public string NameCreator;
            public int portRoom;
            public string NameRoom;
            public string PassWord;
            public UdpListenerRoom RoomServer;
        }
        /// <summary>
        /// Словарь комнат
        /// </summary>
        Dictionary<int, bool> PortRooms = new Dictionary<int, bool>();
        /// <summary>
        /// прослушивание
        /// </summary>
        public bool EnableListen = true;
        /// <summary>
        /// Обьект для синхранизации
        /// </summary>
        static object locker = new object();
        /// <summary>
        /// Список клиентов в холле
        /// </summary>
        Dictionary<String, ClientObj> DictClient = new Dictionary<string, ClientObj>();
        /// <summary>
        /// Список комнат в холле
        /// </summary>
        Dictionary<string, Room> ServerRooms = new Dictionary<string, Room>();
        /// <summary>
        /// Локальная точка для прослушивания
        /// </summary>
        public IPEndPoint _listenOn;
        /// <summary>
        /// слушать входящие со всех адерсов на указанный порт
        /// </summary>
        public UdpListenerHall() : this(new IPEndPoint(IPAddress.Any, 1025)) { }
        public UdpListenerHall(IPEndPoint endpoint)
        {
            //PortRooms = Enumerable.Range(1026, 64500).Zip(Enumerable.Repeat(false, 64500), (d, b) => (used: b, port: d)).ToList();
            PortRooms = Enumerable.Range(1026, 64500).Zip(Enumerable.Repeat(false, 64500), (d, b) => new { used = b, port = d }).ToDictionary(q => q.port, q => q.used);
            _listenOn = endpoint;
            Client = new UdpClient(_listenOn);
        }
        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="endpoint">Конечная точка</param>
        /// <param name="name">Его имя</param>
        public void AddClient(IPEndPoint endpoint, string name)
        {
            byte[] datagram = new byte[0];
            if (!DictClient.Values.Any(q => q.Name == name || name == "serv"))
            {
                DictClient.Add(name, new ClientObj(endpoint, name));
                datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Успешное подключение", typeMessage = TypeMessage.Connect });
                this.massMessage("Подключился. \r\nИмя : " + name, "");
                Console.WriteLine("-------------\r\nКлиент подключился к холлу.\r\nВремя: " + DateTime.Now.TimeOfDay.ToString() + "\r\nip :" + endpoint.Address.ToString() + "\r\nИмя : " + name + "\r\n-------------");
            }
            else
                datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Пользователь с таким именем уже существует", typeMessage = TypeMessage.Disconnect });
            lock (locker)
                Client.Send(datagram, datagram.Length, endpoint);
            // if(typeServer == TypeServer.Room) this.massMessage(DictClient.Values.ToList().Select(q => q.Name).ToList(), name);
            this.massMessage(ServerRooms.Keys.ToList(), name);
        }
        /// <summary>
        /// Новая комната
        /// </summary>
        /// <param name="received"></param>
        public void AddRoom(Received received)
        {
            byte[] datagram = new byte[0];
            string[] ParamServ = received.Package.Message.Split(',');
            if (ServerRooms.Keys.Any(q => q == ParamServ[0]))
            {
                datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Невозможно создать комнату c таким названием " + ParamServ[0], typeMessage = TypeMessage.HallAddRoom });
                lock (locker)
                    Client.Send(datagram, datagram.Length, received.Sender);
                Console.WriteLine("-------------\r\nКлиенту не удалось создать комнату из-за неправильного названия." +
                "\r\nИмя клиента : " + received.Package.Name +
                "\r\nВремя: " + DateTime.Now.TimeOfDay.ToString() +
                "\r\nНазвание комнаты :" + ParamServ[0] +
                "\r\nПароль :" + ParamServ[1] +
                //"\r\nПорт :" + (port - 1).ToString() +
                "\r\n-------------");
            }
            int port = 0;
            lock (locker)
                port = PortRooms.FirstOrDefault(q => q.Value == false).Key;
            if (port == 0)
            {
                datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Невозможно создать комнату. Место закончилось " + ParamServ[0], typeMessage = TypeMessage.HallAddRoom });
                lock (locker)
                    Client.Send(datagram, datagram.Length, received.Sender);
                Console.WriteLine("-------------\r\nКлиенту не удалось создать комнату из-за не хватки свободных портов." +
                "\r\nИмя клиента : " + received.Package.Name +
                "\r\nВремя: " + DateTime.Now.TimeOfDay.ToString() +
                "\r\nНазвание комнаты :" + ParamServ[0] +
                "\r\nПароль :" + ParamServ[1] +
                "\r\nПорт :" + (port - 1).ToString() +
                "\r\n-------------");
                return;
            }
            lock (locker)
                PortRooms[port] = true;//указываем что порт занят 

            UdpListenerRoom newroom = null;
            try
            {
                newroom = new UdpListenerRoom(new IPEndPoint(IPAddress.Any, port));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ServerRooms.Add(ParamServ[0], new Room() { NameCreator = received.Package.Name, portRoom = port, NameRoom = ParamServ[0], PassWord = ParamServ[1], RoomServer = newroom });
            //Console.WriteLine("Сервер комнаты запущен " + ParamServ[0]);
            Console.WriteLine("-------------\r\nКлиент создал комнату в холле." +
                "\r\nИмя клиента : " + received.Package.Name +
                "\r\nВремя: " + DateTime.Now.TimeOfDay.ToString() +
                "\r\nНазвание комнаты :" + ParamServ[0] +
                "\r\nПароль :" + ParamServ[1] +
                "\r\nПорт :" + (port).ToString() +
                "\r\n-------------");
            //новый сервер слушает сообщения 
            newroom.RunListen();
            datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Успешное создание комнаты " + ParamServ[0], typeMessage = TypeMessage.HallAddRoom });
            lock (locker)
                Client.Send(datagram, datagram.Length, received.Sender);

            this.massMessage(ServerRooms.Keys.ToList(), received.Package.Name);
        }
        /// <summary>
        /// Удооолить комнату к хуям собачьим
        /// </summary>
        /// <param name="received"></param>
        public void DeleteRoom (Received received)
        {
            byte[] datagram = new byte[0];
            string[] ParamServ = received.Package.Message.Split(',');

            if (!(new Func<string, string, bool>((ip,pass) => 
            {
                lock (locker)
                {
                    if (!ServerRooms.Keys.Any(q => q == ip)) return false;//проверяем имя
                    if (ServerRooms[ip].PassWord != pass) return false;//проверяем пароль
                    ServerRooms[ip].RoomServer.connectsrc?.Cancel(); //EnableListen = false;//выключаем получение сообщений
                    ServerRooms[ip].RoomServer.Client.Close();//закрываем соединение
                    PortRooms[ServerRooms[ip].portRoom] = false;//освобождаем порт
                    return ServerRooms.Remove(ip);
                }
                
            }).Invoke(ParamServ[0], ParamServ[1])))
            {
                
                Console.WriteLine("Пользователь \""+ received.Package.Name+"\" попытался удалить комнату " + ParamServ[0]);
                datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Не удалось удалить комнату " + ParamServ[0], typeMessage = TypeMessage.HallDelRoom });

                lock (locker)
                    Client.Send(datagram, datagram.Length, received.Sender);
                return;
            }
            Console.WriteLine("Сервер комнаты удалён " + ParamServ[0]);
            datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Комната успешно удалена " + ParamServ[0], typeMessage = TypeMessage.HallDelRoom });

            lock (locker)
                Client.Send(datagram, datagram.Length, received.Sender);
            this.massMessage(ServerRooms.Keys.ToList(), received.Package.Name);

        }
        /// <summary>
        /// Подключение к комнате
        /// Получает имя и пароль от комнаты к которой пользователь хочет подключится.
        /// Возвращает ему данные для подключения к этой комнате(серверу)
        /// </summary>
        /// <param name="received"></param>
        public void ConnectedRoom(Received received)
        {
            byte[] datagram = new byte[0];
            string[] ParamServ = received.Package.Message.Split(',');
            if (!ServerRooms.Keys.Any(q => q == ParamServ[0])) return;
            string pubIp = /*"127.0 0.1";*/ new System.Net.WebClient().DownloadString("https://api.ipify.org");
            if (ServerRooms[ParamServ[0]].PassWord != ParamServ[1])
            {
                datagram = HelpSerialize.Serialize(new MessageObject() { typeMessage = TypeMessage.ConnectToRoom, Name = "Serv", Message = pubIp + "," + "Не верный пароль." });
            }
            else
            {
                datagram = HelpSerialize.Serialize(new MessageObject() { typeMessage = TypeMessage.ConnectToRoom, Name = "Serv", Message = pubIp + "," + ServerRooms[ParamServ[0]].portRoom });
            }
            lock (locker)
                Client.Send(datagram, datagram.Length, received.Sender);
        }
        /// <summary>
        /// Ответ на пингование клиентов.
        /// Переписывает конечные точки клиентов если они изменились
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        public void SupClient(IPEndPoint endpoint, string name)
        {
           // System.Threading.Thread.Sleep(1000);
            if (!DictClient.Keys.Any(q => q == name)) return;
                DictClient[name] = new ClientObj() { iP = endpoint, Name = name };//перепишем адрес пользователя, он может изменится 
                byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = "", typeMessage = TypeMessage.SupConnect });
                lock (locker)
                    Client.Send(datagram, datagram.Length, endpoint);
            
        }
        /// <summary>
        /// Массовая рассылка сообщений
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        public void massMessage(string message, string name)
        {
            if (!DictClient.Values.Any(q => q.Name == name || name == "serv")) return;//если такой клиент не был добавлен
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = message, typeMessage = TypeMessage.Message , Name = name });
            foreach ( string i in DictClient.Keys)
            {
                if (i == name) continue;
                lock (locker)
                    Client.Send(datagram, datagram.Length, DictClient[i].iP);
            }
        }
        /// <summary>
        /// Массовая рассылка списка
        /// </summary>
        /// <param name="ListClient"></param>
        /// <param name="name"></param>
        /// <param name="leave"></param>
        public void massMessage(List<string> ListClient, string name, bool leave = false)
        {
            if (!leave && !DictClient.Values.Any(q => q.Name == name || name == "serv")) return;//если такой клиент не был добавлен
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = "" , listRoom = ListClient, typeMessage = TypeMessage.Listing, Name = name });
            foreach (string i in DictClient.Keys)
                lock (locker)
                    Client.Send(datagram, datagram.Length, DictClient[i].iP);
                
            
        }
        /// <summary>
        /// Запрос листа пользователем
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        public void CheckList(IPEndPoint endpoint, string name)
        {
            if ( !DictClient.Values.Any(q => q.Name == name || name == "serv")) return;//если такой клиент не был добавлен
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = "", listRoom = ServerRooms.Keys.ToList(), typeMessage = TypeMessage.Listing, Name = name });
            lock (locker)
                Client.Send(datagram, datagram.Length, endpoint);
        }
        /// <summary>
        /// Обработка отключения пользователя
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        public void Disconnect(IPEndPoint endpoint, string name)
        {
            if (!DictClient.Values.Any(q => q.Name == name)) return;
                byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Вы отключены от сервера", typeMessage = TypeMessage.Disconnect });
            /*if (typeServer == TypeServer.Room)*/ //this.massMessage("Пользователь " + name + " отключился", name);
                
                lock (locker)
                    Client.Send(datagram, datagram.Length, endpoint);                
                Console.WriteLine("-------------\r\nКлиент отключился от холла." + DateTime.Now.ToString() + "\r\nip :" + endpoint.Address.ToString() + "\r\nИмя : " + name + "\r\n-------------");
                DictClient.Remove(name);
            /*if (typeServer == TypeServer.Room)*/ //this.massMessage(DictClient.Values.ToList().Select(q => q.Name).ToList(), name,true);  
            this.massMessage(ServerRooms.Keys.ToList(), name);

        }
    }
    /// <summary>
    /// Класс сервера комнаты
    /// </summary>
    public class UdpListenerRoom : UdpBase
    {        
        /// <summary>
        /// Клиенты в комнате
        /// </summary>
        public struct ClientObj
        {
            public IPEndPoint iP;
            public string Name;
            public ClientObj(IPEndPoint ip, string name)
            {
                iP = ip;
                Name = name;
            }
        }
        //прослушивание 
        //public bool EnableListen = true;
        /// <summary>
        /// Токен отмены
        /// </summary>
        public CancellationTokenSource connectsrc;
        public CancellationToken tokenconnect;
        static object locker = new object();
        /// <summary>
        ///Список клиентов в комнате 
        /// </summary>
        Dictionary<String, ClientObj> DictClient = new Dictionary<string, ClientObj>();
        //Локальная точка для прослушивания
        public IPEndPoint _listenOn;
        public UdpListenerRoom(IPEndPoint endpoint)
        {
            connectsrc = new CancellationTokenSource();
            tokenconnect = connectsrc.Token;
            _listenOn = endpoint;
            Client = new UdpClient(_listenOn);
        }
        /// <summary>
        /// Запуск прослушивания  у сервера комнаты
        /// </summary>
        /// <returns></returns>
        public async Task RunListen()
        {
            await Task.Factory.StartNew(async () =>
            {
                while (!tokenconnect.IsCancellationRequested)
                {
                    Received receivedRoom = await this.Receive();

                    _ = Task.Factory.StartNew(async () =>
                    {
                        switch (receivedRoom.Package.typeMessage)
                        {
                            case TypeMessage.Connect:
                                AddClientToRoom(receivedRoom.Sender, receivedRoom.Package.Name);
                                break;
                            case TypeMessage.SupConnect:
                                SupClient(receivedRoom.Sender, receivedRoom.Package.Name);
                                break;
                            case TypeMessage.Disconnect:
                                DisconnectClientRoom(receivedRoom.Sender, receivedRoom.Package.Name);
                                break;
                            case TypeMessage.Message:
                                massMessage(receivedRoom.Package.Message as string, receivedRoom.Package.Name);
                                break;
                        }
                    });

                }
            });
        }
        public void AddClientToRoom(IPEndPoint endpoint, string name)
        {
            byte[] datagram = new byte[0];

            if (!DictClient.Values.Any(q => q.Name == name))
            {
                DictClient.Add(name, new ClientObj(endpoint, name));
                datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Успешное подключение", typeMessage = TypeMessage.Connect });
                this.massMessage("Подключился. \r\nИмя : " + name, "");
                Console.WriteLine("-------------\r\nКлиент подключился к комнате.\r\nВремя: " + DateTime.Now.TimeOfDay.ToString() + "\r\nip :" + endpoint.Address.ToString() + "\r\nИмя : " + name + "\r\n-------------");
            }
            else
                datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Пользователь с таким именем уже существует", typeMessage = TypeMessage.Disconnect });
            lock (locker)
                Client.Send(datagram, datagram.Length, endpoint);
             massMessage(DictClient.Values.ToList().Select(q => q.Name).ToList(), name);
        }
        /// <summary>
        /// Ответ на пингование клиентов.
        /// Переписывает конечные точки клиентов если они изменились
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        public void SupClient(IPEndPoint endpoint, string name)
        {
            if (!DictClient.Keys.Any(q => q == name)) return;
            DictClient[name] = new ClientObj() { iP = endpoint, Name = name };//перепишем адрес пользователя, он может изменится 
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = "", typeMessage = TypeMessage.SupConnect });
            lock (locker)
                Client.Send(datagram, datagram.Length, endpoint);
        }
        /// <summary>
        /// Массовая рассылка сообщений
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        public void massMessage(string message, string name)
        {
            if (!DictClient.Values.Any(q => q.Name == name || name == "serv")) return;//если такой клиент не был добавлен
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = message, typeMessage = TypeMessage.Message, Name = name });
            foreach (string i in DictClient.Keys)
            {
                if (i == name) continue;
                lock (locker)
                    Client.Send(datagram, datagram.Length, DictClient[i].iP);
            }
        }
        /// <summary>
        /// Массовая рассылка списка
        /// </summary>
        /// <param name="ListClient"></param>
        /// <param name="name"></param>
        /// <param name="leave"></param>
        public void massMessage(List<string> ListClient, string name, bool leave = false)
        {
            if (!leave && !DictClient.Values.Any(q => q.Name == name || name == "serv")) return;//если такой клиент не был добавлен
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = "", listRoom = ListClient, typeMessage = TypeMessage.Listing, Name = name });
            foreach (string i in DictClient.Keys)
                lock (locker)
                    Client.Send(datagram, datagram.Length, DictClient[i].iP);
        }
        /// <summary>
        /// Обработка отключения пользователя
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="name"></param>
        public void DisconnectClientRoom(IPEndPoint endpoint, string name)
        {
            if (!DictClient.Values.Any(q => q.Name == name)) return;
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Вы отключены от сервера", typeMessage = TypeMessage.Disconnect });
            massMessage("Пользователь " + name + " отключился", name);

            lock (locker)
                Client.Send(datagram, datagram.Length, endpoint);
            Console.WriteLine("-------------\r\nКлиент отключился от комнаты." + DateTime.Now.ToString() + "\r\nip :" + endpoint.Address.ToString() + "\r\nИмя : " + name + "\r\n-------------");
            DictClient.Remove(name);
            massMessage(DictClient.Values.ToList().Select(q => q.Name).ToList(), name, true);
        }
    }
    #endregion
    #region HelpSerial 
    /// <summary>
    /// Класс сериализаций
    /// </summary>
    public static class HelpSerialize
    {
        public static MessageObject DeSerialize(UdpReceiveResult result)
        {
            XmlSerializer MesSerializer = new XmlSerializer(typeof(MessageObject));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(result.Buffer, 0, result.Buffer.Length);//создаем поток из полученных байт
                memoryStream.Position = 0;
                return (MessageObject)MesSerializer.Deserialize(memoryStream);//десериализуем
            }
        }
        public static byte[] Serialize(MessageObject messageObject)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    XmlSerializer StructSer = new XmlSerializer(typeof(MessageObject));
                    StructSer.Serialize(memoryStream, messageObject);
                    memoryStream.Position = 0;
                    byte[] mesbyt = new byte[memoryStream.Length];
                    memoryStream.Read(mesbyt, 0, Convert.ToInt32(memoryStream.Length));
                    return mesbyt;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new byte[0];
            }
        }
    }
    #endregion
}
