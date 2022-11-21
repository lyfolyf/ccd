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
using static Hix_CCD_Module.FrmMain;
namespace Hix_CCD_Module.UI
{
    public partial class FrmAddNewTask : Form
    {
        public event HixDataChangedEventHandler TaskConfigurationChanged;

        private void OnTaskConfigurationChanged(HixDataChangedEventArgs e) => TaskConfigurationChanged?.Invoke(this, e);
        public FrmAddNewTask()
        {
            InitializeComponent();
        }

        private TaskInfo NewTaskInfo
        {
            get
            {
                if (txtNewTaskFilePath.Text == string.Empty)
                {
                    return new TaskInfo
                    {
                        Name = txtNewTaskName.Text,
                        Description = txtNewTaskDescription.Text,
                        FilePath = $@"{Environment.CurrentDirectory}\Task\{txtNewTaskName.Text}.vpp"
                    };
                }
                else
                    return new TaskInfo
                    {
                        Name = txtNewTaskName.Text,
                        Description = txtNewTaskDescription.Text,
                        FilePath = txtNewTaskFilePath.Text
                    };
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
                txtNewTaskFilePath.Text = openFileDialog.FileName;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (CheckTaskConfg())
            {
                SysParams.DicTaskInfos.Add(txtNewTaskName.Text, NewTaskInfo);
                SysParams.SaveToFile();
                OnTaskConfigurationChanged(new HixDataChangedEventArgs { });
                Close();
            }
        }

        private bool CheckTaskConfg()
        {
            string errorString = string.Empty;

            if (txtNewTaskName.Text == string.Empty)
            {
                errorString += "☆ 不能使用空字符串作为任务名称，请重新命名！\n";
            }
            if (txtNewTaskFilePath.Text != string.Empty)
            {
                if (!System.IO.File.Exists(txtNewTaskFilePath.Text))
                {
                    errorString += "☆ 文件不存在！\n";
                }
            }
            if (SysParams.DicTaskInfos.ContainsKey(txtNewTaskName.Text))
            {
                errorString += "☆ 已存在同名任务，请重新命名！\n";
            }
            if (errorString != string.Empty)
            {
                MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtNewTaskFilePath.Text == string.Empty)
            {
                if (MessageBox.Show("检测到新建任务文件路径为null，是否继续创建默认任务？",
                    "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
