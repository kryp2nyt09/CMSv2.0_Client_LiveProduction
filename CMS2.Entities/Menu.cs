using System;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class Menu:BaseEntity
    {
        [Key]
        public Guid MenuId { get; set; }
        public string AppTitle { get; set; }
        [Required]
        public string MenuName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string FunctionName { get; set; }
        public string MenuUrl { get; set; }
    }
}
