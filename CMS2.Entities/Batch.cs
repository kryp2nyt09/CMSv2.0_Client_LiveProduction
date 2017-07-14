namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Batch")]
    public partial class Batch : BaseEntity
    {
        public Guid BatchID { get; set; }

        [Required]
        [StringLength(20)]
        public string BatchName { get; set; }

        [Required]
        [StringLength(20)]
        public string BatchCode { get; set; }
    }
}
