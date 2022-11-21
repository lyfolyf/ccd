using Hix_CCD_Module.HixEventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hix_CCD_Module.Setting;
using static Hix_CCD_Module.FrmMain;

namespace Hix_CCD_Module.UI
{
    public partial class FrmDeleteTask : Form
    {
        public event HixDataChangedEventHandler TaskConfigurationChanged;

        private void OnTaskConfigurationChanged(HixDataChangedEventArgs e) => TaskConfigurationChanged?.Invoke(this, e);
        public FrmDeleteTask()
        {
            InitializeComponent();
        }

        private void FrmDeleteTask_Load(object sender, EventArgs e)
        {
            cbTasks.DataSource = SysParams.DicTaskInfos.Values.ToList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            TaskInfo taskInfo = (TaskInfo)cbTasks.SelectedItem;
            string delTaskName = taskInfo.Name;
            if (MessageBox.Show($"确定删除任务[{delTaskName}]？",
                    "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                SysParams.DicTaskInfos.Remove(delTaskName);
                cbTasks.DataSource = SysParams.DicTaskInfos.Values.ToList();
                SysParams.SaveToFile();
                OnTaskConfigurationChanged(new HixDataChangedEventArgs { });
            }
        }
    }
}
