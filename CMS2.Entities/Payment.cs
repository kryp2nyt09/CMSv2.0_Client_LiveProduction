using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    /// <summary>
    /// Shipment / AWB Payments
    /// </summary>
    public class Payment : BaseEntity
    {
        [Key]
        public Guid PaymentId { get; set; }
        [DisplayName("Shipment")]
        public Guid ShipmentId { get; set; }
        [DisplayName("Shipment")]
        public virtual Shipment Shipment { get; set; }
        [Required]
        [DisplayName("Payment Date")]
        public DateTime PaymentDate { get; set; }
        [MaxLength(10)]
        [DisplayName("OR No")]
        public string OrNo { get; set; }
        [MaxLength(10)]
        [DisplayName("PR No")]
        public string PrNo { get; set; }
        [Required]
        [DisplayName("Amount Paid")]
        [DefaultValue(0)]
        public decimal Amount { get; set; }
        [DisplayName("Payment Type")]
        public Guid PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        [MaxLength(50)]
        [DisplayName("Check Bank")]
        public string CheckBankName { get; set; }
        [MaxLength(15)]
        [DisplayName("Check No")]
        public string CheckNo { get; set; }
        [DisplayName("Check Date")]
        public DateTime? CheckDate { get; set; }
        [DisplayName("Received By")]
        public Guid ReceivedById { get; set; }
        [ForeignKey("ReceivedById")]
        public virtual Employee ReceivedBy { get; set; }
        [DisplayName("Verified Date")]
        public DateTime? VerifiedDate { get; set; }
        [DisplayName("Verified By")]
        public Guid? VerifiedById { get; set; }
        [ForeignKey("VerifiedById")]
        public virtual Employee VerifiedBy { get; set; }
        [MaxLength(250)]
        public string Remarks { get; set; }
        [DisplayName("SOA Payment")]
        public Guid? StatementOfAccountPaymentId { get; set; }
        [DisplayName("SOA Payment")]
        public virtual StatementOfAccountPayment StatementOfAccountPayment { get; set; }
        [DisplayName("Tax Withheld")]
        [DefaultValue(0)]
        public decimal TaxWithheld { get; set; }


        [NotMapped]
        [DisplayName("Amount Paid")]
        public string AmountString { get { return Amount.ToString("N"); } }
        [NotMapped]
        [DisplayName("Payment Date")]
        public string PaymentDateString { get { return PaymentDate.ToString("MMM dd, yyyy"); } }
        [NotMapped]
        [DisplayName("Tax Withheld")]
        public string TaxWithheldString { get { return TaxWithheld.ToString("N"); } }
    }
}
