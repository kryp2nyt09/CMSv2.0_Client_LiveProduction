using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Inbound:BaseEntity
    {
        [Key]
        public Guid InboundId { get; set; }
        [Required]
        public DateTime InboundDate { get; set; }
        public Guid GatewayId { get; set; }
        public virtual Gateway Gateway { get; set; }
        public Guid DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public virtual City DestinationCity { get; set; }

        [Required]
        public Guid ScannedById { get; set; }
        [ForeignKey("ScannedById")]
        public virtual Employee ScannedBy { get; set; }

        public virtual List<PackageNumber> PackageNumbers { get; set; }
        public virtual List<Bundle> Bundles { get; set; } 
    }
}
