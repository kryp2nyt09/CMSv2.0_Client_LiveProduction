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
    /// Summary description for GatewayOutboundReportView.
    /// </summary>
    public partial class GatewayOutboundReportView : Telerik.Reporting.Report
    {
        public GatewayOutboundReportView()
        {
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            objectDataSource.DataSource = ReportGlobalModel.GatewayOutboundReportData;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtGateway.Value = ReportGlobalModel.Gateway;
            txtDriver.Value = ReportGlobalModel.Driver;
            txtPlateNo.Value = ReportGlobalModel.PlateNo;

            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
            // txtRemarks.Value = TrackingReportGlobalModel.Remarks;
            // txtNotes.Value = TrackingReportGlobalModel.Notes;
        }
    }
}