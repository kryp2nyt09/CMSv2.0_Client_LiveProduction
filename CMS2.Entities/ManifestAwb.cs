using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    /// <summary>
    /// Airline Manifest AWB or Transmittal
    /// Contains the flight information
    /// Contains information of the Bundles being Shipped
    /// </summary>
    public class ManifestAwb : BaseEntity
    {
        [Key]
        public Guid ManifestAwbId { get; set; }
        [Required]
        [MaxLength(15)]
        public string ManifestAwbNo { get; set; }
        [Required]
        public DateTime ManifestDate { get; set; }
        [Required]
        public Guid ManifestById { get; set; }
        [ForeignKey("ManifestById")]
        public virtual Employee ManifestBy { get; set; }
        public Guid GatewayId { get; set; }
        public virtual Gateway Gateway { get; set; }
        public Guid OriginCityId { get; set; }
        [ForeignKey("OriginCityId")]
        public virtual City OriginCity { get; set; }
        public Guid DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public virtual City DestinationCity { get; set; }

        public Guid? TransmittalStatusId { get; set; }
        public virtual TransmittalStatus TransmittalStatus { get; set; }
        public virtual List<PackageNumber> PackageNumbers { get; set; }
        public virtual List<Bundle> Bundles { get; set; } 
    }
}
