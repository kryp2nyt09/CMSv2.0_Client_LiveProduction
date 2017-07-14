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
    public class DailyTripReport
    {

        public DataTable getDailyTripDatabyAllFilter(DateTime date, Guid? areaId, Guid? batchId, Guid? paymentModeid, int num)
        {
            DistributionBL distributionService = new DistributionBL();
            List<Distribution> list = new List<Distribution>();
            if (num == 1)
            {
                list = distributionService.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString() && x.Area.RevenueUnitId == areaId && x.Batch.BatchID == batchId && x.PaymentMode.PaymentModeId == paymentModeid).ToList();
            }
            else if(num == 2)
            {
                list = distributionService.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString() && x.Area.RevenueUnitId == areaId && x.Batch.BatchID == batchId).ToList();
            }
            else if(num ==3)
            {
                list = distributionService.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString() && x.Area.RevenueUnitId == areaId && x.PaymentMode.PaymentModeId == paymentModeid).ToList();
            }
            else if (num == 4)
            {
                list = distributionService.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString() && x.Area.RevenueUnitId == areaId).ToList();
            }
            else if(num == 5)
            {
                list = distributionService.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString() && x.Area.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();
            }

           List<DailyTripViewModel> modelList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB/SOA", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(int)));
            dt.Columns.Add(new DataColumn("AGW", typeof(string)));
            dt.Columns.Add(new DataColumn("Service Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));

            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));

            dt.Columns.Add(new DataColumn("BCO", typeof(string)));

            dt.Columns.Add(new DataColumn("PaymentCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));

            dt.BeginLoadData();
            int ctr = 1;
            foreach (DailyTripViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.AirwayBillNo;
                row[2] = item.Consignee;
                row[3] = item.Address;
                row[4] = item.QTY;
                row[5] = item.AGW;
                row[6] = item.ServiceMode;
                row[7] = item.PaymentMode;
                row[8] = item.Amount;
                row[9] = item.Area;
                row[10] = item.Driver;
                row[11] = item.Checker;
                row[12] = item.BCO;
                row[13] = item.PaymentCode;
                row[14] = item.Scannedby;
                row[15] = item.Batch;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public DataTable getData(DateTime date)
        {
            DistributionBL shipmentService = new DistributionBL();
            List<Distribution> list = shipmentService.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString() && x.Area.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();

            List<DailyTripViewModel> modelList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB/SOA", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(int)));
            dt.Columns.Add(new DataColumn("AGW", typeof(string)));
            dt.Columns.Add(new DataColumn("Service Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));

            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            
            dt.Columns.Add(new DataColumn("BCO", typeof(string)));

            dt.Columns.Add(new DataColumn("PaymentCode", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));

            dt.BeginLoadData();
            int ctr = 1;
            foreach (DailyTripViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.AirwayBillNo;
                row[2] = item.Consignee;
                row[3] = item.Address;
                row[4] = item.QTY;
                row[5] = item.AGW;
                row[6] = item.ServiceMode;
                row[7] = item.PaymentMode;
                row[8] = item.Amount;
                row[9] = item.Area;
                row[10] = item.Driver;
                row[11] = item.Checker;
                row[12] = item.BCO;
                row[13] = item.PaymentCode;
                row[14] = item.Scannedby;
                row[15] = item.Batch;
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
            width.Add(220);
            width.Add(200);
            width.Add(80);
            width.Add(100);
            width.Add(140);
            width.Add(140);
            width.Add(100);
            width.Add(0);
            width.Add(0);
            width.Add(0);
            width.Add(0);
            width.Add(0);
            return width;
        }

        public List<DailyTripViewModel> Match(List<Distribution> _distribution)
        {
            List<DailyTripViewModel> _results = new List<DailyTripViewModel>();
            PackageNumberBL _packageNumberService = new PackageNumberBL();
            UserStore _userService = new UserStore();
           // ShipmentBL shipmentService = new ShipmentBL();
            foreach (Distribution distribution in _distribution) {
                DailyTripViewModel model = new DailyTripViewModel();
                string _airwaybill = "";
                try {
                    _airwaybill = _packageNumberService.GetAll().Find(x => x.ShipmentId == distribution.ShipmentId).Shipment.AirwayBillNo;
                }
                catch (Exception) { continue; }
                DailyTripViewModel isExist = _results.Find(x => x.AirwayBillNo == _airwaybill);

                if (isExist != null)
                {
                    isExist.QTY++;
                    isExist.Amount += distribution.Amount;
                }
                else
                {
                    model.AirwayBillNo = _airwaybill;
                    model.QTY++;
                    model.Consignee = distribution.Consignee.FullName;
                    model.Address = distribution.Consignee.Address1 + " " + distribution.Consignee.Address2;
                    model.AGW += distribution.Shipment.Weight;
                    model.ServiceMode = distribution.ServiceMode.ServiceModeName;
                    model.PaymentMode = distribution.PaymentMode.PaymentModeName;
                    model.Amount += distribution.Amount;
                    //model.Area = distribution.Area.RevenueUnitName;
                   
                    model.Driver = distribution.Driver;
                    model.Checker = distribution.Checker;
                    //model.BCO = distribution.Area.
                    model.PaymentCode = distribution.PaymentMode.PaymentModeCode;
                    //model.Scannedby = AppUser.User.Employee.FullName;
                    model.Scannedby = "N/A";
                    string employee = _userService.FindById(distribution.CreatedBy).Employee.FullName;
                    if (employee != "")
                    {
                        model.Scannedby = employee;
                    }
                    model.Batch = distribution.Batch.BatchName;
                    _results.Add(model);
                }

            }

            return _results;

        }

    }
}
