using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Models
{
    public class DeliveryTrackingResponse
    {
        public DeliveryData data { get; set; }
    }

    public class DeliveryData {
        public Summary summary { get; set; }
        public Detail detail { get; set; }
        public List<History> history { get; set;  }
    }

    public class Summary {
        public string awb { get; set; }
        public string courier { get; set; }
        public string service { get; set; }
        public string status { get; set; }
    }

    public class Detail {
        public string origin { get; set; }
        public string destination { get; set; }
        public string shipper { get; set; }
        public string receiver { get; set; }
    }

    public class History {
        public string date { get; set; }
        public string desc { get; set; }
    }
}
