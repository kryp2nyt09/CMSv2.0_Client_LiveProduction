using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Commodity:BaseEntity
    { 
        [Key]
        public Guid CommodityId { get; set; }
        [DisplayName("Commodity")]
        [MaxLength(50)]
        public string CommodityName { get; set; }
        [DisplayName("Commority Type")]
        public Guid CommodityTypeId { get; set; }
        [ForeignKey("CommodityTypeId")]
        public CommodityType CommodityType { get; set; }
    }
}
