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
    public partial class FrmDeleteCamera : Form
    {
        public event HixDataChangedEventHandler CameraConfigurationChanged;
        private void OnCameraConfigurationChanged(HixDataChangedEventArgs e) => CameraConfigurationChanged?.Invoke(this, e);
        public FrmDeleteCamera()
        {
            InitializeComponent();
        }

        private void FrmDeleteCamera_Load(object sender, EventArgs e)
        {
            cbCameras.DataSource = SysParams.DicCameraInfos.Values.ToList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            CameraInfo CameraInfo = (CameraInfo)cbCameras.SelectedItem;
            string delCameraName = CameraInfo.Name;
            if (MessageBox.Show($"确定删除相机[{delCameraName}]？",
                    "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SysParams.DicCameraInfos.Remove(delCameraName);
                cbCameras.DataSource = SysParams.DicCameraInfos.Values.ToList();
                SysParams.SaveToFile();
                OnCameraConfigurationChanged(new HixDataChangedEventArgs { });
            }
        }
    }
}
