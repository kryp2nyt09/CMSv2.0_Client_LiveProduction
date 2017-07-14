namespace CMS2.Client.Forms.TrackingReportsView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Entities;
    using System.Data;
    using CMS2.Entities.ReportModel;

    /// <summary>
    /// Summary description for PickupCargo.
    /// </summary>
    public partial class PickupCargoManifestReportView : Telerik.Reporting.Report
    {
        public PickupCargoManifestReportView()
        {
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            List<PickupCargoManifestViewModel> dataTable = ReportGlobalModel.PickUpCargoReportData; 
            objectDataSource.DataSource = dataTable;
            table1.DataSource = objectDataSource;

            txtDate.Value = ReportGlobalModel.Date;
            txtArea.Value = ReportGlobalModel.Area;
            //txtDriver.Value = ReportGlobalModel.Driver;
            //txtChecker.Value = ReportGlobalModel.Checker;
            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
        }
    }
}