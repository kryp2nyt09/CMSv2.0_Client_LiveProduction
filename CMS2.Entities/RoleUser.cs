using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class RoleUser
    {
        [Required]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
