using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class ShipmentAdjustment:BaseEntity
    {
        [Key]
        public Guid ShipmentAdjustmentId { get; set; }
        [DisplayName("Shipment")]
        public Guid ShipmentId { get; set; }
        [DisplayName("Shipment")]
        public virtual Shipment Shipment { get; set; }
        [DisplayName("Statement Of Account")]
        public Guid StatementOfAccountId { get; set; }
        [DisplayName("Statement Of Account")]
        public virtual StatementOfAccount StatementOfAccount { get; set; }
        [DisplayName("Date Adjusted")]
        public DateTime DateAdjusted { get; set; }
        [DisplayName("Adjustment Amount")]
        [DefaultValue(0)]
        public decimal AdjustmentAmount { get; set; }
        [DisplayName("Adjustment Reason")]
        public Guid AdjustmentReasonId { get; set; }
        [DisplayName("Adjustment Reason")]
        public virtual AdjustmentReason AdjustmentReason { get; set; }
        [DisplayName("Adjusted By")]
        public Guid AdjustedById { get; set; }
        [ForeignKey("AdjustedById")]
        public virtual Employee AdjustedBy { get; set; }


        [DisplayName("Adjustment Amount")]
        [NotMapped]
        public string AdjustmentAmountString { get { return AdjustmentAmount.ToString("N"); } }
        [DisplayName("Date Adjusted")]
        [NotMapped]
        public string DateAdjustedString { get { return DateAdjusted.ToString("MMM dd, yyyy"); } }
    }
}
