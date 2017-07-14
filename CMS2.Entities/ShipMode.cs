using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class ShipMode:BaseEntity
    {     
        [Key]
        public Guid ShipModeId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Ship Mode")]
        public string ShipModeName { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
    }
}
