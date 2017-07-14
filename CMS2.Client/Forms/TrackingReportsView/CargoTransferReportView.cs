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
    /// Summary description for CargoTransferReportView.
    /// </summary>
    public partial class CargoTransferReportView : Telerik.Reporting.Report
    {
        public CargoTransferReportView()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            objectDataSource.DataSource = ReportGlobalModel.CargoTransferReportData;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtOrigin.Value = ReportGlobalModel.Origin;
            txtDestination.Value = ReportGlobalModel.Destination;

            txtDriver.Value = ReportGlobalModel.Driver;
            txtChecker.Value = ReportGlobalModel.Checker;
            txtPlateNo.Value = ReportGlobalModel.PlateNo;

            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
            // txtRemarks.Value = TrackingReportGlobalModel.Remarks;
            // txtNotes.Value = TrackingReportGlobalModel.Notes;
        }
    }
}