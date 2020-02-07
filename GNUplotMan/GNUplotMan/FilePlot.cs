using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace GNUplotMan
{
    public enum LineStyle { Line, LineAndPoint, Point };

    public partial class FilePlot : UserControl
    {
        private static ColorDialog color = new ColorDialog()
        {
            FullOpen = true,
            CustomColors = new int[] { 6916092, 15195440, 16107657, 1836924,
                3758726, 12566463, 7526079, 7405793,
                6945974, 241502, 2296476, 5130294,
                3102017, 7324121, 14993507, 11730944, },
            AllowFullOpen = true

        };
        private static int StaticCounter;
        private string FullName;
        private LineStyle WithType;//тип построения линии 
        
        public string Style
        {
            get
            {
                String HexConverter(Color c) => "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
                string s = $"'{FullName}' using {ColumUse.Text} title " + (legendLine.Text == "" ? "notitle" : $"'{legendLine.Text}' with ");
                
                switch (WithType)
                {
                    case LineStyle.Line:
                        return s + $" lines lc rgb '{HexConverter(ColorLine.BackColor)}' lw {LineWidht.Text} dt {DashType.Text}";
                    case LineStyle.LineAndPoint:
                        return s + $" linespoints lc rgb '{HexConverter(ColorLine.BackColor)}' lw {LineWidht.Text} dt {DashType.Text} ps {PointSize.Text} pt {PointType.Text}";
                    case LineStyle.Point:
                        return s + $" points lc rgb '{HexConverter(ColorLine.BackColor)}' ps {PointSize.Text} pt {PointType.Text}";
                    default:
                        return "";
                }
            }
        }

        public FilePlot() => InitializeComponent();

        public FilePlot(string filename)
        {
            if (!File.Exists(filename))
            {
                MessageBox.Show("Файл " + filename + " не найден.", "Ошибко.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            InitializeComponent();
            Dock = DockStyle.Top;
            FileName.Text = new FileInfo(filename).Name;

            var Filestr = File.ReadAllLines(filename);
            Count.Text = (Filestr.Length).ToString();
            Colum.Text = (Regex.Matches(Filestr[0], @"(?:\-?\d+\.\d{1,16}[E,e](\++|\-+)\d{1,4})|(?:\-?\d{1,5}\.\d{1,16})|(?:\-?\d{1,5})").Count).ToString();

            FullName = filename;

            ToolTip Hint = new ToolTip();
            Hint.IsBalloon = true;
            Hint.ShowAlways = true;
            Hint.SetToolTip(FileName, filename);

            Hint.SetToolTip(Count, "Количество строк.");
            Hint.SetToolTip(Colum, "Количество столбцов.");
            Hint.SetToolTip(ColumUse, "Какие столбцы использовать.\r\nПо умолчанию - 1:2 ");

            legendLine.Text = "Линия " + StaticCounter;
            ColumUse.Text = "1:2";
            ColorLine.BackColor = color.Color;
            
            TypeWith.SelectedIndex = 0;
            color.Color = ColorTranslator.FromOle(color.CustomColors[StaticCounter]);
            PointSize.SelectedIndex = 1;
            PointType.SelectedIndex = StaticCounter;

            LineWidht.SelectedIndex = 1;
            DashType.SelectedIndex = StaticCounter++;
        }

        private void DeleteFile_Click(object sender, EventArgs e)
        {
            this.Dispose();
            StaticCounter--;
        }

        private void TypeWith_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch( (sender as ComboBox).SelectedIndex)//Какой выбран тип соединения точек 
            {
                case 0://Line
                    WithType = LineStyle.Line;
                    PanelLine.Visible = true;
                    PanelPoint.Visible = false;
                    break;
                case 1://LineAndPoint
                    WithType = LineStyle.LineAndPoint;
                    PanelLine.Visible = true;
                    PanelPoint.Visible = true;
                    break;
                case 2://Point
                    WithType = LineStyle.Point;
                    PanelLine.Visible = false;
                    PanelPoint.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void ColorLine_Click(object sender, EventArgs e)
        {
            if ((color.ShowDialog()) == DialogResult.OK)
                (sender as Button).BackColor = color.Color;
        }
    }
}
