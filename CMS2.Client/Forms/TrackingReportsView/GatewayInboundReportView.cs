namespace CMS2.Client.Forms.TrackingReportsView
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for GatewatInboundReportView.
    /// </summary>
    public partial class GatewayInboundReportView : Telerik.Reporting.Report
    {
        public GatewayInboundReportView()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            objectDataSource.DataSource = ReportGlobalModel.GatewayInboundReportData;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtGateway.Value = ReportGlobalModel.Gateway;
            txtMAWB.Value = ReportGlobalModel.AirwayBillNo;
            txtFlightNo.Value = ReportGlobalModel.FlightNo;
            txtCommodityType.Value = ReportGlobalModel.CommodityType;

            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
            // txtRemarks.Value = TrackingReportGlobalModel.Remarks;
            // txtNotes.Value = TrackingReportGlobalModel.Notes;
        }
    }
}