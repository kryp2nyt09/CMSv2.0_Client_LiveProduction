using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class GoodsDescription:BaseEntity
    {
        [Key]
        public Guid GoodsDescriptionId { get; set; }
        [Required]
        [MaxLength(80)]
        [DisplayName("Goods Description")]
        public string GoodsDescriptionName { get; set; }
    }
}
