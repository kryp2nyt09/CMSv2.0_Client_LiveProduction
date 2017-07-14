using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class RateMatrix:BaseEntity
    {
        [Key]
        public Guid RateMatrixId { get; set; }
        [DisplayName("Applicable Rate")]
        public Guid ApplicableRateId { get; set; }
        public ApplicableRate ApplicableRate { get; set; }
        //[DisplayName("Commodity Type")]
        //public Guid CommodityTypeId { get; set; }
        //public CommodityType CommodityType { get; set; }
        //[DisplayName("Service Type")]
        //public Guid? ServiceTypeId { get; set; }
        //public ServiceType ServiceType { get; set; }
        //[DisplayName("Service Mode")]
        //public Guid? ServiceModeId { get; set; }
        //public ServiceMode ServiceMode { get; set; }

        public virtual List<ExpressRate> ExpressRates { get; set; }


        // options
        [DisplayName("Has Fuel Surcharge")]
        [DefaultValue(0)]
        public bool HasFuelCharge { get; set; }
        [DisplayName("Has AWB Fee")]
        [DefaultValue(0)]
        public bool HasAwbFee { get; set; }
        [DisplayName("Has Insurance")]
        [DefaultValue(0)]
        public bool HasInsurance { get; set; }
        [DisplayName("Apply EVM")]
        public bool ApplyEvm { get; set; }
        [DisplayName("Apply Weight")]
        public bool ApplyWeight { get; set; }
        [DisplayName("Is Vatable")]
        public bool IsVatable { get; set; }
        [DisplayName("Has Delivery Fee")]
        public bool HasDeliveryFee { get; set; }
        [DisplayName("Has Perishable Fee")]
        public bool HasPerishableFee { get; set; }
        [DisplayName("Has Dangerous Fee")]
        public bool HasDangerousFee { get; set; }
        [DisplayName("Has Valuation Charge")]
        public bool HasValuationCharge { get; set; }        
        public decimal Amount { get; set; }
    }
}
