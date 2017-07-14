using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class TntMaint:BaseEntity
    {
        [Key]
        public Guid TntMaintId { get; set; }
        [MaxLength(150)]
        [DisplayName("Module")]
        public string Module { get; set; }
        [MaxLength(150)]
        [DisplayName("Field")]
        public string FieldName { get; set; }
        [MaxLength(20)]
        [DisplayName("Code")]
        public string FieldCode { get; set; }
        [MaxLength(20)]
        [DisplayName("Value")]
        public string FieldValue { get; set; }
    }
}
