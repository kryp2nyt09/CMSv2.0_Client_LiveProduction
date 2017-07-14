using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class Industry:BaseEntity
    {
        [Key]
        public Guid IndustryId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Industry")]
        public string IndustryName { get; set; }
        public int ListOrder { get; set; }
    }
}
