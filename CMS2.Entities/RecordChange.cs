using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class RecordChange
    {
        [Key]
        public Guid RecordChangeId { get; set; }
        [Required]
        [MaxLength(6)]
        public string ChangeType { get; set; }
        [Required]
        public string Entity { get; set; }
        [Required]
        public Guid EntityId { get; set; }
        [Required]
        public DateTime ChangeDate { get; set; }
    }
}
