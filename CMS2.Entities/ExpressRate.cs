using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class ExpressRate : BaseEntity
    {
        [Key]
        public Guid ExpressRateId { get; set; }
        public Guid RateMatrixId { get; set; }
        public RateMatrix RateMatrix { get; set; }

        [Column("1to5Cost")]
        public decimal C1to5Cost { get; set; }

        [Column("6to49Cost")]
        public decimal C6to49Cost { get; set; }

        [Column("50to249Cost")]
        public decimal C50to249Cost { get; set; }

        [Column("250to999Cost")]
        public decimal C250to999Cost { get; set; }

        [Column("1000to10000Cost")]
        public decimal C1000_10000Cost { get; set; }
        [Required]
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DisplayName("Origin City")]
        public Guid OriginCityId { get; set; }
        [ForeignKey("OriginCityId")]
        public virtual City OriginCity { get; set; }
        [DisplayName("Destination City")]
        public Guid DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public virtual City DestinationCity { get; set; }

        [NotMapped]
        [DisplayName("Min Weight")]
        public string C1to5CostString { get { return C1to5Cost.ToString("N"); } }

        [NotMapped]
        [DisplayName("Min Weight")]
        public string C6to49CostString { get { return C6to49Cost.ToString("N"); } }

        [NotMapped]
        [DisplayName("Min Weight")]
        public string C50to249CostString { get { return C50to249Cost.ToString("N"); } }

        [NotMapped]
        [DisplayName("Min Weight")]
        public string C250to999CostString { get { return C250to999Cost.ToString("N"); } }

        [NotMapped]
        [DisplayName("Max Weight")]
        public string C1000_10000CostString { get { return C1000_10000Cost.ToString("N"); } }
    
        [NotMapped]
        [DisplayName("Effective Date")]
        public string EffectiveDateString { get { return EffectiveDate.ToString("MMM dd, yyyy"); } }


    }
}
