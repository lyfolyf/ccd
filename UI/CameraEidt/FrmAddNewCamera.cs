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
    [Obsolete]
    public partial class FrmAddNewCamera : Form
    {
        public event HixDataChangedEventHandler CameraConfigurationChanged;
        private void OnCameraConfigurationChanged(HixDataChangedEventArgs e) => CameraConfigurationChanged?.Invoke(this, e);
        public FrmAddNewCamera()
        {
            InitializeComponent();
        }
        private CameraInfo NewCameraInfo
        {
            get
            {
                if (txtNewCameraFilePath.Text == string.Empty)
                {
                    return new CameraInfo
                    {
                        Name = txtNewCameraName.Text,
                        Description = txtNewCameraDescription.Text,
                        FilePath = $@"{Environment.CurrentDirectory}\Camera\{txtNewCameraName.Text}.vpp"
                    };
                }
                else
                    return new CameraInfo
                    {
                        Name = txtNewCameraName.Text,
                        Description = txtNewCameraDescription.Text,
                        FilePath = txtNewCameraFilePath.Text
                    };
            }
        }

        private bool CheckCameraConfg()
        {
            string errorString = string.Empty;

            if (txtNewCameraName.Text == string.Empty)
            {
                errorString += "☆ 不能使用空字符串作为任务名称，请重新命名！\n";
            }
            if (txtNewCameraFilePath.Text != string.Empty)
            {
                if (!System.IO.File.Exists(txtNewCameraFilePath.Text))
                {
                    errorString += "☆ 文件不存在！\n";
                }
            }
            if (SysParams.DicCameraInfos.ContainsKey(txtNewCameraName.Text))
            {
                errorString += "☆ 已存在同名任务，请重新命名！\n";
            }
            if (errorString != string.Empty)
            {
                MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtNewCameraFilePath.Text == string.Empty)
            {
                if (MessageBox.Show("检测到新建相机文件路径为null，是否继续创建默认相机？",
                    "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
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
                txtNewCameraFilePath.Text = openFileDialog.FileName;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (CheckCameraConfg())
            {
                SysParams.DicCameraInfos.Add(txtNewCameraName.Text, NewCameraInfo);
                SysParams.SaveToFile();
                OnCameraConfigurationChanged(new HixDataChangedEventArgs { });
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
