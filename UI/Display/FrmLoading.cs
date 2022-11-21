using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hix_CCD_Module.UI
{
    public partial class FrmLoading : Form
    {
        public FrmLoading()
        {
            InitializeComponent();
            bingProgressBar1.BackColor = this.BackColor;
            cSize = Size;
        }
        Size cSize;
        public int Max { set { bingProgressBar1.Maximum = value; } }
        public int Value
        {
            set
            {
                if (value > bingProgressBar1.Maximum)
                    return;
                bingProgressBar1.Value = value;
            }
        }

        private void FrmLoading_Resize(object sender, EventArgs e)
        {
            Size = cSize;
        }
    }
}
