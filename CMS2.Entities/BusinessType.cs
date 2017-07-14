using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class BusinessType : BaseEntity
    {
        [Key]
        public Guid BusinessTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Business Type")]
        public string BusinessTypeName { get; set; }
        public int ListOrder { get; set; }
    }
}
