using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hix_CCD_Module.Setting;
using static Hix_CCD_Module.FrmMain;

namespace Hix_CCD_Module.Controls
{
    public partial class CImageFlow : UserControl
    {
        public CImageFlow()
        {
            InitializeComponent();
            InitTaskItems();
            InitCameraItems();
        }

        private void InitCameraItems()
        {
            foreach (var item in SysParams.DicCameraInfos.Keys)
            {
                cbCameras.Items.Add(item);
            }
            cbCameras.SelectedIndex = 0;
        }

        private void InitTaskItems()
        {
            foreach (var item in SysParams.DicTaskInfos.Keys)
            {
                cbTasks.Items.Add(item);
            }
            cbTasks.SelectedIndex = 0;
        }

        public ImageFlow ImageFlow
        {
            set
            {
                cbCameras.Items.Clear();
                cbTasks.Items.Clear();
                if (value.CameraName == null || !SysParams.DicCameraInfos.ContainsKey(value.CameraName))
                {
                    InitCameraItems();
                    gbCamera.ForeColor = Color.OrangeRed;
                }
                else

                    cbCameras.Text = value.CameraName;
                if (value.TaskName == null || !SysParams.DicTaskInfos.ContainsKey(value.TaskName))
                {
                    InitTaskItems();
                    gbTask.ForeColor = Color.OrangeRed;
                }
                else
                {
                    cbTasks.Text = value.TaskName;
                    cbTaskInputImage.Text = value.InputImageName;
                }
                txtExp.Text = value.Exprosure.ToString();
                txtGain.Text = value.Gain.ToString();
                lbId.Text = value.ID.ToString();
            }
            get
            {
                return new ImageFlow
                {
                    CameraName = cbCameras.Text,
                    TaskName = cbTasks.Text,
                    InputImageName = cbTaskInputImage.Text,
                    Exprosure = double.Parse(txtExp.Text),
                    Gain = double.Parse(txtGain.Text),
                    ID = int.Parse(lbId.Text)
                };
            }
        }

        private void CbTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbTaskInputImage.Items.Clear();
                foreach (var item in DicTasks[cbTasks.Text].Inputs)
                {
                    cbTaskInputImage.Items.Add(item.Name);
                }
                cbTaskInputImage.SelectedIndex = 0;
            }
            catch
            {

            }
        }
    }
}
