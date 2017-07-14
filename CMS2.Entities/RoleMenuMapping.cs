using System;
using System.ComponentModel.DataAnnotations;

namespace APCargo.Entities
{
    public class RoleMenuMapping : BaseEntity
    {
        [Key]
        public Guid RoleMenuMappingId { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
