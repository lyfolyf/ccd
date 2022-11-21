using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hix_CCD_Module.Controls;
using Hix_CCD_Module.Setting;
using static Hix_CCD_Module.FrmMain;

namespace Hix_CCD_Module.UI
{
    public partial class FrmExecutionFlowConfiguration : Form
    {
        public FrmExecutionFlowConfiguration()
        {
            InitializeComponent();
        }
        private void FrmExecutionFlowConfiguration_Load(object sender, EventArgs e)
        {
            List<ImageFlow> imageFlows = new List<ImageFlow>();
            imageFlows.Add(new ImageFlow { Exprosure = 30, Gain = 10 });
            imageFlows.Add(new ImageFlow());
            imageFlows.Add(new ImageFlow());
            imageFlows.Add(new ImageFlow());
            imageFlows.Add(new ImageFlow());
            cListImageFlows1.ImageFlows = imageFlows;
            SysParams.DicOrderExecutionInfos = new Dictionary<string, OrderExecutionInfo>();
            SysParams.DicOrderExecutionInfos.Add("001", new OrderExecutionInfo { ImageFlows = imageFlows });
            SysParams.DicOrderExecutionInfos.Add("002", new OrderExecutionInfo { ImageFlows = imageFlows });
            SysParams.DicOrderExecutionInfos.Add("003", new OrderExecutionInfo { ImageFlows = imageFlows });
            SysParams.DicOrderExecutionInfos.Add("004", new OrderExecutionInfo { ImageFlows = imageFlows });
            SysParams.DicOrderExecutionInfos.Add("005", new OrderExecutionInfo { ImageFlows = imageFlows });
            SysParams.DicOrderExecutionInfos.Add("006", new OrderExecutionInfo { ImageFlows = imageFlows });
            cOrderTreeView1.OrderExecutionInfos = SysParams.DicOrderExecutionInfos;
        }
    }
}
