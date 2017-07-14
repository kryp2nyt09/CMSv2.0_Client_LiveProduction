using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class ApplicableRate : BaseEntity
    {
        [Key]
        public Guid ApplicableRateId { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Applicable Rate")]
        public string ApplicableRateName { get; set; }
        public Guid CommodityTypeId { get; set; }
        public Guid ServiceModeId { get; set; }
        public Guid ServiceTypeId{get; set; }

        [ForeignKey("CommodityTypeId")]
        public virtual CommodityType CommodityType { get; set; }

        [ForeignKey("ServiceModeId")]
        public virtual ServiceMode ServiceMode { get; set; }

        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }
    }
}
