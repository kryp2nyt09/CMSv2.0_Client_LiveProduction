using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class FuelSurcharge:BaseEntity
    {
        [Key]
        public Guid FuelSurchargeId { get; set; }
        [DisplayName("Origin Group")]
        public Guid OriginGroupId { get; set; }
        [DisplayName("Origin Group")]
        [ForeignKey("OriginGroupId")]
        public virtual Group OriginGroup { get; set; }
        [DisplayName("Destination Group")]
        public Guid DestinationGroupId { get; set; }
        [DisplayName("Destination Group")]
        [ForeignKey("DestinationGroupId")]
        public virtual Group DestinationGroup { get; set; }
        [Required]
        [DisplayName("Amount")]
        [DefaultValue(0)]
        public decimal Amount { get; set; }
        [DisplayName("Is Vatable")]
        [DefaultValue(0)]
        public bool IsVatable { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }


        [DisplayName("Effective Date")]
        [NotMapped]
        public string EffectiveDateString { get { return EffectiveDate.ToString("MMM dd, yyyy"); } }
        [DisplayName("Amount")]
        [NotMapped]
        public string AmountString { get { return Amount.ToString("N"); } }
    }
}
