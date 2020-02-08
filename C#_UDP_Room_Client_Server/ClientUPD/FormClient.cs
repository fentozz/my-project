using ClassUDP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUPD
{
    /// <summary>
    /// класс формы клиента в комнате
    /// </summary>
    public partial class FormClientInRoom : Form
    {
        UPDClientInRoomConnection ServerRoom = null;
        /// <summary>
        /// ip комнаты
        /// </summary>
        string Room_ip = "";
        int Port = 0;
        /// <summary>
        /// Имя текущего пользователя
        /// </summary>
        public string CurrName;
        /// <summary>
        /// Создание формы чата в комнате
        /// </summary>
        /// <param name="serverRoom">ip комнаты</param>
        /// <param name="port">порт комнаты</param>
        public FormClientInRoom(string serverRoom, int port)
        {
            InitializeComponent();
            inpChatBox.Items.Clear();
            Room_ip = serverRoom;
            Port = port;
        }
        /// <summary>
        /// перемещение формы мышкой
        /// </summary>
        private void FormClient_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        #region обработка событий
        /// <summary>
        /// Событие пинга
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ping"></param>
        public void PingCheck(object sender, long ping) => pingLb.Invoke(new Action(() => pingLb.Text = ping.ToString()));
        /// <summary>
        /// Событие обновления списка пользователей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="clients"></param>
        public void RoomCheck(object sender, List<string> clients)
        {
            ListRoomClients.Invoke(new Action(() =>
            {
                ListRoomClients.Items.Clear();
                //string[] ss = received.Package.listRoom.ToArray();
                ListRoomClients.Items.AddRange(clients.ToArray());
            }));
        }
        /// <summary>
        /// Событие при запуске подключения к серверу 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ActivateConnect(object sender, EventArgs e)
        {
           // InteractServ.Invoke(new Action(() => InteractServ.Enabled = false));
            messToServ.Invoke(new Action (()=> messToServ.Enabled = true));
        }
        /// <summary>
        /// Событие при отключении от сервера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeActivateConnect(object sender, EventArgs e)
        {
            //InteractServ.Invoke(new Action(() => InteractServ.Enabled = true));
            messToServ.Invoke(new Action(() => messToServ.Enabled = false));
        }
        #endregion
        /// <summary>
        /// Подключение при нажатии
        /// Больше не используется
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ConnectBut_Click(object sender, EventArgs e)
        {
            try
            {
                ServerRoom = new UPDClientInRoomConnection(Room_ip, Port, CurrName, inpChatBox);
            
                ServerRoom.EventPing += PingCheck;
                ServerRoom.EventListing += RoomCheck;
                ServerRoom.EventActivateClient += ActivateConnect;
                ServerRoom.EventDeActivateClient += DeActivateConnect;
                ServerRoom.StartConnect();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Отправка сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterMess_Click(object sender, EventArgs e)
        {
            if (outChatBox.Text == "") return;
            enterMess.Enabled = false;
            ServerRoom.EnterMess(outChatBox.Text);
            outChatBox.Text = "";
            enterMess.Enabled = true;
        }
        /// <summary>
        /// закрытие формы
        /// </summary>
        private void ExitBut_Click(object sender, EventArgs e) => this.Close();
        /// <summary>
        /// Действия при закрытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerRoom?.Disconnect();
           // ServerRoom.enable = false;
            ServerRoom?.Server.Client.Close();
        }
        /// <summary>
        /// Запуск формы
        /// При запуске формы производится подключение к серверу комнаты, данные которого были переданы в форму при запуске.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClientInRoom_Load(object sender, EventArgs e)
        {
            try
            {
                ServerRoom = new UPDClientInRoomConnection(Room_ip, Port, CurrName, inpChatBox);
                ServerRoom.EventPing += PingCheck;
                ServerRoom.EventListing += RoomCheck;
                ServerRoom.EventActivateClient += ActivateConnect;
                ServerRoom.EventDeActivateClient += DeActivateConnect;
               // ServerRoom.enable = true;
                ServerRoom.StartConnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
