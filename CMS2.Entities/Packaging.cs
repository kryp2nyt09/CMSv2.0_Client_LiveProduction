using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class Packaging : BaseEntity
    {
        [Key]
        public Guid PackagingId { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Packaging Name")]
        public string PackagingName { get; set; }
        [Required]
        [DisplayName("Min Weight")]
        public int BaseMinimum { get; set; }
        [Required]
        [DisplayName("Max Weight")]
        public int BaseMaximum { get; set; }
        [Required]
        [DisplayName("Minimum Cost")]
        public decimal BaseFee { get; set; }
    }
}
