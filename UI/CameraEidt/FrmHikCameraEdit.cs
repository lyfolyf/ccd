using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hix_CCD_Module.HixEventArgs;
using Hix_CCD_Module.Setting;
using Hix_CCD_Module.Tool;
using static Hix_CCD_Module.FrmMain;


namespace Hix_CCD_Module.UI
{
    public partial class FrmHikCameraEdit : Form
    {
        public event HixDataChangedEventHandler CameraConfigurationChanged;
        public FrmHikCameraEdit()
        {
            InitializeComponent();
        }

        private void FrmHikCameraEdit_Load(object sender, EventArgs e)
        {
            UpdataList();
        }

        private void UpdataList()
        {
            #region 在线的相机
            listBoxOnlineCameras.Items.Clear();
            foreach (var item in HikCameraOperator.GigECameras)
            {
                listBoxOnlineCameras.Items.Add($"SN:[{item.SN}] - - GigE");
            }
            foreach (var item in HikCameraOperator.USB3Cameras)
            {
                listBoxOnlineCameras.Items.Add($"SN:[{item.SN}] - - USB3.0");
            }
            #endregion

            #region 注册的相机
            listBoxRegisteredCameras.Items.Clear();
            foreach (var item in SysParams.DicHikCameraInfos.Values)
            {
                if (HikCameraOperator.GigECameras.Where(itm => itm.SN == item.SN).ToList().Count > 0
                    && item.InterfaceType == Setting.InterfaceType.GigE)
                {
                    listBoxRegisteredCameras.Items.Add($"SN:[{item.SN}] - - GigE (Online)");
                }
                else if (HikCameraOperator.USB3Cameras.Where(itm => itm.SN == item.SN).ToList().Count > 0
                     && item.InterfaceType == Setting.InterfaceType.USB3)
                {
                    listBoxRegisteredCameras.Items.Add($"SN:[{item.SN}] - - USB3.0 (Online)");
                }
                else
                {
                    listBoxRegisteredCameras.Items.Add($"SN:[{item.SN}] - - GigE (Offline)");
                }
            }
            #endregion
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FrmAddNewHikCamera frmAddNewHikCamera = new FrmAddNewHikCamera();
            frmAddNewHikCamera.CameraConfigurationChanged += FrmAddNewHikCamera_CameraConfigurationChanged;
            frmAddNewHikCamera.ShowDialog();
        }

        private void FrmAddNewHikCamera_CameraConfigurationChanged(object sender, HixEventArgs.HixDataChangedEventArgs e)
        {
            UpdataList();
            CameraConfigurationChanged?.Invoke(this, new HixDataChangedEventArgs { });
        }

        private void ListBoxRegisteredCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sn = listBoxRegisteredCameras.SelectedItem.ToString().Split(new char[] { '[', ']' })[1];
                foreach (var item in SysParams.DicHikCameraInfos.Values)
                {
                    if (item.SN == sn)
                    {
                        propertyGridCamera.SelectedObject = item;
                    }
                }
            }
            catch
            { }
        }

        private void PropertyGridCamera_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            int index = listBoxRegisteredCameras.SelectedIndex;
            HikCameraInfo hikCameraInfo = (HikCameraInfo)propertyGridCamera.SelectedObject;
            if (e.ChangedItem.Label == "Name")
            {
                if (SysParams.DicHikCameraInfos.Values.Where(item => item.Name == hikCameraInfo.Name).ToList().Count > 0)
                {
                    listBoxRegisteredCameras.SelectedIndex = index;
                    return;
                }
            }
            if (e.ChangedItem.Label == "Id")
            {
                if (SysParams.DicHikCameraInfos.Values.Where(item => item.Id == hikCameraInfo.Id).ToList().Count > 0)
                {
                    listBoxRegisteredCameras.SelectedIndex = index;
                    return;
                }
            }
            SysParams.DicHikCameraInfos[hikCameraInfo.Name] = hikCameraInfo;
            UpdataList();
            CameraConfigurationChanged?.Invoke(this, new HixDataChangedEventArgs { });
            listBoxRegisteredCameras.SelectedIndex = index;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sn = listBoxRegisteredCameras.SelectedItem.ToString().Split(new char[] { '[', ']' })[1];
                string delCam = string.Empty;
                foreach (var item in SysParams.DicHikCameraInfos)
                {
                    if (item.Value.SN == sn)
                    {
                        delCam = item.Key;
                    }
                }
                SysParams.DicHikCameraInfos.Remove(delCam);
                UpdataList();
                CameraConfigurationChanged?.Invoke(this, new HixDataChangedEventArgs { });
            }
            catch
            { }
        }
    }
}
