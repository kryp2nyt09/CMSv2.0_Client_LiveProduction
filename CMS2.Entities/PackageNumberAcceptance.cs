using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CMS2.Entities;

namespace CMS2.Entities
{
    public class PackageNumberAcceptance:BaseEntity
    {
        [Key]
        public Guid PackageNumberAcceptanceId { get; set; }
        public Guid TransferAcceptanceId { get; set; }
        [ForeignKey("TransferAcceptanceId")]
        public virtual TransferAcceptance TransferAcceptance { get; set; }
        public Guid PackageNumberId { get; set; }
        [ForeignKey("PackageNumberId")]
        public virtual PackageNumber PackageNumber { get; set; }
    }
}
