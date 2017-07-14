using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class Crating:BaseEntity
    {
        [Key]
        public Guid CratingId { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Crating Name")]
        public string CratingName { get; set; }
        [Required]
        [DisplayName("Factor")]
        public decimal Multiplier { get; set; }
        [Required]
        [DisplayName("Min Weight")]
        public int BaseMinimum { get; set; }
        [DisplayName("Max Weight")]
        [Required]
        public int BaseMaximum { get; set; }
        [Required]
        [DisplayName("Minimum Cost")]
        public decimal BaseFee { get; set; }
        [Required]
        [DisplayName("Excess Cost")]
        public decimal ExcessCost { get; set; }
    }
}
