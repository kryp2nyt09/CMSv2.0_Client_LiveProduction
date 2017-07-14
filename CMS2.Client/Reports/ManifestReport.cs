namespace CMS2.Client.Reports
{
    using Forms.TrackingReportsView;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for ManifestReport.
    /// </summary>
    public partial class ManifestReport : Telerik.Reporting.Report
    {
        public ManifestReport()
        {

            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            objectDataSource.DataSource = ReportGlobalModel.ManifestReportData;

            table1.DataSource = objectDataSource;
            txtDestinationBCO.Value = ReportGlobalModel.Manifest_DestinationBCO;
            txtOriginBco.Value = ReportGlobalModel.Manifest_OriginBCO;
            txtServiceMode.Value = ReportGlobalModel.Manifest_ServiceMode;
            txtPaymode.Value = ReportGlobalModel.Manifest_Paymode; ;
            txtServiceType.Value = ReportGlobalModel.Manifest_ServiceType;
            txtShipmode.Value = ReportGlobalModel.Manifest_Shipmode;
            
        }
    }
}