using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class TransShipmentRoute:BaseEntity
    {
        [Key]
        public Guid TransShipmentRouteId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Route Name")]
        public string TransShipmentRouteName { get; set; }
        [DisplayName("Origin City")]
        public Guid OriginCityId { get; set; }
        [ForeignKey("OriginCityId")]
        public City OriginCity { get; set; }
        [DisplayName("Destination City")]
        public Guid DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public City DestinationCity { get; set; }
        //public List<TransShipmentLeg> Legs { get; set; } 
    }
}
