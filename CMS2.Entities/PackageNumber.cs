using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class PackageNumber : BaseEntity
    {
        [Key]
        public Guid PackageNumberId { get; set; }
        [Required]
        public Guid ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }
        
        [Required]
        [MaxLength(15)]
        [DisplayName("Package No")]
        public string PackageNo { get; set; }
        
        [NotMapped]
        [DisplayName("Package No Barcode")]
        public byte[] PackageNoBarcode { get; set; }
        public Guid ScannedById { get; set; }
        [ForeignKey("ScannedById")]
        public virtual Employee ScannedBy { get; set; }
        public virtual List<DeliveredPackage> DeliveredPackages { get; set; }
        public DateTime ScannedDate { get; set; }
        
        //public Guid? BundleId { get; set; }
        //public virtual Bundle Bundle { get; set; }
        //public Guid? ManifestAwbId { get; set; }
        //public virtual ManifestAwb ManifestAwb { get; set; }
        //public Guid? InboundId { get; set; }
        //public virtual Inbound Inbound { get; set; }

        //public List<GatewayTransmittalPackageNumber> GatewayTransmittalPackageNumbers { get; set; }
    }
}
