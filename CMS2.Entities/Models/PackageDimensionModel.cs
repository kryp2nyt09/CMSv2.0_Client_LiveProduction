using System;

namespace CMS2.Entities.Models
{
    public class PackageDimensionModel
    {
        public Guid PackageDimensionId { get; set; }
        public Guid ShipmentId { get; set; }
        public Guid CommodityTypeId { get; set; }
        public int Index { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Evm { get; set; }
        public Guid? CratingId { get; set; }
        public string CratingName { get; set; }
        public decimal CratingFee { get; set; }
        public bool ForDraining { get; set; }
        public Guid? DrainingId { get; set; }
        public decimal DrainingFee { get; set; }
        public bool ForPackaging { get; set; }
        public Guid? PackagingId { get; set; }
        public decimal PackagingFee { get; set; }
        public decimal WeighCharge { get; set; }

        public string LengthString { get { return Length.ToString("N"); } }
        public string WidthString { get { return Width.ToString("N"); } }
        public string HeightString { get { return Height.ToString("N"); } }
        public string EvmString { get { return Evm.ToString("N"); } }
        public string CratingFeeString { get { return CratingFee.ToString("N"); } }
        public string PackagingFeeString { get { return PackagingFee.ToString("N"); } }
        public string DrainingFeeString { get { return DrainingFee.ToString("N"); } }
        public string WeightChargeString { get { return WeighCharge.ToString("N"); } }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int RecordStatus { get; set; }
    }
}
