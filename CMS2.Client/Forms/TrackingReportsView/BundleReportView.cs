namespace CMS2.Client.Forms.TrackingReportsView
{
    using CMS2.Entities.ReportModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Bundle.
    /// </summary>
    public partial class BundleReportView : Telerik.Reporting.Report
    {
        public BundleReportView()
        {
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            List<BundleViewModel> reportData = ReportGlobalModel.BundleReportData;
            objectDataSource.DataSource = reportData;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtBundleNo.Value = ReportGlobalModel.SackNo;
            txtDestination.Value = ReportGlobalModel.Destination;
            txtWeight.Value = ReportGlobalModel.Weight;

            //txtRemarks.Value = TrackingReportGlobalModel.Remarks;
            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
           // txtNotes.Value = TrackingReportGlobalModel.Notes;
        }
    }
}