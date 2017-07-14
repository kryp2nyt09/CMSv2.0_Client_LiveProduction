using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Company : BaseEntity
    {
        public Company()
        {
            Discount = 0;
        }

        [Key]
        [DisplayName("Company")]
        public Guid CompanyId { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Account No")]
        public string AccountNo { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(15)]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [MaxLength(15)]
        [DisplayName("Fax")]
        public string Fax { get; set; }
        [MaxLength(80)]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [MaxLength(300)]
        [DisplayName("Address")]
        public string Address1 { get; set; }
        [MaxLength(300)]
        [DisplayName("Address2")]
        public string Address2 { get; set; }
        [DisplayName("City")]
        public Guid CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [MaxLength(5)]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [MaxLength(40)]
        [DisplayName("Website")]
        public string Website { get; set; }
        [MaxLength(80)]
        [DisplayName("President")]
        public string President { get; set; }
        [MaxLength(15)]
        [DisplayName("TIN No")]
        public string TinNo { get; set; }
        [DisplayName("Mother Company")]
        public Guid? MotherCompanyId { get; set; }
        [DisplayName("Mother Company")]
        [ForeignKey("MotherCompanyId")]
        public virtual Company MotherCompany { get; set; }
        

        // Company Contact Info
        [MaxLength(50)]
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        [MaxLength(30)]
        [DisplayName("Position")]
        public string ContactPosition { get; set; }
        [MaxLength(30)]
        [DisplayName("Department")]
        public string ContactDepartment { get; set; }
        [MaxLength(15)]
        [DisplayName("Contact No")]
        public string ContactTelNo { get; set; }
        [MaxLength(15)]
        [DisplayName("Mobile No")]
        public string ContactMobile { get; set; }
        [MaxLength(30)]
        [DisplayName("Email")]
        public string ContactEmail { get; set; }
        [MaxLength(15)]
        [DisplayName("Fax")]
        public string ContactFax { get; set; }


        // Billling Info Info
        [MaxLength(300)]
        [DisplayName("Address 1")]
        public string BillingAddress1 { get; set; }
        [MaxLength(300)]
        [DisplayName("Address 2")]
        public string BillingAddress2 { get; set; }
        [DisplayName("City")]
        public Guid BillingCityId { get; set; }
        [DisplayName("City")]
        [ForeignKey("BillingCityId")]
        public virtual City BillingCity { get; set; }
        [MaxLength(5)]
        [DisplayName("Zip Code")]
        public string BillingZipCode { get; set; }
        [MaxLength(50)]
        [DisplayName("Contact Person")]
        public string BillingContactPerson { get; set; }
        [MaxLength(30)]
        [DisplayName("Position")]
        public string BillingContactPosition { get; set; }
        [MaxLength(300)]
        [DisplayName("Deaprtment")]
        public string BillingContactDepartment { get; set; }
        [MaxLength(15)]
        [DisplayName("Contact No")]
        public string BillingContactTelNo { get; set; }
        [MaxLength(15)]
        [DisplayName("Mobile No")]
        public string BillingContactMobile { get; set; }
        [MaxLength(30)]
        [DisplayName("Email")]
        public string BillingContactEmail { get; set; }
        [MaxLength(15)]
        [DisplayName("Fax")]
        public string BillingContactFax { get; set; }
        

        // Account Info
        [DisplayName("Account Type")]
        public Guid AccountTypeId { get; set; }
        [DisplayName("Account Type")]
        public virtual AccountType AccountType { get; set; }
        [DisplayName("Industry")]
        public Guid? IndustryId { get; set; }
         [DisplayName("Industry")]
        [ForeignKey("IndustryId")]
        public virtual Industry Industry { get; set; }
        [DisplayName("Business Type")]
        public Guid BusinessTypeId { get; set; }
        [DisplayName("Business Type")]
        [ForeignKey("BusinessTypeId")]
        public virtual BusinessType BusinessType { get; set; }
        [DisplayName("Organization Type")]
        public Guid OrganizationTypeId { get; set; }
        [DisplayName("Organization Type")]
        public virtual OrganizationType OrganizationType { get; set; }
        [DisplayName("Account Status")]
        public Guid AccountStatusId { get; set; }
        [DisplayName("Account Status")]
        public virtual AccountStatus AccountStatus { get; set; }
        [DisplayName("Date Approved")]
        [DataType(DataType.Date)]
        public DateTime ApprovedDate { get; set; }
        [DisplayName("Approved By")]
        public Guid ApprovedById { get; set; }
        [DisplayName("Approved By")]
        [ForeignKey("ApprovedById")]
        public virtual Employee ApprovedBy { get; set; }
        [DisplayName("Payment term")]
        public Guid? PaymentTermId { get; set; }
        [DisplayName("Payment term")]
        public virtual PaymentTerm PaymentTerm { get; set; }
        [DisplayName("Payment Mode")]
        public Guid? PaymentModeId { get; set; }
        [DisplayName("Payment Mode")]
        public virtual PaymentMode PaymentMode { get; set; }
        [DisplayName("Billing Period")]
        public Guid BillingPeriodId { get; set; }
        [DisplayName("Billing Period")]
        [ForeignKey("BillingPeriodId")]
        public virtual BillingPeriod BillingPeriod { get; set; }
        [DefaultValue(0)]
        public decimal Discount { get; set; }
        [DefaultValue(0)]
        [DisplayName("Has AWB Fee")]
        public bool HasAwbFee { get; set; }
        [DefaultValue(0)]
        [DisplayName("Is Vatable")]
        public bool IsVatable { get; set; }
        [DefaultValue(0)]
        [DisplayName("Has Valuation Charge")]
        public bool HasValuationCharge { get; set; }
        [DefaultValue(0)]
        [DisplayName("Has Insurance")]
        public bool HasInsurance { get; set; }
        [DefaultValue(0)]
        [DisplayName("Has Charge Invoice")]
        public bool HasChargeInvoice { get; set; }
        [DefaultValue(0)]
        [DisplayName("Credit Limit")]
        public decimal CreditLimit { get; set; }
        public Guid? AreaId { get; set; }
        [ForeignKey("AreaId")]
        public virtual RevenueUnit Area { get; set; }
        public bool ApplyEvm { get; set; }
        public bool ApplyWeight { get; set; }
        public bool HasFreightCollectCharge { get; set; }
        public bool HasFuelCharge { get; set; }
        public bool HasDeliveryFee { get; set; }
        public bool HasPerishableFee { get; set; }
        public bool HasDangerousFee { get; set; }


        // Approving Authority
        public virtual List<ApprovingAuthority> ApprovingAuthorities { get; set; } 
        
        public string Remarks{get;set;}

        // Authorized Representatives
        public virtual List<Client> Clients { get; set; }


        [DisplayName("Date Approved")]
        [NotMapped]
        public string ApprovedDateString { get { return ApprovedDate.ToString("MMM dd, yyyy"); } }
        [DisplayName("Credit Limit")]
        [NotMapped]
        public string CreditLimitString { get { return CreditLimit.ToString("N"); } }
         [DisplayName("Discount")]
        [NotMapped]
        public string DiscountString { get { return Discount.ToString("N"); } }
    }
}
