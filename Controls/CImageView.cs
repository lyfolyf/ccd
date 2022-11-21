using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hix_CCD_Module.Controls
{
    public partial class CImageView : UserControl
    {
        public CImageView()
        {
            InitializeComponent();
            this.Resize += CImageView_Resize;
        }

        public Image Image
        {
            get
            {
                return bingPictrueBox1.Image;
            }
            set
            {
                if (bingPictrueBox1.Image != null)
                {
                    bingPictrueBox1.Image.Dispose();
                }
                try
                {
                    bingPictrueBox1.Image = (Image)value.Clone();
                }
                catch
                {
                    return;
                }
            }
        }

        private void CImageView_Resize(object sender, EventArgs e)
        {
            bingPictrueBox1.IsInit = false;
        }
    }
}
