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
    /// Summary description for GatewayTransmitalReportView.
    /// </summary>
    public partial class GatewayTransmitalReportView : Telerik.Reporting.Report
    {
        public GatewayTransmitalReportView()
        {
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            objectDataSource.DataSource = ReportGlobalModel.GatewayTransmittalReportData;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtDriver.Value = ReportGlobalModel.Driver;
            txtPlateNo.Value = ReportGlobalModel.PlateNo;
            txtMAWB.Value = ReportGlobalModel.AirwayBillNo;
            txtArea.Value = ReportGlobalModel.Area;
            txtGateway.Value = ReportGlobalModel.Gateway;
            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
           // txtRemarks.Value = TrackingReportGlobalModel.Remarks;
           // txtNotes.Value = TrackingReportGlobalModel.Notes;
        }
    }
}