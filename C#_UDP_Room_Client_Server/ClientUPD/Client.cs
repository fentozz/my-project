using System;
using System.Drawing;

namespace ClientUPD
{
    /// <summary>
    /// Класс клиента
    /// </summary>
    class Client
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConnectHallServer connectHallServer = new ConnectHallServer(/*"127.0.0.1"*/"212.109.223.13", 1025);
            Rectangle recLoc = connectHallServer.DesktopBounds;

            while (!connectHallServer.ExitForm)
            {
                connectHallServer.DesktopBounds =  recLoc;
                connectHallServer.ShowDialog();
                recLoc = connectHallServer.DesktopBounds;

                if (connectHallServer.ConnectedToRoom)
                    using (FormClientInRoom FrmClient = new FormClientInRoom(connectHallServer.ServerHall.ConnectionRoom.hostname, connectHallServer.ServerHall.ConnectionRoom.port))
                    {
                        FrmClient.CurrName = connectHallServer.CurName;
                        FrmClient.ShowDialog();
                    }
            }
        }
    }
}
