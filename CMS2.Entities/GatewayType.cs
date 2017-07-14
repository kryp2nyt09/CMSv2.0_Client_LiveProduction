using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace CMS2.Entities
{
    public class GatewayType:BaseEntity 
    {
        [Key]
        public Guid GatewayTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Gateway Type")]
        public string GatewayTypeName { get; set; }
    }
}
