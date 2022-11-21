using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bing.VisionProTool;

namespace Hix_CCD_Module.Tool
{
    [Obsolete]
    public class CameraRunner : VisionProCamera
    {
        [Browsable(false)]
        public bool IsLoaded { get; set; } = false;
        public CameraRunner()
        {

        }
    }
}
