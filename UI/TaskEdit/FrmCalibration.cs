using Bing.IVisionTool;
using Hix_CCD_Module.Setting;
using Hix_CCD_Module.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Hix_CCD_Module.FrmMain;

namespace Hix_CCD_Module.UI
{
    public partial class FrmCalibration : Form
    {
        Bitmap calibImage = null;
        Bitmap camImage = null;
        private bool bEventSet;
        Control calibControl = null;
        public FrmCalibration()
        {
            InitializeComponent();
        }

        private void RbLocal_CheckedChanged(object sender, EventArgs e)
        {
            gbCamera.Enabled = rbCamera.Checked ? true : false;
            gbLocal.Enabled = rbLocal.Checked ? true : false;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "bmp文件|*.bmp";
            openFileDialog.Multiselect = false;
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtLocalImagePath.Text = openFileDialog.FileName;
                calibImage = new Bitmap(openFileDialog.FileName);
                cImageView1.Image = calibImage;
                UpdateLog($"成功加载本地图片！", Color.Blue);
            }
        }
        private void UpdataList()
        {
            cbCalibTools.DataSource = DicCalibTools.Values.ToList();
        }

        public void UpdateLog(string log, Color color)
        {
            this.Invoke(new Action(() =>
            {
                log = DateTime.Now.ToLongTimeString() + " : " + log + "\n";
                richTextBox1.AppendText(log);
                richTextBox1.SelectionStart = richTextBox1.TextLength - log.Length;
                richTextBox1.SelectionLength = log.Length;
                richTextBox1.SelectionColor = color;
                richTextBox1.SelectionStart = richTextBox1.TextLength - 1;
                richTextBox1.SelectionLength = 0;
                richTextBox1.ScrollToCaret();
            }));
        }

        private void FrmCalibration_Load(object sender, EventArgs e)
        {
            UpdataList();
        }

        private void BtnGetImage_Click(object sender, EventArgs e)
        {
            try
            {
                HikCamera hikCamera = (HikCamera)cbCameras.SelectedItem;
                double oExp = hikCamera.Exposure;
                double oGain = hikCamera.Gain;
                hikCamera.Exposure = double.Parse(txtExp.Text);
                hikCamera.Gain = double.Parse(txtGain.Text);
                if (!bEventSet)
                {
                    bEventSet = true;
                    hikCamera.ImageTaked += HikCamera_ImageTaked;
                }
                hikCamera.TriggerOnce();
                hikCamera.Exposure = oExp;
                hikCamera.Gain = oGain;
            }
            catch (Exception ex)
            {
                UpdateLog($"{ex.Message}", Color.Red);
            }

        }

        private void HikCamera_ImageTaked(object sender, HixEventArgs.HixDataTakedEventArgs e)
        {
            camImage = e.Image;
            cImageView1.Image = e.Image;
            UpdateLog($"拍照成功！", Color.Blue);
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            calibImage = camImage;
            UpdateLog($"已选择拍照图片作为标定图片！", Color.Green);
        }

        private void CbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            bEventSet = false;
        }

        private void BtnCalibrate_Click(object sender, EventArgs e)
        {
            UpdateLog($"开始标定,请等待...", Color.Tomato);
            Task.Run(() =>
            {
                Invoke(new Action(() =>
                {


                    DicCalibTools[cbCalibTools.Text].CalibrationImage = new Cognex.VisionPro.CogImage8Grey(calibImage);
                    RunResult runResult = DicCalibTools[cbCalibTools.Text].Calibretion();
                    switch (runResult)
                    {
                        case RunResult.Accept:
                            UpdateLog($"标定成功！[{runResult}]", Color.Lime);
                            DicCalibTools[cbCalibTools.Text].SaveCalibFile();
                            break;
                        case RunResult.Error:
                            UpdateLog($"标定失败！[{runResult}]", Color.Red);
                            break;
                        case RunResult.Warning:
                            UpdateLog($"标定失败！[{runResult}]", Color.Tomato);
                            break;
                        default:
                            break;
                    }
                }));
            });
        }

        private void BtnCalibTool_Click(object sender, EventArgs e)
        {
            calibControl = DicCalibTools[cbCalibTools.Text].GetControl();
            new Display.FrmControlView() { DispControl = calibControl }.ShowDialog();
        }

        private void CbCalibTools_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
