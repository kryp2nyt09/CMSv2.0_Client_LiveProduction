using CMS2.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Client.Reports
{
    public static class AcceptanceModelReport
    {
        public static DataTable table { get; set; }

        public static String ServiceMode { get; set; }
        public static String PaymentMode { get; set; }

        public static String Origin { get; set; }
        public static String Destination { get; set; }
        public static String DateandPlace { get; set; }

        public static String AWB1 { get; set; }
        public static String AWB2 { get; set; }

        public static String ShipperName { get; set; }
        public static String ShipperAddress { get; set; }
        public static String ConsigneeName { get; set; }
        public static String ConsigneeAddress { get; set; }

        public static String txt1 { get; set; }
        public static String txt2 { get; set; }
        public static String txt3 { get; set; }
        public static String txt4 { get; set; }
        public static String txt5 { get; set; }
        public static String txt6 { get; set; }
        public static String txt7 { get; set; }
        public static String txt8 { get; set; }
        public static String txt9 { get; set; }
        public static String txt10 { get; set; }
        public static String txt11 { get; set; }
        public static String txt12 { get; set; }
        public static String txt13 { get; set; }
        public static String txt14 { get; set; }
        public static String txt15 { get; set; }
        public static String txt16 { get; set; }
        public static String txt17 { get; set; }
        public static String txt18 { get; set; }

        public static String GrandTotal { get; set; }
    }

}
