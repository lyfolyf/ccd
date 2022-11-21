using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hix_CCD_Module.UI
{
    public partial class FrmWaitting : Form
    {
        public FrmWaitting()
        {
            InitializeComponent();
        }
        DateTime start;

        private void FrmWaitting_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        public void Timing()
        {
            start = DateTime.Now;
            new Thread(() =>
            {
                try
                {
                    while (!this.IsDisposed)
                    {
                        Thread.Sleep(50);
                        this.Invoke(new Action(() =>
                        {
                            TimeSpan timeSpan = DateTime.Now - start;
                            label2.Text = timeSpan.ToString();
                            Refresh();
                        }));
                    }
                }
                catch { }
            })
            { IsBackground = true }.Start();
        }

        public void Timing2()
        {
            this.Invoke(new Action(() => MaxVal(150)));
            start = DateTime.Now;
            new Thread(() =>
            {
                try
                {
                    int index = 0;
                    while (!this.IsDisposed)
                    {
                        if (index != 140)
                        {
                            index++;
                        }
                        Thread.Sleep(50);
                        this.Invoke(new Action(() =>
                        {
                            TimeSpan timeSpan = DateTime.Now - start;
                            label2.Text = timeSpan.ToString();
                            Value(index);
                            Refresh();
                        }));
                    }
                }
                catch { }
            })
            { IsBackground = true }.Start();
        }
        public void Timing3()
        {
            this.Invoke(new Action(() => progressBar1.Style = ProgressBarStyle.Marquee));
            start = DateTime.Now;
            new Thread(() =>
            {
                try
                {
                    while (!this.IsDisposed)
                    {
                        Thread.Sleep(50);
                        this.Invoke(new Action(() =>
                        {
                            TimeSpan timeSpan = DateTime.Now - start;
                            label2.Text = timeSpan.ToString();
                            //Value(100);
                            Refresh();
                        }));
                    }
                }
                catch { }
            })
            { IsBackground = true }.Start();
        }
        public void MaxVal(int val)
        {
            progressBar1.Maximum = val;
        }
        public void Value(int val)
        {
            progressBar1.Value = val;
        }
    }
}
