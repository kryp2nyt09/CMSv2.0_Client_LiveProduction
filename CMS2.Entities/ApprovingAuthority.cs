using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class ApprovingAuthority: BaseEntity
    {
        [Key]
        public Guid ApprovingAuthorityId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [NotMapped]
        [DisplayName("Fullname")]
        public string FullName { get { return LastName + ", " + FirstName; } }
        [MaxLength(30)]
        public string Title { get; set; }
        [MaxLength(30)]
        public string Position { get; set; }
        [MaxLength(30)]
        public string Department { get; set; }
        [Required]
        [MaxLength(15)]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }
        [MaxLength(15)]
        public string Mobile { get; set; }
        [MaxLength(15)]
        public string Fax { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [DisplayName("Company")]
        public Guid CompanyId { get; set; }
        [DisplayName("Company")]
        public virtual Company Company { get; set; }
    }
}
