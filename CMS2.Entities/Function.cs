using System;
using System.ComponentModel.DataAnnotations;

namespace APCargo.Entities
{
    /// <summary>
    /// Information of Application Functions
    /// </summary>
    public class Function : BaseEntity
    {
        [Key]
        public Guid FunctionId { get; set; }
        public string FunctionName { get; set; }
        public Guid ModuleId { get; set; }
        public virtual Module Module { get; set; }
    }
}
