using System;
using System.Collections.Generic;
using System.ComponentModel;
using CMS2.Common.Enums;

namespace CMS2.Entities.Models
{
    public class StatementOfAccountModel
    {
        public StatementOfAccountModel()
        {
            this.StatementOfAccountPrints = new List<StatementOfAccountPrint>();
            this.CurrentShipments = new List<ShipmentModel>();
            this.PreviousShipments = new List<ShipmentModel>();
            this.SoaPayments = new List<StatementOfAccountPayment>();
            this.PreviousSoaPayments = new List<StatementOfAccountPayment>();
        }
        public Guid StatementOfAccountId { get; set; }
        [DisplayName("SOA No")]
        public string StatementOfAccountNo { get; set; }
        [DisplayName("SOA No Barcode")]
        public byte[] StatementOfAccountNoBarcode { get; set; }
        [DisplayName("SOA Date")]
        public DateTime StatementOfAccountDate { get; set; }
        [DisplayName("Start Date")]
        public DateTime StatementOfAccountPeriodFrom { get; set; }
        [DisplayName("End Date")]
        public DateTime StatementOfAccountPeriodUntil { get; set; }
        [DisplayName("Company")]
        public Guid CompanyId { get; set; }
        [DisplayName("Company")]
        public Company Company { get; set; }
        [DisplayName("Account No Barcode")]
        public byte[] CompanyAccountNoBarcode { get; set; }
        [DisplayName("SOA Due Date")]
        public DateTime SoaDueDate { get; set; }
        [DisplayName("CI No")]
        public string CiNo { get; set; }
        public string Remarks { get; set; }
        public string PdfFileName
        {
            get
            {
                return StatementOfAccountDate.ToString("yyyyMMdd") + "_" + StatementOfAccountNo + "_" + Company.AccountNo;
            }
        }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        [DisplayName("Record Status")]
        public DateTime ModifiedDate { get; set; }
        [DisplayName("Record Status")]
        public int RecordStatus { get; set; }
        public RecordStatus Record_Status { get; set; }
        [DisplayName("Record Status")]
        public string RecordStatusString
        {
            get
            {
                RecordStatus recordStatus = (RecordStatus)this.RecordStatus;
                return recordStatus.ToString();
            }
        }
        public List<StatementOfAccountPrint> StatementOfAccountPrints { get; set; }
        // added this member so that StatementOfAccountPeriod
        // can have value via the Set Accessor
        private string _statementOfAccountPeriod = "";
        [DefaultValue("")]
        [DisplayName("Billing Period")]
        public string StatementOfAccountPeriod
        {
            get
            {
                return StatementOfAccountPeriodFrom.ToString("MMM dd, yyyy") + " - " + StatementOfAccountPeriodUntil.ToString("MMM dd, yyyy");
            }
            set { this._statementOfAccountPeriod = value; } // needed to have this Accessor
        }
        [DisplayName("SOA Date")]
        public string StatementOfAccountDateString
        {
            get { return StatementOfAccountDate.ToString("MMM dd, yyyy"); }
        }
        [DisplayName("SOA Due Date")]
        public string SoaDueDateString
        {
            get { return SoaDueDate.ToString("MMM dd, yyyy"); }
        }
        public List<ShipmentModel> CurrentShipments { get; set; }
        public List<ShipmentModel> PreviousShipments { get; set; }
        public List<StatementOfAccountPayment> SoaPayments { get; set; }
        public List<StatementOfAccountPayment> PreviousSoaPayments { get; set; }



        [DisplayName("SOA Amount Due")]
        [Description("Total amount due of current SOA ")]
        public decimal TotalSoaAmount { get; set; }
        [DisplayName("Totals of Shipment Sub-Total")]
        [Description("Total amount charged excluding vat. Total current Freight Charges")]
        public decimal TotalCurrentSubTotal { get; set; }
        [DisplayName("Total Vat Amount")]
        public decimal TotalCurrentVatAmount { get; set; }
        [DisplayName("Totals of Shipment Total")]
        [Description("Total shipment amount charged including vat")]
        public decimal TotalCurrentTotal { get; set; }
        [DisplayName("Total Previous Ammount Due")]
        [Description("Total amount due from previous SOA before Payments and Adjustment")]
        public decimal TotalPreviousAmountDue { get; set; }
        [DisplayName("Total Previous Payments")]
        [Description("Total Payments from previous SOA")]
        public decimal TotalPreviousPayments { get; set; }
        [DisplayName("Total Previous Adjustments")]
        [Description("Total Adjustments from previous SOA")]
        public decimal TotalPreviousAdjustments { get; set; }
        [DisplayName("Total Previous Balance")]
        [Description("Total of Balances from previous SOA before Surcharge")]
        public decimal TotalPreviousBalance { get; set; }
        [DisplayName("Total Previous Surcharge")]
        [Description("Total Surcharge from previous SOA")]
        public decimal TotalPreviousSurcharge { get; set; }
        [DisplayName("Total Current Charges")]
        [Description("Total charges from current Shipments and Surcharge from previous SOA")]
        public decimal TotalCurrentCharges { get; set; }
        [DisplayName("Total Balances from Previous")]
        [Description("Total balances from previous SOA before Surcharge")]
        public decimal TotalBalancesFromPrevious { get; set; }
        [DisplayName("Total Current Payments")]
        [Description("Total payments from the current SOA Shipments")]
        public decimal TotalCurrentPayments { get; set; }
        [DisplayName("Total Current Adjustments")]
        [Description("Total adjustments from the current SOA Shipments")]
        public decimal TotalCurrentAdjustments { get; set; }
        [DisplayName("Total SOA Payments")]
        public decimal TotalSoaPayments { get; set; }
        [DisplayName("Total SOA Adjustments")]
        public decimal TotalSoaAdjustments { get; set; }

