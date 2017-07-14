using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class TerminalRevenueUnitMapping:BaseEntity
    {
        [Key]
        public Guid TerminalRevenueUnitMappingId { get; set; }
        public Guid TerminalId { get; set; }
        public Terminal Terminal { get; set; }
        public Guid RevenueUnitId { get; set; }
        public RevenueUnit RevenueUnit { get; set; }
        public DateTime DateAssigned { get; set; }
        public Guid AssignedById { get; set; }
        [ForeignKey("AssignedById")]
        public Employee AssignedBy { get; set; }


        [NotMapped]
        [DisplayName("Date Assigned")]
        public string DateAssignedString { get { return DateAssigned.ToString("MMM dd, yyyy"); } }
    }
}
