namespace CMS2.Entities
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BranchAcceptance")]
    public partial class BranchAcceptance : BaseEntity
    {
        public Guid BranchAcceptanceId { get; set; }

        public Guid BranchCorpOfficeID { get; set; }

        public Guid UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string Driver { get; set; }

        [Required]
        [StringLength(20)]
        public string Checker { get; set; }

        public Guid RemarkID { get; set; }

        public Guid BatchID { get; set; }

        [Required]
        [StringLength(20)]
        public string Cargo { get; set; }

        public DateTime BranchAcceptanceDate { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public bool Uploaded { get; set; }

        [ForeignKey("BatchID")]
        public virtual Batch Batch { get; set; }

        [ForeignKey("BranchCorpOfficeID")]
        public virtual BranchCorpOffice BranchCorpOffice { get; set; }

        [ForeignKey("RemarkID")]
        public virtual Remarks Remarks { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }


    }
}
