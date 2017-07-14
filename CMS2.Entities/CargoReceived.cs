using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS2.Entities
{
    [Table("cargoReceived")]
    public class CargoReceived
    {
        public CargoReceived()
        {
            this.CreatedDate = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Column("airwaybill")]
        public Int64? AirwayBill { get; set; }
        [Column("DeliverBy")]
        public string DeliveredBy { get; set; }
        [Column("ReceivedBy")]
        public string ReceivedBy { get; set; }
        [Column("DateReceived")]
        public DateTime? DateReceived { get; set; }
        [Column("Remarks")]
        public string Remarks { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("notes")]
        public string Notes { get; set; }
        [Column("CreatedBy")]
        public string CreatedBy { get; set; }
        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
        [Column("ModifiedBy")]
        public string ModifiedBy { get; set; }
        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

    }
}
