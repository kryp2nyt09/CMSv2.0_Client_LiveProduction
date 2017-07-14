using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class BookingRemark:BaseEntity
    {
        [Key]
        public Guid BookingRemarkId { get; set; }
        [Required]
        [MaxLength(80)]
        [DisplayName("Booking Remark")]
        public string BookingRemarkName { get; set; }
        public int ListOrder { get; set; }
    }
}
