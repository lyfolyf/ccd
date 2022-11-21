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
using Hix_CCD_Module.HixEventArgs;

namespace Hix_CCD_Module.Controls
{
    public partial class COrderTreeView : UserControl
    {

        public event HixNodeSelectedEventHandler NodeSelected;

        public COrderTreeView()
        {
            InitializeComponent();
        }
        public Dictionary<string, OrderExecutionInfo> OrderExecutionInfos
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                treeView1.Nodes["Orders"].Nodes.Clear();
                foreach (var item in value)
                {
                    TreeNode nodeOrderExecutionInfo = new TreeNode(item.Key);
                    nodeOrderExecutionInfo.Name = item.Key;
                    nodeOrderExecutionInfo.ImageIndex = 0;
                    nodeOrderExecutionInfo.SelectedImageIndex = 1;

                    TreeNode nodeTasks = new TreeNode("Tasks");
                    nodeTasks.ImageIndex = 4;
                    nodeTasks.SelectedImageIndex = 4;

                    TreeNode nodeCameras = new TreeNode("Cameras");
                    nodeCameras.ImageIndex = 3;
                    nodeCameras.SelectedImageIndex = 3;

                    TreeNode nodeImageFlow = new TreeNode("ImageFlows");
                    nodeImageFlow.ImageIndex = 5;
                    nodeImageFlow.SelectedImageIndex = 5;

                    //foreach (var imageFlow in item.Value.ImageFlows)
                    //{
                    //    TreeNode node = new TreeNode(imageFlow.ID.ToString());
                    //    node.ImageIndex = 5;
                    //    node.SelectedImageIndex = 5;
                    //    nodeImageFlow.Nodes.Add(node);
                    //}

                    TreeNode nodeDataFlow = new TreeNode("DataFlows");
                    nodeDataFlow.ImageIndex = 6;
                    nodeDataFlow.SelectedImageIndex = 6;

                    nodeOrderExecutionInfo.Nodes.Add(nodeTasks);
                    nodeOrderExecutionInfo.Nodes.Add(nodeCameras);
                    nodeOrderExecutionInfo.Nodes.Add(nodeImageFlow);
                    nodeOrderExecutionInfo.Nodes.Add(nodeDataFlow);
                    treeView1.Nodes["Orders"].Nodes.Add(nodeOrderExecutionInfo);
                }
            }
        }

        public Dictionary<string, OrderExecutionInfo> GetOrderExecutionInfos()
        {
            Dictionary<string, OrderExecutionInfo> OrderExecutionInfos = new Dictionary<string, OrderExecutionInfo>();


            return OrderExecutionInfos;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string orderName = e.Node.Parent.Text;
            string propertyName = e.Node.Text;

        }
    }
}
