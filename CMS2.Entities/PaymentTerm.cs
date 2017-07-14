using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class PaymentTerm:BaseEntity
    {
        [Key]
        public Guid PaymentTermId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Payment Term")]
        public string PaymentTermName { get; set; }
        [Required]
        [DisplayName("No of Days")]
        public int NumberOfDays { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public int ListOrder { get; set; }
    }
}
