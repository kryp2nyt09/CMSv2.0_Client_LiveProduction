using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    /// <summary>
    /// Client Information
    /// Information for regular/walkin customer
    /// Information for authorized representative of a Company
    /// </summary>
    public class Client : BaseEntity
    {
        [Key]
        [DisplayName("Client")]
        public Guid ClientId { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Account No")]
        public string AccountNo { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Fullname")]
        [NotMapped]
        public string FullName { get { return LastName + ", " + FirstName; } }
        [Required]
        [MaxLength(15)]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [MaxLength(15)]
        [DisplayName("Mobile No")]
        public string Mobile { get; set; }
        [MaxLength(50)]
        [DisplayName("Fax")]
        public string Fax { get; set; }
        [MaxLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [MaxLength(250)]
        [DisplayName("House/Bldg No")]
        public string Address1 { get; set; }
        [MaxLength(250)]
        [DisplayName("House/Bldg Name")]
        public string Address2 { get; set; }
        [MaxLength(250)]
        [DisplayName("Street")]
        public string Street { get; set; }
        [MaxLength(150)]
        [DisplayName("Barangay")]
        public string Barangay { get; set; }
        [DisplayName("CIty")]
        public Guid CityId { get; set; }
        [DisplayName("CIty")]
        public virtual City City { get; set; }
        [MaxLength(5)]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        //  optional information for regular customer
        [MaxLength(30)]
        [DisplayName("Zip Code")]
        public string Title { get; set; }
        [MaxLength(50)]
        [DisplayName("Department")]
        public string Department { get; set; }

        [MaxLength(80)]
        [DisplayName("Company")]
        public string CompanyName { get; set; }

        [MaxLength(300)]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        public Guid? AreaId { get; set; }
        [ForeignKey("AreaId")]
        public virtual RevenueUnit Area { get; set; }

        // Company Associated with
        [DisplayName("Company")]
        [DefaultValue(null)]
        public Guid? CompanyId { get; set; }
        [DisplayName("Company")]
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

    }
}
