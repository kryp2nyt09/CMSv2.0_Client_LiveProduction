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
    /// Summary description for DeliveryStatusReportView.
    /// </summary>
    public partial class DeliveryStatusReportView : Telerik.Reporting.Report
    {
        public DeliveryStatusReportView()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            objectDataSource.DataSource = ReportGlobalModel.DeliveryStatusReportData;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtDriver.Value = ReportGlobalModel.Driver;
            txtChecker.Value = ReportGlobalModel.Checker;
            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
            txtRevenueUnit.Value = ReportGlobalModel.Area;
            //txtRemarks.Value = TrackingReportGlobalModel.Remarks;
            //txtNotes.Value = TrackingReportGlobalModel.Notes;
        
    }
    }
}