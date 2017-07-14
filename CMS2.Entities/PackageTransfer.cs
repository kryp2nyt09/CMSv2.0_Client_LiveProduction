using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class PackageTransfer : BaseEntity
    {
        [Key]
        public Guid PackageTransferId { get; set; }
        public DateTime TransferDate { get; set; }
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
        public List<PackageNumberTransfer> PackageNumberTransfers { get; set; }
        public string Remarks { get; set; }
    }
}
