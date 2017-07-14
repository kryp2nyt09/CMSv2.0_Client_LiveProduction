using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class RevenueUnitType: BaseEntity
    {
        [Key]
        public Guid RevenueUnitTypeId { get; set; }
        [MaxLength(50)]
        [DisplayName("Revenue Unit Type")]
        public string RevenueUnitTypeName { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
    }
}
