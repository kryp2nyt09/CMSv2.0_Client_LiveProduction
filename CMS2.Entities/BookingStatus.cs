using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class BookingStatus:BaseEntity
    {
        [Key]
        public Guid BookingStatusId { get; set; }
        [Required]
        [MaxLength(80)]
        [DisplayName("Booking Status")]
        public string BookingStatusName { get; set; }
        public int ListOrder { get; set; }
    }
}
