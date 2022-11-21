using Bing.VisionProTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hix_CCD_Module.UI;
using System.ComponentModel;

namespace Hix_CCD_Module.Tool
{
    public class TaskRunner : VisionProTask
    {
        [Browsable(false)]
        public bool IsLoaded { get; set; } = false;

        FrmResultsView frmResultsView = null;
        public TaskRunner()
        {

        }
        public FrmResultsView GetTaskFrmResults()
        {
            if (frmResultsView == null || frmResultsView.IsDisposed)
            {
                frmResultsView = new FrmResultsView(this);
                frmResultsView.Subject = Outputs;
            }
            frmResultsView.Text = $"{Name} - 计算结果";
            frmResultsView.TaskName = Name;
            return frmResultsView;
        }

    }
}
