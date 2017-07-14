using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class Gateway :BaseEntity
    {
        [Key]
        public Guid GatewayId { get; set; }
        [MaxLength(10)]
        [DisplayName("Gateway Code")]
        public string GatewayCode { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Gateway")]
        public string GatewayName { get; set; }
        [DisplayName("Gateway Type")]
        public Guid GatewayTypeId { get; set; }
        public virtual GatewayType GatewayType { get; set; }
    }
}
