using Hix_CCD_Module.HixEventArgs;
using Hix_CCD_Module.Setting;
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
    public partial class FrmAddNewCalibTool : Form
    {
        public event HixDataChangedEventHandler CalibConfigurationChanged;

        private void OnCalibConfigurationChanged(HixDataChangedEventArgs e) => CalibConfigurationChanged?.Invoke(this, e);
        public FrmAddNewCalibTool()
        {
            InitializeComponent();
        }

        private CalibInfo NewCalibInfo
        {
            get
            {
                return new CalibInfo
                {
                    Id = int.Parse(txtNewCalibId.Text),
                    FilePath = txtNewCalibFilePath.Text,
                    Name = txtNewCalibName.Text
                };
            }
        }
        private bool CheckCalibConfg()
        {
            string errorString = string.Empty;

            if (txtNewCalibName.Text == string.Empty)
            {
                errorString += "☆ 不能使用空字符串作为任务名称，请重新命名！\n";
            }
            if (txtNewCalibFilePath.Text != string.Empty)
            {
                if (!System.IO.File.Exists(txtNewCalibFilePath.Text))
                {
                    errorString += "☆ 文件不存在！\n";
                }
            }
            int r = 0;
            if (int.TryParse(txtNewCalibId.Text, out r))
            {
                if (SysParams.DicCalibInfos.Values.Where(item => item.Id == r).ToList().Count > 0)
                {
                    errorString += "☆ Id已存在！\n";
                }
            }
            else
            {
                errorString += "☆ Id必须为数字！\n";
            }
            if (SysParams.DicCalibInfos.ContainsKey(txtNewCalibName.Text))
            {
                errorString += "☆ 已存在同名任务，请重新命名！\n";
            }
            if (errorString != string.Empty)
            {
                MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtNewCalibFilePath.Text == string.Empty)
            {
                if (MessageBox.Show("检测到新建任务文件路径为null，是否继续创建默认任务？",
                    "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (CheckCalibConfg())
            {
                SysParams.DicCalibInfos.Add(txtNewCalibName.Text, NewCalibInfo);
                SysParams.SaveToFile();
                OnCalibConfigurationChanged(new HixDataChangedEventArgs { });
                Close();
            }
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.vpp(Cognex Vpp)|*.vpp|*.*(所有文件)|*.*",
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtNewCalibFilePath.Text = openFileDialog.FileName;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
