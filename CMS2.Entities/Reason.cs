namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reason")]
    public partial class Reason :BaseEntity
    {
        public Guid ReasonID { get; set; }

        public Guid StatusID { get; set; }

        [Required]
        [StringLength(20)]
        public string ReasonCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ReasonName { get; set; }

        public virtual Status Status { get; set; }
    }
}
