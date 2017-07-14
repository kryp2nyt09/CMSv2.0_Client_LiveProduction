using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class RevenueUnit:BaseEntity
    {
        [Key]
        public Guid RevenueUnitId { get; set; }
        [Required]
        [MaxLength(50)]
        public string RevenueUnitName { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [DisplayName("Revenue Unit Type")]
        public Guid RevenueUnitTypeId { get; set; }
        [ForeignKey("RevenueUnitTypeId")]
        public virtual RevenueUnitType RevenueUnitType { get; set; }        
        public Guid CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        public Guid ClusterId { get; set; }
        [ForeignKey("ClusterId")]
        public virtual Cluster Cluster { get; set; }
        [MaxLength(300)]
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
        public virtual List<Employee> Employees { get; set; }
    }
}
