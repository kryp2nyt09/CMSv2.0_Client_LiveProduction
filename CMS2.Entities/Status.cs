namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status : BaseEntity
    {

        public Guid StatusID { get; set; }

        [Required]
        [StringLength(20)]
        public string StatusCode { get; set; }

        [Required]
        [StringLength(20)]
        public string StatusName { get; set; }
        public virtual List<Reason> Reason { get; set; }
    }
}
