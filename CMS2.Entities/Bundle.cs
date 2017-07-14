namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bundle")]
    public partial class Bundle : BaseEntity
    {
        public Guid BundleID { get; set; }

        public Guid UserID { get; set; }

        public Guid BranchCorpOfficeID { get; set; }

        [Required]
        [StringLength(20)]
        public string Cargo { get; set; }

        [Required]
        [StringLength(20)]
        public string SackNo { get; set; }

        public decimal Weight { get; set; }

        public bool Uploaded { get; set; }

        [ForeignKey("BranchCorpOfficeID")]
        public virtual BranchCorpOffice DestinationBco { get; set; }

        [ForeignKey("UserID")]
        public virtual User BundleBy { get; set; }
    }
}
