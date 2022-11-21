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
using WeifenLuo.WinFormsUI.Docking;

namespace Hix_CCD_Module.UI
{
    public partial class FrmSkin : DockContent
    {
        public event HixDataChangedEventHandler SkinChanged;
        public DockPanelSkin Skin
        {
            set
            {
                propertyGrid1.SelectedObject = value;
            }
            get
            {
                return (DockPanelSkin)propertyGrid1.SelectedObject;
            }
        }

        public FrmSkin()
        {
            InitializeComponent();
            propertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;
        }

        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            SkinChanged?.Invoke(this, new HixDataChangedEventArgs { NewValue = Skin });
        }
    }
}
