namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GatewayInbound")]
    public partial class GatewayInbound : BaseEntity
    {
        [Key]
        [Column("GatewayInbound")]
        public Guid GatewayInbound1 { get; set; }

        public Guid UserID { get; set; }

        public Guid OriginBcoID { get; set; }

        [Required]
        [StringLength(50)]
        public string Gateway { get; set; }

        [Required]
        [StringLength(20)]
        public string FlightNumber { get; set; }

        public Guid ShipmodeID { get; set; }

        public Guid CommodityID { get; set; }

        [Required]
        [StringLength(20)]
        public string MasterAirwayBill { get; set; }

        [Required]
        [StringLength(20)]
        public string Cargo { get; set; }

        public DateTime InboundDate { get; set; }

        public Guid RemarkID { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public bool Uploaded { get; set; }

        [ForeignKey("OriginBcoID")]
        public virtual BranchCorpOffice OriginBco { get; set; }

        [ForeignKey("CommodityID")]
        public virtual CommodityType CommodityType { get; set; }
        [ForeignKey("UserID")]
        public virtual User InboundBy { get; set; }
    }
}
