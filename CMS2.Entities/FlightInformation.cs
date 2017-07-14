namespace CMS2.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FlightInformation")]
    public partial class FlightInformation : BaseEntity
    {
        [Key]
        public Guid FlightInfoId { get; set; }

        [Required]
        [StringLength(30)]
        public string FlightNo { get; set; }

        public DateTime? ETD { get; set; }

        public DateTime? ETA { get; set; }

        public Guid GateWayId { get; set; }

        public Guid OriginCityID { get; set; }

        public Guid DestinationCityID { get; set; }
        [ForeignKey("GateWayId")]
        public virtual Airlines Airlines { get; set; }
        [ForeignKey("OriginCityID")]
        public virtual BranchCorpOffice OriginBco { get; set; }
        [ForeignKey("DestinationCityID")]
        public virtual BranchCorpOffice DestinationBco { get; set; }
    }
}
