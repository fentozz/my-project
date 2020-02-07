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
    public partial class FilePlotManager : UserControl
    {
        public IEnumerable<FilePlot> Plots => Controls.Cast<FilePlot>();

        public FilePlotManager() => InitializeComponent();
        private void Add(string FileDirectory) => Controls.Add(new FilePlot(FileDirectory));
        

        private void FilePlotManager_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;
        }

        private void FilePlotManager_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) this.Add(file);
            FilesDropped?.Invoke();//событие произошло
        }
        //новое событие
        public event PlotManagerEvent FilesDropped;
        public delegate void PlotManagerEvent();
    }
}
