using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class ApplicationSetting:BaseEntity
    {
        [Key]
        public Guid ApplicationSettingId { get; set; }
        [Required]
        [MaxLength(80)]
        [DisplayName("Setting Name")]
        public string SettingName { get; set; }
        [Required]
        [MaxLength(250)]
        [DisplayName("Setting Value")]
        public string SettingValue { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }

    }
}
