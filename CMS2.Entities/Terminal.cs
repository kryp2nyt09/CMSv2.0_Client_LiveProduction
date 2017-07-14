using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class Terminal :BaseEntity
    {
        [Key]
        public Guid TerminalId { get; set; }
        [Required]
        [MaxLength(4)]
        public string TerminalCode { get; set; }
        [MaxLength(30)]
        public string TerminalName { get; set; }
        [MaxLength(250)]
        public string TerminalDescription { get; set; }
    }
}
