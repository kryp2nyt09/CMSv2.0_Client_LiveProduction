namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Distribution")]
    public partial class Distribution : BaseEntity
    {
        public Guid DistributionID { get; set; }

        public Guid UserID { get; set; }

        public Guid AreaID { get; set; }

        [ForeignKey("AreaID")]
        public virtual RevenueUnit Area { get; set; }
            
        public Guid BatchID { get; set; }

        [ForeignKey("BatchID")]
        public virtual Batch Batch { get; set; }

        [Required]
        [StringLength(50)]
        public string Driver { get; set; }

        [Required]
        [StringLength(50)]
        public string Checker { get; set; }

        [Required]
        [StringLength(50)]
        public string PlateNo { get; set; }

        public Guid ShipmentId { get; set; }

        public Guid ConsigneeID { get; set; }

        public Guid PaymentModeID { get; set; }

        public Guid ServiceModeID { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(20)]
        public string AirwayBillNo { get; set; }

        [Required]
        [StringLength(20)]
        public string Cargo { get; set; }

        public bool Uploaded { get; set; }

        [ForeignKey("ShipmentId")]
        public virtual Shipment Shipment { get; set; }
        [ForeignKey("ConsigneeID")]
        public virtual Client Consignee { get; set; }
        [ForeignKey("PaymentModeID")]
        public virtual PaymentMode PaymentMode { get; set; }
        [ForeignKey("ServiceModeID")]
        public virtual ServiceMode ServiceMode { get; set; }
        [ForeignKey("UserID")]
        public virtual User DistibutedBy { get; set; }
    }
}
