namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GatewayTransmittal")]
    public partial class GatewayTransmittal : BaseEntity
    {
        public Guid GatewayTransmittalID { get; set; }

        public Guid UserID { get; set; }

        public Guid DestinationID { get; set; }

        [Required]
        [StringLength(20)]
        public string Gateway { get; set; }

        public Guid CommodityTypeID { get; set; }

        [Required]
        [StringLength(20)]
        public string MasterAirwayBillNo { get; set; }

        [Required]
        [StringLength(20)]
        public string AirwayBillNo { get; set; }

        [Required]
        [StringLength(50)]
        public string Cargo { get; set; }

        [Required]
        [StringLength(20)]
        public string FlightNumber { get; set; }

        public Guid BatchID { get; set; }

        [Required]
        [StringLength(20)]
        public string PlateNo { get; set; }

        [Required]
        [StringLength(20)]
        public string Driver { get; set; }

        public DateTime TransmittalDate { get; set; }

        public bool Uploaded { get; set; }

        [ForeignKey("DestinationID")]
        public virtual BranchCorpOffice BranchCorpOffice { get; set; }

        [ForeignKey("CommodityTypeID")]
        public virtual CommodityType CommodityType { get; set; }

        [ForeignKey("BatchID")]
        public virtual Batch Batch { get; set; }

        [ForeignKey("UserID")]
        public virtual User TransmittalBy { get; set; }
    }
}
