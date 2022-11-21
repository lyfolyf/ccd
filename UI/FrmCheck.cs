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
    public partial class FrmCheck : Form
    {
        public FrmCheck()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmCheck_Load(object sender, EventArgs e)
        {

        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileopen = new OpenFileDialog();
                fileopen.Filter = "csv文件|*.csv";
                if (fileopen.ShowDialog() == DialogResult.OK)
                {

                    CommonValue.isCheck = chCheck.Checked;
                    CommonValue.iCheckCount = Convert.ToInt32(numCheckCount.Value);
                    CommonValue.dWHValue = Convert.ToDouble(numWH.Value);
                    CommonValue.strCheckPath = fileopen.FileName;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    return;
                }


            }
            catch (Exception ex)
            {

                
            }
        }
    }
}
