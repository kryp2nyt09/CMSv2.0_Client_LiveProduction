using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.ReportModel
{
    public class DeliveryStatusViewModel
    {
        public int No { get; set; }
        public string AirwayBillNo { get; set; }
        public int QTY { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string RevenueUnitType { get; set; }
        public string Area { get; set; }
        public string Driver { get; set; }
        public string Checker { get; set; }
        public string PlateNo { get; set; }
        public string Batch { get; set; }
        public string BCO { get; set; }
        public string ScannedBy { get; set; }
        public string DeliveredBy { get; set; }
        public string ReceivedBy { get; set; }
       
    }
}
