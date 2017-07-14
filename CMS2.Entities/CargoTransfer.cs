namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CargoTransfer")]
    public partial class CargoTransfer  : BaseEntity
    {
        public Guid CargoTransferID { get; set; }

        public Guid UserID { get; set; }

        public Guid BranchCorpOfficeID { get; set; }

        public Guid RevenueUnitTypeID { get; set; }

        public Guid RevenueUnitID { get; set; }

        [Required]
        [StringLength(20)]
        public string Driver { get; set; }

        [Required]
        [StringLength(20)]
        public string Checker { get; set; }

        [Required]
        [StringLength(20)]
        public string Cargo { get; set; }

        [Required]
        [StringLength(50)]
        public string PlateNo { get; set; }

        public Guid BatchID { get; set; }

        public Guid ReasonID { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public bool Uploaded { get; set; }               

        [ForeignKey("BranchCorpOfficeID")]
        public virtual BranchCorpOffice DestinationBco { get; set; }

        [ForeignKey("RevenueUnitID")]
        public virtual RevenueUnit DestinationArea { get; set; }

        [ForeignKey("RevenueUnitTypeID")]
        public virtual RevenueUnitType RevenueUnitType { get; set; }

        [ForeignKey("ReasonID")]
        public virtual Reason Reason { get; set; }

        [ForeignKey("BatchID")]
        public virtual Batch Batch { get; set; }

        [ForeignKey("UserID")]
        public virtual User TransferBy { get; set; }

    }
}
