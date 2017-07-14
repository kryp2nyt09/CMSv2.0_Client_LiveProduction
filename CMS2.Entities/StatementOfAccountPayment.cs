using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class StatementOfAccountPayment:BaseEntity   
    {
        
        [Key]
        public Guid StatementOfAccountPaymentId { get; set; }
        [Required]
        [DisplayName("Statement Of Account")]
        public Guid StatementOfAccountId { get; set; }
        [DisplayName("Statement Of Account")]
        public virtual StatementOfAccount StatementOfAccount { get; set; }
        [MaxLength(10)]
        [DisplayName("OR No")]
        public string OrNo { get; set; }
        [MaxLength(10)]
        [DisplayName("PR No")]
        public string PrNo { get; set; }
        [Required]
        [DisplayName("Payment Date")]
        public DateTime PaymentDate { get; set; }
        [Required]
        [DisplayName("Amount Paid")]
        [DefaultValue(0)]
        public decimal Amount { get; set; }
        [DisplayName("Payment Type")]
        public Guid PaymentTypeId { get; set; }
        [DisplayName("Payment Type")]
        public virtual PaymentType PaymentType { get; set; }
        [DisplayName("Check Bank")]
        [MaxLength(80)]
        public string CheckBankName { get; set; }
        [DisplayName("Check No")]
        [MaxLength(15)]
        public string CheckNo { get; set; }
        [DisplayName("Check Date")]
        public DateTime? CheckDate { get; set; }
        [DisplayName("Received By")]
        public Guid ReceivedById { get; set; }
        [ForeignKey("ReceivedById")]
        [DisplayName("Received By")]
        public virtual Employee ReceivedBy { get; set; }
        [MaxLength(250)]
        public string Remarks { get; set; }
        public virtual List<Payment> Payments { get; set; }
        

        [NotMapped]
        [DisplayName("Payment Date")]
        public string PaymentDateString {
            get { return PaymentDate.ToString("MMM dd, yyyy"); }
        }
        [NotMapped]
        [DisplayName("Amount Paid")]
        public string AmountString {
            get { return Amount.ToString("N"); }
        }
    }
}
