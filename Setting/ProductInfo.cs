using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Hix_CCD_Module
{
    public class ProductInfo
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { set; get; }
        public string ID { set; get; }
        public int Idx { set; get; }

        public string ProjectName { set; get; }
        public string StationName { set; get; }
        public string LineName { set; get; }
        public string PartDir { set; get; }

        public List<MeasureItem> Measure2D { set; get; }

        public bool OKNG { set; get; } = true;
        public string NGInfo { set; get; } = "";

        public List<string> ImagePath { set; get; }

        public bool NeedReport { set; get; } = false;
        public bool IsReport { set; get; } = false;
    }
}
