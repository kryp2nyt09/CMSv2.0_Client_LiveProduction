using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class PackageNumberTransfer : BaseEntity
    {
        [Key]
        
        public Guid PackageNumberTransferId { get; set; }
        public Guid PackageTransferId { get; set; }
        [ForeignKey("PackageTransferId")]
        public virtual PackageTransfer PackageTransfer { get; set; }
        public Guid PackageNumberId { get; set; }
        [ForeignKey("PackageNumberId")]
        public virtual PackageNumber PackageNumber { get; set; }
    }
}
