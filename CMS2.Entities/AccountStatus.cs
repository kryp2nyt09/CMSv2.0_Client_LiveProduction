using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class AccountStatus:BaseEntity
    {
        [Key]
        public Guid AccountStatusId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Account Status")]
        public string AccountStatusName { get; set; }
        public int ListOrder { get; set; }
    }
}
