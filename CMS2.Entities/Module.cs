using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APCargo.Entities
{
    /// <summary>
    /// Information if Application Modules 
    /// </summary>
    public class Module : BaseEntity
    {
        [Key]
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        public IList<Function> Functions { get; set; }
    }
}
