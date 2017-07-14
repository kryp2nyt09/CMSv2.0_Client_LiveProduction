using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class DeliveryReceipt : BaseEntity
    {
        [Key]
        public Guid DeliveryReceiptId { get; set; }
        [Required]
        [MaxLength(80)]
        [DisplayName("Received By")]
        public string ReceivedBy { get; set; }
        [Required]
        public byte[] Signature { get; set; }
        public Guid DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }
    }
}
