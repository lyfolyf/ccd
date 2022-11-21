using Hix_CCD_Module.HixEventArgs;
using Hix_CCD_Module.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Hix_CCD_Module.FrmMain;

namespace Hix_CCD_Module.UI
{
    public partial class FrmCalibToolConfiguration : Form
    {
        public event HixDataChangedEventHandler CalibConfigurationChanged;
        private void OnCalibConfigurationChanged(HixDataChangedEventArgs e) => CalibConfigurationChanged?.Invoke(this, e);

        Control calibControl = null;
        public FrmCalibToolConfiguration()
        {
            InitializeComponent();
        }

        private void FrmCalibToolConfiguration_Load(object sender, EventArgs e)
        {
            listBoxCalibTools.DataSource = DicCalibTools.Values.ToList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FrmAddNewCalibTool frmAddNewCalibTool = new FrmAddNewCalibTool();
            frmAddNewCalibTool.CalibConfigurationChanged += FrmAddNewCalibTool_CalibConfigurationChanged;
            frmAddNewCalibTool.ShowDialog();
        }

        private void FrmAddNewCalibTool_CalibConfigurationChanged(object sender, HixEventArgs.HixDataChangedEventArgs e)
        {
            OnCalibConfigurationChanged(new HixDataChangedEventArgs { });
        }

        private void ListBoxCalibTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            HixCalibTool hixCalibTool = (HixCalibTool)listBoxCalibTools.SelectedItem;
            this.propertyGrid1.SelectedObject = hixCalibTool;
            calibControl = hixCalibTool.GetControl();

        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            new Display.FrmControlView() { DispControl = calibControl }.ShowDialog();
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            int oindex = listBoxCalibTools.SelectedIndex;
            if (e.ChangedItem.Label == "名称")
            {
                string cName = (string)e.ChangedItem.Value;
                string oName = (string)e.OldValue;
                if (DicCalibTools.ContainsKey(cName))
                {

                    MessageBox.Show("该名称已存在", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    foreach (var item in DicTasks)
                    {
                        item.Value.Name = item.Key;
                    }
                    listBoxCalibTools.DataSource = DicCalibTools.Values.ToList();
                    listBoxCalibTools.SelectedIndex = oindex;
                    return;
                }
                else
                {
                    HixCalibTool calibTool = (HixCalibTool)propertyGrid1.SelectedObject;
                    DicCalibTools.Remove(oName);
                    DicCalibTools.Add(cName, calibTool);
                    SysParams.DicCalibInfos.Remove(oName);
                    SysParams.DicCalibInfos.Add(cName, new Setting.CalibInfo(calibTool.Name, calibTool.Id, calibTool.CalibFilePath));
                    OnCalibConfigurationChanged(new HixDataChangedEventArgs { });
                }

            }
            else if (e.ChangedItem.Label == "路径")
            {
                HixCalibTool calibTool = (HixCalibTool)propertyGrid1.SelectedObject;
                DicCalibTools[calibTool.Name] = calibTool;
                DicCalibTools[calibTool.Name].IsLoaded = false;
                SysParams.DicCalibInfos[calibTool.Name].FilePath = calibTool.CalibFilePath;
                OnCalibConfigurationChanged(new HixDataChangedEventArgs { });
            }
            else if (e.ChangedItem.Label == "编号")
            {
                //listBoxCalibTools.DataSource = DicCalibTools.Values.ToList();
                //listBoxCalibTools.SelectedIndex = oindex;
                HixCalibTool calibTool = (HixCalibTool)propertyGrid1.SelectedObject;
                DicCalibTools[calibTool.Name] = calibTool;
                SysParams.DicCalibInfos[calibTool.Name].Id = calibTool.Id;
                OnCalibConfigurationChanged(new HixDataChangedEventArgs { });
            }
            else
            {
                HixCalibTool calibTool = (HixCalibTool)propertyGrid1.SelectedObject;
                DicCalibTools[calibTool.Name] = calibTool;
                SysParams.DicCalibInfos[calibTool.Name] = new Setting.CalibInfo(calibTool.Name, calibTool.Id, calibTool.CalibFilePath);
                OnCalibConfigurationChanged(new HixDataChangedEventArgs { });
            }
            listBoxCalibTools.DataSource = DicCalibTools.Values.ToList();
            listBoxCalibTools.SelectedIndex = oindex;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            HixCalibTool hixCalibTool = (HixCalibTool)listBoxCalibTools.SelectedItem;
            SysParams.DicCalibInfos.Remove(hixCalibTool.Name);
            SysParams.SaveToFile();
            OnCalibConfigurationChanged(new HixDataChangedEventArgs { });
        }

        private void BtnReflesh_Click(object sender, EventArgs e)
        {
            try
            {
                int oindex = listBoxCalibTools.SelectedIndex;
                listBoxCalibTools.DataSource = DicCalibTools.Values.ToList();
                listBoxCalibTools.Refresh();
                listBoxCalibTools.SelectedIndex = oindex;
            }
            catch
            { }
        }
    }
}
