using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class BranchCorpOffice : BaseEntity
    {
        [Key]
        [DisplayName("BCO")]
        public Guid BranchCorpOfficeId { get; set; }
        [MaxLength(50)]
        [DisplayName("BCO Name")]
        public string BranchCorpOfficeName { get; set; }
        [DisplayName("Code")]
        public string BranchCorpOfficeCode { get; set; }

        [DisplayName("Province")]
        public Guid ProvinceId { get; set; }
       
        public List<Cluster> Clusters { get; set; }
        
        public List<City> Cities { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
    }
}
