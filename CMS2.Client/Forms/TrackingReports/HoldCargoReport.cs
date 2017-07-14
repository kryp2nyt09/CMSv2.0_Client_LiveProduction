using CMS2.BusinessLogic;
using CMS2.Entities;
using CMS2.Entities.ReportModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.Client.Forms.TrackingReports
{
    public class HoldCargoReport
    {
        public DataTable getData(DateTime fromdate , DateTime todate) {

            HoldCargoBL holdcargoBl = new HoldCargoBL();
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
            List<HoldCargo> list = holdcargoBl.GetAll().Where(x => x.CreatedDate >= fromdate && x.CreatedDate <= todate ).ToList();
            string bcoName = bcoService.GetAll().Find(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).BranchCorpOfficeName;

            //List<HoldCargoViewModel> modelList = Match(list).FindAll(x => x.Branch == bcoName);
            List<HoldCargoViewModel> modelList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipper", typeof(string)));            
            dt.Columns.Add(new DataColumn("Consignee", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Service Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            dt.Columns.Add(new DataColumn("Reason", typeof(string)));
            dt.Columns.Add(new DataColumn("Endorse by", typeof(string)));
            dt.Columns.Add(new DataColumn("Scanned by", typeof(string)));
            dt.Columns.Add(new DataColumn("Prepared by(Cut by)", typeof(string)));
            dt.Columns.Add(new DataColumn("Aging", typeof(string)));

            dt.Columns.Add(new DataColumn("Branch", typeof(string)));

            dt.Columns.Add(new DataColumn("BSO", typeof(string)));

            dt.BeginLoadData();
            int ctr = 1;
            foreach (HoldCargoViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.Date.ToShortDateString();
                row[2] = item.AirwayBillNo;
                row[3] = item.Shipper;
                row[4] = item.Consignee;
                row[5] = item.Address;
                row[6] = item.PaymentMode;
                row[7] = item.ServiceMode;
                row[8] = item.Status;
                row[9] = item.Reason;
                row[10] = item.EndorsedBy;
                row[11] = item.ScannedBy;
                row[12] = item.PreparedBy;
                row[13] = item.Aging; //Present Date - Transaction Date
                row[14] = item.Branch;
                row[15] = item.BSO;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public DataTable getHCDataByFilter(DateTime fromdate, DateTime todate, Guid? revenueunitid, Guid? statusid, Guid? reasonid)
        {

            HoldCargoBL holdcargoBl = new HoldCargoBL();
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
            string bcoName = bcoService.GetAll().Find(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).BranchCorpOfficeName;

            List<HoldCargo> list = holdcargoBl.GetAll().Where
                (x => x.CreatedDate >= fromdate && x.CreatedDate <= todate
                && ((x.HoldCargoBy.Employee.AssignedToArea.RevenueUnitId == revenueunitid && x.HoldCargoBy.Employee.AssignedToArea.RevenueUnitId != Guid.Empty) || (x.HoldCargoBy.Employee.AssignedToArea.RevenueUnitId == x.HoldCargoBy.Employee.AssignedToArea.RevenueUnitId && revenueunitid == Guid.Empty))
                && ((x.StatusID == statusid && x.StatusID != Guid.Empty) || (x.StatusID == x.StatusID && statusid == Guid.Empty))
                && ((x.ReasonID == reasonid && x.ReasonID != Guid.Empty) || (x.ReasonID == x.ReasonID && reasonid == Guid.Empty))
                ).ToList();

            //List<HoldCargoViewModel> modelList = Match(list).FindAll(x => x.Branch == bcoName);
            List<HoldCargoViewModel> modelList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Date", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipper", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Service Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            dt.Columns.Add(new DataColumn("Reason", typeof(string)));
            dt.Columns.Add(new DataColumn("Endorse by", typeof(string)));
            dt.Columns.Add(new DataColumn("Scanned by", typeof(string)));
            dt.Columns.Add(new DataColumn("Prepared by(Cut by)", typeof(string)));
            dt.Columns.Add(new DataColumn("Aging", typeof(string)));

            dt.Columns.Add(new DataColumn("Branch", typeof(string)));

            dt.Columns.Add(new DataColumn("BSO", typeof(string)));

            dt.BeginLoadData();
            int ctr = 1;
            foreach (HoldCargoViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.Date.ToShortDateString();
                row[2] = item.AirwayBillNo;
                row[3] = item.Shipper;
                row[4] = item.Consignee;
                row[5] = item.Address;
                row[6] = item.PaymentMode;
                row[7] = item.ServiceMode;
                row[8] = item.Status;
                row[9] = item.Reason;
                row[10] = item.EndorsedBy;
                row[11] = item.ScannedBy;
                row[12] = item.PreparedBy;
                row[13] = item.Aging; //Present Date - Transaction Date
                row[14] = item.Branch;
                row[15] = item.BSO;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }



        public List<int> setWidth()
        {
            List<int> width = new List<int>();
            width.Add(25);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(0);
            width.Add(100);
            width.Add(0);
            width.Add(0);
            return width;
        }

        public List<HoldCargoViewModel> Match(List<HoldCargo> _holdcargo)
        {
            List<HoldCargoViewModel> _results = new List<HoldCargoViewModel>();
            PackageNumberBL _packageNumberService = new PackageNumberBL();
            StatusBL status = new StatusBL();
            ReasonBL reason = new ReasonBL();
            UserRoleBL user = new UserRoleBL();
            ShipmentBL shipmentService = new ShipmentBL();
            List<Shipment> shipList = shipmentService.GetAll();
            UserStore _userService = new UserStore();
            foreach (HoldCargo holdCargo in _holdcargo)
            {
                HoldCargoViewModel model = new HoldCargoViewModel();
                //string _airwaybill = _packageNumberService.GetAll().Find(x => x.PackageNo == holdCargo.Cargo).Shipment.AirwayBillNo;
                HoldCargoViewModel isExist = _results.Find(x => x.AirwayBillNo == holdCargo.AirwayBillNo);

                if (isExist != null)
                {
                   
                }
                else
                {
                    model.Date = holdCargo.HoldCargoDate;
                    model.AirwayBillNo = holdCargo.AirwayBillNo;

                    Shipment ship = shipList.Find(x => x.AirwayBillNo == holdCargo.AirwayBillNo);
                    
                    if (ship !=null)
                    {
                        model.Shipper = ship.Shipper.FullName;
                        model.Consignee = ship.Consignee.FullName;
                        model.Address = ship.Consignee.Address1;
                        model.PaymentMode = ship.PaymentMode.PaymentModeName;
                        model.ServiceMode = ship.ServiceMode.ServiceModeName;
                        model.Status = status.GetById(holdCargo.StatusID).StatusName; // status.GetAll().Find(x => x.StatusID == holdCargo.StatusID).StatusName;
                        model.Reason = reason.GetById(holdCargo.ReasonID).ReasonName; // .Find(x => x.ReasonID == holdCargo.ReasonID).ReasonName;
                        model.EndorsedBy = holdCargo.Endorsedby;
                        //model.ScannedBy = user.GetActiveRoles().Find(x => x.RoleId == AppUser.User.UserId).RoleName;
                        //model.ScannedBy = holdCargo.User.Employee.FullName;
                        model.ScannedBy = "N/A";
                        string employee = _userService.FindById(holdCargo.CreatedBy).Employee.FullName;
                        if (employee != "")
                        {
                            model.ScannedBy = employee;
                        }
                        //model.PreparedBy = user.GetAllUsers().Find(x => x.UserId == shi)
                        model.Aging = Math.Round((DateTime.Now - holdCargo.HoldCargoDate).TotalDays,2);
                        model.Branch = holdCargo.HoldCargoBy.Employee.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeName;
                        _results.Add(model);
                    }
                    
                }
            }
            return _results;

        }
    }
}
