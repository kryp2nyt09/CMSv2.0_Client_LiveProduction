using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class ServiceMode:BaseEntity
    {
        [Key]
        public Guid ServiceModeId { get; set; }
        [Required]
        [StringLength(2)]
        [DisplayName("Service Mode Code")]
        public string ServiceModeCode { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Service Mode")]
        public string ServiceModeName { get; set; }
        public int ListOrder { get; set; }
        
    }
}
