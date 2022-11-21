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
using Cognex.VisionPro;
using Bing.IVisionTool;

namespace Hix_CCD_Module.UI
{
    public partial class FormCa : Form
    {


        List<CogImage8Grey> DicImage = new List<CogImage8Grey>();
        public FormCa()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                txtLocalImagePath.Clear();
                DicImage.Clear();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "bmp文件|*.bmp";
                openFileDialog.Multiselect = true;
                DialogResult dr = openFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string[] files = openFileDialog.FileNames;
                    if (files.Length > 0)
                    {
                        for (int i = 0; i < files.Length; i++)
                        {
                            FileStream fs = new FileStream(files[i], FileMode.Open);
                            Bitmap bitmap = (Bitmap)Bitmap.FromStream(fs);
                            DicImage.Add(new CogImage8Grey(bitmap));
                            txtLocalImagePath.AppendText(files[i] + Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void FormCa_Load(object sender, EventArgs e)
        {

        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                    if (string.IsNullOrEmpty(cbCalibTools.Text))
                    {
                        MessageBox.Show("请先选择穴位!");
                        return;
                    }
                    if (DicImage.Count != 8)
                    {
                        MessageBox.Show("载入图片数量少于8张!");
                        return;
                    }

                    txtLocalImagePath.AppendText("标定开始!" + Environment.NewLine);
                    string[] arrStr1 = new string[] { "1#载具01", "1#载具02", "1#载具03", "1#载具04", "1#载具05", "1#载具06", "1#载具07", "1#载具08"};
                Task.Run(() => Invoke(new Action(() =>
                {
                    for (int i = 0; i < arrStr1.Length; i++)
                        {
                            string s = arrStr1[i];
                            if (cbCalibTools.Text.Contains("1#"))
                            {
                                s = arrStr1[i];
                            }
                            else
                            {
                                s = s.Replace("1#", "2#");
                            }
                            FrmMain.DicCalibTools[s].CalibrationImage = new Cognex.VisionPro.CogImage8Grey(DicImage[i]);
                            RunResult res = FrmMain.DicCalibTools[s].Calibretion();
                        System.Threading.Thread.Sleep(10000);
                            switch (res)
                            {
                                case RunResult.Accept:
                                    txtLocalImagePath.AppendText(s + "标定成功!" + Environment.NewLine);
                                    FrmMain.DicCalibTools[s].SaveCalibFile();
                                    break;
                                case RunResult.Error:
                                    txtLocalImagePath.AppendText(s + "标定失败!" + Environment.NewLine);

                                    break;
                                case RunResult.Warning:
                                    txtLocalImagePath.AppendText(s + "标定失败!" + Environment.NewLine);

                                    break;
                                default:
                                    break;
                            }
                        }
                   })));
            }
            catch (Exception ex)
            {

                txtLocalImagePath.AppendText( "标定出错!" + Environment.NewLine);
            }
        }
    }
}
