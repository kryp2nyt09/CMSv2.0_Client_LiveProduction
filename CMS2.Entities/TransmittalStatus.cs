using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class TransmittalStatus:BaseEntity
    {
        [Key]
        public Guid TransmittalStatusId { get; set; }
        [Required]
        [MaxLength(5)]
        public string TransmittalStatusCode { get; set; }
        [Required]
        [MaxLength(30)]
        public string TransmittalStatusName { get; set; }
    }
}
