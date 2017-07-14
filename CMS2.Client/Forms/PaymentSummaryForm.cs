using CMS2.Client.Reports;
using CMS2.Entities.Models;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace CMS2.Client.Forms
{
    public partial class PaymentSummaryForm : Telerik.WinControls.UI.RadForm
    {
        public PaymentSummaryForm()
        {
            InitializeComponent();
        }

        List<PaymentSummaryModel> new_Model = new List<PaymentSummaryModel>();
        List<PaymentSummaryDetails> paymentsummaryDetails = new List<PaymentSummaryDetails>();
        List<PaymentSummary_MainDetailsModel> paymentMainDetails = new List<PaymentSummary_MainDetailsModel>();

        public void passListofSummary(List<PaymentSummaryModel> model)
        {
            new_Model = model;
        }

        public void passListofSummaryDetails(List<PaymentSummaryDetails> details)
        {
            paymentsummaryDetails = details;
        }

        public void passListofMainDetails(List<PaymentSummary_MainDetailsModel> maindetails)
        {
            paymentMainDetails = maindetails;
        }

        private void PaymentSummaryForm_Load(object sender, EventArgs e)
        {
            List<PaymentSummaryDetails> prepaid = new List<PaymentSummaryDetails>();
            List<PaymentSummaryDetails> freightCollect = new List<PaymentSummaryDetails>();
            int count = new_Model.Count;
            
            Console.WriteLine("Number of record" + "" + count);

            prepaid = paymentsummaryDetails.FindAll(x => x.PaymentModeCode.Equals("PP"));
            int cntPrepaid = prepaid.Count;
            Console.WriteLine("Prepaid Count" + "" + cntPrepaid);

            freightCollect = paymentsummaryDetails.FindAll(x => x.PaymentModeCode.Equals("FC"));
            int cntFreightCollect = freightCollect.Count;
            Console.WriteLine("freightCollect Count" + "" + cntFreightCollect);


            //report_PaymentSummary report = new report_PaymentSummary();
            //report.SetDataSource(prepaid);
            //crystalReportViewer_PaymentSummary.ReportSource = report;
            ReportDocument rptdoc = new ReportDocument();
            rptdoc.Load(AppDomain.CurrentDomain.BaseDirectory + "Reports\\PaymentSummary.rpt");
            rptdoc.SetDataSource(paymentMainDetails);
            rptdoc.Subreports[0].SetDataSource(prepaid);
            rptdoc.Subreports[1].SetDataSource(freightCollect);
            crystalReportViewer_PaymentSummary.ReportSource = rptdoc;
            crystalReportViewer_PaymentSummary.Refresh();
            crystalReportViewer_PaymentSummary.RefreshReport();
        }
    }
}
