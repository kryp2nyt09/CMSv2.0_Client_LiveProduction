using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Entities
{
    public class MenuAccess : BaseEntity
    {
        public Guid MenuAccessId { get; set; }
        public Guid MenuId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }
    }
}
