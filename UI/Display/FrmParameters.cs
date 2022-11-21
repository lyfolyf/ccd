using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bing.Controls;
using Hix_CCD_Module.Setting;
using WeifenLuo.WinFormsUI.Docking;
using Hix_CCD_Module.HixEventArgs;

namespace Hix_CCD_Module.UI
{
    public partial class FrmParameters : DockContent
    {
        public event HixDataChangedEventHandler ParametersChanged;
        private void OnParametersChanged(HixDataChangedEventArgs e) => ParametersChanged?.Invoke(this, e);
        public Parameters Param
        {
            set
            {
                bingPropertyGrid.Subject = value;
            }
            get
            {
                return bingPropertyGrid.Subject;
            }
        }
        public FrmParameters(Parameters parameters)
        {
            InitializeComponent();
            bingPropertyGrid = new BingPropertyGrid<Parameters>
            {
                Dock = DockStyle.Fill
            };
            bingPropertyGrid.PropertyValueChanged += BingPropertyGrid_PropertyValueChanged;
            Controls.Add(bingPropertyGrid);
            Param = parameters;
        }

        private void BingPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            FrmMain.SysParams = Param;
            FrmMain.SysParams.SaveToFile();
            if (e.ChangedItem.Label == "是否显示补偿系数")
            {
                OnParametersChanged(new HixDataChangedEventArgs { });
            }
        }
    }
}
