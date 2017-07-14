namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Airlines :BaseEntity
    {
        [Key]
        public Guid AirlineID { get; set; }

        [Required]
        [StringLength(20)]
        public string AirlineCode { get; set; }

        [Required]
        [StringLength(20)]
        public string AirlineName { get; set; }
    }
}
