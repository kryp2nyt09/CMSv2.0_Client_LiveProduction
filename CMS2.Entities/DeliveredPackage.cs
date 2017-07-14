using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class DeliveredPackage :BaseEntity
    {
        [Key]
        public Guid DeliveredPackageId { get; set; }
        public Guid DeliveryId { get; set; }
        public virtual Delivery Delivery { get; set; }
        public Guid PackageNumberId { get; set; }
        public virtual PackageNumber PackageNumber { get; set; }
    }
}
