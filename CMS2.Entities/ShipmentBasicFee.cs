using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class ShipmentBasicFee : BaseEntity
    {
        [Key]
        public Guid ShipmentBasicFeeId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Shipment Fee")]
        public string ShipmentFeeName { get; set; }
        [Required]
        [DisplayName("Amount")]
        [DefaultValue(0)]
        public decimal Amount { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [DisplayName("Is Vatable")]
        [DefaultValue(0)]
        public bool IsVatable { get; set; }
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }


        [NotMapped]
        [DisplayName("Effective Date")]
        public string EffectiveDateString { get { return EffectiveDate.ToString("MMM dd, yyyy"); } }
        [NotMapped]
        [DisplayName("Amount")]
        public string AmountString { get { return Amount.ToString("N"); } }
    }
}
