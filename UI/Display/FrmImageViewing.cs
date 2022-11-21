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
using WeifenLuo.WinFormsUI.Docking;

namespace Hix_CCD_Module.UI
{
    public partial class FrmImageViewing : DockContent
    {
        List<Bitmap> images;
        bool isLoop = false;
        public FrmImageViewing()
        {
            InitializeComponent();
        }
        private int index = 0;
        public List<Bitmap> Images
        {
            get
            {
                return images;
            }
            set
            {
                if (isLoop)
                {
                    BtnLoop_Click(null, null);
                }
                images = new List<Bitmap>();
                images = value;
                if (value != null && value.Count > 0)
                {
                    index = 0;
                    if (images[index] != null)
                        cImageView1.Image = value[0];
                }
            }
        }
        public void SetImage(Image image)
        {
            cImageView1.Image = image;
        }
        public void SetImage_Manual(ImageUnit image,string path)
        {
            //cImageView1.Image = image.Image;
            Task.Run(() => this.Invoke(new Action(() =>
            {
                image.Image.Save(path);
            })));
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            index--;
            if (images != null && images.Count > 0)
            {
                if (index < 0)
                {
                    index = images.Count - 1;
                }
                if (images[index] != null)
                    cImageView1.Image = images[index];
            }
        }

        private void BtnLoop_Click(object sender, EventArgs e)
        {
            if (!isLoop)
            {
                isLoop = true;
                btnLoop.BackgroundImage = Properties.Resources.IcoStop;
                Task.Run(() =>
                {
                    while (isLoop)
                    {
                        Invoke(new Action(() => BtnNext_Click(null, null)));
                        Thread.Sleep(500);
                    }
                });

            }
            else
            {
                isLoop = false;
                btnLoop.BackgroundImage = Properties.Resources.Loop;
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            index++;
            if (images != null && images.Count > 0)
            {
                if (index >= images.Count)
                {
                    index = 0;
                }
                if (images[index] != null)
                    cImageView1.Image = images[index];
            }
        }
    }
}
