using CMS2.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.ReportModel
{
    public class DailyTripViewModel
    {

        public int No { get; set; }
        public string AirwayBillNo { get; set; }
        public string Consignee { get; set; }
        public string Address { get; set; }
        public int QTY { get; set; }
        public decimal AGW { get; set; }
        public string ServiceMode { get; set; }
        public string PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public string PlateNo { get; set; }
        public string Area { get; set; }
        public string Driver { get; set; }
        public string Checker { get; set; }
        public string BCO { get; set; }
        public string PaymentCode { get; set; }
        public string Scannedby { get; set; }
        public string Batch { get; set; }
        
    }
}
