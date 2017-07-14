using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class PackageDimension : BaseEntity
    {
        [Key]
        public Guid PackageDimensionId { get; set; }

        [Required]
        public Guid ShipmentId { get; set; }

        public virtual Shipment Shipment { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Length { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Width { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Height { get; set; }

        [DisplayName("Crating Fee")]
        public Guid? CratingId { get; set; }
        public Crating Crating { get; set; }
        [DisplayName("Draining Fee")]
        public Guid? DrainingId { get; set; }
        [ForeignKey("DrainingId")]
        public ShipmentBasicFee DrainingFee { get; set; }
        public Guid? PackagingId { get; set; }
        public Packaging Packaging { get; set; }
    }
}
