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
    /// Summary description for BranchAcceptance.
    /// </summary>
    public partial class BranchAcceptanceReportView : Telerik.Reporting.Report
    {
        public BranchAcceptanceReportView()
        {
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            objectDataSource.DataSource = ReportGlobalModel.BranchAcceptanceReportData;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtArea.Value = ReportGlobalModel.Branch; //BRANCH
            txtDriver.Value = ReportGlobalModel.Driver;
            txtChecker.Value = ReportGlobalModel.Checker;
            txtBatch.Value = ReportGlobalModel.Batch;
            txtPlateNo.Value = ReportGlobalModel.PlateNo;
            txtRemarks.Value = ReportGlobalModel.Remarks;
            txtNotes.Value = ReportGlobalModel.Notes;
        }
    }
}