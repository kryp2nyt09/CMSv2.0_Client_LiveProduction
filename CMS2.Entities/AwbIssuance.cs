using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class AwbIssuance : BaseEntity
    {
        [Key]
        public Guid AwbIssuanceId { get; set; }
        [Required]
        [MaxLength(8)]
        [DisplayName("Series Start")]
        public string SeriesStart { get; set;}
        [Required]
        [MaxLength(8)]
        [DisplayName("Series End")]
        public string SeriesEnd { get; set; }
        [Required]
        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }
        [DisplayName("Branch Issued")]
        public Guid RevenueUnitId { get; set; }
        [DisplayName("Branch Issued")]
        public virtual RevenueUnit RevenueUnit { get; set; }
        [DisplayName("Issued To")]
        public Guid IssuedToId { get; set; }
        [ForeignKey("IssuedToId")]
        [DisplayName("Issued To")]
        public Employee IssuedTo { get; set; }
        
    }
}
