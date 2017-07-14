using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    /// <summary>
    /// APCargo Departments 
    /// </summary>
    public class Department:BaseEntity
    {
        [Key]
        public Guid DepartmentId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Department")]
        public string DepartmentName { get; set; }
    }
}
