using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.Entities.ReportModel;
using CMS2.Entities.Models;
using CMS2.Entities;

namespace CMS2.Client.Forms.TrackingReportsView
{
    public static class ReportGlobalModel
    {
        public static DataTable table { get; set; }

        public static List<PickupCargoManifestViewModel> PickUpCargoReportData { get; set; }
        public static List<BranchAcceptanceViewModel> BranchAcceptanceReportData { get; set; }
        public static List<GatewayInboundViewModel> GatewayInboundReportData { get; set; }
        public static List<BundleViewModel> BundleReportData { get; set; }
        public static List<UnbundleViewModel> UnbundleReportData { get; set; }
        public static List<GatewayTransmitalViewModel> GatewayTransmittalReportData { get; set; }
        public static List<GatewayOutboundViewModel> GatewayOutboundReportData { get; set; }
        public static List<CargoTransferViewModel> CargoTransferReportData { get; set; }
        public static List<SegregationViewModel> SegregationReportData { get; set; }
        public static List<DailyTripViewModel> DailyTripReportData { get; set; }
        public static List<HoldCargoViewModel> HoldCargoReportData { get; set; }
        public static List<DeliveryStatusViewModel> DeliveryStatusReportData { get; set; }
        public static List<PaymentSummaryDetails> PaymentSummaryDetailsReportData { get; set; }
        public static List<PaymentSummary_MainDetailsModel> PaymentSummary_MainDetailsReportData { get; set; }
        public static List<Shipment> ManifestReportData { get; set; }
        public static string Manifest_DestinationBCO { get; set; }
        public static string Manifest_OriginBCO { get; set; }
        public static string Manifest_ServiceMode { get; set; }
        public static string Manifest_Paymode { get; set; }
        public static string Manifest_ServiceType { get; set; }
        public static string Manifest_Shipmode { get; set; }

        public static String Date { get; set; }
        public static String Area { get; set; }
        public static String Origin { get; set; }
        public static String SackNo { get; set; }
        public static String Gateway { get; set; }
        public static String Driver { get; set; }
        public static String Checker { get; set; }
        public static String AirwayBillNo { get; set; }
        public static String PlateNo { get; set; }
        public static String Batch { get; set; }
        public static String ScannedBy { get; set; }
        public static String Remarks { get; set; }
        public static String Notes { get; set;}
        public static String Branch { get; set; }
        public static String Destination { get; set; }
        public static String Weight { get; set; }
        public static String FlightNo { get; set; }
        public static String CommodityType { get; set; }
        public static String Report { get; set; }
        public static DataTable table2 { get; set; }
        public static DataTable table3 { get; set; }
        public static DataTable table4 { get; set; }
        public static String PaymentMode { get; set; }
        public static String Status { get; set; }
        public static String DeliveredBy { get; set; }


    }
}
