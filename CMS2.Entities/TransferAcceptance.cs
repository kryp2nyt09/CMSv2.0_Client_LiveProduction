using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class TransferAcceptance :BaseEntity
    {
        [Key]
        public Guid TransferAcceptanceId { get; set; }
        public DateTime AcceptanceDate { get; set; }
        public Guid DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual Employee Driver { get; set; }
        public Guid ScannedById { get; set; }
        [ForeignKey("ScannedById")]
        public virtual Employee ScannedBy { get; set; }
        public string TransferType { get; set; }
        public Guid TransferFromId { get; set; }
        public virtual dynamic TransferFrom { get; set; }
        public Guid TransferToId { get; set; }
        public virtual dynamic TransferTo { get; set; }
        public List<PackageNumberAcceptance> PackageNumberAcceptances { get; set; }
        public string Remarks { get; set; }
    }
}
