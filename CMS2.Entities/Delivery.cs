using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Delivery : BaseEntity
    {
        [Key]
        public Guid DeliveryId { get; set; }
        [Required]
        public DateTime DateDelivered { get; set; }
        public Guid DeliveredById { get; set; }
        [ForeignKey("DeliveredById")]
        public virtual Employee DeliveredBy { get; set; }
        public Guid DeliveryStatusId { get; set; }
        public virtual DeliveryStatus DeliveryStatus { get; set; }
        public Guid? DeliveryRemarkId { get; set; }

        [ForeignKey("DeliveryRemarkId")]
        public virtual DeliveryRemark DeliveryRemark { get; set; }
        public Guid? ShipmentId { get; set; }
      
        public virtual Shipment Shipment { get; set; }
        [MaxLength(250)]
        public string Note { get; set; }
        public virtual List<DeliveredPackage> DeliveredPackages { get; set; }
        public virtual List<DeliveryReceipt> DeliveryReceipts { get; set; } 
        
    }
}
