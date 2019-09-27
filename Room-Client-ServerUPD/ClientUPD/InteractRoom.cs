using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientUPD
{
    public partial class InteractRoom : Form
    {
        public enum TypeRoom
        {
            Create, Delete, Connect
        }

        public string Servername;
        public string Password;

        public TypeRoom type;

        public InteractRoom(TypeRoom typeRoom ,string Name ="")
        {
            InitializeComponent();
            type = typeRoom;
            nameServer.Text = Name;
            switch (type)
            {
                case TypeRoom.Create:
                    EnterRoom.Text = "Создать";
                    break;
                case TypeRoom.Delete:
                    EnterRoom.Text = "Удалить";
                    break;
                case TypeRoom.Connect:
                    EnterRoom.Text = "Подключиться";
                    break;
            }
        }
    
        //public InteractRoom(string Name)
        //{
        //    InitializeComponent();
        //    EnterRoom.Text = "Удалить";
        //    nameServer.Text = Name;
        //    nameServer.ReadOnly = true;
        //}

        

        private void Createroom_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void EnterRoom_Click(object sender, EventArgs e)
        {
            Servername = nameServer.Text;
            Password = keyword.Text;
        }
    }
}
