using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    /// <summary>
    /// Shipment Transaction Information
    /// </summary>
    public class Shipment : BaseEntity
    {
        [Key]
        
        public Guid ShipmentId { get; set; }
        [Required]
        [MaxLength(8)]
        //[Index("IX_AirwayBillNo", IsUnique = true)] //CREATE UNIQUE INDEX IX_AirwayBillNo ON Shipment (AirwayBillNo)
        public string AirwayBillNo { get; set; }
        public Guid OriginCityId { get; set; }
        [ForeignKey("OriginCityId")]
        public virtual City OriginCity { get; set; }
        public Guid DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public virtual City DestinationCity { get; set; }
        [Required]
        [MaxLength(700)]
        public string OriginAddress { get; set; }
        [MaxLength(150)]
        public string OriginBarangay { get; set; }
        [Required]
        [MaxLength(700)]
        public string DestinationAddress { get; set; }
        [MaxLength(150)]
        public string DestinationBarangay { get; set; }
        [DisplayName("Consignee")]
        public Guid ConsigneeId { get; set; }
        [ForeignKey("ConsigneeId")]
        public virtual Client Consignee { get; set; }
        public Guid ShipperId { get; set; }
        [ForeignKey("ShipperId")]
        public virtual Client Shipper { get; set; }
        [Required]
        public Guid AcceptedById { get; set; }
        [ForeignKey("AcceptedById")]
        public virtual Employee AcceptedBy { get; set; }
        public DateTime DateAccepted { get; set; }
        [Required]
        public Guid CommodityTypeId { get; set; }
        [ForeignKey("CommodityTypeId")]
        public virtual CommodityType CommodityType { get; set; }
       [Required]
        public Guid CommodityId { get; set; }
        public virtual Commodity Commodity { get; set; }
        [Required]
        public Guid ServiceModeId { get; set; }
        public virtual ServiceMode ServiceMode { get; set; }
        [Required]
        public Guid PaymentModeId { get; set; }
        public virtual PaymentMode PaymentMode { get; set; }
        public Guid PaymentTermId { get; set; }
        public virtual PaymentTerm PaymentTerm { get; set; }
        [DefaultValue(0)]
        public string Remarks { get; set; }
        public decimal DeclaredValue { get; set; }
        public Guid? FuelSurchargeId { get; set; }
        [ForeignKey("FuelSurchargeId")]
        public virtual FuelSurcharge FuelSurcharge { get; set; }
        public Guid? StatementOfAccountId { get; set; }
        [ForeignKey("StatementOfAccountId")]
        public virtual StatementOfAccount StatementOfAccount { get; set; }
        public Guid? AwbFeeId { get; set; }
        [ForeignKey("AwbFeeId")]
        public virtual ShipmentBasicFee AwbFee { get; set; }
        public Guid? FreightCollectChargeId { get; set; }
        [ForeignKey("FreightCollectChargeId")]
        public virtual ShipmentBasicFee FreightCollectCharge { get; set; }
        public Guid? PeracFeeId { get; set; }
        [ForeignKey("PeracFeeId")]
        public virtual ShipmentBasicFee PeracFee { get; set; }
        public Guid? InsuranceId { get; set; }
        [ForeignKey("InsuranceId")]
        public virtual ShipmentBasicFee Insurance { get; set; }
        public Guid? EvatId { get; set; }
        [ForeignKey("EvatId")]
        public virtual ShipmentBasicFee EVat { get; set; }
        public Guid? ValuationFactorId { get; set; }
        [ForeignKey("ValuationFactorId")]
        public virtual ShipmentBasicFee ValuationFactor { get; set; }
        public DateTime? DateOfFullPayment { get; set; }
        public virtual List<PackageNumber> PackageNumbers { get; set; } 
        public virtual List<Payment> Payments { get; set; }
        public virtual List<ShipmentAdjustment> Adjustments { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Quantity { get; set; }
        [Required]
        [DefaultValue(0)]
        public decimal Weight { get; set; }
        public List<PackageDimension> PackageDimensions { get; set; }

        public Guid BookingId { get; set; }
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }
        public virtual List<Delivery> Deliveries { get; set; }
        public Guid ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public virtual ServiceType ServiceType { get; set; }
        public Guid ShipModeId { get; set; }

        public Guid? TransShipmentLegId { get; set; }

        [ForeignKey("TransShipmentLegId")]
        public TransShipmentLeg TransShipmentLeg { get; set; }

        [ForeignKey("ShipModeId")]
        public virtual ShipMode ShipMode { get; set; }
        public Guid GoodsDescriptionId { get; set; }
        [ForeignKey("GoodsDescriptionId")]
        public virtual GoodsDescription GoodsDescription { get; set; }
        [DefaultValue(0)]
        public decimal HandlingFee { get; set; }
        [DefaultValue(0)]
        public decimal QuarantineFee { get; set; }
       [DefaultValue(0)]
        public decimal Discount { get; set; } //Discount or RFA

       public decimal FreightCharge { get; set; }

        public decimal TotalAmount { get; set; }
        [MaxLength(300)]
        public string Notes { get; set; }
        public Guid? DeliveryFeeId { get; set; }
        [ForeignKey("DeliveryFeeId")]
        public virtual ShipmentBasicFee DeliveryFee { get; set; }
        public Guid? DangerousFeeId { get; set; }
        [ForeignKey("DangerousFeeId")]
        public virtual ShipmentBasicFee DangerousFee { get; set; }
        public bool IsVatable { get; set; }
        [NotMapped]
        public int No { get; set; }
    }
}