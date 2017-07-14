using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMS2.Entities
{
    public class Truck : BaseEntity
    {
        [Key]
        public Guid TruckId { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Truck Model")]
        public string TruckModel { get; set; }
        [Required]
        [MaxLength(8)]
        [DisplayName("Plate No")]
        public string PlateNo { get; set; }
    }
}
