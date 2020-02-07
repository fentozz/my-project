namespace GNUplotMan
{
    partial class FilePlot
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.DeleteFile = new System.Windows.Forms.Button();
            this.PanelName = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.Label();
            this.PanelColum = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Colum = new System.Windows.Forms.Label();
            this.PanelCount = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.Count = new System.Windows.Forms.Label();
            this.PanelColumUse = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.ColumUse = new System.Windows.Forms.TextBox();
            this.PanelLegend = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.legendLine = new System.Windows.Forms.TextBox();
            this.PanelWith = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.TypeWith = new System.Windows.Forms.ComboBox();
            this.PanelColorLine = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.ColorLine = new System.Windows.Forms.Button();
            this.PanelLineWidht = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.LineWidht = new System.Windows.Forms.ComboBox();
            this.PanelDash = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.DashType = new System.Windows.Forms.ComboBox();
            this.PanelLine = new System.Windows.Forms.Panel();
            this.PanelPoint = new System.Windows.Forms.Panel();
            this.PanelTypePoint = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.PointType = new System.Windows.Forms.ComboBox();
            this.PanelPointSize = new System.Windows.Forms.Panel();
            this.Размер = new System.Windows.Forms.Label();
            this.PointSize = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PanelName.SuspendLayout();
            this.PanelColum.SuspendLayout();
            this.PanelCount.SuspendLayout();
            this.PanelColumUse.SuspendLayout();
            this.PanelLegend.SuspendLayout();
            this.PanelWith.SuspendLayout();
            this.PanelColorLine.SuspendLayout();
            this.PanelLineWidht.SuspendLayout();
            this.PanelDash.SuspendLayout();
            this.PanelLine.SuspendLayout();
            this.PanelPoint.SuspendLayout();
            this.PanelTypePoint.SuspendLayout();
            this.PanelPointSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeleteFile
            // 
            this.DeleteFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.DeleteFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteFile.Location = new System.Drawing.Point(861, 2);
            this.DeleteFile.Name = "DeleteFile";
            this.DeleteFile.Size = new System.Drawing.Size(37, 38);
            this.DeleteFile.TabIndex = 3;
            this.DeleteFile.Text = "Del";
            this.DeleteFile.UseVisualStyleBackColor = true;
            this.DeleteFile.Click += new System.EventHandler(this.DeleteFile_Click);
            // 
            // PanelName
            // 
            this.PanelName.Controls.Add(this.label1);
            this.PanelName.Controls.Add(this.FileName);
            this.PanelName.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelName.Location = new System.Drawing.Point(0, 2);
            this.PanelName.Name = "PanelName";
            this.PanelName.Size = new System.Drawing.Size(125, 38);
            this.PanelName.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Расположение файла";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FileName
            // 
            this.FileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FileName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FileName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileName.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.FileName.Location = new System.Drawing.Point(0, 17);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(125, 21);
            this.FileName.TabIndex = 1;
            this.FileName.Text = "File Name";
            this.FileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelColum
            // 
            this.PanelColum.Controls.Add(this.label2);
            this.PanelColum.Controls.Add(this.Colum);
            this.PanelColum.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelColum.Location = new System.Drawing.Point(125, 2);
            this.PanelColum.Name = "PanelColum";
            this.PanelColum.Size = new System.Drawing.Size(66, 38);
            this.PanelColum.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Столбцы";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Colum
            // 
            this.Colum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Colum.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Colum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Colum.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Colum.Location = new System.Drawing.Point(0, 17);
            this.Colum.Name = "Colum";
            this.Colum.Size = new System.Drawing.Size(66, 21);
            this.Colum.TabIndex = 2;
            this.Colum.Text = "0";
            this.Colum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelCount
            // 
            this.PanelCount.Controls.Add(this.label3);
            this.PanelCount.Controls.Add(this.Count);
            this.PanelCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelCount.Location = new System.Drawing.Point(191, 2);
            this.PanelCount.Name = "PanelCount";
            this.PanelCount.Size = new System.Drawing.Size(69, 38);
            this.PanelCount.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Строки";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Count
            // 
            this.Count.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Count.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Count.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.Count.Location = new System.Drawing.Point(0, 17);
            this.Count.Name = "Count";
            this.Count.Size = new System.Drawing.Size(69, 21);
            this.Count.TabIndex = 7;
            this.Count.Text = "1";
            this.Count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PanelColumUse
            // 
            this.PanelColumUse.Controls.Add(this.label4);
            this.PanelColumUse.Controls.Add(this.ColumUse);
            this.PanelColumUse.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelColumUse.Location = new System.Drawing.Point(260, 2);
            this.PanelColumUse.Name = "PanelColumUse";
            this.PanelColumUse.Size = new System.Drawing.Size(80, 38);
            this.PanelColumUse.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Исп. столбцы";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ColumUse
            // 
            this.ColumUse.BackColor = System.Drawing.Color.White;
            this.ColumUse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColumUse.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ColumUse.Location = new System.Drawing.Point(0, 17);
            this.ColumUse.Multiline = true;
            this.ColumUse.Name = "ColumUse";
            this.ColumUse.Size = new System.Drawing.Size(80, 21);
            this.ColumUse.TabIndex = 8;
            this.ColumUse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PanelLegend
            // 
            this.PanelLegend.Controls.Add(this.label5);
            this.PanelLegend.Controls.Add(this.legendLine);
            this.PanelLegend.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelLegend.Location = new System.Drawing.Point(340, 2);
            this.PanelLegend.Name = "PanelLegend";
            this.PanelLegend.Size = new System.Drawing.Size(80, 38);
            this.PanelLegend.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Легенда";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // legendLine
            // 
            this.legendLine.BackColor = System.Drawing.Color.White;
            this.legendLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.legendLine.Location = new System.Drawing.Point(0, 17);
            this.legendLine.Multiline = true;
            this.legendLine.Name = "legendLine";
            this.legendLine.Size = new System.Drawing.Size(80, 21);
            this.legendLine.TabIndex = 13;
            this.legendLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.legendLine.WordWrap = false;
            // 
            // PanelWith
            // 
            this.PanelWith.Controls.Add(this.label6);
            this.PanelWith.Controls.Add(this.TypeWith);
            this.PanelWith.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelWith.Location = new System.Drawing.Point(518, 2);
            this.PanelWith.Name = "PanelWith";
            this.PanelWith.Size = new System.Drawing.Size(63, 38);
            this.PanelWith.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Тип соед.";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TypeWith
            // 
            this.TypeWith.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TypeWith.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeWith.FormattingEnabled = true;
            this.TypeWith.Items.AddRange(new object[] {
            "Линия",
            "Линия и точки",
            "Точки"});
            this.TypeWith.Location = new System.Drawing.Point(0, 17);
            this.TypeWith.Name = "TypeWith";
            this.TypeWith.Size = new System.Drawing.Size(63, 21);
            this.TypeWith.TabIndex = 9;
            this.TypeWith.SelectedIndexChanged += new System.EventHandler(this.TypeWith_SelectedIndexChanged);
            // 
            // PanelColorLine
            // 
            this.PanelColorLine.Controls.Add(this.label7);
            this.PanelColorLine.Controls.Add(this.ColorLine);
            this.PanelColorLine.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelColorLine.Location = new System.Drawing.Point(420, 2);
            this.PanelColorLine.Name = "PanelColorLine";
            this.PanelColorLine.Size = new System.Drawing.Size(98, 38);
            this.PanelColorLine.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(0, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Цвет";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ColorLine
            // 
            this.ColorLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ColorLine.Location = new System.Drawing.Point(0, 17);
            this.ColorLine.Name = "ColorLine";
            this.ColorLine.Size = new System.Drawing.Size(98, 21);
            this.ColorLine.TabIndex = 10;
            this.ColorLine.UseVisualStyleBackColor = true;
            this.ColorLine.Click += new System.EventHandler(this.ColorLine_Click);
            // 
            // PanelLineWidht
            // 
            this.PanelLineWidht.Controls.Add(this.label8);
            this.PanelLineWidht.Controls.Add(this.LineWidht);
            this.PanelLineWidht.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelLineWidht.Location = new System.Drawing.Point(0, 0);
            this.PanelLineWidht.Name = "PanelLineWidht";
            this.PanelLineWidht.Size = new System.Drawing.Size(84, 38);
            this.PanelLineWidht.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Ширина линии";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LineWidht
            // 
            this.LineWidht.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LineWidht.FormattingEnabled = true;
            this.LineWidht.Items.AddRange(new object[] {
            "0.5",
            "1",
            "1.5",
            "2",
            "2.5",
            "3",
            "3.5",
            "4",
            "4.5",
            "5",
            "5.5",
            "6",
            "6.5",
            "7",
            "7.5",
            "8",
            "8.5",
            "9",
            "9.5",
            "10"});
            this.LineWidht.Location = new System.Drawing.Point(0, 17);
            this.LineWidht.Name = "LineWidht";
            this.LineWidht.Size = new System.Drawing.Size(84, 21);
            this.LineWidht.TabIndex = 11;
            // 
            // PanelDash
            // 
            this.PanelDash.Controls.Add(this.label9);
            this.PanelDash.Controls.Add(this.DashType);
            this.PanelDash.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelDash.Location = new System.Drawing.Point(84, 0);
            this.PanelDash.Name = "PanelDash";
            this.PanelDash.Size = new System.Drawing.Size(59, 38);
            this.PanelDash.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Тип линии";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DashType
            // 
            this.DashType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DashType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DashType.FormattingEnabled = true;
            this.DashType.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.DashType.Location = new System.Drawing.Point(0, 17);
            this.DashType.Name = "DashType";
            this.DashType.Size = new System.Drawing.Size(59, 21);
            this.DashType.TabIndex = 12;
            // 
            // PanelLine
            // 
            this.PanelLine.Controls.Add(this.PanelDash);
            this.PanelLine.Controls.Add(this.PanelLineWidht);
            this.PanelLine.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelLine.Location = new System.Drawing.Point(581, 2);
            this.PanelLine.Name = "PanelLine";
            this.PanelLine.Size = new System.Drawing.Size(145, 38);
            this.PanelLine.TabIndex = 22;
            // 
            // PanelPoint
            // 
            this.PanelPoint.Controls.Add(this.PanelTypePoint);
            this.PanelPoint.Controls.Add(this.PanelPointSize);
            this.PanelPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelPoint.Location = new System.Drawing.Point(726, 2);
            this.PanelPoint.Name = "PanelPoint";
            this.PanelPoint.Size = new System.Drawing.Size(135, 38);
            this.PanelPoint.TabIndex = 23;
            // 
            // PanelTypePoint
            // 
            this.PanelTypePoint.Controls.Add(this.label10);
            this.PanelTypePoint.Controls.Add(this.PointType);
            this.PanelTypePoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelTypePoint.Location = new System.Drawing.Point(77, 0);
            this.PanelTypePoint.Name = "PanelTypePoint";
            this.PanelTypePoint.Size = new System.Drawing.Size(57, 38);
            this.PanelTypePoint.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(0, 1);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Тип точки";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PointType
            // 
            this.PointType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PointType.FormattingEnabled = true;
            this.PointType.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.PointType.Location = new System.Drawing.Point(0, 17);
            this.PointType.Name = "PointType";
            this.PointType.Size = new System.Drawing.Size(57, 21);
            this.PointType.TabIndex = 12;
            // 
            // PanelPointSize
            // 
            this.PanelPointSize.Controls.Add(this.Размер);
            this.PanelPointSize.Controls.Add(this.PointSize);
            this.PanelPointSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelPointSize.Location = new System.Drawing.Point(0, 0);
            this.PanelPointSize.Name = "PanelPointSize";
            this.PanelPointSize.Size = new System.Drawing.Size(77, 38);
            this.PanelPointSize.TabIndex = 21;
            // 
            // Размер
            // 
            this.Размер.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Размер.AutoSize = true;
            this.Размер.Location = new System.Drawing.Point(0, 1);
            this.Размер.Name = "Размер";
            this.Размер.Size = new System.Drawing.Size(77, 13);
            this.Размер.TabIndex = 12;
            this.Размер.Text = "Размер точки";
            this.Размер.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PointSize
            // 
            this.PointSize.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PointSize.FormattingEnabled = true;
            this.PointSize.Items.AddRange(new object[] {
            "0.5",
            "1",
            "1.5",
            "2",
            "2.5",
            "3",
            "3.5",
            "4",
            "4.5",
            "5",
            "5.5",
            "6",
            "6.5",
            "7",
            "7.5",
            "8",
            "8.5",
            "9",
            "9.5",
            "10"});
            this.PointSize.Location = new System.Drawing.Point(0, 17);
            this.PointSize.Name = "PointSize";
            this.PointSize.Size = new System.Drawing.Size(77, 21);
            this.PointSize.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(956, 2);
            this.panel1.TabIndex = 12;
            // 
            // FilePlot
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.DeleteFile);
            this.Controls.Add(this.PanelPoint);
            this.Controls.Add(this.PanelLine);
            this.Controls.Add(this.PanelWith);
            this.Controls.Add(this.PanelColorLine);
            this.Controls.Add(this.PanelLegend);
            this.Controls.Add(this.PanelColumUse);
            this.Controls.Add(this.PanelCount);
            this.Controls.Add(this.PanelColum);
            this.Controls.Add(this.PanelName);
            this.Controls.Add(this.panel1);
            this.Name = "FilePlot";
            this.Size = new System.Drawing.Size(956, 40);
            this.PanelName.ResumeLayout(false);
            this.PanelName.PerformLayout();
            this.PanelColum.ResumeLayout(false);
            this.PanelColum.PerformLayout();
            this.PanelCount.ResumeLayout(false);
            this.PanelCount.PerformLayout();
            this.PanelColumUse.ResumeLayout(false);
            this.PanelColumUse.PerformLayout();
            this.PanelLegend.ResumeLayout(false);
            this.PanelLegend.PerformLayout();
            this.PanelWith.ResumeLayout(false);
            this.PanelWith.PerformLayout();
            this.PanelColorLine.ResumeLayout(false);
            this.PanelColorLine.PerformLayout();
            this.PanelLineWidht.ResumeLayout(false);
            this.PanelLineWidht.PerformLayout();
            this.PanelDash.ResumeLayout(false);
            this.PanelDash.PerformLayout();
            this.PanelLine.ResumeLayout(false);
            this.PanelPoint.ResumeLayout(false);
            this.PanelTypePoint.ResumeLayout(false);
            this.PanelTypePoint.PerformLayout();
            this.PanelPointSize.ResumeLayout(false);
            this.PanelPointSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button DeleteFile;
        private System.Windows.Forms.Panel PanelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label FileName;
        private System.Windows.Forms.Panel PanelColum;
        public System.Windows.Forms.Label Colum;
        private System.Windows.Forms.Panel PanelCount;
        private System.Windows.Forms.Label Count;
        private System.Windows.Forms.Panel PanelColumUse;
        private System.Windows.Forms.TextBox ColumUse;
        private System.Windows.Forms.Panel PanelLegend;
        private System.Windows.Forms.TextBox legendLine;
        private System.Windows.Forms.Panel PanelWith;
        private System.Windows.Forms.ComboBox TypeWith;
        private System.Windows.Forms.Panel PanelColorLine;
        private System.Windows.Forms.Button ColorLine;
        private System.Windows.Forms.Panel PanelLineWidht;
        private System.Windows.Forms.ComboBox LineWidht;
        private System.Windows.Forms.Panel PanelDash;
        private System.Windows.Forms.ComboBox DashType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel PanelLine;
        private System.Windows.Forms.Panel PanelPoint;
        private System.Windows.Forms.Panel PanelTypePoint;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox PointType;
        private System.Windows.Forms.Panel PanelPointSize;
        private System.Windows.Forms.Label Размер;
        private System.Windows.Forms.ComboBox PointSize;
        private System.Windows.Forms.Panel panel1;
    }
}
