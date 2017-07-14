namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GatewayOutbound")]
    public partial class GatewayOutbound : BaseEntity
    {
        public Guid GatewayOutboundID { get; set; }

        public Guid BranchCorpOfficeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Gateway { get; set; }

        public Guid ShipModeID { get; set; }

        public Guid UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string MasterAirwayBill { get; set; }

        [Required]
        [StringLength(20)]
        public string Cargo { get; set; }

        [Required]
        [StringLength(20)]
        public string Driver { get; set; }

        [Required]
        [StringLength(20)]
        public string PlateNo { get; set; }

        public Guid BatchID { get; set; }

        public Guid RemarkID { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public bool Uploaded { get; set; }

        [ForeignKey("BatchID")]
        public virtual Batch Batch { get; set; }

        [ForeignKey("BranchCorpOfficeID")]
        public virtual BranchCorpOffice BranchCorpOffice { get; set; }
         [ForeignKey("UserID")]
        public virtual User OutboundBy { get; set; }
        //public virtual PackageNumber PackageNumber { get; set; }

    }
}
