using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Cluster: BaseEntity
    {
        [Key]
        [DisplayName("Cluster")]
        public Guid ClusterId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Cluster")]
        public string ClusterName { get; set; }
        [DisplayName("BCO")]
        public Guid BranchCorpOfficeId { get; set; }

        [ForeignKey("BranchCorpOfficeId")]
        public virtual BranchCorpOffice BranchCorpOffice { get; set; }
        public virtual List<RevenueUnit> RevenueUnits { get; set; }
        
       
    }
}
