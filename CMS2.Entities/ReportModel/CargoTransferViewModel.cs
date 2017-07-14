using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.ReportModel
{
    public class CargoTransferViewModel
    {
        public int No { get; set; }
        public string RevenueUnitType { get; set; }
        public string OriginArea { get; set; }
        public string DestinationBco { get; set; }
        public string Driver { get; set; }
        public string Checker { get; set; }
        public int Pieces { get; set; }
        public string PlateNo { get; set; }
        public string Batch { get; set; }
        public string AirwayBillNo { get; set; }
        public int QTY { get; set; }
        public string DestinationArea { get; set; }
        public string ScannedBy { get; set; }
    }
}
