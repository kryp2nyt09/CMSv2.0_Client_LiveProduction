using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class StatementOfAccount : BaseEntity
    {
        [Key]
        public Guid StatementOfAccountId { get; set; }
        // Create SOANo once SOA is final
        // 8-digit string
        [Required]
        [MaxLength(5)]
        [DisplayName("SOA No")]
        public string StatementOfAccountNo { get; set; }
        [DisplayName("SOA Date")]
        public DateTime StatementOfAccountDate { get; set; }
        [DisplayName("Start Date")]
        public DateTime StatementOfAccountPeriodFrom { get; set; }
        [DisplayName("End Date")]
        public DateTime StatementOfAccountPeriodUntil { get; set; }
        [DisplayName("Company")]
        public Guid CompanyId { get; set; }
        [DisplayName("Company")]
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [DisplayName("SOA Due Date")]
        public DateTime SoaDueDate { get; set; }
        
        [DisplayName("CI No")]
        public string CiNo { get; set; }
        [MaxLength(250)]
        public string Remarks { get; set; }
        //public virtual List<StatementOfAccountPrint> StatementOfAccountPrints { get; set; }
        //public virtual List<Shipment> Shipments { get; set; }



        [NotMapped]
        public int StatementOfAccountNoInt
        {
            set { StatementOfAccountNo = value.ToString("00000"); }
            get { return string.IsNullOrEmpty(StatementOfAccountNo) ? 0 : Convert.ToInt32(StatementOfAccountNo); }
        }

        // added this member so that StatementOfAccountPeriod
        // can have value via the Set Accessor
        private string _statementOfAccountPeriod = "";
        [NotMapped]
        [DefaultValue("")]
        [DisplayName("Billing Period")]
        public string StatementOfAccountPeriod
        {
            get
            {
                return StatementOfAccountPeriodFrom.ToString("MMM dd, yyyy") + " - " +
                       StatementOfAccountPeriodUntil.ToString("MMM dd, yyyy");
            }
            set { this._statementOfAccountPeriod = value; } // needed to have this Accessor
        }
        [NotMapped]
        [DisplayName("SOA Date")]
        public string StatementOfAccountDateString
        {
            get { return StatementOfAccountDate.ToString("MMM dd, yyyy"); }
        }
        [NotMapped]
        [DisplayName("SOA Due Date")]
        public string SoaDueDateString
        {
            get { return SoaDueDate.ToString("MMM dd, yyyy"); }
        }
        [DisplayName("Amount Due")]
        [NotMapped]
        public decimal AmountDue { get; set; }
        [NotMapped]
        [DisplayName("Amount Due")]
        public string AmountDueString
        {
            get { return AmountDue.ToString("N"); }
        }
        [DisplayName("Previous Balance")]
        [NotMapped]
        public decimal PreviousBalance { get; set; }
        [DisplayName("Previous Balance")]
        [NotMapped]
        public string PreviousBalanceString
        {
            get { return PreviousBalance.ToString("N"); }
        }
        [DisplayName("Outstanding Balance")]
        [NotMapped]
        public decimal OutstandingBalance { get; set; }
        [DisplayName("Outstanding Balance")]
        [NotMapped]
        public string OutstandingBalanceString
        {
            get
            {
                return OutstandingBalance.ToString("N");
            }
        }
        [NotMapped]
        [DisplayName("Total Adjustments")]
        public decimal TotalAdjustment { get; set; }
        [NotMapped]
        [DisplayName("Total Adjustments")]
        public string TotalAdjustmentString
        {
            get { return TotalAdjustment.ToString("N"); }
        }

        [NotMapped]
        [DisplayName("Total Surcharge")]
        public decimal TotalSurcharge { get; set; }
        [NotMapped]
        [DisplayName("Total Surcharge")]
        public string TotalSurchargeString
        {
            get { return TotalSurcharge.ToString("N"); }
        }

        // Total Balance of the Current Charges(Shipments/AWB)
        [NotMapped]
        [DisplayName("Current Charge Balance")]
        public decimal CurrentChargeBalance { get; set; }
        [NotMapped]
        [DisplayName("Current Charge Balance")]
        public string CurrentChargeBalanceString
        {
            get { return CurrentChargeBalance.ToString("N"); }
        }
        [NotMapped]
        [DisplayName("Total Previous Payments")]
        public decimal TotalPreviousPayment { get; set; }
        [NotMapped]
        [DisplayName("Total Previous Payments")]
        public string TotalPreviousPaymentString
        {
            get { return TotalPreviousPayment.ToString("N"); }
        }
    }
}