        [DisplayName("SOA Amount Due")]
        public string TotalSoaAmountString { get { return TotalSoaAmount.ToString("N"); } }
        [DisplayName("Freight Charges")]
        public string TotalCurrentSubTotalString { get { return TotalCurrentSubTotal.ToString("N"); } }
        [DisplayName("Total VAT Amount")]
        public string TotalCurrentVatAmountString { get { return TotalCurrentVatAmount.ToString("N"); } }
        [DisplayName("Total Amount Due")]
        public string TotalCurrentTotalString { get { return TotalCurrentTotal.ToString("N"); } }
        [DisplayName("Previous Amount Due")]
        public string TotalPreviousAmountDueString { get { return TotalPreviousAmountDue.ToString("N"); } }
        [DisplayName("Previous Payments")]
        public string TotalPreviousPaymentsString { get { return TotalPreviousPayments.ToString("N"); } }
        [DisplayName("Previous Adjustments")]
        public string TotalPreviousAdjustmentsString { get { return TotalPreviousAdjustments.ToString("N"); } }
        [DisplayName("Previous Balances")]
        public string TotalPreviousBalanceString { get { return TotalPreviousBalance.ToString("N"); } }
        [DisplayName("Total Surcharge")]
        public string TotalPreviousSurchargeString { get { return TotalPreviousSurcharge.ToString("N"); } }
        [DisplayName("Total Current Balance")]
        public string TotalCurrentChargesString { get { return TotalCurrentCharges.ToString("N"); } }
        [DisplayName("Total Previous Balance")]
        public string TotalBalancesFromPreviousString { get { return TotalBalancesFromPrevious.ToString("N"); } }
        [DisplayName("Current Payments")]
        public string TotalCurrentPaymentsString { get { return TotalCurrentPayments.ToString("N"); } }
        [DisplayName("Current Adjustments")]
        public string TotalCurrentAdjustmentsString { get { return TotalCurrentAdjustments.ToString("N"); } }
        public string TotalSoaPaymentsString { get { return TotalSoaPayments.ToString("N"); } }
        public string TotalSoaAdjustmentsString { get { return TotalSoaAdjustments.ToString("N"); } }


        // for SOA History
        public decimal HistoryAmountDueFromPrevious { get; set; }
        [DisplayName("Total SOA Adjustments")]
        public decimal HistoryAdjustments { get; set; }
        [DisplayName("Total SOA Payments")]
        public decimal HistoryPayments { get; set; }
        public decimal HistoryPreviousBalance { get; set; }
        public decimal HistoryFreightCharges { get; set; }
        public decimal HistorySurcharge { get; set; }
        public decimal HistoryCurrentCharges { get; set; }
        public decimal HistoryAmountDue { get; set; }

        public string HistoryAmountDueFromPreviousString { get { return HistoryAmountDueFromPrevious.ToString("N"); } }
        public string HistoryAdjustmentsString { get { return HistoryAdjustments.ToString("N"); } }
        public string HistoryPaymentsString { get { return HistoryPayments.ToString("N"); } }
        public string HistoryPreviousBalanceString { get { return HistoryPreviousBalance.ToString("N"); } }
        public string HistoryFreightChargesString { get { return HistoryFreightCharges.ToString("N"); } }
        public string HistorySurchargeString { get { return HistorySurcharge.ToString("N"); } }
        public string HistoryCurrentChargesString { get { return HistoryCurrentCharges.ToString("N"); } }
        public string HistoryAmountDueString { get { return HistoryAmountDue.ToString("N"); } }
    }
}
