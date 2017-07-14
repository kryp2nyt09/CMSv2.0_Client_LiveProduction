using CMS2.BusinessLogic;
using CMS2.Entities;
using CMS2.Entities.ReportModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Client.Forms.TrackingReports
{
    public class PickupCargoManifestReport
    {
        
        public DataTable getPickUpCargoData(DateTime date)
        {
            //GET LIST 
            ShipmentBL _shipmentService = new ShipmentBL();
            List<Shipment> _shipments = _shipmentService.FilterActiveBy(x => x.Booking.BookingStatus.BookingStatusName == "Picked-up" && x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && (x.CreatedDate).ToShortDateString() == date.ToShortDateString()).ToList();

            List<PickupCargoManifestViewModel> modelList = Match(_shipments).OrderByDescending(x => x.AirwayBillNo).Distinct().ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipper", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipper Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Commodity", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("AGW", typeof(string)));
            dt.Columns.Add(new DataColumn("Service Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));
            //dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            //dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (PickupCargoManifestViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.AirwayBillNo;
                row[2] = item.Shipper;
                row[3] = item.ShipperAddress;
                row[4] = item.Consignee;
                row[5] = item.ConsigneeAddress;
                row[6] = item.Commodity;
                row[7] = item.QTY.ToString();
                row[8] = item.AGW.ToString();
                row[9] = item.ServiceMode;
                row[10] = item.PaymentMode;
                row[11] = item.Amount;
                row[12] = item.Area;
                //row[13] = item.Driver;
                //row[14] = item.Checker;
                row[13] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }


        public DataTable getPickupCargoDataByRevenueUnit(DateTime date, Guid? revenueUnitTypeId, Guid? revenueUnitId)
        {
            //GET LIST 
            ShipmentBL _shipmentService = new ShipmentBL();
            List<Shipment> _shipments = _shipmentService.FilterActive().Where(x => x.Booking.BookingStatus.BookingStatusName == "Picked-up"
            && x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId
            && x.RecordStatus == 1
            && (x.CreatedDate).ToShortDateString() == date.ToShortDateString()
            &&
            //&& x.AcceptedBy.AssignedToAreaId == revenueUnitId
            ((x.AcceptedBy.AssignedToAreaId == revenueUnitId && x.AcceptedBy.AssignedToAreaId != Guid.Empty) || (x.AcceptedBy.AssignedToAreaId == x.AcceptedBy.AssignedToAreaId && revenueUnitId == Guid.Empty))
            //&& x.AcceptedBy.AssignedToArea.RevenueUnitTypeId == revenueUnitTypeId).ToList();
            && ((x.AcceptedBy.AssignedToArea.RevenueUnitTypeId == revenueUnitTypeId && x.AcceptedBy.AssignedToArea.RevenueUnitTypeId != Guid.Empty) || (x.AcceptedBy.AssignedToArea.RevenueUnitTypeId == x.AcceptedBy.AssignedToArea.RevenueUnitTypeId && revenueUnitTypeId == Guid.Empty))
            ).ToList();

            List <PickupCargoManifestViewModel> modelList = Match(_shipments).OrderByDescending(x => x.AirwayBillNo).ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipper", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipper Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Commodity", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("AGW", typeof(string)));
            dt.Columns.Add(new DataColumn("Service Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));

            dt.Columns.Add(new DataColumn("Area", typeof(string)));
            //dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            //dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Scanned By", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (PickupCargoManifestViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.AirwayBillNo;
                row[2] = item.Shipper;
                row[3] = item.ShipperAddress;
                row[4] = item.Consignee;
                row[5] = item.ConsigneeAddress;
                row[6] = item.Commodity;
                row[7] = item.QTY.ToString();
                row[8] = item.AGW.ToString();
                row[9] = item.ServiceMode;
                row[10] = item.PaymentMode;
                row[11] = item.Amount;
                row[12] = item.Area;
                //row[13] = item.Driver;
                //row[14] = item.Checker;
                row[13] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public List<int> setPickUpCargoWidth()
        {
            List<int> width = new List<int>();
            width.Add(25); //No
            width.Add(60); //AWB
            width.Add(110); //Shipper
            width.Add(150); //Address
            width.Add(110); //Consignee
            width.Add(150); //Address
            width.Add(110); //Commodity
            width.Add(30); //Qty
            width.Add(50); //AGW
            width.Add(100); //Servicemode
            width.Add(100); //Paymentmode
            width.Add(95); //Amount
            width.Add(0); //Area
            width.Add(0); //Driver
            width.Add(0); //Checker
            width.Add(100); //Amount
            return width;
        }

        public List<PickupCargoManifestViewModel> Match(List<Shipment> _shipment )
        {
            BranchAcceptanceBL branchAcceptanceBL = new BranchAcceptanceBL();
            PackageNumberBL _packageNumberService = new PackageNumberBL();
            List<PickupCargoManifestViewModel> _results = new List<PickupCargoManifestViewModel>();

            List<PackageNumber> packageList = new List<PackageNumber>();
            int numberOfPackage = 0;

            foreach (Shipment shipment  in _shipment)
            {
                PickupCargoManifestViewModel model = new PickupCargoManifestViewModel();
                try
                {
                    //packageList = _packageNumberService.FilterActiveBy(x => x.ShipmentId == shipment.ShipmentId).Distinct().ToList();
                    //numberOfPackage = packageList.Count;

                    numberOfPackage = shipment.PackageNumbers.Count();

                }
                catch (Exception) { continue; }

                //foreach (PackageNumber number in packageList)
                //{
                //    List<BranchAcceptance> branchList = branchAcceptanceBL.FilterActiveBy(x => x.Cargo == number.PackageNo).Distinct().ToList();
                //    foreach (BranchAcceptance branch in branchList)
                //    {
                //        model.Driver = (branch.Driver != null || branch.Driver != "") ? branch.Driver : "N/A";
                //        model.Checker = (branch.Checker != null || branch.Checker != "") ? branch.Checker : "N/A";                                           
                //    }
                   
                //}
                
                PickupCargoManifestViewModel isExist = _results.Find(x => x.AirwayBillNo == shipment.AirwayBillNo);

                if (isExist != null)
                {
                    isExist.QTY++;
                }
                else
                {
                    model.AirwayBillNo = shipment.AirwayBillNo;
                    model.Shipper = shipment.Shipper.FullName;
                    model.Consignee = shipment.Consignee.FullName;

                    model.ConsigneeAddress = shipment.Consignee.Address1 + " " + shipment.Consignee.Address2;
                    model.ShipperAddress = shipment.Shipper.Address1 + " " + shipment.Shipper.Address2;
                    model.Commodity = shipment.Commodity.CommodityName;
                    // model.QTY++;
                    model.QTY = numberOfPackage;
                    model.AGW += shipment.Weight;
                    model.ServiceMode = shipment.ServiceMode.ServiceModeName;
                    model.PaymentMode = shipment.PaymentMode.PaymentModeName;
                    model.Amount = shipment.TotalAmount.ToString();
                    model.ScannedBy = shipment.AcceptedBy.FullName;
                    try
                    {
                        model.Area = (shipment.Booking.AssignedToArea != null) ? shipment.Booking.AssignedToArea.City.CityName : "N/A";
                    }
                    catch (Exception)
                    {
                        model.Area = "N/A";
                    }
                    _results.Add(model);
                }
            }
            
            return _results;

        }
    }
}
