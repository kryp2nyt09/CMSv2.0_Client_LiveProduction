using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class PaymentMode : BaseEntity
    {
        [Key]
        public Guid PaymentModeId { get; set; }
        [Required]
        [StringLength(3)]
        [DisplayName("Payment Mode Code")]
        public string PaymentModeCode { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Payment Mode")]
        public string PaymentModeName { get; set; }
        public int ListOrder { get; set; }
        
    }
}
