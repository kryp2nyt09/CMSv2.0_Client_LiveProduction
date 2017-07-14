namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Remarks : BaseEntity
    {
        [Key]
        public Guid RemarkID { get; set; }

        [Required]
        [StringLength(20)]
        public string RemarkCode { get; set; }

        [Required]
        [StringLength(20)]
        public string RemarkName { get; set; }
        
    }
}
