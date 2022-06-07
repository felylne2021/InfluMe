using System;
using System.Collections.Generic;
using System.Text;

namespace InfluMe.Models
{
    public static class DeliveryCourier
    {
        public static List<CourierMapping> Couriers = new List<CourierMapping>()
        {
            new CourierMapping(){ courierCode = "jne", courierName = "JNE Express"},
            new CourierMapping(){ courierCode = "pos", courierName = "POS Indonesia"},
            new CourierMapping(){ courierCode = "jnt", courierName = "J&T Express Indonesia"},
            new CourierMapping(){ courierCode = "sicepat", courierName = "SiCepat"},
            new CourierMapping(){ courierCode = "tiki", courierName = "TIKI"},
            new CourierMapping(){ courierCode = "anteraja", courierName = "AnterAja"},
            new CourierMapping(){ courierCode = "wahana", courierName = "Wahana"},
            new CourierMapping(){ courierCode = "ninja", courierName = "Ninja Express"},
            new CourierMapping(){ courierCode = "lion", courierName = "Lion Parcel"},
            new CourierMapping(){ courierCode = "pcp", courierName = "PCP Express"},
            new CourierMapping(){ courierCode = "jet", courierName = "JET Express"},
            new CourierMapping(){ courierCode = "rex", courierName = "REX Express"},
            new CourierMapping(){ courierCode = "first", courierName = "First Logistics"},
            new CourierMapping(){ courierCode = "ide", courierName = "ID Express"},
            new CourierMapping(){ courierCode = "spx", courierName = "Shopee Express"},
            new CourierMapping(){ courierCode = "kgx", courierName = "KGXpress"},
            new CourierMapping(){ courierCode = "sap", courierName = "SAP Express"},
            new CourierMapping(){ courierCode = "rpx", courierName = "RPX"}
        };
    }

    public class CourierMapping
    {
        public string courierCode { get; set; }
        public string courierName { get; set; }
    }
}
