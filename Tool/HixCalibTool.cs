using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hix_CCD_Module.Tool
{
    public class HixCalibTool : Bing.VisionProTool.VisionProCalibTool
    {
        [Category("信息"), DisplayName("名称")]
        public string Name { get; set; } = string.Empty;
        [Category("信息"), DisplayName("编号")]
        public int Id { get; set; } = 0;
        
    }
}
