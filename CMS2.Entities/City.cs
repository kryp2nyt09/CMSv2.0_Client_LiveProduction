using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class City : BaseEntity
    {
        [Key]
        public Guid CityId { get; set; }
        [Required]
       [StringLength(3)]
        [DisplayName("City Code")]
        public string CityCode { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("City Name")]
        public string CityName { get; set; }
        public Guid BranchCorpOfficeId { get; set; }
       
        [MaxLength(250)]
        [DisplayName("Street")]
        public string StreetAddress { get; set; }
        [MaxLength(15)]
        [DisplayName("Contact No1")]
        public string ContactNo1 { get; set; }
        [MaxLength(15)]
        [DisplayName("Contact No2")]
        public string ContactNo2 { get; set; }
        [MaxLength(15)]
        public string Fax { get; set; }
        [MaxLength(5)]
        public string ZipCode { get; set; }
        [ForeignKey("BranchCorpOfficeId")]
        public virtual BranchCorpOffice BranchCorpOffice{ get; set; }
        public List<RevenueUnit> RevenueUnits { get; set; }
    }
}
