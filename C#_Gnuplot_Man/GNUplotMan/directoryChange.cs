using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GNUplotMan
{
    public partial class directoryChange : UserControl
    {
        public directoryChange()
        {
            InitializeComponent();
        }

        public string TextDir
        {
            get
            {
                return Directory.Text;
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog DirDialog = new FolderBrowserDialog();
            DirDialog.Description = "Выбор директории для сохранения изображения:";
            if (DirDialog.ShowDialog(this) == DialogResult.OK)
            {
                Directory.Text = DirDialog.SelectedPath;
            }
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
    }
}
