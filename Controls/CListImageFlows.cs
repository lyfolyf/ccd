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

namespace Hix_CCD_Module.Controls
{
    public partial class CListImageFlows : FlowLayoutPanel
    {
        public CListImageFlows()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        public List<ImageFlow> ImageFlows
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].ID == 0)
                        value[i].ID = i;
                    CImageFlow cImageFlow = new CImageFlow { ImageFlow = value[i] };
                    cImageFlow.TabIndex = i;
                    Controls.Add(cImageFlow);
                }
            }
        }

        public List<ImageFlow> GetImageFlows()
        {
            List<ImageFlow> imageFlows = new List<ImageFlow>(Controls.Count);
            foreach (CImageFlow item in Controls)
            {
                imageFlows[item.TabIndex] = item.ImageFlow;
            }
            return imageFlows;
        }
    }
}
