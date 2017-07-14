namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("Unbundle")]
    public partial class Unbundle : BaseEntity
    {
        public Guid UnBundleID { get; set; }

        public Guid UserID { get; set; }

        public Guid BranchCorpOfficeID { get; set; }

        [Required]
        [StringLength(20)]
        public string Cargo { get; set; }

        [Required]
        [StringLength(20)]
        public string SackNo { get; set; }        

        public bool Uploaded { get; set; }
        [ForeignKey("UserID")]
        public virtual User UnbundleBy { get; set; }

        [ForeignKey("BranchCorpOfficeID")]
        public virtual BranchCorpOffice OriginBco { get; set; }
    }
}
