namespace ClientUPD
{
    partial class ConnectHallServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InteractServ = new System.Windows.Forms.Panel();
            this.ConnectBut = new System.Windows.Forms.Button();
            this.nameuser = new System.Windows.Forms.TextBox();
            this.NameLab = new System.Windows.Forms.Label();
            this.inpChatBox = new System.Windows.Forms.ListBox();
            this.PingSTlab = new System.Windows.Forms.Label();
            this.pingLb = new System.Windows.Forms.Label();
            this.ListRooms = new System.Windows.Forms.ListBox();
            this.EnterRoom = new System.Windows.Forms.Button();
            this.CreateRoom = new System.Windows.Forms.Button();
            this.DeleteRoom = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExitBut = new System.Windows.Forms.Button();
            this.InteractServ.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InteractServ
            // 
            this.InteractServ.Controls.Add(this.ConnectBut);
            this.InteractServ.Controls.Add(this.nameuser);
            this.InteractServ.Controls.Add(this.NameLab);
            this.InteractServ.Location = new System.Drawing.Point(1, 18);
            this.InteractServ.Name = "InteractServ";
            this.InteractServ.Size = new System.Drawing.Size(313, 26);
            this.InteractServ.TabIndex = 6;
            // 
            // ConnectBut
            // 
            this.ConnectBut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConnectBut.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ConnectBut.ForeColor = System.Drawing.Color.White;
            this.ConnectBut.Location = new System.Drawing.Point(0, 2);
            this.ConnectBut.Name = "ConnectBut";
            this.ConnectBut.Size = new System.Drawing.Size(120, 22);
            this.ConnectBut.TabIndex = 1;
            this.ConnectBut.Text = "Подключится";
            this.ConnectBut.UseVisualStyleBackColor = true;
            this.ConnectBut.Click += new System.EventHandler(this.ConnectBut_Click);
            // 
            // nameuser
            // 
            this.nameuser.BackColor = System.Drawing.Color.DarkGray;
            this.nameuser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameuser.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.nameuser.Location = new System.Drawing.Point(213, 1);
            this.nameuser.Name = "nameuser";
            this.nameuser.Size = new System.Drawing.Size(89, 23);
            this.nameuser.TabIndex = 3;
            // 
            // NameLab
            // 
            this.NameLab.AutoSize = true;
            this.NameLab.BackColor = System.Drawing.Color.Transparent;
            this.NameLab.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLab.ForeColor = System.Drawing.Color.White;
            this.NameLab.Location = new System.Drawing.Point(129, 4);
            this.NameLab.Name = "NameLab";
            this.NameLab.Size = new System.Drawing.Size(79, 16);
            this.NameLab.TabIndex = 4;
            this.NameLab.Text = "Ваше имя:";
            // 
            // inpChatBox
            // 
            this.inpChatBox.BackColor = System.Drawing.Color.Black;
            this.inpChatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inpChatBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inpChatBox.ForeColor = System.Drawing.Color.Lime;
            this.inpChatBox.FormattingEnabled = true;
            this.inpChatBox.IntegralHeight = false;
            this.inpChatBox.ItemHeight = 16;
            this.inpChatBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.inpChatBox.Location = new System.Drawing.Point(1, 276);
            this.inpChatBox.Name = "inpChatBox";
            this.inpChatBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.inpChatBox.Size = new System.Drawing.Size(313, 51);
            this.inpChatBox.TabIndex = 7;
            // 
            // PingSTlab
            // 
            this.PingSTlab.AutoSize = true;
            this.PingSTlab.Location = new System.Drawing.Point(213, 216);
            this.PingSTlab.Name = "PingSTlab";
            this.PingSTlab.Size = new System.Drawing.Size(31, 13);
            this.PingSTlab.TabIndex = 12;
            this.PingSTlab.Text = "Ping:";
            // 
            // pingLb
            // 
            this.pingLb.AutoSize = true;
            this.pingLb.Location = new System.Drawing.Point(243, 216);
            this.pingLb.Name = "pingLb";
            this.pingLb.Size = new System.Drawing.Size(25, 13);
            this.pingLb.TabIndex = 11;
            this.pingLb.Text = "000";
            // 
            // ListRooms
            // 
            this.ListRooms.BackColor = System.Drawing.Color.Black;
            this.ListRooms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListRooms.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListRooms.ForeColor = System.Drawing.Color.Lime;
            this.ListRooms.FormattingEnabled = true;
            this.ListRooms.IntegralHeight = false;
            this.ListRooms.ItemHeight = 16;
            this.ListRooms.Location = new System.Drawing.Point(0, 3);
            this.ListRooms.Name = "ListRooms";
            this.ListRooms.Size = new System.Drawing.Size(207, 226);
            this.ListRooms.TabIndex = 13;
            this.ListRooms.SelectedIndexChanged += new System.EventHandler(this.ListRooms_SelectedIndexChanged);
            // 
            // EnterRoom
            // 
            this.EnterRoom.Enabled = false;
            this.EnterRoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EnterRoom.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.EnterRoom.ForeColor = System.Drawing.Color.White;
            this.EnterRoom.Location = new System.Drawing.Point(210, 3);
            this.EnterRoom.Name = "EnterRoom";
            this.EnterRoom.Size = new System.Drawing.Size(100, 26);
            this.EnterRoom.TabIndex = 14;
            this.EnterRoom.Text = "Войти";
            this.EnterRoom.UseVisualStyleBackColor = true;
            this.EnterRoom.Click += new System.EventHandler(this.EnterRoom_Click);
            // 
            // CreateRoom
            // 
            this.CreateRoom.Enabled = false;
            this.CreateRoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CreateRoom.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.CreateRoom.ForeColor = System.Drawing.Color.White;
            this.CreateRoom.Location = new System.Drawing.Point(210, 35);
            this.CreateRoom.Name = "CreateRoom";
            this.CreateRoom.Size = new System.Drawing.Size(100, 26);
            this.CreateRoom.TabIndex = 15;
            this.CreateRoom.Text = "Создать";
            this.CreateRoom.UseVisualStyleBackColor = true;
            this.CreateRoom.Click += new System.EventHandler(this.CreateRoom_Click);
            // 
            // DeleteRoom
            // 
            this.DeleteRoom.Enabled = false;
            this.DeleteRoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteRoom.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.DeleteRoom.ForeColor = System.Drawing.Color.White;
            this.DeleteRoom.Location = new System.Drawing.Point(210, 67);
            this.DeleteRoom.Name = "DeleteRoom";
            this.DeleteRoom.Size = new System.Drawing.Size(100, 26);
            this.DeleteRoom.TabIndex = 16;
            this.DeleteRoom.Text = "Удалить";
            this.DeleteRoom.UseVisualStyleBackColor = true;
            this.DeleteRoom.Click += new System.EventHandler(this.DeleteRoom_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ExitBut);
            this.panel1.Controls.Add(this.pingLb);
            this.panel1.Controls.Add(this.PingSTlab);
            this.panel1.Controls.Add(this.ListRooms);
            this.panel1.Controls.Add(this.EnterRoom);
            this.panel1.Controls.Add(this.CreateRoom);
            this.panel1.Controls.Add(this.DeleteRoom);
            this.panel1.Location = new System.Drawing.Point(1, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 230);
            this.panel1.TabIndex = 17;
            // 
            // ExitBut
            // 
            this.ExitBut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitBut.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ExitBut.ForeColor = System.Drawing.Color.White;
            this.ExitBut.Location = new System.Drawing.Point(210, 187);
            this.ExitBut.Name = "ExitBut";
            this.ExitBut.Size = new System.Drawing.Size(100, 26);
            this.ExitBut.TabIndex = 17;
            this.ExitBut.Text = "Выйти";
            this.ExitBut.UseVisualStyleBackColor = true;
            this.ExitBut.Click += new System.EventHandler(this.ExitBut_Click);
            // 
            // ConnectHallServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(315, 330);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inpChatBox);
            this.Controls.Add(this.InteractServ);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConnectHallServer";
            this.Text = "ConnectMainServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConnectMainServer_FormClosing);
            this.Load += new System.EventHandler(this.ConnectHallServer_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ConnectMainServer_MouseDown);
            this.InteractServ.ResumeLayout(false);
            this.InteractServ.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel InteractServ;
        private System.Windows.Forms.Button ConnectBut;
        private System.Windows.Forms.TextBox nameuser;
        private System.Windows.Forms.Label NameLab;
        private System.Windows.Forms.ListBox inpChatBox;
        private System.Windows.Forms.Label PingSTlab;
        private System.Windows.Forms.Label pingLb;
        private System.Windows.Forms.ListBox ListRooms;
        private System.Windows.Forms.Button EnterRoom;
        private System.Windows.Forms.Button CreateRoom;
        private System.Windows.Forms.Button DeleteRoom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ExitBut;
    }
}