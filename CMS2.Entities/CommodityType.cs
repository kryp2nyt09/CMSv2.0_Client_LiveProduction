using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class CommodityType:BaseEntity
    {
        [Key]
        public Guid CommodityTypeId { get; set; }
        [StringLength(10)]
        [DisplayName("Code")]
        public string CommodityTypeCode { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Commodity Type")]
        public string CommodityTypeName { get; set; }
        //public int ListOrder { get; set; }
        [MaxLength(300)]
        [DisplayName("Description")]
        public string CommodityTypeDesc { get; set; }
        [DisplayName("EVM Divisor")]
        [DefaultValue(1)]
        public int EvmDivisor { get; set; }

        public List<Commodity> Commoditites { get; set; }
        public List<WeightBreak> WeightBreaks { get; set; } 
        
    }
}
