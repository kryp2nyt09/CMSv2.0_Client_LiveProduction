using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities.ReportModel
{
    public class GatewayInboundViewModel
    {

        public int No { get; set; }
        public String Gateway { get; set; }
        public String Origin { get; set; }
        public int Pieces { get; set; }
        public String MAWB { get; set; }
        public String FlightNo { get; set; }
        public String CommodityType { get; set; }
        public String AirwayBillNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ScannedBy { get; set; }

    }
}
