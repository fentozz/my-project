namespace ClientUPD
{
    partial class FormClientInRoom
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
            this.ExitBut = new System.Windows.Forms.Button();
            this.ConnectBut = new System.Windows.Forms.Button();
            this.inpChatBox = new System.Windows.Forms.ListBox();
            this.nameuser = new System.Windows.Forms.TextBox();
            this.NameLab = new System.Windows.Forms.Label();
            this.InteractServ = new System.Windows.Forms.Panel();
            this.outChatBox = new System.Windows.Forms.TextBox();
            this.enterMess = new System.Windows.Forms.Button();
            this.messToServ = new System.Windows.Forms.Panel();
            this.pingLb = new System.Windows.Forms.Label();
            this.ListRoomClients = new System.Windows.Forms.ListBox();
            this.PingSTlab = new System.Windows.Forms.Label();
            this.InteractServ.SuspendLayout();
            this.messToServ.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExitBut
            // 
            this.ExitBut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitBut.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ExitBut.ForeColor = System.Drawing.Color.White;
            this.ExitBut.Location = new System.Drawing.Point(700, 4);
            this.ExitBut.Name = "ExitBut";
            this.ExitBut.Size = new System.Drawing.Size(58, 26);
            this.ExitBut.TabIndex = 0;
            this.ExitBut.Text = "Выйти";
            this.ExitBut.UseVisualStyleBackColor = true;
            this.ExitBut.Click += new System.EventHandler(this.ExitBut_Click);
            // 
            // ConnectBut
            // 
            this.ConnectBut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ConnectBut.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.ConnectBut.ForeColor = System.Drawing.Color.White;
            this.ConnectBut.Location = new System.Drawing.Point(0, 0);
            this.ConnectBut.Name = "ConnectBut";
            this.ConnectBut.Size = new System.Drawing.Size(120, 26);
            this.ConnectBut.TabIndex = 1;
            this.ConnectBut.Text = "Подключится";
            this.ConnectBut.UseVisualStyleBackColor = true;
            this.ConnectBut.Click += new System.EventHandler(this.ConnectBut_Click);
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
            "123",
            "asdasdqwdasd",
            "rtr",
            "FFFFFFFFFFFFQQQQQQQQQQ",
            "1",
            "1",
            "1",
            "12",
            "3",
            "12",
            "4",
            "124",
            "12",
            "42",
            "22",
            "2",
            "2",
            "2",
            "2"});
            this.inpChatBox.Location = new System.Drawing.Point(4, 33);
            this.inpChatBox.Name = "inpChatBox";
            this.inpChatBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.inpChatBox.Size = new System.Drawing.Size(657, 352);
            this.inpChatBox.TabIndex = 2;
            // 
            // nameuser
            // 
            this.nameuser.BackColor = System.Drawing.Color.DarkGray;
            this.nameuser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameuser.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.nameuser.Location = new System.Drawing.Point(213, 3);
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
            this.NameLab.Location = new System.Drawing.Point(129, 6);
            this.NameLab.Name = "NameLab";
            this.NameLab.Size = new System.Drawing.Size(79, 16);
            this.NameLab.TabIndex = 4;
            this.NameLab.Text = "Ваше имя:";
            // 
            // InteractServ
            // 
            this.InteractServ.Controls.Add(this.ConnectBut);
            this.InteractServ.Controls.Add(this.nameuser);
            this.InteractServ.Controls.Add(this.NameLab);
            this.InteractServ.Location = new System.Drawing.Point(5, 3);
            this.InteractServ.Name = "InteractServ";
            this.InteractServ.Size = new System.Drawing.Size(313, 30);
            this.InteractServ.TabIndex = 5;
            this.InteractServ.Visible = false;
            // 
            // outChatBox
            // 
            this.outChatBox.BackColor = System.Drawing.Color.Black;
            this.outChatBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outChatBox.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.outChatBox.ForeColor = System.Drawing.Color.Lime;
            this.outChatBox.Location = new System.Drawing.Point(0, 0);
            this.outChatBox.Multiline = true;
            this.outChatBox.Name = "outChatBox";
            this.outChatBox.Size = new System.Drawing.Size(546, 26);
            this.outChatBox.TabIndex = 6;
            // 
            // enterMess
            // 
            this.enterMess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.enterMess.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.enterMess.ForeColor = System.Drawing.Color.White;
            this.enterMess.Location = new System.Drawing.Point(552, 0);
            this.enterMess.Name = "enterMess";
            this.enterMess.Size = new System.Drawing.Size(105, 26);
            this.enterMess.TabIndex = 5;
            this.enterMess.Text = "Отправить";
            this.enterMess.UseVisualStyleBackColor = true;
            this.enterMess.Click += new System.EventHandler(this.EnterMess_Click);
            // 
            // messToServ
            // 
            this.messToServ.Controls.Add(this.enterMess);
            this.messToServ.Controls.Add(this.outChatBox);
            this.messToServ.Enabled = false;
            this.messToServ.Location = new System.Drawing.Point(4, 388);
            this.messToServ.Name = "messToServ";
            this.messToServ.Size = new System.Drawing.Size(657, 26);
            this.messToServ.TabIndex = 7;
            // 
            // pingLb
            // 
            this.pingLb.AutoSize = true;
            this.pingLb.Location = new System.Drawing.Point(695, 418);
            this.pingLb.Name = "pingLb";
            this.pingLb.Size = new System.Drawing.Size(0, 13);
            this.pingLb.TabIndex = 8;
            // 
            // ListRoomClients
            // 
            this.ListRoomClients.BackColor = System.Drawing.Color.Black;
            this.ListRoomClients.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListRoomClients.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListRoomClients.ForeColor = System.Drawing.Color.Lime;
            this.ListRoomClients.FormattingEnabled = true;
            this.ListRoomClients.IntegralHeight = false;
            this.ListRoomClients.ItemHeight = 16;
            this.ListRoomClients.Location = new System.Drawing.Point(664, 33);
            this.ListRoomClients.Name = "ListRoomClients";
            this.ListRoomClients.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ListRoomClients.Size = new System.Drawing.Size(94, 381);
            this.ListRoomClients.TabIndex = 9;
            // 
            // PingSTlab
            // 
            this.PingSTlab.AutoSize = true;
            this.PingSTlab.Location = new System.Drawing.Point(665, 418);
            this.PingSTlab.Name = "PingSTlab";
            this.PingSTlab.Size = new System.Drawing.Size(31, 13);
            this.PingSTlab.TabIndex = 10;
            this.PingSTlab.Text = "Ping:";
            // 
            // FormClientInRoom
            // 
            this.AcceptButton = this.enterMess;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(763, 435);
            this.Controls.Add(this.PingSTlab);
            this.Controls.Add(this.ListRoomClients);
            this.Controls.Add(this.pingLb);
            this.Controls.Add(this.messToServ);
            this.Controls.Add(this.InteractServ);
            this.Controls.Add(this.inpChatBox);
            this.Controls.Add(this.ExitBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormClientInRoom";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClient_FormClosing);
            this.Load += new System.EventHandler(this.FormClientInRoom_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormClient_MouseDown);
            this.InteractServ.ResumeLayout(false);
            this.InteractServ.PerformLayout();
            this.messToServ.ResumeLayout(false);
            this.messToServ.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExitBut;
        private System.Windows.Forms.Button ConnectBut;
        private System.Windows.Forms.ListBox inpChatBox;
        private System.Windows.Forms.TextBox nameuser;
        private System.Windows.Forms.Label NameLab;
        private System.Windows.Forms.Panel InteractServ;
        private System.Windows.Forms.TextBox outChatBox;
        private System.Windows.Forms.Button enterMess;
        private System.Windows.Forms.Panel messToServ;
        private System.Windows.Forms.Label pingLb;
        private System.Windows.Forms.ListBox ListRoomClients;
        private System.Windows.Forms.Label PingSTlab;
    }
}