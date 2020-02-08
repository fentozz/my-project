using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassUDP;

namespace ClientUPD
{
    //базовый класс клиента
    public abstract class UPDClientBase
    {
        public UdpUser Server = null;//удаленный хост к которому подключаемся
        public string NameCurrentUser;//название клиента
        public ListBox Chat;//изменяемый листбокс куда выводится информация
        //Событие пинга
        public event EventHandler<long> EventPing;
        public void PingEvent(object sender, long e) => EventPing.Invoke(sender, e);
        //Событие изменения списка 
        public event EventHandler<List<string>> EventListing;
        public void ListingEvent(object sender, List<string> e) => EventListing.Invoke(sender, e);
        //Событие деактивации
        public event EventHandler EventDeActivateClient;
        public void DeActivateClientEvent() => EventDeActivateClient.Invoke(this, new EventArgs());
        //Событие активации чата
        public event EventHandler EventActivateClient;
        //токены отмены
        public CancellationTokenSource connectsrc;
        public CancellationToken tokenconnect; 
        //обьект для синхронизации потоков
        public static object locker = new object();
        //счетчик для измерения пинга
        public Stopwatch watch = new Stopwatch();
        /// <summary>
        /// Конструктор заполняет поля.Для запуска - StartConnect
        /// </summary>
        /// <param name="hostname">Ip сервера</param>
        /// <param name="port">Порт сервера</param>
        /// <param name="name">Имя клиента</param>
        /// <param name="listBox">ListBox для вывода сообщений</param>
        public UPDClientBase(string hostname, int port, string name, ListBox listBox)
        {
            connectsrc = new CancellationTokenSource();
            tokenconnect = connectsrc.Token;
            Server = UdpUser.ConnectTo(hostname, port);
            NameCurrentUser = name;
            Chat = listBox;
        }
        /// <summary>
        /// Запуск соединения
        /// </summary>
        /// <returns></returns>
        public async Task<bool> StartConnect()
        {
            
            bool result = false;//успешность соединения
            Chat?.Items.Add("Подключение к серверу");
            await Task.Factory.StartNew(async () =>
            {
                byte[] mesbyt = HelpSerialize.Serialize(new MessageObject() { typeMessage = TypeMessage.Connect, Message = "", Name = NameCurrentUser });
                Server.Send(mesbyt);//отправляем сообщение

                #region меняем надпись в ожидания сервера
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;

                Task.Factory.StartNew(async () =>
                {
                    int index = 0;
                    string messstr = "Ждём ответ от сервера";
                    Chat?.Invoke(new Action(() => index = Chat.Items.Add(messstr)));
                    while (!token.IsCancellationRequested)
                    {
                        Chat?.Invoke(new Action(() =>
                        {
                            Chat?.Refresh();
                            Chat?.Items.RemoveAt(index);
                            Chat?.Items.Insert(index, messstr += ".");
                        }));
                        Thread.Sleep(200);
                    }

                });
                #endregion
                Thread.Sleep(1500);
                try
                {
                    var received = await Server.Receive(true);//ждём ответ от сервера
                    cts.Cancel();//останавливаем вывод собщений о ожидании 
                    switch (received.Package.typeMessage)//обрабатываем ответ сервера
                    {
                        case TypeMessage.Disconnect:
                            Chat?.Invoke(new Action(() => { Chat?.Items.Add(received.Package.Message); }));
                            result = false;
                            break;
                        case TypeMessage.Error:
                            Chat?.Invoke(new Action(() => Chat?.Items.Add(received.Package.Message)));
                            result = false;
                            break;
                        case TypeMessage.Connect:
                            Chat?.Invoke(new Action(() => Chat?.Items.Add(received.Package.Message)));
                            EventActivateClient.Invoke(this, new EventArgs());
                            
                            new HandlerListen(Listen).BeginInvoke(null, null);//слушаем входящие 
                            ActiveConnect();//пинг до сервера

                            result = true;
                            break;
                        default:
                            throw new Exception("Сервер отправил неверные данные");
                    }
                }
                catch (Exception ex)
                {
                    Chat?.Invoke(new Action(() => Chat?.Items.Add(ex.Message)));
                    result = false;
                }

                GC.Collect();
            });
            return result;
        }
        //Метод прослушивания сервера
        private delegate Task HandlerListen();
        public abstract Task Listen();
        /// <summary>
        /// Поддержание активного соединения
        /// Отображения пинга
        /// </summary>
        /// <returns></returns>
        public async Task ActiveConnect()
        {
            await Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    if (tokenconnect.IsCancellationRequested)
                        return;
                    try
                    {
                        byte[] mesbyt = new byte[0];
                        lock (locker)
                        {
                            mesbyt = HelpSerialize.Serialize(new MessageObject() { Message = "", Name = NameCurrentUser, typeMessage = TypeMessage.SupConnect });
                            watch.Start();
                            int succByte = Server.Send(mesbyt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex);
                    }
                }
            });
        }
        /// <summary>
        /// Добавление новой строчки в листбокс с прокруткой вниз
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="mess"></param>
        public void ChatAdd(ListBox listBox, string mess)
        {
            listBox?.Invoke(new Action(() =>
            {
                int visibleItems = listBox.ClientSize.Height / listBox.ItemHeight;
                var padding = 3;
                var atEnd = listBox.TopIndex >= Math.Max(listBox.Items.Count - visibleItems - padding, 0);
                listBox?.Items.Add(mess);
                if (atEnd)
                    listBox.TopIndex = Math.Max(listBox.Items.Count - visibleItems, 0);
            }));
        }
        /// <summary>
        /// Отправка обьекта на сервер
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        public int EnterMess(MessageObject mess)
        {
            lock (locker)
                return Server.Send(HelpSerialize.Serialize(mess));
        }
        /// <summary>
        /// Отключится от сервера
        /// </summary>
        public void Disconnect()
        {
            byte[] mesbyt = HelpSerialize.Serialize(new MessageObject() { typeMessage = TypeMessage.Disconnect, Message = "Шаурма", Name = NameCurrentUser });
            Server?.Send(mesbyt);//отправляем сообщение
            connectsrc?.Cancel();
        }
    }
    /// <summary>
    /// Класс клиента в холле
    /// </summary>
    public class UPDClientInHallConnection : UPDClientBase
    {
        /// <summary>
        /// Проверка пароля
        /// </summary>
        public enum StatusPass
        {
            /// <summary>
            /// Ожидание пароля
            /// </summary>
            Wait,
            /// <summary>
            /// Правильный пароль
            /// </summary>
            Succ,
            /// <summary>
            /// Неправильный пароль
            /// </summary>
            Error
        }
        /// <summary>
        /// Структура для подключения к комнате
        /// </summary>
        public struct ConnectionRoomServer
        {
            public StatusPass exists;
            public int port;
            public string hostname;
        }
        public StatusPass StatusAddRoom;
        public StatusPass StatusDelRoom;
        public ConnectionRoomServer ConnectionRoom;
        public UPDClientInHallConnection(string hostname, int port, string name, ListBox listBox) : base(hostname, port, name, listBox) { }
        /// <summary>
        /// Слушаем входящие
        /// </summary>
        /// <returns></returns>
        public override async Task Listen()
        {
            await Task.Factory.StartNew(async () =>
           {
               while (!tokenconnect.IsCancellationRequested)
               {
                   try
                   {
                       Received received = await Server.Receive();
                        // вызываем новый поток сразу же, чтобы продолжить слушать 
                        _ = Task.Factory.StartNew(async () =>
                       {
                           switch (received.Package.typeMessage)// тип сообщения
                            {
                               case TypeMessage.SupConnect:
                                   watch.Stop();
                                   PingEvent(this, watch.ElapsedMilliseconds);
                                   watch.Reset();
                                   break;
                               case TypeMessage.Listing://список клиентов в комнате
                                   ListingEvent(this, received.Package.listRoom);
                                   break;
                               case TypeMessage.Disconnect://дисконект
                                   connectsrc.Cancel();
                                   ChatAdd(Chat, (received.Package.Name == "serv" ? received.Package.Name : "") + " : " + received.Package.Message);
                                   DeActivateClientEvent();
                                   break;
                               case TypeMessage.ConnectToRoom:
                                   string[] ParamServ = received.Package.Message.Split(',');
                                   if (ParamServ[1] == "Не верный пароль.")
                                   {
                                       ConnectionRoom.exists = StatusPass.Error;
                                       ChatAdd(Chat, "Не верный пароль комнаты.");
                                   }
                                   else
                                   {
                                       ConnectionRoom.exists = StatusPass.Succ;
                                       ConnectionRoom.hostname = ParamServ[0];
                                       ConnectionRoom.port = int.Parse(ParamServ[1]);
                                   }
                                   break;
                               case TypeMessage.CheckLisnting:
                                   ListingEvent(this, received.Package.listRoom);
                                   break;
                               case TypeMessage.Error:
                                   ChatAdd(Chat, (received.Package.Message));
                                   DeActivateClientEvent();
                                   break;
                               case TypeMessage.HallDelRoom:
                                   string[] ParamDel = received.Package.Message.Split(',');
                                   if (ParamDel[0].IndexOf("Комната успешно удалена ") != -1)
                                   {
                                       StatusDelRoom = StatusPass.Succ;
                                       ChatAdd(Chat, (received.Package.Message));                                       
                                   }
                                   else
                                   {
                                       StatusDelRoom = StatusPass.Error;
                                       ChatAdd(Chat, (received.Package.Message));
                                   }
                                   break;
                               case TypeMessage.HallAddRoom:
                                   string[] ParamAdd = received.Package.Message.Split(',');
                                   if (ParamAdd[0].IndexOf("Успешное создание комнаты ") != -1)
                                   {
                                       StatusAddRoom = StatusPass.Succ;
                                       ChatAdd(Chat, (received.Package.Message));
                                   }
                                   else
                                   {
                                       StatusAddRoom = StatusPass.Error;
                                       ChatAdd(Chat, (received.Package.Message));
                                   }
                                   break;
                           }
                       });
                   }
                   catch (Exception ex)
                   {
                       Debug.Write(ex);
                   }
               }
           });
        }
        /// <summary>
        /// Отправка сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public int EnterMess(string mess)
        {
            if (mess == "") return 0;

            ChatAdd(Chat, "Вы : " + mess);

            byte[] mesbyt = HelpSerialize.Serialize(new MessageObject() { typeMessage = TypeMessage.Message, Message = mess, Name = NameCurrentUser });
            lock (locker)
                return Server.Send(mesbyt);
        }
    }
    /// <summary>
    /// Класс клиента в комнате
    /// </summary>
    public class UPDClientInRoomConnection : UPDClientBase
    {
        /// <summary>
        /// Класс клиента находящегося в комнате
        /// Конструктор заполняет поля.Для запуска - StartConnect
        /// </summary>
        /// <param name="hostname">Ip сервера</param>
        /// <param name="port">Порт сервера</param>
        /// <param name="name">Имя клиента</param>
        /// <param name="listBox">ListBox для вывода сообщений</param>
        public UPDClientInRoomConnection(string hostname, int port, string name, ListBox listBox) : base(hostname, port, name, listBox) { }
        /// <summary>
        /// Слушаем входящие
        /// </summary>
        /// <returns></returns>
        public override async Task Listen()
        {
            await Task.Factory.StartNew(async () =>
            {
                while (!tokenconnect.IsCancellationRequested)
                {
                    try
                    {
                        Received received = await Server.Receive();
                        // вызываем новый поток сразу же, чтобы продолжить слушать 
                        _ = Task.Factory.StartNew(async () =>
                        {
                            switch (received.Package.typeMessage)// тип сообщения
                            {
                                case TypeMessage.Message://просто сообщение
                                    ChatAdd(Chat, (received.Package.Name == "serv" ? "" : received.Package.Name) + " : " + received.Package.Message);
                                    break;
                                case TypeMessage.SupConnect://пинг
                                    watch.Stop();
                                    PingEvent(this, watch.ElapsedMilliseconds);
                                    watch.Reset();
                                    break;
                                case TypeMessage.Listing://список клиентов в комнате
                                    ListingEvent(this, received.Package.listRoom);
                                    break;
                                case TypeMessage.Disconnect://дисконект
                                    connectsrc.Cancel();
                                    ChatAdd(Chat, (received.Package.Name == "serv" ? received.Package.Name : "") + " : " + received.Package.Message);
                                    DeActivateClientEvent();
                                    break;
                                case TypeMessage.Error:
                                    ChatAdd(Chat, (received.Package.Message));
                                    DeActivateClientEvent();
                                    break;
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Debug.Write(ex);
                    }
                }
            });
        }
        /// <summary>
        /// Отправка сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public int EnterMess(string mess)
        {
            if (mess == "") return 0;

            ChatAdd(Chat, "Вы : " + mess);

            byte[] mesbyt = HelpSerialize.Serialize(new MessageObject() { typeMessage = TypeMessage.Message, Message = mess, Name = NameCurrentUser });
            lock (locker)
                return Server.Send(mesbyt);
        }
    }
}
