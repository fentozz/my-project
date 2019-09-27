using System;

namespace ObjUDP
{
    public enum TypeMessage
    {
        Connect, Message
    }

    [Serializable]
    public struct MessageObject
    {
        public TypeMessage typeMessage;
        public string Message;
    }

    public struct Received
    {
        public IPEndPoint Sender;
        public MessageObject Message;
    }

    //Base
    abstract class UdpBase
    {
        protected UdpClient Client;

        protected UdpBase()
        {
            Client = new UdpClient();
        }

        public async Task<Received> Receive()
        {
            var result = await Client.ReceiveAsync();
            return new Received() { Message = HelpSerialize.DeSerialize(result), Sender = result.RemoteEndPoint };
        }
    }

    //Client
    class UdpUser : UdpBase
    {
        private UdpUser() { }

        public static UdpUser ConnectTo(string hostname, int port)
        {
            UdpUser connection = new UdpUser();
            connection.Client.Connect(hostname, port);
            return connection;
        }

        public void Send(string message)
        {
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = message, typeMessage = TypeMessage.Message });
            //Encoding.Unicode.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }

        public void Send(byte[] datagram) => Client.Send(datagram, datagram.Length);
    }

    //Server
    class UdpListener : UdpBase
    {
        static int sumClient = 0;

        public struct ClientObj
        {
            public IPEndPoint iP;
            public ClientObj(IPEndPoint ip) => iP = ip;
        }

        List<ClientObj> clients = new List<ClientObj>();

        private IPEndPoint _listenOn;

        public UdpListener() : this(new IPEndPoint(IPAddress.Any, 9086)) { }

        public UdpListener(IPEndPoint endpoint)
        {
            _listenOn = endpoint;
            Client = new UdpClient(_listenOn);
        }

        public void Reply(string message, IPEndPoint endpoint)
        {
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = message, typeMessage = TypeMessage.Message });
            Console.WriteLine("Дублирование сообщения от " + endpoint.Address.ToString());
            Client.Send(datagram, datagram.Length, endpoint);
        }

        public void AddClient(IPEndPoint endpoint)
        {
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = "Успешное подключение", typeMessage = TypeMessage.Connect });
            Console.WriteLine("Подключен новый клиент " + endpoint.Address.ToString());
            clients.Add(new ClientObj(endpoint));
            Client.Send(datagram, datagram.Length, endpoint);
        }

        public void massMessage(string message)
        {
            byte[] datagram = HelpSerialize.Serialize(new MessageObject() { Message = message, typeMessage = TypeMessage.Message });
            foreach (ClientObj client in clients)
                Client.Send(datagram, datagram.Length, client.iP);
        }
    }

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
    }
}