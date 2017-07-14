using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class OrganizationType:BaseEntity
    {
        [Key]
        public Guid OrganizationTypeId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Organization Type")]
        public string OrganizationTypeName { get;set; }
        public int ListOrder { get; set; }
    }
}
