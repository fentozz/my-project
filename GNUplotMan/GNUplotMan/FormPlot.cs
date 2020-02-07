using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GNUplotMan
{
    public partial class FormPlot : Form
    {
        Process GnuPlotProc;
        StreamWriter gnupStWr;//поток для общения с GnuPlot

        ToolTip Hint = new ToolTip();
        List<string> funcMult = new List<string>();

        public FormPlot()
        {
            InitializeComponent();
        }

        private void FuncCheck_CheckedChanged(object sender, EventArgs e) =>
            Func.Enabled = (sender as CheckBox).Checked;

        private void OutPng_CheckedChanged(object sender, EventArgs e) =>
            typeMetricMash.Enabled = ImgHeight.Enabled = ImgWidth.Enabled = DirFile.Enabled = FileName.Enabled = (sender as CheckBox).Checked;

        /// <summary>
        /// Чтение из загруженных файлов
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> ReadFromFile() => ManagePlot.Plots.Select(q => q.Style);

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        void WritePolar()
        {
            if (!terminalOut()) return;
            //gnupStWr.WriteLine("\n" + (terminal.SelectedIndex == 1 ? "\nset terminal pngcairo size " + (typeMetGraf ? (Math.Round(double.Parse(GrafWidth.Text) * 37.938105)).ToString() : GrafWidth.Text) +
            //    "," + (typeMetGraf ? (Math.Round(double.Parse(GrafHeight.Text) * 37.938105)).ToString() : GrafHeight.Text) + " enhanced font 'Verdana,9' " : "\nset terminal windows"));
            //gnupStWr.WriteLine(terminal.SelectedIndex == 1 ? "\nset output '" + @Directory.Text + @"\" + @FileName.Text + ".png'" : "\nunset output");

            gnupStWr.WriteLine("\nset polar");
            gnupStWr.WriteLine("\nset rrange [0:1]");

            gnupStWr.WriteLine("\nset size square 0.9,0.9");
            gnupStWr.WriteLine("\nset origin 0,0.05");

            gnupStWr.WriteLine("\nset angles degrees");
            gnupStWr.WriteLine("\nset key lmargin");
            gnupStWr.WriteLine("\nunset border");
            gnupStWr.WriteLine("\nset xtics axis nomirror scale 0 font \", 10\"");
            gnupStWr.WriteLine("\nset ytics axis nomirror font \", 10\"");

            gnupStWr.WriteLine("\nset grid polar 10 ls 1 lw 0.6 lc rgb '#000000' dashtype 3");

            //gnupStWr.WriteLine("\nset xtics (\"\" " + string.Join(", \"\" ", Enumerable.Range(0, 10).Select(i => i * .001)) + ", \"\" 0.01)");
            gnupStWr.WriteLine("\nset xtics (\"\" 0, \"\" 0.001, \"\" 0.002, \"\" 0.003, \"\" 0.004, \"\" 0.005, \"\" 0.006, \"\" 0.007, \"\" 0.008, \"\" 0.009, \"\" 0.01)");
            gnupStWr.WriteLine("\nset rtics (\"\"0.1, \"\"0.2, \"\"0.3, \"\"0.4,\"\"0.5, \"\"0.6, \"\"0.7, \"\"0.8, \"\"0.9, \"\"1)");

            gnupStWr.WriteLine("\nset ytics 0,0.1,1");
        }

        bool terminalOut()
        {
            string widht = (typeMetGraf ? (Math.Round(double.Parse(GrafWidth.Text) * 37.938105)).ToString() : GrafWidth.Text);
            string height = (typeMetGraf ? (Math.Round(double.Parse(GrafHeight.Text) * 37.938105)).ToString() : GrafHeight.Text);
            if (widht == "" || widht == "0" || height == "" || height == "0")
            {
                MessageBox.Show("Укажите размер графика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            switch (terminal.Text)
            {
                case "Windows":
                    gnupStWr.WriteLine("\nset terminal windows");
                    gnupStWr.WriteLine("\nunset output");
                    break;
                case "Png":
                    //gnupStWr.WriteLine("\nset bmargin 4");
                    //gnupStWr.WriteLine("\nset size 0.2,0.2");
                    if (GrafCh.Checked) gnupStWr.WriteLine("\nset terminal pngcairo size " + widht + "," + height + " enhanced font 'Verdana,9' ");
                    else gnupStWr.WriteLine("\nset terminal pngcairo enhanced font 'Verdana,9'");
                    if (!Directory.Exists(DirFile.TextDir))
                    {
                        MessageBox.Show("Нет такой директории", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    gnupStWr.WriteLine("\nset output '" + DirFile.TextDir + @"\" + FileName.Text + ".png'");
                    if (File.Exists(DirFile.TextDir + @"\" + FileName.Text + ".png")) File.Delete(DirFile.TextDir + @"\" + FileName.Text + ".png");
                    break;
                case "Svg":
                    if (GrafCh.Checked) gnupStWr.WriteLine("\nset terminal svg size " + widht + "," + height + " fixed standalone enhanced font 'Verdana,9' ");
                    else gnupStWr.WriteLine("\nset terminal svg fixed standalone enhanced font 'Verdana,9'");
                    if (!Directory.Exists(DirFile.TextDir))
                    {
                        MessageBox.Show("Нет такой директории", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    gnupStWr.WriteLine("\nset output '" + DirFile.TextDir + @"\" + FileName.Text + ".svg'");
                    if (File.Exists(DirFile.TextDir + @"\" + FileName.Text + ".svg")) File.Delete(DirFile.TextDir + @"\" + FileName.Text + ".svg");
                    break;
                case "Dumb":
                    gnupStWr.WriteLine("\nset term dumb feed");
                    //gnupStWr.WriteLine("\nset term dumb mono size 60,15");
                    //gnupStWr.WriteLine("\nset autoscale");

                    gnupStWr.WriteLine("\nset output '" + DirFile.TextDir + @"\" + FileName.Text + ".txt'");
                    break;
            }
            return true;
        }

        private void RunPlot(object sender, EventArgs e)
        {
            //сбрасываем все настройки 
            gnupStWr.WriteLine("\nreset");//reset не сбрасывает multiplot

            

            string TypePlot = "\nplot";

            switch (systemCord.Text)
            {
                case "Декартовая":
                    //терминал выхода        
                    if (!terminalOut()) return;
                    

                    //Отключаем верхнюю и правую рамку
                    gnupStWr.WriteLine("\nset border 3 back lw 2");
                    gnupStWr.WriteLine("\nset tics nomirror");
                    //включение сетки
                    gnupStWr.WriteLine("\nset grid back lc rgb '#808080' lt 0 lw 1");

                    //3д или нет
                    TypePlot = check3D.Checked ? "\nsplot " : "\nplot ";

                    if (XrangeStart.Text != "") gnupStWr.WriteLine("\nset xrange " + "[" + XrangeStart.Text + ":" + XrangeEnd.Text + "]");
                    if (YrangeStart.Text != "") gnupStWr.WriteLine("\nset yrange " + "[" + YrangeStart.Text + ":" + YrangeEnd.Text + "]");

                    if (NameX.Text != "") gnupStWr.WriteLine("\nset xlabel '" + NameX.Text + "'");
                    if (NameY.Text != "") gnupStWr.WriteLine("\nset ylabel '" + NameY.Text + "'");

                    if (TicsX.Text != "") gnupStWr.WriteLine("\nset xtics " + TicsX.Text);
                    if (TicsY.Text != "") gnupStWr.WriteLine("\nset ytics " + TicsY.Text);
                    break;
                case "Полярная":
                case "Сферическая - горизонтальная":
                    WritePolar();
                    for (int i = 0; i < 360; i += 30)
                        gnupStWr.WriteLine("\nset label \"{0}\" at 1.1*cos({0}),1.1*sin({0}) center font \", 10\"", i);
                    gnupStWr.WriteLine("\nset style data lines");
                    break;
                case "Сферическая - вертикальная":
                    WritePolar();
                    gnupStWr.WriteLine("\nset label \"90\" at 1.1 * cos(0),1.1 * sin(0) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"60\" at 1.1 * cos(30),1.1 * sin(30) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"30\" at 1.1 * cos(60),1.1 * sin(60) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"0\" at 1.1 * cos(90),1.1 * sin(90) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"330\" at 1.1 * cos(120),1.1 * sin(120) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"300\" at 1.1 * cos(150),1.1 * sin(150) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"270\" at 1.1 * cos(180),1.1 * sin(180) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"240\" at 1.1 * cos(210),1.1 * sin(210) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"210\" at 1.1 * cos(240),1.1 * sin(240) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"180\" at 1.1 * cos(270),1.09 * sin(270) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"150\" at 1.1 * cos(300),1.1 * sin(300) center font \",10\"");
                    gnupStWr.WriteLine("\nset label \"120\" at 1.1 * cos(330),1.1 * sin(330) center font \",10\"");
                    gnupStWr.WriteLine("\nset style data lines");
                    break;
            }

            string Plot = FuncCheck.Checked ? Func.Text + (DontLegend.Checked ? " notitle" : "") : string.Join(", \\\n", ReadFromFile());

            gnupStWr.WriteLine(TypePlot + Plot);
            gnupStWr.Flush();

            if (MultiPlot.Checked)
            {
                if (FuncCheck.Checked) funcMult.Add(Func.Text);
                else funcMult.AddRange(ReadFromFile());
                Hint.SetToolTip(Func, string.Join("\n\r", funcMult));
            }
            else if ( !MultiPlot.Checked && terminal.SelectedIndex == 1 && MashCh.Checked)
            {//только с png
                gnupStWr.WriteLine("\nreset");
                gnupStWr.WriteLine("\nset terminal windows");
                gnupStWr.WriteLine("\nunset output");
                gnupStWr.Flush();

                {
                    int i = 0;
                    while (!File.Exists(DirFile.TextDir + @"\" + FileName.Text + ".png"))
                    {
                        i++;
                        System.Threading.Thread.Sleep(100);
                        if ( i == 300)
                        {
                            MessageBox.Show("Не удалось найти файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                void resize()
                {
                    using (FileStream stream = new FileStream(DirFile.TextDir + @"\" + FileName.Text + ".png", FileMode.Open))
                    {
                        int width = ImgWidth.Text == "" ? 0 : (typeMetMash ? (int)(Math.Round(double.Parse(ImgWidth.Text) * 37.938105)) : (int)double.Parse(ImgWidth.Text));
                        int height = ImgHeight.Text == "" ? 0 : (typeMetMash ? (int)(Math.Round(double.Parse(ImgHeight.Text) * 37.938105)) : (int)double.Parse(ImgHeight.Text));
                        if (width == 0 && height != 0)
                        {
                            int widthGraf = typeMetMash ? (int)(Math.Round(double.Parse(GrafWidth.Text) * 37.938105)) : (int)double.Parse(GrafWidth.Text);
                            int heightGraf = typeMetMash ? (int)(Math.Round(double.Parse(GrafHeight.Text) * 37.938105)) : (int)double.Parse(GrafHeight.Text);
                            ImgWidth.Text = (Math.Round((double)widthGraf / heightGraf * height)).ToString();
                        }
                        else if (height == 0 && width != 0)
                        {
                            int widthGraf = typeMetMash ? (int)(Math.Round(double.Parse(GrafWidth.Text) * 37.938105)) : (int)double.Parse(GrafWidth.Text);
                            int heightGraf = typeMetMash ? (int)(Math.Round(double.Parse(GrafHeight.Text) * 37.938105)) : (int)double.Parse(GrafHeight.Text);
                            ImgHeight.Text = (Math.Round((double)heightGraf / widthGraf * width)).ToString();
                        }
                        else if (height == 0 && width == 0) return;

                        Bitmap ressiz = new Bitmap(stream);
                        ressiz = ResizeImage(ressiz,
                        typeMetMash ? (int)(Math.Round(double.Parse(ImgWidth.Text) * 37.938105)) : (int)double.Parse(ImgWidth.Text),
                        typeMetMash ? (int)(Math.Round(double.Parse(ImgHeight.Text) * 37.938105)) : (int)double.Parse(ImgHeight.Text));
                        stream.Close();
                        ressiz.Save(DirFile.TextDir + @"\" + FileName.Text + ".png");
                    }
                }

                int[] indexTemp = { typeMetricMash.SelectedIndex, typeMetricGraf.SelectedIndex };
                typeMetricMash.SelectedIndex = typeMetricGraf.SelectedIndex = 0;

                if (File.Exists(DirFile.TextDir + @"\" + FileName.Text + ".png"))
                    try
                    {
                        resize();
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1000);
                        resize();
                    }
                else MessageBox.Show("Ошипке");

                typeMetricMash.SelectedIndex = indexTemp[0];
                typeMetricGraf.SelectedIndex = indexTemp[1];
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //запускаем процесс GnuPlot при запуске программы
            GnuPlotProc = new Process();
            GnuPlotProc.StartInfo.FileName = @"Lib\gnuplot.exe";
            GnuPlotProc.StartInfo.UseShellExecute = false;
            GnuPlotProc.StartInfo.RedirectStandardInput = true;
            GnuPlotProc.StartInfo.CreateNoWindow = true;

            GnuPlotProc.Start();
            gnupStWr = GnuPlotProc.StandardInput;

            systemCord.SelectedIndex = 0;
            typeMetricMash.SelectedIndex = 0;
            typeMetricGraf.SelectedIndex = 0;
            terminal.SelectedIndex = 0;
            
            Hint.IsBalloon = true;
            Hint.ShowAlways = true;
            Hint.AutoPopDelay = 0;
            //Hint.SetToolTip(FileName, filename);
        }

        private void Directory_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директории для сохранения изображения:";
            if (DirDialog.ShowDialog(this) == DialogResult.OK)
            {
                (sender as TextBox).Text = DirDialog.SelectedPath;
            }
        }

        private void MultiPlot_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked) gnupStWr.WriteLine("\nset multiplot");
            else if (!(sender as CheckBox).Checked)
            {
                funcMult.Clear();
                Hint.RemoveAll();
                Hint.Hide(Func);
                gnupStWr.WriteLine("\nunset multiplot");
            }
            gnupStWr.Flush();
        }

        private void ManagePlot_FilesDropped() => FuncCheck.Checked = false;

        private void SystemCord_SelectedIndexChanged(object sender, EventArgs e) => gbDecart.Enabled = (sender as ComboBox).SelectedIndex == 0 ? true : false;

        bool typeMetMash = false;
        bool typeMetGraf = false;

        void ChangeMetric(ComboBox comboBox,ref TextBox width,ref TextBox hight, ref bool flag)
        {
            if (comboBox.SelectedIndex == 0 && flag)
            {
                flag = false;
                width.Text = width.Text != "" ? (Math.Round(double.Parse(width.Text) * 37.938105)).ToString() : "";
                hight.Text = hight.Text != "" ? (Math.Round(double.Parse(hight.Text) * 37.938105)).ToString() : "";
            }
            else if (comboBox.SelectedIndex == 1 && !flag)
            {
                flag = true;
                width.Text = width.Text != "" ? (double.Parse(width.Text) / 37.938105).ToString() : "";
                hight.Text = hight.Text != "" ? (double.Parse(hight.Text) / 37.938105).ToString() : "";
            }
        }

        private void Typemetric_SelectedIndexChanged(object sender, EventArgs e) => ChangeMetric(sender as ComboBox,ref ImgWidth,ref ImgHeight, ref typeMetMash);

        private void TypeMetricGraf_SelectedIndexChanged(object sender, EventArgs e) => ChangeMetric(sender as ComboBox, ref GrafWidth, ref GrafHeight, ref typeMetGraf);
        
        private void Terminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    gpTerm.Enabled = false;
                    break;
                case 1:
                    gpTerm.Enabled = true;
                    MashCh.Enabled = typeMetricMash.Enabled = ImgWidth.Enabled = ImgHeight.Enabled = true;
                    GrafCh.Enabled = GrafHeight.Enabled = GrafWidth.Enabled = typeMetricGraf.Enabled = true;
                    break;
                case 2:
                    gpTerm.Enabled = true;
                    MashCh.Enabled = typeMetricMash.Enabled = ImgWidth.Enabled = ImgHeight.Enabled = false;
                    GrafCh.Enabled = GrafHeight.Enabled = GrafWidth.Enabled = typeMetricGraf.Enabled = true;
                    break;
                case 3:
                    gpTerm.Enabled = true;
                    MashCh.Enabled = typeMetricMash.Enabled = ImgWidth.Enabled = ImgHeight.Enabled = false;
                    GrafCh.Enabled = GrafHeight.Enabled = GrafWidth.Enabled = typeMetricGraf.Enabled = false;
                    break;

            }
        }

        private void GrafCh_CheckedChanged(object sender, EventArgs e) => GrafHeight.Enabled = GrafWidth.Enabled = typeMetricGraf.Enabled = (sender as CheckBox).Checked;

        private void MashCh_CheckedChanged(object sender, EventArgs e) => ImgHeight.Enabled = ImgWidth.Enabled = typeMetricMash.Enabled = (sender as CheckBox).Checked;

        private void ManagePlot_DragEnter(object sender, DragEventArgs e)
        {
            ManagePlot_FilesDropped();
        }
    }
}
