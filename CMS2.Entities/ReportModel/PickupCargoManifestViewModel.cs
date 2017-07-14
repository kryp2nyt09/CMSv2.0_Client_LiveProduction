using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.ReportModel
{
    public class PickupCargoManifestViewModel
    {
        public int No { get; set; }
        public string AirwayBillNo { get; set; }
        public string Shipper { get; set; }
        public string ShipperAddress { get; set; }
        public string Consignee { get; set; }
        public string ConsigneeAddress { get; set; }
        public string Commodity { get; set; }
        public int QTY { get; set; }
        public decimal AGW { get; set; }
        public string ServiceMode { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string Area { get; set; }       
        public string ScannedBy { get; set; }
        public RevenueUnitType RevenueUnitType { get; set; }


    }
}
