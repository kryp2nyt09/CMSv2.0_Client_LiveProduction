using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.ReportModel
{
    public class GatewayTransmitalViewModel
    {

        public int No { get; set; }
        public string AirwayBillNo { get; set; }
        public string Shipper { get; set; }
        public string Consignee { get; set; }
        public string Address { get; set; }
        public string CommodityType { get; set; }
        public string Commodity { get; set; }
        public int QTY { get; set; }
        public decimal AGW { get; set; }
        public string ServiceMode { get; set; }
        public string PaymentMode { get; set; }

        public string Gateway { get; set; }
        public string BCO { get; set; }
        public string Destination { get; set; }
        public string Batch { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Driver { get; set; }
        public string PlateNo { get; set; }
        public string MAWB { get; set; }
        public string ScannedBy { get; set; }

    }
}
