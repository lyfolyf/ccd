using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hix_CCD_Module.UI.Display
{
    public partial class FrmControlView : Form
    {
        public FrmControlView()
        {
            InitializeComponent();
        }
        public Control DispControl
        {
            set
            {
                this.Controls.Clear();
                value.Dock = DockStyle.Fill;
                Controls.Add(value);
            }
        }
    }
}
