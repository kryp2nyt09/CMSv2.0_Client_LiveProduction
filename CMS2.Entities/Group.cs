using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    /// <summary>
    /// Contains a Collection of Regions
    /// </summary>
    public class Group : BaseEntity
    {
        [Key]
        [DisplayName("Group")]
        public Guid GroupId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Group")]
        public string GroupName { get; set; }
        public virtual List<Region> Regions { get; set; }
    }
}
