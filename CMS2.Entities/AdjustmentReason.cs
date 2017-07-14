using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class AdjustmentReason:BaseEntity
    {
        [Key]
        public Guid AdjustmentReasonId { get; set; }
        [Required]
        [DisplayName("Adjustment Reason")]
        [MaxLength(80)]
        public string Reason { get; set; }
        public int ListOrder { get; set; }
    }
}
