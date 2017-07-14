using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class AccountType:BaseEntity
    {
        [Key]
        public Guid AccountTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Account Type")]
        public string AccountTypeName { get; set; }
        public int ListOrder { get; set; }
    }
}
