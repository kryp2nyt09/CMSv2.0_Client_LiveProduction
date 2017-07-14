using System;

namespace CMS2.Entities.Models
{
    public class ShipmentByDateBcoModel
    {
        public Shipment Shipment { get; set; }
        public Guid BranchCorpOfficeId { get; set; }
        public string BranchCorpOffice { get; set; }
        public Guid AreaId { get; set; }
        public RevenueUnit Area { get; set; }
        public Guid TruckId { get; set; }
        public Truck Truck { get; set; }
        public Guid DriverId { get; set; }
        public Employee Driver { get;set;}
    }
}
