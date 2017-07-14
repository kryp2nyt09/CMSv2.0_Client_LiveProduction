using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class DeliveryStatus : BaseEntity
    {
        [Key]
        public Guid DeliveryStatusId { get; set; }
        [Required]
        [MaxLength(80)]
        [DisplayName("Delivery Status")]
        public string DeliveryStatusName { get; set; }
    }
}
