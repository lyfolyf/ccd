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
using Hix_CCD_Module.Tool;
using static Hix_CCD_Module.FrmMain;


namespace Hix_CCD_Module.UI
{
    public partial class FrmTaskConfiguration : Form
    {
        public FrmTaskConfiguration()
        {
            InitializeComponent();
        }
        public event HixDataChangedEventHandler TaskConfigurationChanged;

        private void OnTaskConfigurationChanged(HixDataChangedEventArgs e) => TaskConfigurationChanged?.Invoke(this, e);
        private void FrmTaskConfiguration_Load(object sender, EventArgs e)
        {
            UpdateTaskList();
        }

        private void UpdateTaskList()
        {
            listView1.Items.Clear();
            foreach (var item in DicTasks)
            {
                listView1.Items.Add(item.Key, 0);
            }
            propertyGrid1.Refresh();
            foreach (ListViewItem item in listView1.Items)
            {
                if (propertyGrid1.SelectedObject != null)
                {
                    if (((TaskRunner)propertyGrid1.SelectedObject).Name == item.Text)
                    {
                        item.ForeColor = Color.Tomato;
                        item.BackColor = Color.LightYellow;
                        break;
                    }
                }
            }

        }

        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            int index = listView1.SelectedItems[0].Index;
            propertyGrid1.SelectedObject = DicTasks[listView1.Items[index].Text];
            foreach (ListViewItem item in listView1.Items)
            {
                item.ForeColor = Color.Black;
                item.BackColor = Color.White;
            }
            listView1.SelectedItems[0].ForeColor = Color.Tomato;
            listView1.SelectedItems[0].BackColor = Color.LightYellow;
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "名称")
            {
                string cName = (string)e.ChangedItem.Value;
                string oName = (string)e.OldValue;
                if (DicTasks.ContainsKey(cName))
                {

                    MessageBox.Show("该名称已存在", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    foreach (var item in DicTasks)
                    {
                        item.Value.Name = item.Key;
                    }
                    UpdateTaskList();
                    return;
                }
                else
                {
                    TaskRunner taskRunner = (TaskRunner)propertyGrid1.SelectedObject;
                    DicTasks.Remove(oName);
                    DicTasks.Add(cName, (TaskRunner)propertyGrid1.SelectedObject);
                    SysParams.DicTaskInfos.Remove(oName);
                    SysParams.DicTaskInfos.Add(cName, new Setting.TaskInfo(cName, taskRunner.Description, taskRunner.TaskFilePath));

                    if (SysParams.ListDisplayView.Contains(oName))
                    {
                        SysParams.ListDisplayView.Remove(oName);
                        SysParams.ListDisplayView.Add(cName);
                    }


                    if (SysParams.ListResultView.Contains(oName))
                    {
                        SysParams.ListResultView.Remove(oName);
                        SysParams.ListResultView.Add(cName);
                    }


                    if (SysParams.ListTaskEditView.Contains(oName))
                    {
                        SysParams.ListTaskEditView.Remove(oName);
                        SysParams.ListTaskEditView.Add(cName);
                    }

                    OnTaskConfigurationChanged(new HixDataChangedEventArgs { });
                }

            }
            else if (e.ChangedItem.Label == "路径")
            {
                TaskRunner taskRunner = (TaskRunner)propertyGrid1.SelectedObject;
                DicTasks[taskRunner.Name] = taskRunner;
                DicTasks[taskRunner.Name].IsLoaded = false;
                DicTasks[taskRunner.Name].GetFrmTaskEdit().Dispose();
                SysParams.DicTaskInfos[taskRunner.Name].FilePath = taskRunner.TaskFilePath;
                OnTaskConfigurationChanged(new HixDataChangedEventArgs { });
            }
            else
            {
                TaskRunner taskRunner = (TaskRunner)propertyGrid1.SelectedObject;
                DicTasks[taskRunner.Name] = taskRunner;
                SysParams.DicTaskInfos[taskRunner.Name] = new Setting.TaskInfo(taskRunner.Name, taskRunner.Description, taskRunner.TaskFilePath);
                OnTaskConfigurationChanged(new HixDataChangedEventArgs { });
            }
            SysParams.SaveToFile();
            UpdateTaskList();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int index = listView1.SelectedItems[0].Index;
                propertyGrid1.SelectedObject = DicTasks[listView1.Items[index].Text];
                foreach (ListViewItem item in listView1.Items)
                {
                    item.ForeColor = Color.Black;
                    item.BackColor = Color.White;
                }
                listView1.SelectedItems[0].ForeColor = Color.Tomato;
                listView1.SelectedItems[0].BackColor = Color.LightYellow;
            }
        }
    }
}
