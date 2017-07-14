using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class GatewayTransmittalPackageNumber:BaseEntity
    {
        [Key]
        public Guid GatewayTransmittalPackageNumberId { get; set; }
        public Guid GatewayTransmittalId { get; set; }
        public virtual GatewayTransmittal GatewayTransmittal { get; set; }
        public Guid PackageNumberId { get; set; }
        public virtual PackageNumber PackageNumber { get; set; }
    }
}
