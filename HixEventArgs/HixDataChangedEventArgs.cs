using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hix_CCD_Module.Setting;

namespace Hix_CCD_Module.HixEventArgs
{
    public delegate void HixDataChangedEventHandler(object sender, HixDataChangedEventArgs e);
    public delegate void HixNodeSelectedEventHandler(object sender, HixNodeSelectedEventArgs e);
    public delegate void HixDataTakedEventHandler(object sender, HixDataTakedEventArgs e);

    public class HixDataTakedEventArgs : EventArgs
    {
        public bool IsDone { get; set; } = false;
        public string CameraName { get; set; } = string.Empty;
        public Bitmap Image { get; set; }
        public int CameraID { get; set; }
    }

    public class HixNodeSelectedEventArgs : EventArgs
    {
        public string OrderName { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
    }

    public class HixDataChangedEventArgs : EventArgs
    {
        public string Name { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
