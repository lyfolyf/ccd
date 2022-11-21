
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace Hix_CCD_Module.UI
{
    public partial class FrmLog : DockContent
    {
        public FrmLog()
        {
            InitializeComponent();
        }
        public void UpdateLog(string log, Color color)
        {
            log = DateTime.Now.ToString("HH-mm-ss-fff")+ " : " + log + "\n";
            richTextBox1.AppendText(log);
            richTextBox1.SelectionStart = richTextBox1.TextLength - log.Length;
            richTextBox1.SelectionLength = log.Length;
            richTextBox1.SelectionColor = color;
            richTextBox1.SelectionStart = richTextBox1.TextLength - 1;
            richTextBox1.SelectionLength = 0;
            richTextBox1.ScrollToCaret();
            if (richTextBox1.TextLength > 30000)
            {
                richTextBox1.Clear();
            }
            //using (FileStream fs = new FileStream($@"D:\5.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //{
            //    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            //    {
            //        sw.WriteLine(log);
            //    }
            //}
        }
    }
}
