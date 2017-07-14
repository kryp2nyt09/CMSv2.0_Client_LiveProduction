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
    using System.Linq;
    using CMS2.Common.Enums;
    using CMS2.Common.Constants;
    /// <summary>
    /// Summary description for DailyTripReportView.
    /// </summary>
    public partial class DailyTripReportView : Telerik.Reporting.Report
    {
        public DailyTripReportView()
        {
            InitializeComponent();

            var objectDataSource = new Telerik.Reporting.ObjectDataSource();
            var objectDataSource2 = new Telerik.Reporting.ObjectDataSource();
            var objectDataSource3 = new Telerik.Reporting.ObjectDataSource();
            var objectDataSource4 = new Telerik.Reporting.ObjectDataSource();

            List<DailyTripViewModel> Prepaid = ReportGlobalModel.DailyTripReportData.Where(x => x.PaymentCode == PaymentModes.Prepaid).ToList();
            int prepaidCtr = 1;
            Prepaid.ForEach(x => x.No = prepaidCtr++);
            objectDataSource.DataSource = Prepaid;
            table1.DataSource = objectDataSource;


            List<DailyTripViewModel> FC = ReportGlobalModel.DailyTripReportData.Where(x => x.PaymentCode == PaymentModes.FreightCollect).ToList();
            int FCCtr = 1;
            FC.ForEach(x => x.No = FCCtr++);
            objectDataSource2.DataSource = FC;
            table2.DataSource = objectDataSource2;

            List<DailyTripViewModel> DeferredPayment = ReportGlobalModel.DailyTripReportData.Where(x => x.PaymentMode == PaymentModes.DeferredPayment).ToList();


            List<DailyTripViewModel> CAS = ReportGlobalModel.DailyTripReportData.Where(x => x.PaymentCode == PaymentModes.CorporateAccountConsignee).ToList();
            int CASCtr = 1;
            FC.ForEach(x => x.No = CASCtr++);
            objectDataSource3.DataSource = CAS;
            table3.DataSource = objectDataSource3;

            List<DailyTripViewModel> CAC = ReportGlobalModel.DailyTripReportData.Where(x => x.PaymentCode == PaymentModes.CorporateAccountShipper).ToList();
            int CACCtr = 1;
            FC.ForEach(x => x.No = CACCtr++);
            objectDataSource4.DataSource = CAC;
            table4.DataSource = objectDataSource4;

            txtDate.Value = ReportGlobalModel.Date;
            txtArea.Value = ReportGlobalModel.Area;
            txtBatch.Value = ReportGlobalModel.Batch;
            txtpaymentMode.Value = ReportGlobalModel.PaymentMode;
            txtPlateNo.Value = ReportGlobalModel.PlateNo;

            txtScannedBy.Value = ReportGlobalModel.ScannedBy;
            // txtRemarks.Value = TrackingReportGlobalModel.Remarks;
            // txtNotes.Value = TrackingReportGlobalModel.Notes;
        }
    }
}