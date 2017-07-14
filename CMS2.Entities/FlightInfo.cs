using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    public class FlightInfo : BaseEntity
    {
        [Key]
        public Guid FlightInfoId { get; set; }
        [Required]
        [MaxLength(30)]
        public string FlightNo { get; set; }
        //public Guid GatewayId { get; set; }
        //public Gateway Gateway { get; set; }
        public Guid OriginCityId { get; set; }
        [ForeignKey("OriginCityId")]
        public City OriginCity { get; set; }
        public Guid DestinationCityId { get; set; }
        [ForeignKey("DestinationCityId")]
        public City DestinationCity { get; set; }
        [MaxLength(10)]
        public string ETD { get; set; }
        [MaxLength(10)]
        public string ETA { get; set; }
    }
}
