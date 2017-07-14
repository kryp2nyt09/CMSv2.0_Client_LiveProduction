namespace CMS2.Client.Reports
{
    using CMS2.Common.Constants;
    using Entities.Models;
    using Forms.TrackingReportsView;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for PaymentSummaryReportView.
    /// </summary>
    public partial class PaymentSummaryReportView : Telerik.Reporting.Report
    {
        public PaymentSummaryReportView()
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();
            var objectDataSourcePrepaid = new Telerik.Reporting.ObjectDataSource();
            var objectDataSourceFreight = new Telerik.Reporting.ObjectDataSource();

            List<PaymentSummaryDetails> paymentSummaryDetails = ReportGlobalModel.PaymentSummaryDetailsReportData;
            List<PaymentSummary_MainDetailsModel> paymentSummaryMainDetails = ReportGlobalModel.PaymentSummary_MainDetailsReportData;


            objectDataSourcePrepaid.DataSource = paymentSummaryDetails.FindAll(x => x.PaymentModeCode == PaymentModes.Prepaid);
            //objectDataSourcePrepaid.DataSource = null;
            objectDataSourceFreight.DataSource = paymentSummaryDetails.FindAll(x => x.PaymentModeCode == PaymentModes.FreightCollect);

            //if(objectDataSourcePrepaid != null)
            //{
            //    lblPrepaid.Visible = true;
            //    tblPrepaid.Visible = true;
            //    tblPrepaid.DataSource = objectDataSourcePrepaid;
            //}
            //if(objectDataSourceFreight != null && objectDataSourcePrepaid == null)
            //{
            //    lblFreight.Visible = true;
            //    tblFreight.Visible = true;
            //    tblFreight.DataSource = objectDataSourceFreight;
            //    lblFreight.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Inch(0D));
            //    tblFreight.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.30000004172325134D), Telerik.Reporting.Drawing.Unit.Inch(0.40000009536743164D));
            //}
            //else
            //{
            //    lblFreight.Visible = true;
            //    tblFreight.Visible = true;
            //    tblFreight.DataSource = objectDataSourceFreight;
            //}
            tblPrepaid.DataSource = objectDataSourcePrepaid;
            tblFreight.DataSource = objectDataSourceFreight;

            foreach (PaymentSummary_MainDetailsModel item in paymentSummaryMainDetails)
            {
                txtCollectedBy.Value = item.CollectedBy;
                txtArea_Branch.Value = item.Area;
                txtRemittanceDate.Value = item.CollectionDate;
                txtValidatedBy.Value = item.ValidatedBy;

                txtTotalCash.Value = item.TotalCash.ToString();
                txtTotalCheck.Value = item.TotalCheck.ToString();
                txtTotalCollection.Value = item.TotalCollection.ToString();
                txtTaxWithheld.Value = item.TotalTaxWithheld.ToString();

                txtTotalCashReceived.Value = item.TotalCashReceived.ToString();
                txtTotalCheckReceived.Value = item.TotalCheckReceived.ToString();
                txtAmtReceived.Value = item.TotalAmountReceived.ToString();
                txtAmtDifference.Value = item.Difference.ToString();
                txtRemittedBy.Value = item.RemittedBy;
                pictureBox1.Value = ByteToImage(item.Signature);
                

            }
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
    }
}