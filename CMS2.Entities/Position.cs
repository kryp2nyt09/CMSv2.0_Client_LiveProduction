using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    /// <summary>
    /// Employee Position
    /// </summary>
    public class Position : BaseEntity
    {
        [Key]
        public Guid PositionId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Position")]
        public string PositionName { get; set; }
    }
}
