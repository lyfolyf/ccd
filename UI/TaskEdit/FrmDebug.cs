using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using static Hix_CCD_Module.FrmMain;
using Hix_CCD_Module.Tool;
using WeifenLuo.WinFormsUI.Docking;
using Bing.IVisionTool;
using Mark.CommonFile;

namespace Hix_CCD_Module.UI
{
    public partial class FrmDebug : Form
    {
        string[] files;
        List<CogImage8Grey> originImages = new List<CogImage8Grey>();
        List<CogImage8Grey> calibImages = new List<CogImage8Grey>();
        CogImage8Grey stichImage = null;

        string[] strs = new string[4] { "1#载具", "2#载具", "3#载具", "4#载具" };
        int[] SN = new int[4] { 100, 200, 300, 400 };
        public FrmDebug()
        {
            InitializeComponent();
            this.comboBox1.Items.AddRange(strs);
            this.comboBox1.SelectedItem = strs[0];
        }

        private void btnOpen2_Click(object sender, EventArgs e)
        {
            if (Open(listBoxStitchingImages))
            {
                this.cogRecordDisplay1.AutoFit = true;
                FrmWaitting frmWaitting = new FrmWaitting();
                frmWaitting.Show();
                frmWaitting.Refresh();
                frmWaitting.Timing3();
                Task.Run(() =>
                {
                    originImages.Clear();
                    calibImages.Clear();
                    if (files.Length < SysParams.PlanedImageNamber)
                    {
                        MessageBox.Show($"导入图像数量不正确,请导入{SysParams.PlanedImageNamber}张图像！", "提示:");
                        return;
                    }
                    for (int i = 0; i < files.Length; i++)
                    {
                        CogImage8Grey cogImage = new CogImage8Grey(new Bitmap(files[i]));
                        originImages.Add(cogImage);
                        this.Invoke(new Action(() => cogRecordDisplay1.Image = cogImage));
                    }
                    this.Invoke(new Action(() => frmWaitting.Close()));
                });
            }
        }
        public bool Open(ListBox listBox)
        {
            listBox.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "bmp文件|*.bmp";
            openFileDialog.Multiselect = true;
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                files = openFileDialog.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    listBox.Items.Add(files[i]);
                }
                return true;
            }
            else
                return false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRun_Click(object sender, EventArgs e)
        {

            if (originImages.Count != SysParams.PlanedImageNamber)
            {
                MessageBox.Show("导入图像数量不正确!!!");
                return;
            }

            int index = this.comboBox1.SelectedIndex;
            for (int i = 0; i < SysParams.PlanedImageNamber; i++)
            {
                if (DicCalibTools.Values.Where(item => item.Id / 100 == (index + 1)).Where(item => item.Id % SN[index] == i).ToList().Count > 0)
                {
                    HixCalibTool hixCalibTool = DicCalibTools.Values.Where(item => item.Id / 100 == (index + 1)).Where(item => item.Id % SN[index] == i).ToList()[0];
                    CogImage8Grey outputImage = null;
                    Actuator.RunCalib(hixCalibTool, originImages[i].ToBitmap(), out outputImage);
                    calibImages.Add(outputImage);
                }
            }
            if (!Actuator.ImageStitching(calibImages, SysParams.UnfilledPelValue, out stichImage, SysParams.Mode, SysParams.rate))
            {
                MessageBox.Show("图像拼接失败！", "提示:");
                return;
            }
            DicTasks[CommonValue.TaskName].Image = stichImage;
            Actuator.RunTask(DicTasks[CommonValue.TaskName], new string[] { "InputImage" }, new object[] { stichImage });
            //this.cogRecordDisplay1.Record = DicTasks[CommonValue.TaskName].Record.SubRecords[0];
            //this.cogRecordDisplay1.Fit(true);

            //输出debug数据
            string data2 = string.Empty;
            string strDay = DateTime.Now.ToString("yyyyMMdd");
            string strTime = DateTime.Now.ToString("HH_mm_ss");
            string timeStamp = $"{strDay}#{strTime}";
            string frontMSG1 = $"{timeStamp},1#载具,";
            string dataMSG1 = string.Empty;
            string dataFilePaht = "";
            string path = $@"{SysParams.DataSavePath}";
            dataFilePaht = $@"{path}\{DateTime.Now.ToString("yyyyMMdd")}-debug.csv";
            if (DicTasks[CommonValue.TaskName].Outputs.Count > 0)
            {
                foreach (Terminal t in DicTasks[CommonValue.TaskName].Outputs)
                {
                    if (t.ValueType == typeof(List<>))
                    {
                        List<double> res = t.Value as List<double>;
                        if (res != null)
                        {
                            for (int i = 0; i < res.Count; i++)
                            {
                                data2 += res[i].ToString("f3") + ",";
                            }
                        }
                        continue;
                    }

    
                    if (t.ValueType == typeof(double) ||
                    t.ValueType == typeof(int) ||
                    t.ValueType == typeof(string) ||
                    t.ValueType == typeof(bool))
                    {
                        string valueRes = ((double)t.Value).ToString("f3");
                        data2 += valueRes + ",";
                    }
                }
            }
            dataMSG1 = frontMSG1 + data2;
            CsvHepler.WriteCSV(dataFilePaht, dataMSG1);
        }

        private void bt_Modify_Click(object sender, EventArgs e)
        {
            DockContent frmTaskEdit = DicTasks[CommonValue.TaskName].GetFrmTaskEdit();
            frmTaskEdit.Show();
        }
    }
}
