using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Cognex.VisionPro;

namespace Hix_CCD_Module.UI
{
    public partial class FrmResultViewing : DockContent
    {
        public FrmResultViewing()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">1或者2</param>
        /// <param name="image"></param>
        public void ShowImage(int index,CogImage8Grey image)
        {
            if (index == 1) { this.cogRecordDisplay1.Image = image;
                this.cogRecordDisplay1.Fit();
            }
            if (index == 2) { this.cogRecordDisplay2.Image = image;
                this.cogRecordDisplay2.Fit();
            }
        }
        public void ShowImage(CogImage8Grey image1, CogImage8Grey image2)
        {
            this.cogRecordDisplay1.Image = image1;
            this.cogRecordDisplay2.Image = image2;
         
            this.cogRecordDisplay1.Fit();
            this.cogRecordDisplay2.Fit();
        }

        public void ShowText(int num,string msg1,CogColorConstants col)
        {

            CogGraphicLabel label = new CogGraphicLabel();
            label.Font = new Font("宋体",30);
            label.Color = col;
            label.Text = msg1;
            label.X = 230;
            label.Y =50;
            if (num ==1)
            {
                this.cogRecordDisplay1.StaticGraphics.Clear();
                this.cogRecordDisplay1.InteractiveGraphics.Clear();
                this.cogRecordDisplay1.InteractiveGraphics.Add(label,"",false);
            }
            else
            {
                this.cogRecordDisplay2.StaticGraphics.Clear();
                this.cogRecordDisplay2.InteractiveGraphics.Clear();
                this.cogRecordDisplay2.InteractiveGraphics.Add(label, "", false);
            }
        }


        public void ShowGraphic(int index, ICogRecord record)
        {
            try
            {
                if (index == 1)
                {
                    this.cogRecordDisplay1.StaticGraphics.Clear();
                    this.cogRecordDisplay1.InteractiveGraphics.Clear();
                    this.cogRecordDisplay1.Record = record.SubRecords[5]; //5
                    this.cogRecordDisplay1.Fit();
                }
                if (index == 2)
                {
                    this.cogRecordDisplay2.StaticGraphics.Clear();
                    this.cogRecordDisplay2.InteractiveGraphics.Clear();
                    this.cogRecordDisplay2.Record = record.SubRecords[5];
                    this.cogRecordDisplay2.Fit();
                }
            }
            catch(Exception ex)
            {
                string str = ex.Message;
            }
        }
        public void ShowGraphic(ICogRecord record1, ICogRecord record2)
        {
            this.cogRecordDisplay1.Record = record1.SubRecords[3];
            this.cogRecordDisplay1.Fit();
            this.cogRecordDisplay2.Record = record2.SubRecords[3];
            this.cogRecordDisplay2.Fit();
        }
    }
}
