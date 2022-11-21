using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bing.IVisionTool;
using WeifenLuo.WinFormsUI.Docking;
using Hix_CCD_Module.Tool;

namespace Hix_CCD_Module.UI
{
    public partial class FrmResultsView : DockContent
    {
        public string TaskName { get; set; } = string.Empty;
        TaskRunner VTask = null;
        

        public FrmResultsView(TaskRunner task)
        {
            InitializeComponent();
            VTask = task;
        }
        public List<Terminal> Subject
        {
            set
            {
                resultsGridView1.DataSource = value;
                resultsGridView1.Refresh();
                resultsGridView1.CellValueChanged += ResultsGridView1_CellValueChanged;
            }
        }
        public bool ShowRatio
        {
            set { this.AdjustCF.Visible = value;
                this.补偿系数2.Visible = value;
                this.补偿系数3.Visible = value;
                this.补偿系数4.Visible = value;
            }
        }

        private void ResultsGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            VTask.SetOutput();
        }
    }
}
