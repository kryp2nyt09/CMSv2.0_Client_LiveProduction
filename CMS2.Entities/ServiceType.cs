using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class ServiceType:BaseEntity
    { 
        [Key]
        public Guid ServiceTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Service Type")]
        public string ServiceTypeName { get; set; }
    }
}
