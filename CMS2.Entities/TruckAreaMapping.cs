using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class TruckAreaMapping : BaseEntity
    {
        [Key]
        public Guid TruckAreaMappingId { get; set; }
        [DisplayName("Truck")]
        public Guid TruckId { get; set; }
        public virtual Truck Truck { get; set; }
        [DisplayName("Area")]
        public Guid RevenueUnitId { get; set; }
        public virtual RevenueUnit RevenueUnit { get; set; }
        [Required]
        [DisplayName("Date Assigned")]
        [DataType(DataType.Date)]
        public DateTime DateAssigned { get; set; }

        [NotMapped]
        [DisplayName("Date Assigned")]
        public string DateAssignedString { get { return DateAssigned.ToString("MMM dd, yyyy"); } }
    }
}
