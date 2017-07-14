using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class PaymentTurnover:BaseEntity
    {
        [Key]
        public Guid PaymentTurnOverId { get; set; }
        public DateTime CollectionDate { get; set; }
        public Guid CollectedById { get; set; }
        [ForeignKey("CollectedById")]
        public virtual Employee CollectedBy { get; set; }
        public decimal ReceivedCashAmount { get; set; }
        public decimal ReceivedCheckAmount { get; set; }
        public string Remarks { get; set; }



        [NotMapped]
        public string CollectionDateString { get { return CollectionDate.ToString("MMM dd, yyyy"); } }
        [NotMapped]
        public string ReceivedCashAmountString { get { return ReceivedCashAmount.ToString("N"); } }
        [NotMapped]
        public string ReceivedCheckAmountString { get { return ReceivedCheckAmount.ToString("N"); } }
    }
}
