using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class TransShipmentLeg:BaseEntity
    {
        [Key]
        public Guid TransShipmentLegId { get; set; }

        [DisplayName("Leg")]
        public string LegName { get; set; }
        
        [Required]
        [DefaultValue(1)]
        public int LegOrder { get; set; }
        public Guid CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City{get;set;}
        
    }
}
