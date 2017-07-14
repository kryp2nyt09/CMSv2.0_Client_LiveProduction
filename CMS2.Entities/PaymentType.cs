using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    /// <summary>
    /// Type of Payment. (Cash,Check,Credit)
    /// </summary>
    public class PaymentType : BaseEntity
    {
        [Key]
        public Guid PaymentTypeId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Payment Type")]
        public string PaymentTypeName { get; set; }
        public int ListOrder { get; set; }
    }
}
