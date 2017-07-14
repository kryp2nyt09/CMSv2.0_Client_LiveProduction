using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    /// <summary>
    /// Defines the Status of a Shipment Transaction for Tracking
    /// </summary>
    public class ShipmentStatus : BaseEntity
    {
        [Key]
        public Guid ShippingStatusId { get; set; }
        [Required]
        [MaxLength(30)]
        public string ShippingStatusName { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
