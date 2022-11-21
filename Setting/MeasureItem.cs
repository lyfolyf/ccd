using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hix_CCD_Module
{
    public class MeasureItem
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; }
        public string ExceptValue { get; set; }
        public string TolMax { get; set; } = "0.1";
        public string TolMin { get; set; } = "-0.1";
        public string Result { get; set; } = bool.FalseString;
        public string FlawType { get; set; } = string.Empty;
    }
}
