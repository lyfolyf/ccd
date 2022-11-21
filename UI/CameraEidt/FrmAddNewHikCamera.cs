using Hix_CCD_Module.HixEventArgs;
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
    public partial class FrmAddNewHikCamera : Form
    {
        public event HixDataChangedEventHandler CameraConfigurationChanged;
        public FrmAddNewHikCamera()
        {
            InitializeComponent();
        }

        private void FrmAddNewHikCamera_Load(object sender, EventArgs e)
        {
            UpdataList();
        }

        private void UpdataList()
        {
            #region 在线的相机
            cbCameras.Items.Clear();
            foreach (var item in HikCameraOperator.GigECameras)
            {
                cbCameras.Items.Add($"{item.SN}");
            }
            foreach (var item in HikCameraOperator.USB3Cameras)
            {
                cbCameras.Items.Add($"{item.SN}");
            }
            if (cbCameras.Items.Count > 0)
            {
                cbCameras.SelectedIndex = 0;
            }
            #endregion
        }

        private HikCameraInfo GetHikCameraInfo()
        {
            return new HikCameraInfo
            {
                Id = int.Parse(txtId.Text),
                Name = txtName.Text,
                Description = txtDescription.Text,
                Exposure = double.Parse(txtExp.Text),
                Gain = double.Parse(txtGain.Text),
                InterfaceType = radioBtnGigE.Checked ? InterfaceType.GigE : InterfaceType.USB3,
                SN = cbCameras.Text,
                TriggerMode = radioBtnTriggerModeOn.Checked ? TriggerMode.On : TriggerMode.Off
            };
        }
        private bool CheckCameraConfg()
        {
            string errorString = string.Empty;

            if (txtName.Text == string.Empty)
            {
                errorString += "☆ 不能使用空字符串作为任务名称，请重新命名！\n";
            }
            int id = 0;
            if (!int.TryParse(txtId.Text, out id))
            {
                errorString += "☆ 相机Id必须为数字！\n";
            }
            if (SysParams.DicHikCameraInfos.ContainsKey(txtName.Text))
            {
                errorString += "☆ 已注册同名相机，请重新命名！\n";
            }

            double exp = 0;
            if (!double.TryParse(txtExp.Text, out exp))
            {
                errorString += "☆ 相机曝光值设置错误！\n";
            }
            double gain = 0;
            if (!double.TryParse(txtGain.Text, out gain))
            {
                errorString += "☆ 相机增益值设置错误！\n";
            }
            if (SysParams.DicHikCameraInfos.Values.Where(item => item.SN == cbCameras.Text).ToList().Count > 0)
            {
                string name = SysParams.DicHikCameraInfos.Values.Where(item => item.SN == cbCameras.Text).ToList()[0].Name;
                errorString += $"☆ 已注册该SN相机[Name: {name}] ！\n";
            }
            if (errorString != string.Empty)
            {
                MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (CheckCameraConfg())
            {
                SysParams.DicHikCameraInfos.Add(txtName.Text, GetHikCameraInfo());
                SysParams.SaveToFile();
                CameraConfigurationChanged?.Invoke(this, new HixDataChangedEventArgs { });
                Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in HikCameraOperator.GigECameras)
            {
                if (item.SN == cbCameras.Text)
                {
                    radioBtnGigE.Checked = true;
                }
            }
            foreach (var item in HikCameraOperator.USB3Cameras)
            {
                if (item.SN == cbCameras.Text)
                {
                    radioBtnUSB3.Checked = true;
                }
            }
        }
    }
}
