using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class BillingPeriod : BaseEntity
    {
        [Key]
        public Guid BillingPeriodId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Billing Period")]
        public string BillingPeriodName { get; set; }
        [Required]
        [DisplayName("No of Days")]
        public int NumberOfDays { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public int ListOrder { get; set; }
    }
}
