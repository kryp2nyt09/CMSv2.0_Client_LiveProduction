using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using CMS2.Common.Enums;

namespace CMS2.Entities.Models
{
    public class ShipmentModel
    {
        public int No { get; set; }
        public Guid ShipmentId { get; set; }
        [DisplayName("AWB No")]
        public string AirwayBillNo { get; set; }
        [DisplayName("AWB No Barcode")]
        public byte[] AirwayBillNoBarcode { get; set; }
        public Guid OriginCityId { get; set; }
        public virtual City OriginCity { get; set; }
        public string OriginAddress { get; set; }
        public string OriginBarangay { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationBarangay { get; set; }
        public Guid DestinationCityId { get; set; }
        public City DestinationCity { get; set; }
        [DisplayName("Consignee")]
        public Guid ConsigneeId { get; set; }
        [DisplayName("Consignee")]
        public Client Consignee { get; set; }
        public string ConsigneeCompanyAccountNo { get; set; }
        [DisplayName("Shipper")]
        public Guid ShipperId { get; set; }

        [DisplayName("Shipper")]
        public Client Shipper { get; set; }
        public string ShipperCompanyAccountNo { get; set; }
        [DisplayName("Accepted By")]
        public Guid AcceptedById { get; set; }
        [DisplayName("Accepted By")]
        public Employee AcceptedBy { get; set; }
        // this is also the Shipment/Transaction Date
        [DisplayName("Date Accepted")]
        public DateTime DateAccepted { get; set; }
        [DisplayName("Area Accepted")]
        public Guid AcceptedAreaId { get; set; }
        [DisplayName("Area Accepted")]
        public RevenueUnit AcceptedArea { get; set; }
        [DisplayName("Commodity")]
        public Guid CommodityTypeId { get; set; }
        [DisplayName("Commodity Type")]
        public CommodityType CommodityType { get; set; }
        [DisplayName("Commodity")]
        public Guid CommodityId { get; set; }
        public Commodity Commodity { get; set; }
        // integer value still need to be converted into percent
        [DisplayName("RFA")]
        public decimal Discount { get; set; }//Discount or RFA in percentage
        public decimal DiscountAmount { get; set; } 
        public Guid ServiceModeId { get; set; }
        public virtual ServiceMode ServiceMode { get; set; }
        public Guid PaymentModeId { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public Guid PaymentTermId { get; set; }
        public virtual PaymentTerm PaymentTerm { get; set; }
        public string Remarks { get; set; }
        [DisplayName("Declared Value")]
        public decimal DeclaredValue { get; set; }
        public decimal FreightCharge { get; set; }
        public Guid? StatementOfAccountId { get; set; }
        public StatementOfAccountModel StatementOfAccount { get; set; }

        [DisplayName("AWB Fee")]
        public Guid? AwbFeeId { get; set; }
        public ShipmentBasicFee AwbFee { get; set; }
        [DisplayName("FCC Fee")]
        public Guid? FreightCollectChargeId { get; set; }
        [DisplayName("FCC Fee")]
        public ShipmentBasicFee FreightCollectCharge { get; set; }
        [DisplayName("Fuel Surcharge")]
        public Guid? FuelSurchargeId { get; set; }
        [ForeignKey("FuelSurchargeId")]
        public FuelSurcharge FuelSurcharge { get; set; }
        [DisplayName("Perac Fee")]
        public Guid? PeracFeeId { get; set; }
        public ShipmentBasicFee PeracFee { get; set; }
        public Guid? EvatId { get; set; }
        public ShipmentBasicFee EVat { get; set; }
        [DisplayName("Insurance")]
        public Guid? InsuranceId { get; set; }
        public ShipmentBasicFee Insurance { get; set; }
        public Guid? ValuationFactorId { get; set; }
        public ShipmentBasicFee ValuationFactor { get; set; }
        public DateTime? DateOfFullPayment { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        [DisplayName("Record Status")]
        public int RecordStatus { get; set; }
        public List<Payment> Payments { get; set; }
        public List<ShipmentAdjustment> Adjustments { get; set; }
        public virtual List<Delivery> Deliveries { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public List<PackageDimensionModel> PackageDimensions { get; set; }
        public List<PackageNumber> PackageNumbers { get; set; }
        [DisplayName("Booking")]
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }
        public Guid ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public Guid ShipModeId { get; set; }
        public ShipMode ShipMode { get; set; }
        public Guid? TransShipmentLegId { get; set; }
        public TransShipmentLeg TransShipmentLeg { get; set; }
        public Guid GoodsDescriptionId { get; set; }
        public GoodsDescription GoodsDescription { get; set; }
        public decimal HandlingFee { get; set; }
        public decimal QuarantineFee { get; set; }
        public decimal CratingFee { get; set; }
        public decimal PackagingFee {get;set;}
        public decimal DrainingFee { get; set; }
        public string Notes { get; set; }
        public Guid? DeliveryFeeId { get; set; }
        public ShipmentBasicFee DeliveryFee { get; set; }
        public Guid? DangerousFeeId { get; set; }
        public ShipmentBasicFee DangerousFee { get; set; }
        public bool IsVatable { get; set; }
        
        // members below are not db fields or part of of the entity
        [DisplayName("Date Accepted")]
        public string DateAcceptedString
        {
            get { return DateAccepted.ToString("MMM dd, yyyy"); }
        }
        public RecordStatus Record_Status { get; set; }
        [DisplayName("Record Status")]
        public string RecordStatusString
        {
            get
            {
                RecordStatus recordStatus = (RecordStatus)this.RecordStatus;
                return recordStatus.ToString();
            }
        }


        // Amount Charged based on the Actual Weight and Commodity
        // Excluding Other Charges
        [DisplayName("Chargeable Weight")]
        public decimal ChargeableWeight { get; set; }
        [DisplayName("Chargeable Weight")]
        public string ChargeableWeightString { get { return ChargeableWeight.ToString("N"); } }
        [DisplayName("Weight Charge")]
        public decimal WeightCharge { get; set; }
        [DisplayName("Weight Charge")]
        public string WeightChargeString { get { return WeightCharge.ToString("N"); } }
        [DisplayName("Discount")]
        public string DiscountAmountString { get { return DiscountAmount.ToString("N"); } }
        [DisplayName("Declared Value")]
        public string DeclaredValueString { get { return DeclaredValue.ToString("N"); } }
        [DisplayName("Valuation Amount")]
        public decimal ValuationAmount { get; set; }
        [DisplayName("Valuation Amount")]
        public string ValuationAmountString { get { return ValuationAmount.ToString("N"); } }
        [DisplayName("Last Payment")]
        public DateTime? LastPaymentDate { get; set; }
        [DisplayName("Last Payment")]
        public string LastPaymentDateString { get { return LastPaymentDate == null ? "" : LastPaymentDate.ToString(); } }
        [DisplayName("Date Full Payment")]
        public string DateOfFullPaymentString { get { return DateOfFullPayment.GetValueOrDefault().ToString("MMM dd, yyyy"); } }
        [DisplayName("Insurance Amount")]
        public decimal InsuranceAmount { get; set; }
        [DisplayName("Insurance Amount")]
        public string InsuranceAmountString { get { return InsuranceAmount.ToString("N"); } }
        [DisplayName("Fuel Surcharge")]
        public decimal FuelSurchargeAmount { get; set; }
        [DisplayName("Fuel Surcharge")]
        public string FuelSurchargeAmountstring { get { return FuelSurchargeAmount.ToString("N"); } }
        [DisplayName("Handling Fee")]
        public string HandlingFeeString { get { return HandlingFee.ToString("N"); } }
        [DisplayName("Quarantine Fee")]
        public string QuanrantineFeeString { get { return QuarantineFee.ToString("N"); } }
        [DisplayName("Crating Fee")]
        public string CratingFeeString { get { return CratingFee.ToString("N"); } }
        [DisplayName("Packaging Fee")]
        public string PackagingFeeString { get { return PackagingFee.ToString("N"); } }
        [DisplayName("Draining Fee")]
        public string DrainingFeeString { get { return DrainingFee.ToString("N"); } }

        [DisplayName("Sub-Total")]
        [Description("Amount charged excluding vat")]
        public decimal ShipmentSubTotal { get; set; }
        [DisplayName("Vat Amount")]
        public decimal ShipmentVatAmount { get; set; }
        [DisplayName("Total Amount")]
        [Description("Total amount charged including vat")]
        public decimal ShipmentTotal { get; set; }
        [DisplayName("Current Payments")]
        [Description("Payments made in current SOA")]
        public decimal CurrentPayments { get; set; }
        [DisplayName("Previous SOA Payments")]
        [Description("Payments made in previous SOA")]
        public decimal PreviousPayments { get; set; }
        [DisplayName("Total Payments")]
        [Description("Payments made for this Shipment")]
        public decimal TotalPayments { get; set; }
        [DisplayName("Current Adjustment")]
        public decimal Adjustment { get; set; }
        [DisplayName("Adjustment Reason")]
        public string AdjustmentReason { get; set; }
        [DisplayName("Previous SOA Adjustments")]
        [Description("Adjustment made in previous SOA")]
        public decimal PreviousAdjustments { get; set; }
        [DisplayName("Total Adjustments")]
        [Description("Total Adjustment made for this Shipment")]
        public decimal TotalAdjustments { get; set; }
        [DisplayName("Current Balance")]
        [Description("Balance after Payment and Adjustments")]
        public decimal CurrentBalance { get; set; }
        [DisplayName("Surcharge")]
        public decimal Surcharge { get; set; } // 3% charge on Balance after DueDate
        [DisplayName("Previous Balance")]
        public decimal PreviousBalance { get; set; }
        [DisplayName("Previous Amount Due")]
        public decimal PreviousAmountDue { get; set; }
        public decimal SoaPreviousAmountDue { get; set; }

        [DisplayName("Sub-Total")]
        public string ShipmentSubTotalString { get { return ShipmentSubTotal.ToString("N"); } }
        [DisplayName("Vat Amount")]
        public string ShipmentVatAmountString { get { return ShipmentVatAmount.ToString("N"); } }
        [DisplayName("Total Amount")]
        public string ShipmentTotalString { get { return ShipmentTotal.ToString("N"); } }
        [DisplayName("Current Payments")]
        public string CurrentPaymentsString { get { return CurrentPayments.ToString("N"); } }
        [DisplayName("Previous SOA Payments")]
        public string PreviousPaymentsString { get { return PreviousPayments.ToString("N"); } }
        [DisplayName("Total Payments")]
        public string TotalPaymentsString { get { return TotalPayments.ToString("N"); } }
        [DisplayName("Adjustment")]
        public string AdjustmentString { get { return Adjustment.ToString("N"); } }
        [DisplayName("Previous SOA Adjustments")]
        public string PreviousAdjustmentsString { get { return PreviousAdjustments.ToString("N"); } }
        public string TotalAdjustmentsString { get { return TotalAdjustments.ToString("N"); } }
        [DisplayName("Current Balance")]
        public string CurrentBalanceString { get { return CurrentBalance.ToString("N"); } }
        [DisplayName("Surcharge")]
        public string SurchargeString { get { return Surcharge.ToString("N"); } }
        [DisplayName("Previous Balance")]
        public string PreviousBalanceString { get { return PreviousBalance.ToString("N"); } }
        public string PreviousAmountDueString { get { return PreviousAmountDue.ToString("N"); } }
        public string SoaPreviousAmountDueString { get { return SoaPreviousAmountDue.ToString("N"); } }
        
    }
}
