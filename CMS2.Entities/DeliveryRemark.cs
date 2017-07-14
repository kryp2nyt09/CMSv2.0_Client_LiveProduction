using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace CMS2.Entities
{
    public class DeliveryRemark:BaseEntity
    {
        [Key]
        public Guid DeliveryRemarkId { get; set; }
        [Required]
        [MaxLength(80)]
        [DisplayName("Delivery Remark")]
        public string DeliveryRemarkName { get; set; }
        public int ListOrder { get; set; }
    }
}
