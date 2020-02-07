namespace GNUplotMan
{
    partial class FormPlot
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Plot = new System.Windows.Forms.Button();
            this.check3D = new System.Windows.Forms.CheckBox();
            this.FileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FuncCheck = new System.Windows.Forms.CheckBox();
            this.Func = new System.Windows.Forms.TextBox();
            this.XrangeStart = new System.Windows.Forms.TextBox();
            this.YrangeStart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ZrangeStart = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.NameZ = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.NameY = new System.Windows.Forms.TextBox();
            this.NameX = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TicsZ = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TicsY = new System.Windows.Forms.TextBox();
            this.TicsX = new System.Windows.Forms.TextBox();
            this.MultiPlot = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.DontLegend = new System.Windows.Forms.CheckBox();
            this.ImgWidth = new System.Windows.Forms.TextBox();
            this.ImgHeight = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.XrangeEnd = new System.Windows.Forms.TextBox();
            this.YrangeEnd = new System.Windows.Forms.TextBox();
            this.ZrangeEnd = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.gbDecart = new System.Windows.Forms.GroupBox();
            this.systemCord = new System.Windows.Forms.ComboBox();
            this.typeMetricMash = new System.Windows.Forms.ComboBox();
            this.typeMetricGraf = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.GrafHeight = new System.Windows.Forms.TextBox();
            this.GrafWidth = new System.Windows.Forms.TextBox();
            this.gpTerm = new System.Windows.Forms.GroupBox();
            this.MashCh = new System.Windows.Forms.CheckBox();
            this.GrafCh = new System.Windows.Forms.CheckBox();
            this.terminal = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.ManagePlot = new GNUplotMan.FilePlotManager();
            this.DirFile = new GNUplotMan.directoryChange();
            this.gbDecart.SuspendLayout();
            this.gpTerm.SuspendLayout();
            this.SuspendLayout();
            // 
            // Plot
            // 
            this.Plot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Plot.Location = new System.Drawing.Point(12, 429);
            this.Plot.Margin = new System.Windows.Forms.Padding(3, 3, 3, 33);
            this.Plot.Name = "Plot";
            this.Plot.Size = new System.Drawing.Size(75, 25);
            this.Plot.TabIndex = 0;
            this.Plot.Text = "Построить";
            this.Plot.UseVisualStyleBackColor = true;
            this.Plot.Click += new System.EventHandler(this.RunPlot);
            // 
            // check3D
            // 
            this.check3D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.check3D.AutoSize = true;
            this.check3D.Location = new System.Drawing.Point(93, 434);
            this.check3D.Name = "check3D";
            this.check3D.Size = new System.Drawing.Size(38, 17);
            this.check3D.TabIndex = 1;
            this.check3D.Text = "3d";
            this.check3D.UseVisualStyleBackColor = true;
            this.check3D.Visible = false;
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(225, 25);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(198, 20);
            this.FileName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Директория";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Название файла";
            // 
            // FuncCheck
            // 
            this.FuncCheck.AutoSize = true;
            this.FuncCheck.Checked = true;
            this.FuncCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FuncCheck.Location = new System.Drawing.Point(12, 136);
            this.FuncCheck.Name = "FuncCheck";
            this.FuncCheck.Size = new System.Drawing.Size(72, 17);
            this.FuncCheck.TabIndex = 7;
            this.FuncCheck.Text = "Функция";
            this.FuncCheck.UseVisualStyleBackColor = true;
            this.FuncCheck.CheckedChanged += new System.EventHandler(this.FuncCheck_CheckedChanged);
            // 
            // Func
            // 
            this.Func.Location = new System.Drawing.Point(93, 133);
            this.Func.Name = "Func";
            this.Func.Size = new System.Drawing.Size(211, 20);
            this.Func.TabIndex = 8;
            this.Func.Text = "((-x/sin(x) + cos(x+5)**2)/10)*tan(x)";
            // 
            // XrangeStart
            // 
            this.XrangeStart.Location = new System.Drawing.Point(7, 31);
            this.XrangeStart.Name = "XrangeStart";
            this.XrangeStart.Size = new System.Drawing.Size(44, 20);
            this.XrangeStart.TabIndex = 12;
            this.XrangeStart.Text = "-50.1";
            // 
            // YrangeStart
            // 
            this.YrangeStart.Location = new System.Drawing.Point(7, 69);
            this.YrangeStart.Name = "YrangeStart";
            this.YrangeStart.Size = new System.Drawing.Size(44, 20);
            this.YrangeStart.TabIndex = 13;
            this.YrangeStart.Text = "-50.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Область Х";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Область Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Область Z";
            this.label6.Visible = false;
            // 
            // ZrangeStart
            // 
            this.ZrangeStart.Location = new System.Drawing.Point(7, 110);
            this.ZrangeStart.Name = "ZrangeStart";
            this.ZrangeStart.Size = new System.Drawing.Size(44, 20);
            this.ZrangeStart.TabIndex = 16;
            this.ZrangeStart.Text = "1.1";
            this.ZrangeStart.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(115, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Название оси Z";
            this.label7.Visible = false;
            // 
            // NameZ
            // 
            this.NameZ.Location = new System.Drawing.Point(117, 110);
            this.NameZ.Name = "NameZ";
            this.NameZ.Size = new System.Drawing.Size(100, 20);
            this.NameZ.TabIndex = 22;
            this.NameZ.Text = "Z";
            this.NameZ.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(115, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Название оси Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(115, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Название оси Х";
            // 
            // NameY
            // 
            this.NameY.Location = new System.Drawing.Point(117, 69);
            this.NameY.Name = "NameY";
            this.NameY.Size = new System.Drawing.Size(100, 20);
            this.NameY.TabIndex = 19;
            this.NameY.Text = "Y";
            // 
            // NameX
            // 
            this.NameX.Location = new System.Drawing.Point(117, 31);
            this.NameX.Name = "NameX";
            this.NameX.Size = new System.Drawing.Size(100, 20);
            this.NameX.TabIndex = 18;
            this.NameX.Text = "X";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(227, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Шаг оси Z";
            this.label10.Visible = false;
            // 
            // TicsZ
            // 
            this.TicsZ.Location = new System.Drawing.Point(229, 110);
            this.TicsZ.Name = "TicsZ";
            this.TicsZ.Size = new System.Drawing.Size(55, 20);
            this.TicsZ.TabIndex = 28;
            this.TicsZ.Text = "5";
            this.TicsZ.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(227, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Шаг оси Y";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(229, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Шаг оси Х";
            // 
            // TicsY
            // 
            this.TicsY.Location = new System.Drawing.Point(229, 69);
            this.TicsY.Name = "TicsY";
            this.TicsY.Size = new System.Drawing.Size(55, 20);
            this.TicsY.TabIndex = 25;
            this.TicsY.Text = "5";
            // 
            // TicsX
            // 
            this.TicsX.Location = new System.Drawing.Point(229, 31);
            this.TicsX.Name = "TicsX";
            this.TicsX.Size = new System.Drawing.Size(55, 20);
            this.TicsX.TabIndex = 24;
            this.TicsX.Text = "5";
            // 
            // MultiPlot
            // 
            this.MultiPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MultiPlot.Appearance = System.Windows.Forms.Appearance.Button;
            this.MultiPlot.AutoSize = true;
            this.MultiPlot.Location = new System.Drawing.Point(137, 431);
            this.MultiPlot.Margin = new System.Windows.Forms.Padding(3, 3, 3, 33);
            this.MultiPlot.Name = "MultiPlot";
            this.MultiPlot.Size = new System.Drawing.Size(57, 23);
            this.MultiPlot.TabIndex = 30;
            this.MultiPlot.Text = "MultiPlot";
            this.MultiPlot.UseVisualStyleBackColor = true;
            this.MultiPlot.CheckedChanged += new System.EventHandler(this.MultiPlot_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(251, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Система координат:";
            // 
            // DontLegend
            // 
            this.DontLegend.AutoSize = true;
            this.DontLegend.Location = new System.Drawing.Point(309, 135);
            this.DontLegend.Name = "DontLegend";
            this.DontLegend.Size = new System.Drawing.Size(90, 17);
            this.DontLegend.TabIndex = 34;
            this.DontLegend.Text = "Без подписи";
            this.DontLegend.UseVisualStyleBackColor = true;
            // 
            // ImgWidth
            // 
            this.ImgWidth.Location = new System.Drawing.Point(226, 77);
            this.ImgWidth.Name = "ImgWidth";
            this.ImgWidth.Size = new System.Drawing.Size(86, 20);
            this.ImgWidth.TabIndex = 35;
            this.ImgWidth.Text = "640";
            this.ImgWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ImgHeight
            // 
            this.ImgHeight.Location = new System.Drawing.Point(336, 77);
            this.ImgHeight.Name = "ImgHeight";
            this.ImgHeight.Size = new System.Drawing.Size(87, 20);
            this.ImgHeight.TabIndex = 36;
            this.ImgHeight.Text = "400";
            this.ImgHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(318, 81);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(12, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "x";
            // 
            // XrangeEnd
            // 
            this.XrangeEnd.Location = new System.Drawing.Point(62, 31);
            this.XrangeEnd.Name = "XrangeEnd";
            this.XrangeEnd.Size = new System.Drawing.Size(45, 20);
            this.XrangeEnd.TabIndex = 38;
            this.XrangeEnd.Text = "100.1";
            // 
            // YrangeEnd
            // 
            this.YrangeEnd.Location = new System.Drawing.Point(62, 69);
            this.YrangeEnd.Name = "YrangeEnd";
            this.YrangeEnd.Size = new System.Drawing.Size(45, 20);
            this.YrangeEnd.TabIndex = 39;
            this.YrangeEnd.Text = "100.1";
            // 
            // ZrangeEnd
            // 
            this.ZrangeEnd.Location = new System.Drawing.Point(62, 110);
            this.ZrangeEnd.Name = "ZrangeEnd";
            this.ZrangeEnd.Size = new System.Drawing.Size(45, 20);
            this.ZrangeEnd.TabIndex = 40;
            this.ZrangeEnd.Text = "100.1";
            this.ZrangeEnd.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(53, 34);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(10, 13);
            this.label15.TabIndex = 41;
            this.label15.Text = ":\r\n";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(53, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(10, 13);
            this.label16.TabIndex = 42;
            this.label16.Text = ":\r\n";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(53, 115);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(10, 13);
            this.label17.TabIndex = 43;
            this.label17.Text = ":\r\n";
            this.label17.Visible = false;
            // 
            // gbDecart
            // 
            this.gbDecart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDecart.Controls.Add(this.NameX);
            this.gbDecart.Controls.Add(this.ZrangeEnd);
            this.gbDecart.Controls.Add(this.YrangeEnd);
            this.gbDecart.Controls.Add(this.XrangeEnd);
            this.gbDecart.Controls.Add(this.label12);
            this.gbDecart.Controls.Add(this.label3);
            this.gbDecart.Controls.Add(this.TicsX);
            this.gbDecart.Controls.Add(this.label17);
            this.gbDecart.Controls.Add(this.TicsY);
            this.gbDecart.Controls.Add(this.label15);
            this.gbDecart.Controls.Add(this.label11);
            this.gbDecart.Controls.Add(this.XrangeStart);
            this.gbDecart.Controls.Add(this.label16);
            this.gbDecart.Controls.Add(this.TicsZ);
            this.gbDecart.Controls.Add(this.YrangeStart);
            this.gbDecart.Controls.Add(this.label10);
            this.gbDecart.Controls.Add(this.NameY);
            this.gbDecart.Controls.Add(this.label5);
            this.gbDecart.Controls.Add(this.label9);
            this.gbDecart.Controls.Add(this.label6);
            this.gbDecart.Controls.Add(this.label8);
            this.gbDecart.Controls.Add(this.ZrangeStart);
            this.gbDecart.Controls.Add(this.NameZ);
            this.gbDecart.Controls.Add(this.label7);
            this.gbDecart.Location = new System.Drawing.Point(491, 39);
            this.gbDecart.Margin = new System.Windows.Forms.Padding(2);
            this.gbDecart.Name = "gbDecart";
            this.gbDecart.Padding = new System.Windows.Forms.Padding(2);
            this.gbDecart.Size = new System.Drawing.Size(292, 93);
            this.gbDecart.TabIndex = 44;
            this.gbDecart.TabStop = false;
            this.gbDecart.Text = "Область построения";
            // 
            // systemCord
            // 
            this.systemCord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.systemCord.FormattingEnabled = true;
            this.systemCord.Items.AddRange(new object[] {
            "Декартовая",
            "Полярная",
            "Сферическая - горизонтальная",
            "Сферическая - вертикальная"});
            this.systemCord.Location = new System.Drawing.Point(366, 3);
            this.systemCord.Name = "systemCord";
            this.systemCord.Size = new System.Drawing.Size(151, 21);
            this.systemCord.TabIndex = 48;
            this.systemCord.SelectedIndexChanged += new System.EventHandler(this.SystemCord_SelectedIndexChanged);
            // 
            // typeMetricMash
            // 
            this.typeMetricMash.BackColor = System.Drawing.Color.White;
            this.typeMetricMash.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeMetricMash.FormattingEnabled = true;
            this.typeMetricMash.Items.AddRange(new object[] {
            "Pix",
            "Cm"});
            this.typeMetricMash.Location = new System.Drawing.Point(168, 77);
            this.typeMetricMash.Name = "typeMetricMash";
            this.typeMetricMash.Size = new System.Drawing.Size(51, 21);
            this.typeMetricMash.TabIndex = 49;
            this.typeMetricMash.SelectedIndexChanged += new System.EventHandler(this.Typemetric_SelectedIndexChanged);
            // 
            // typeMetricGraf
            // 
            this.typeMetricGraf.BackColor = System.Drawing.Color.White;
            this.typeMetricGraf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeMetricGraf.FormattingEnabled = true;
            this.typeMetricGraf.Items.AddRange(new object[] {
            "Pix",
            "Cm"});
            this.typeMetricGraf.Location = new System.Drawing.Point(168, 48);
            this.typeMetricGraf.Name = "typeMetricGraf";
            this.typeMetricGraf.Size = new System.Drawing.Size(51, 21);
            this.typeMetricGraf.TabIndex = 55;
            this.typeMetricGraf.SelectedIndexChanged += new System.EventHandler(this.TypeMetricGraf_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(318, 52);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(12, 13);
            this.label20.TabIndex = 54;
            this.label20.Text = "x";
            // 
            // GrafHeight
            // 
            this.GrafHeight.Location = new System.Drawing.Point(336, 48);
            this.GrafHeight.Name = "GrafHeight";
            this.GrafHeight.Size = new System.Drawing.Size(87, 20);
            this.GrafHeight.TabIndex = 53;
            this.GrafHeight.Text = "400";
            this.GrafHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GrafWidth
            // 
            this.GrafWidth.Location = new System.Drawing.Point(226, 48);
            this.GrafWidth.Name = "GrafWidth";
            this.GrafWidth.Size = new System.Drawing.Size(86, 20);
            this.GrafWidth.TabIndex = 52;
            this.GrafWidth.Text = "640";
            this.GrafWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gpTerm
            // 
            this.gpTerm.Controls.Add(this.DirFile);
            this.gpTerm.Controls.Add(this.MashCh);
            this.gpTerm.Controls.Add(this.label2);
            this.gpTerm.Controls.Add(this.FileName);
            this.gpTerm.Controls.Add(this.label1);
            this.gpTerm.Controls.Add(this.GrafCh);
            this.gpTerm.Controls.Add(this.GrafWidth);
            this.gpTerm.Controls.Add(this.GrafHeight);
            this.gpTerm.Controls.Add(this.label20);
            this.gpTerm.Controls.Add(this.typeMetricGraf);
            this.gpTerm.Controls.Add(this.typeMetricMash);
            this.gpTerm.Controls.Add(this.label14);
            this.gpTerm.Controls.Add(this.ImgWidth);
            this.gpTerm.Controls.Add(this.ImgHeight);
            this.gpTerm.Location = new System.Drawing.Point(12, 24);
            this.gpTerm.Name = "gpTerm";
            this.gpTerm.Size = new System.Drawing.Size(426, 108);
            this.gpTerm.TabIndex = 56;
            this.gpTerm.TabStop = false;
            // 
            // MashCh
            // 
            this.MashCh.AutoSize = true;
            this.MashCh.Checked = true;
            this.MashCh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MashCh.Location = new System.Drawing.Point(9, 79);
            this.MashCh.Name = "MashCh";
            this.MashCh.Size = new System.Drawing.Size(158, 17);
            this.MashCh.TabIndex = 59;
            this.MashCh.Text = "Масштабируемый размер";
            this.MashCh.UseVisualStyleBackColor = true;
            this.MashCh.CheckedChanged += new System.EventHandler(this.MashCh_CheckedChanged);
            // 
            // GrafCh
            // 
            this.GrafCh.AutoSize = true;
            this.GrafCh.Checked = true;
            this.GrafCh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GrafCh.Location = new System.Drawing.Point(9, 51);
            this.GrafCh.Name = "GrafCh";
            this.GrafCh.Size = new System.Drawing.Size(111, 17);
            this.GrafCh.TabIndex = 60;
            this.GrafCh.Text = "Размер графика";
            this.GrafCh.UseVisualStyleBackColor = true;
            this.GrafCh.CheckedChanged += new System.EventHandler(this.GrafCh_CheckedChanged);
            // 
            // terminal
            // 
            this.terminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.terminal.FormattingEnabled = true;
            this.terminal.Items.AddRange(new object[] {
            "Windows",
            "Png",
            "Svg",
            "Dumb"});
            this.terminal.Location = new System.Drawing.Point(120, 3);
            this.terminal.Name = "terminal";
            this.terminal.Size = new System.Drawing.Size(121, 21);
            this.terminal.TabIndex = 57;
            this.terminal.SelectedIndexChanged += new System.EventHandler(this.Terminal_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 6);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(102, 13);
            this.label21.TabIndex = 58;
            this.label21.Text = "Терминал вывода:";
            // 
            // ManagePlot
            // 
            this.ManagePlot.AllowDrop = true;
            this.ManagePlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ManagePlot.AutoScroll = true;
            this.ManagePlot.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ManagePlot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ManagePlot.Location = new System.Drawing.Point(12, 159);
            this.ManagePlot.Name = "ManagePlot";
            this.ManagePlot.Size = new System.Drawing.Size(771, 269);
            this.ManagePlot.TabIndex = 59;
            this.ManagePlot.DragEnter += new System.Windows.Forms.DragEventHandler(this.ManagePlot_DragEnter);
            // 
            // DirFile
            // 
            this.DirFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DirFile.Location = new System.Drawing.Point(9, 25);
            this.DirFile.Name = "DirFile";
            this.DirFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DirFile.Size = new System.Drawing.Size(210, 20);
            this.DirFile.TabIndex = 60;
            // 
            // FormPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(794, 461);
            this.Controls.Add(this.ManagePlot);
            this.Controls.Add(this.Plot);
            this.Controls.Add(this.check3D);
            this.Controls.Add(this.FuncCheck);
            this.Controls.Add(this.Func);
            this.Controls.Add(this.MultiPlot);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.DontLegend);
            this.Controls.Add(this.gbDecart);
            this.Controls.Add(this.systemCord);
            this.Controls.Add(this.gpTerm);
            this.Controls.Add(this.terminal);
            this.Controls.Add(this.label21);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(960, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(810, 500);
            this.Name = "FormPlot";
            this.ShowIcon = false;
            this.Text = "GnuPlot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbDecart.ResumeLayout(false);
            this.gbDecart.PerformLayout();
            this.gpTerm.ResumeLayout(false);
            this.gpTerm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Plot;
        private System.Windows.Forms.CheckBox check3D;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox FuncCheck;
        public System.Windows.Forms.TextBox Func;
       // private FilePlotManager ManagePlot;
        private System.Windows.Forms.TextBox XrangeStart;
        private System.Windows.Forms.TextBox YrangeStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ZrangeStart;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox NameZ;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox NameY;
        private System.Windows.Forms.TextBox NameX;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TicsZ;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TicsY;
        private System.Windows.Forms.TextBox TicsX;
        private System.Windows.Forms.CheckBox MultiPlot;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox DontLegend;
        private System.Windows.Forms.TextBox ImgWidth;
        private System.Windows.Forms.TextBox ImgHeight;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox XrangeEnd;
        private System.Windows.Forms.TextBox YrangeEnd;
        private System.Windows.Forms.TextBox ZrangeEnd;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox gbDecart;
        private System.Windows.Forms.ComboBox systemCord;
        private System.Windows.Forms.ComboBox typeMetricMash;
        private System.Windows.Forms.ComboBox typeMetricGraf;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox GrafHeight;
        private System.Windows.Forms.TextBox GrafWidth;
        private System.Windows.Forms.GroupBox gpTerm;
        private System.Windows.Forms.ComboBox terminal;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox GrafCh;
        private System.Windows.Forms.CheckBox MashCh;
        private FilePlotManager ManagePlot;
        private directoryChange DirFile;
    }
}

