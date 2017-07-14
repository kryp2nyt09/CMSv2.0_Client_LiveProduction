using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class WeightBreak:BaseEntity
    {
        [Key]
        public Guid WeightBreakId { get; set; }
        public Guid CommodityTypeId { get; set; }
        public CommodityType CommodityType { get; set; }
        [DefaultValue(0)]
        public decimal Minimum { get; set; }
        [DefaultValue(0)]
        public decimal Maximum { get; set; }
    }
}
