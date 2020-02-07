namespace ClientUPD
{
    partial class InteractRoom
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
            this.nameServer = new System.Windows.Forms.TextBox();
            this.NameLab = new System.Windows.Forms.Label();
            this.keyword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EnterRoom = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameServer
            // 
            this.nameServer.BackColor = System.Drawing.Color.DarkGray;
            this.nameServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameServer.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.nameServer.Location = new System.Drawing.Point(150, 12);
            this.nameServer.Name = "nameServer";
            this.nameServer.Size = new System.Drawing.Size(89, 23);
            this.nameServer.TabIndex = 5;
            // 
            // NameLab
            // 
            this.NameLab.AutoSize = true;
            this.NameLab.BackColor = System.Drawing.Color.Transparent;
            this.NameLab.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLab.ForeColor = System.Drawing.Color.White;
            this.NameLab.Location = new System.Drawing.Point(9, 15);
            this.NameLab.Name = "NameLab";
            this.NameLab.Size = new System.Drawing.Size(140, 16);
            this.NameLab.TabIndex = 6;
            this.NameLab.Text = "Название комнаты:";
            // 
            // keyword
            // 
            this.keyword.BackColor = System.Drawing.Color.DarkGray;
            this.keyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.keyword.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.keyword.Location = new System.Drawing.Point(150, 42);
            this.keyword.Name = "keyword";
            this.keyword.Size = new System.Drawing.Size(89, 23);
            this.keyword.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Пароль:";
            // 
            // EnterRoom
            // 
            this.EnterRoom.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.EnterRoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EnterRoom.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.EnterRoom.ForeColor = System.Drawing.Color.White;
            this.EnterRoom.Location = new System.Drawing.Point(12, 79);
            this.EnterRoom.Name = "EnterRoom";
            this.EnterRoom.Size = new System.Drawing.Size(100, 26);
            this.EnterRoom.TabIndex = 15;
            this.EnterRoom.Text = "Создать";
            this.EnterRoom.UseVisualStyleBackColor = true;
            this.EnterRoom.Click += new System.EventHandler(this.EnterRoom_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(139, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 26);
            this.button1.TabIndex = 16;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Createroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(248, 114);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.EnterRoom);
            this.Controls.Add(this.keyword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameServer);
            this.Controls.Add(this.NameLab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Createroom";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Createroom";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Createroom_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameServer;
        private System.Windows.Forms.Label NameLab;
        private System.Windows.Forms.TextBox keyword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button EnterRoom;
        private System.Windows.Forms.Button button1;
    }
}