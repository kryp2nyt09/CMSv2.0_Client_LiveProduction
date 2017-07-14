using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class Region : BaseEntity
    {
        [Key]
        [DisplayName("Region")]
        public Guid RegionId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Region")]
        public string RegionName { get; set; }
        [DisplayName("Group")]
        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        public virtual List<Province> Provinces { get; set; }

    }
}
