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
    /// Summary description for UnbundleReportView.
    /// </summary>
    public partial class UnbundleReportView : Telerik.Reporting.Report
    {
        public UnbundleReportView()
        {
            InitializeComponent();
            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            objectDataSource.DataSource = ReportGlobalModel.UnbundleReportData;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtOrigin.Value = ReportGlobalModel.Origin;
            txtSackNo.Value = ReportGlobalModel.SackNo;
            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
            //txtRemarks.Value = ReportGlobalModel.Remarks;
            //txtNotes.Value = ReportGlobalModel.Notes;
        }
    }
}