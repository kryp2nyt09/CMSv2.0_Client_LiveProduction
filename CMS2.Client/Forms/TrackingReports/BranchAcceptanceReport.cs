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
    public  class BranchAcceptanceReport
    {

        //public DataTable getBranchAcceptanceData(DateTime date)
        //{
        //    BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
        //    BranchAcceptanceBL branchAcceptanceBl = new BranchAcceptanceBL();
        //    ShipmentBL shipmentService = new ShipmentBL();
            
        //    //List<Shipment> shipments = shipmentService.FilterActive().Where(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString() && x.Booking.BookingStatus.BookingStatusName == "Picked-up").ToList();

        //    List<Shipment> shipments = shipmentService.FilterActive().Where(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.Booking.BookingStatus.BookingStatusName == "Picked-up" && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
        //    List<BranchAcceptance> branchAcceptance = branchAcceptanceBl.FilterActive().Where(x => x.User.Employee.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

        //    string bcoName = bcoService.FilterActive().Find(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).BranchCorpOfficeName;

        //    List<BranchAcceptanceViewModel> list = Match(branchAcceptance, shipments).FindAll(x => x.BCO == bcoName 
        //            && x.Area != "N/A" && !x.Area.Contains("Walk-in"));

        //   // List<BranchAcceptanceViewModel> list = Match(branchAcceptance, shipments).FindAll(x => x.BCO == bcoName
        //            //&& x.Area != "N/A");

        //    // ((x.Driver == driver && x.Driver != "All") || (x.Driver == x.Driver && driver == "All"))
        //    //List<BranchAcceptanceViewModel> list = Match(branchAcceptance, shipments);

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("No", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Area/Branch", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Driver", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Checker", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Batch", typeof(string)));
        //    dt.Columns.Add(new DataColumn("AWB", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Recieved(Qty)", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Discrepancy(Qty)", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Total Qty", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BCO", typeof(string)));
        //    dt.Columns.Add(new DataColumn("BSO", typeof(string)));

        //    dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Notes", typeof(string)));
        //    dt.BeginLoadData();
        //    int ctr = 1;
        //    foreach (BranchAcceptanceViewModel item in list)
        //    {
        //        DataRow row = dt.NewRow();
        //        row[0] = ctr++.ToString();
        //        row[1] = item.Area.ToString();
        //        row[2] = item.Driver.ToString();
        //        row[3] = item.Checker.ToString();
        //        row[4] = item.PlateNo.ToString();
        //        row[5] = item.Batch.ToString();
        //        row[6] = item.AirwayBillNo.ToString();
        //        row[7] = item.TotalRecieved.ToString();
        //        row[8] = item.TotalDiscrepency.ToString();
        //        row[9] = item.Total.ToString();

        //        row[10] = item.BCO;
        //        row[11] = item.BSO;
        //        row[12] = item.ScannedBy;
        //        row[13] = item.Remarks;
        //        row[14] = item.Notes;
        //        dt.Rows.Add(row);
        //    }
        //    dt.EndLoadData();

        //    return dt;
        //}

        //Complete Filter
        public DataTable getBranchAcceptanceDataByFilter(DateTime date, Guid? revenueUnitId, string driver, Guid? batchId)
        {
            RevenueUnitBL revenueunitservice = new RevenueUnitBL();
            BatchBL batchservice = new BatchBL();
            BranchAcceptanceBL branchAcceptanceBl = new BranchAcceptanceBL();
            ShipmentBL shipmentService = new ShipmentBL();
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();

            //List<Shipment> shipments = shipmentService.FilterActive().Where(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            List<Shipment> shipments = shipmentService.FilterActive().Where(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.Booking.BookingStatus.BookingStatusName == "Picked-up" && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            // List<BranchAcceptance> branchAcceptance = branchAcceptanceBl.GetAll().Where(x => x.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.BatchID == batchId && x.Driver == driver).ToList();
            //List<BranchAcceptance> branchAcceptance = branchAcceptanceBl.GetAll().Where(x => x.Users.Employee.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.Users.Employee.AssignedToArea.RevenueUnitId == revenueUnitId && x.RecordStatus == 1 && x.BatchID == batchId && x.Driver == driver && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

            List<BranchAcceptanceViewModel> list = new List<BranchAcceptanceViewModel>();
            List<BranchAcceptance> branchAcceptance = branchAcceptanceBl.FilterActive().
                Where(
                x => x.RecordStatus == 1
                //&& ((x.Users.Employee.AssignedToArea.RevenueUnitId == revenueUnitId && x.Users.Employee.AssignedToArea.RevenueUnitId != Guid.Empty) || (x.Users.Employee.AssignedToArea.RevenueUnitId == x.Users.Employee.AssignedToArea.RevenueUnitId && revenueUnitId == Guid.Empty))
                && ((x.BatchID == batchId && x.BatchID != Guid.Empty) || (x.BatchID == x.BatchID && batchId == Guid.Empty))
                && ((x.Driver == driver && x.Driver != "All") || (x.Driver == x.Driver && driver == "All"))
                && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

            string revenueunitname = "";
            string batchname = "";
            string bcoName = "";

            if(batchId != Guid.Empty)
            {
                batchname = batchservice.FilterActive().Find(x => x.BatchID == batchId).BatchName;
            }
            else
            {
                batchname = "All";
            }
            
            if(revenueUnitId != Guid.Empty)
            {
                revenueunitname = revenueunitservice.FilterActive().Find(x => x.RevenueUnitId == revenueUnitId).RevenueUnitName;
            }
            else
            {
                revenueunitname = "All";
            }
            
            bcoName = bcoService.FilterActive().Find(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).BranchCorpOfficeName;

            list = Match(branchAcceptance, shipments).
                FindAll
                (x => 
                ((x.Area == revenueunitname && x.Area !="N/A") || (x.Area == x.Area && revenueunitname == "All"))
                && ((x.Driver == driver && x.Driver != "N/A") || (x.Driver == x.Driver && driver == "All"))
                && ((x.Batch == batchname && x.Batch != "N/A") || (x.Batch == x.Batch && batchname == "All"))
                && ((x.BCO == bcoName && x.BCO != "N/A") || (x.BCO == x.BCO && bcoName == ""))
                && !x.Area.Contains("Walk-in"));

            //if (revenueUnitId != Guid.Empty && batchId !=Guid.Empty)
            //{
            //    batchname = batchservice.GetAll().Find(x => x.BatchID == batchId).BatchName;
            //    revenueunitname = revenueunitservice.GetAll().Find(x => x.RevenueUnitId == revenueUnitId).RevenueUnitName;
            //    list = Match(branchAcceptance, shipments).FindAll(x => x.Area == revenueunitname && x.Batch == batchname);
            //}
            //else if (revenueUnitId == Guid.Empty && batchId != Guid.Empty)
            //{
            //    batchname = batchservice.GetAll().Find(x => x.BatchID == batchId).BatchName;
            //    list = Match(branchAcceptance, shipments).FindAll(x => x.Batch == batchname && x.Area != "N/A");
            //}
            //else if (revenueUnitId != Guid.Empty && batchId == Guid.Empty)
            //{
            //    revenueunitname = revenueunitservice.GetAll().Find(x => x.RevenueUnitId == revenueUnitId).RevenueUnitName;
            //    list = Match(branchAcceptance, shipments).FindAll(x => x.Area == revenueunitname);
            //}
            //else
            //{
            //    bcoName = bcoService.GetAll().Find(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).BranchCorpOfficeName;
            //    list = Match(branchAcceptance, shipments).FindAll(x => x.BCO == bcoName && x.Area != "N/A");
            //    //list = Match(branchAcceptance, shipments);
            //}

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Area/Branch", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Recieved(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Discrepancy(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Total Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("BSO", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("Notes", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (BranchAcceptanceViewModel item in list)
            {
                DataRow row = dt.NewRow();
                row[0] = ctr++.ToString();
                row[1] = item.Area.ToString();
                row[2] = item.Driver.ToString();
                row[3] = item.Checker.ToString();
                row[4] = item.PlateNo.ToString();
                row[5] = item.Batch.ToString();
                row[6] = item.AirwayBillNo.ToString();
                row[7] = item.TotalRecieved.ToString();
                row[8] = item.TotalDiscrepency.ToString();
                row[9] = item.Total.ToString();

                row[10] = item.BCO;
                row[11] = item.BSO;
                row[12] = item.ScannedBy;
                row[13] = item.Remarks;
                row[14] = item.Notes;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        //Filter By Batch and BCO only
        public DataTable getBranchAcceptanceDataByBatch(DateTime date, Guid revenueUnitId, Guid batchId)
        {
            BranchAcceptanceBL branchAcceptanceBl = new BranchAcceptanceBL();
            ShipmentBL shipmentService = new ShipmentBL();
             List<Shipment> shipments = shipmentService.FilterActive().Where(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.Booking.BookingStatus.BookingStatusName == "Picked-up").ToList();
            List<BranchAcceptance> branchAcceptance = branchAcceptanceBl.FilterActive().Where(x => x.RecordStatus == 1 && x.User.Employee.AssignedToArea.RevenueUnitId == revenueUnitId && x.BatchID == batchId && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).Distinct().ToList();

            List<BranchAcceptanceViewModel> list = Match(branchAcceptance, shipments).FindAll(x => x.Area != "N/A" && !x.Area.Contains("Walk-in"));

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Area/Branch", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Recieved(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Discrepancy(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Total Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("BSO", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("Notes", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (BranchAcceptanceViewModel item in list)
            {
                DataRow row = dt.NewRow();
                row[0] = ctr++.ToString();
                row[1] = item.Area.ToString();
                row[2] = item.Driver.ToString();
                row[3] = item.Checker.ToString();
                row[4] = item.PlateNo.ToString();
                row[5] = item.Batch.ToString();
                row[6] = item.AirwayBillNo.ToString();
                row[7] = item.TotalRecieved.ToString();
                row[8] = item.TotalDiscrepency.ToString();
                row[9] = item.Total.ToString();

                row[10] = item.BCO;
                row[11] = item.BSO;
                row[12] = item.ScannedBy;
                row[13] = item.Remarks;
                row[14] = item.Notes;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }
        
        //Filter By BCOID and Driver and ALl batch
        public DataTable getBranchAcceptanceData1(DateTime date, Guid revenueUnitId, string driver)
        {
            BranchAcceptanceBL branchAcceptanceBl = new BranchAcceptanceBL();
            ShipmentBL shipmentService = new ShipmentBL();
            //List<Shipment> shipments = shipmentService.FilterActive().Where(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            //List<BranchAcceptance> branchAcceptance = branchAcceptanceBl.GetAll().Where(x => x.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.BatchID == batchId).ToList();
            List<Shipment> shipments = shipmentService.FilterActive().Where(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.Booking.BookingStatus.BookingStatusName == "Picked-up").ToList();

            //List<BranchAcceptance> branchAcceptance = branchAcceptanceBl.GetAll().Where(x => x.RecordStatus == 1 && x.Users.Employee.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.Users.Employee.AssignedToArea.RevenueUnitId == revenueUnitId && x.Driver == driver && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).Distinct().ToList();
            List<BranchAcceptance> branchAcceptance = branchAcceptanceBl.FilterActive().Where(x => x.RecordStatus == 1 && x.User.Employee.AssignedToArea.RevenueUnitId == revenueUnitId && x.Driver == driver && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).Distinct().ToList();
            List<BranchAcceptanceViewModel> list = Match(branchAcceptance, shipments).FindAll(x => x.Area != "N/A" && !x.Area.Contains("Walk-in"));

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Area/Branch", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Recieved(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Discrepancy(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Total Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("BSO", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("Notes", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (BranchAcceptanceViewModel item in list.Distinct())
            {
                DataRow row = dt.NewRow();
                row[0] = ctr++.ToString();
                row[1] = item.Area.ToString();
                row[2] = item.Driver.ToString();
                row[3] = item.Checker.ToString();
                row[4] = item.PlateNo.ToString();
                row[5] = item.Batch.ToString();
                row[6] = item.AirwayBillNo.ToString();
                row[7] = item.TotalRecieved.ToString();
                row[8] = item.TotalDiscrepency.ToString();
                row[9] = item.Total.ToString();

                row[10] = item.BCO;
                row[11] = item.BSO;
                row[12] = item.ScannedBy;
                row[13] = item.Remarks;
                row[14] = item.Notes;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public List<int> setBranchAcceptanceWidth()
        {
            List<int> width = new List<int>();
            width.Add(25); //No
            width.Add(180); //Area/Branch
            width.Add(150); //Driver
            width.Add(150); //Checker
            width.Add(147); //Plate #
            width.Add(100); //Batch

            width.Add(60); //AWB
            width.Add(95); //Recieved
            width.Add(95); //Dis
            width.Add(90); //Total

            width.Add(0); //BCO
            width.Add(0); //BSO 

            width.Add(110); //Scanned by
            width.Add(0); //Remarks
            width.Add(0); //Notes
            return width;
        }

        public List<BranchAcceptanceViewModel> Match(List<BranchAcceptance> _branchAcceptances, List<Shipment> _shipments)
        {

            PackageNumberBL _packageNumberService = new PackageNumberBL();
            //BranchAcceptanceBL _branchAcceptanceService = new BranchAcceptanceBL();
            List<BranchAcceptanceViewModel> _results = new List<BranchAcceptanceViewModel>();
            List<BranchAcceptanceViewModel> _resultsFilter = new List<BranchAcceptanceViewModel>();
            //UserBL _userService = new UserBL();
            UserStore _userService = new UserStore();
            foreach (Shipment shipment in _shipments)
            {
                BranchAcceptanceViewModel model = new BranchAcceptanceViewModel();
                List<PackageNumber> _packageNumbers = _packageNumberService.FilterActive().Where(x => x.ShipmentId == shipment.ShipmentId).ToList();

                foreach (PackageNumber packagenumber in _packageNumbers)
                {
                    BranchAcceptanceViewModel isAirawayBillExist = _results.Find(x => x.AirwayBillNo == shipment.AirwayBillNo);

                    BranchAcceptance _brachAcceptance = _branchAcceptances.Find(x => x.Cargo == packagenumber.PackageNo);


                    if (_brachAcceptance != null)
                    {
                        if (isAirawayBillExist != null)
                        {
                            isAirawayBillExist.TotalRecieved++;
                            isAirawayBillExist.Total = isAirawayBillExist.TotalRecieved;
                        }
                        else
                        {
                            model.AirwayBillNo = shipment.AirwayBillNo;

                            model.Area = "N/A";
                            if (shipment.Booking != null)
                            {
                                if (shipment.Booking.AssignedToArea != null)
                                {
                                    model.Area = shipment.Booking.AssignedToArea.RevenueUnitName;
                                }
                            }
                            model.Driver = _brachAcceptance.Driver;
                            model.Checker = _brachAcceptance.Checker;
                            model.PlateNo = "N/A";
                            model.Batch = _brachAcceptance.Batch.BatchName;
                            model.TotalRecieved++;
                            model.Total = model.TotalRecieved;
                            model.CreatedBy = _brachAcceptance.CreatedDate;

                            model.BCO = _brachAcceptance.BranchCorpOffice.BranchCorpOfficeName;
                            model.BSO = "N/A";
                            if (shipment.Booking != null) 
                            {
                                if (shipment.Booking.AssignedToArea != null)
                                {
                                    model.BSO = shipment.Booking.AssignedToArea.RevenueUnitName;
                                }
                                    
                            }
                            model.ScannedBy = "N/A";
                            //string employee = _userService.FilterActive().Find(x => x.UserId == _brachAcceptance.CreatedBy).Employee.FullName;
                            string employee = _userService.FindById(_brachAcceptance.CreatedBy).Employee.FullName;
                            if (employee != "")
                            {
                                model.ScannedBy = employee;
                            }
                            
                            model.Remarks = shipment.Remarks;
                            model.Notes = _brachAcceptance.Notes;
                            _results.Add(model);

                        }
                    }
                    else
                    {
                        if (isAirawayBillExist != null)
                        {
                            isAirawayBillExist.TotalDiscrepency++;
                            isAirawayBillExist.Total = isAirawayBillExist.TotalDiscrepency;
                        }
                        else
                        {
                            model.AirwayBillNo = shipment.AirwayBillNo;
                            model.Area = "N/A";
                            if (shipment.Booking != null)
                            {
                                if (shipment.Booking.AssignedToArea != null)
                                {
                                    model.Area = shipment.Booking.AssignedToArea.RevenueUnitName;
                                }
                            }
                            model.Driver = "N/A"; //_brachAcceptance.Driver;
                            model.Checker = "N/A"; //_brachAcceptance.Checker;
                            model.PlateNo = "N/A";
                            model.Batch = "N/A"; //_brachAcceptance.Batch.BatchName;
                            model.TotalDiscrepency++;
                            model.Total = model.TotalDiscrepency;

                            model.BCO = "N/A"; //_brachAcceptance.BranchCorpOffice.BranchCorpOfficeName;
                            //model.BCO = shipment.Booking.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeName;
                            if (shipment.Booking != null)
                            {
                                if (shipment.Booking.AssignedToArea != null)
                                {
                                    model.BCO = shipment.Booking.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeName;
                                }
                            }


                            //model.BSO = "N/A"; //shipment.Booking.AssignedToArea.RevenueUnitName;
                            // model.BCO = _brachAcceptance.BranchCorpOffice.BranchCorpOfficeName;
                            model.BSO = "N/A";
                            if (shipment.Booking != null)
                            {
                                if (shipment.Booking.AssignedToArea != null)
                                {
                                    model.BSO = shipment.Booking.AssignedToArea.RevenueUnitName;
                                }

                            }
                            model.ScannedBy = "N/A";
                            //string employee = _userService.FilterActive().Find(x => x.UserId == _brachAcceptance.CreatedBy).Employee.FullName;
                            //if (employee != "")
                            //{
                            //    model.ScannedBy = employee;
                            //}
                            //model.Remarks = shipment.Remarks;
                            //model.Notes = _brachAcceptance.Notes;
                            _results.Add(model);

                        }
                    }
                }
            }

            //_resultsFilter = _results.GroupBy(d => new
            //{
            //    d.Area,
            //    d.Driver,
            //    d.Checker,
            //    d.PlateNo,
            //    d.Batch,
            //    d.AirwayBillNo,
            //    d.TotalRecieved,
            //    d.TotalDiscrepency,
            //    d.Total,
            //    d.Match,
            //    d.CreatedBy,
            //    d.BCO,
            //    d.BSO,
            //    d.ScannedBy,
            //    d.Remarks,
            //    d.Notes
            //})
            //                    .Select(d => d.First())
            //                    .ToList();
            return _results;
            //return _resultsFilter;

        }
    }
}
