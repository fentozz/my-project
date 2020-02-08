using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassUDP;

namespace ClientUPD
{
    /// <summary>
    /// Класс формы клиента в холле
    /// </summary>
    public partial class ConnectHallServer : Form
    {
        /// <summary>
        /// Подключение к серверу холла
        /// </summary>
        public UPDClientInHallConnection ServerHall = null;
        /// <summary>
        /// Айпи удаленного сервера холла
        /// </summary>
        string Hall_ip = "";
        /// <summary>
        /// Порт удаленного сервера
        /// </summary>
        int Port = 0;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string CurName;
        /// <summary>
        /// Было ли произведено подключение
        /// </summary>
        public bool EnableConnection = false;
        /// <summary>
        /// Был инициирован выход 
        /// </summary>
        public bool ExitForm = false;
        /// <summary>
        /// Был инициировано подключение к комнате
        /// </summary>
        public bool ConnectedToRoom = false;
        /// <summary>
        /// Комнаты созданные текущим пользователем.
        /// После перезапуска будут утеряны.
        /// </summary>
        List<string> Myroom = new List<string>();
        public ConnectHallServer(string Hallip, int port)
        {
            InitializeComponent();
            inpChatBox.Items.Clear();
            Hall_ip = Hallip;
            Port = port;
        }
        /// <summary>
        /// Перемещение формы мышкой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectMainServer_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
        #region Обработка событий
        /// <summary>
        /// Событие пинга
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ping"></param>
        public void PingCheck(object sender, long ping) => pingLb.Invoke(new Action(() => pingLb.Text = ping.ToString()));
        /// <summary>
        /// Событие обновления списка комнат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="clients"></param>
        public void RoomCheck(object sender, List<string> clients)
        {
            ListRooms.Invoke(new Action(() =>
            {
                ListRooms.Items.Clear();
                ListRooms.Items.AddRange(clients.ToArray());
                EnterRoom.Enabled = false;
            }));
        }
        /// <summary>
        /// Событие при запуске подключения к серверу 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ActivateConnect(object sender, EventArgs e)
        {
            InteractServ.Invoke(new Action(() =>
            {
                InteractServ.Enabled = false;
                CreateRoom.Enabled = true;
            }));
        }
        /// <summary>
        /// Событие при отключении от сервера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeActivateConnect(object sender, EventArgs e)
        {
            InteractServ.Invoke(new Action(() =>
            {
                InteractServ.Enabled = true;
                CreateRoom.Enabled = false;
            }));
        }
        #endregion
        /// <summary>
        /// Подключение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectBut_Click(object sender, EventArgs e)
        {
            if (nameuser.Text == "")
            {
                inpChatBox.Items.Add("Введите имя пользователя");
                return;
            }
            CurName = nameuser.Text;
            ServerHall = new UPDClientInHallConnection(Hall_ip, Port, nameuser.Text, inpChatBox);
            ServerHall.EventPing += PingCheck;
            ServerHall.EventListing += RoomCheck;
            ServerHall.EventActivateClient += ActivateConnect;
            ServerHall.EventDeActivateClient += DeActivateConnect;
            ServerHall.StartConnect();
            EnableConnection = true;
        }
        /// <summary>
        /// Нажатие кнопки выход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBut_Click(object sender, EventArgs e)
        {
            ExitForm = true;
            ServerHall?.Disconnect();
            this.Close();
        }
        private void ConnectMainServer_FormClosing(object sender, FormClosingEventArgs e) { }
        /// <summary>
        /// Создание новой комнаты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateRoom_Click(object sender, EventArgs e)
        {
            using (InteractRoom createRoom = new InteractRoom(InteractRoom.TypeRoom.Create))
            {
                if (createRoom.ShowDialog(this) == DialogResult.Cancel) return;
                ServerHall?.EnterMess(new MessageObject() { typeMessage = TypeMessage.HallAddRoom, Name = nameuser.Text, Message = createRoom.Servername + "," + createRoom.Password });
                while(ServerHall.StatusAddRoom == UPDClientInHallConnection.StatusPass.Wait) { }
                if (ServerHall.StatusAddRoom == UPDClientInHallConnection.StatusPass.Succ)                
                    Myroom.Add(createRoom.Servername);                
            }
            ServerHall.StatusAddRoom = UPDClientInHallConnection.StatusPass.Wait;
        }
        /// <summary>
        /// Подключение к комнате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterRoom_Click(object sender, EventArgs e)
        {
            using (InteractRoom connectRoom = new InteractRoom(InteractRoom.TypeRoom.Connect, ListRooms.SelectedItem.ToString()))
            {
                if (connectRoom.ShowDialog(this) == DialogResult.Cancel) return;
                ServerHall.EnterMess(new MessageObject() { typeMessage = TypeMessage.ConnectToRoom, Name = nameuser.Text, Message = connectRoom.Servername + "," + connectRoom.Password });
            }

            while (ServerHall.ConnectionRoom.exists == UPDClientInHallConnection.StatusPass.Wait) {} //ждём пока пароль проверится 
            if (ServerHall.ConnectionRoom.exists == UPDClientInHallConnection.StatusPass.Succ)
            {
                ConnectedToRoom = true;
                ServerHall.ConnectionRoom.exists = UPDClientInHallConnection.StatusPass.Wait;
                this.Close();
            }
        }
        /// <summary>
        /// События выделения обьекта в листбоксе комнат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnterRoom.Enabled = true;
            DeleteRoom.Enabled = Myroom.Any(q => q == (sender as ListBox).SelectedItem.ToString()) ? true : false;//если комната была создана текущим пользователем то ему можно её удалять
        }
        /// <summary>
        /// Удаление комнаты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteRoom_Click(object sender, EventArgs e)
        {
            using (InteractRoom deleteRoom = new InteractRoom(InteractRoom.TypeRoom.Delete, ListRooms.SelectedItem.ToString()))
            {
                if (deleteRoom.ShowDialog(this) == DialogResult.Cancel) return;
                ServerHall.EnterMess(new MessageObject() { typeMessage = TypeMessage.HallDelRoom, Name = nameuser.Text, Message = deleteRoom.Servername + "," + deleteRoom.Password });
                while(ServerHall.StatusDelRoom == UPDClientInHallConnection.StatusPass.Wait) { }
                if (ServerHall.StatusDelRoom == UPDClientInHallConnection.StatusPass.Succ) 
                    Myroom.Remove(deleteRoom.Servername);
            }
            ServerHall.StatusDelRoom = UPDClientInHallConnection.StatusPass.Wait;
            EnterRoom.Enabled = (sender as Button).Enabled = false;
        }
        /// <summary>
        /// Запуск формы
        /// Сбрасываем флаги.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectHallServer_Load(object sender, EventArgs e)
        {
            if (EnableConnection) ServerHall?.EnterMess(new MessageObject() { typeMessage = TypeMessage.CheckLisnting, Name = nameuser.Text, Message = "" });
            ConnectedToRoom = false;
            if (ServerHall != null) ServerHall.ConnectionRoom.exists = UPDClientInHallConnection.StatusPass.Wait;
        }
    }
}
