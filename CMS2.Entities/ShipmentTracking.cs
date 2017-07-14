using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    /// <summary>
    /// Tracking Information of a particular shipment
    /// </summary>
    public class ShipmentTracking : BaseEntity
    {
        [Key]
        public Guid ShipmentTrackingId { get; set; }
         [Required]
        public Guid ShipmentId { get; set; }
        public virtual Shipment Shipment{get; set; }
        [Required]
        public Guid ShipmentStatusId { get; set; }
        public virtual ShipmentStatus ShipmentStatus { get; set; }
        [Required]
        public DateTime TrackDate { get; set; }
        [Required]
        public Guid TrackedById { get; set; }
        [ForeignKey("TrackedById")]
        public virtual Employee TrackedBy { get; set; }
        public string Remarks { get; set; }
    }
}
