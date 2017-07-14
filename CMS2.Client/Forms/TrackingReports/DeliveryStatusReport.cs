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
    public class DeliveryStatusReport
    {

        public DataTable getData(DateTime date)
        {
            DeliveryBL deliveryStatusBL = new DeliveryBL();
            List<Delivery> list = deliveryStatusBL.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

            List<DeliveryStatusViewModel> modelList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate No", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));

            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (DeliveryStatusViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.AirwayBillNo;
                row[2] = item.QTY.ToString();
                row[3] = item.Status;
                row[4] = item.Remarks;
                row[5] = item.Area;
                row[6] = item.Driver;
                row[7] = item.Checker;
                row[8] = item.PlateNo;
                row[9] = item.Batch;
                row[10] = item.BCO;
                row[11] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public DataTable getDataByAllFilter(DateTime date, string revenueUnitName, Guid? deliveredById, Guid? statusId)
        {

            DeliveryBL deliveryStatusBL = new DeliveryBL();
           
            List <DeliveryStatusViewModel> resultList = new List<DeliveryStatusViewModel>();
            List<DeliveryStatusViewModel> modelList = new List<DeliveryStatusViewModel>();


            List<Delivery> list = deliveryStatusBL.GetAll().Where
                (x => x.RecordStatus == 1
                && ((x.DeliveredBy.EmployeeId == deliveredById && x.DeliveredBy.EmployeeId != Guid.Empty) || (x.DeliveredBy.EmployeeId == x.DeliveredBy.EmployeeId && deliveredById == Guid.Empty))
                && ((x.DeliveryStatus.DeliveryStatusId == statusId && x.DeliveryStatus.DeliveryStatusId != Guid.Empty) || (x.DeliveryStatus.DeliveryStatusId == x.DeliveryStatus.DeliveryStatusId && statusId == Guid.Empty))
                && x.CreatedDate.ToShortDateString() == date.ToShortDateString()
                ).ToList();

            if(revenueUnitName != "All")
            {
                modelList = Match(list).FindAll(x => x.Area == revenueUnitName);
            }
            else
            {
                modelList = Match(list);
            }

            //if (num == 1)
            //{
            //    list = deliveryStatusBL.GetAll().Where
            //        (x => x.RecordStatus == 1 
            //        && x.DeliveredBy.EmployeeId == deliveredById 
            //        && x.DeliveryStatus.DeliveryStatusId == statusId 
            //        //&& x.DeliveredBy.AssignedToArea.RevenueUnitName == revenueUnitName
            //        && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            //    List<DeliveryStatusViewModel> modelList = Match(list);
            //    resultList = modelList.FindAll(x => x.Area == revenueUnitName);

            //}
            //else if(num == 2)
            //{
            //    list = deliveryStatusBL.GetAll().Where(x => x.RecordStatus == 1 && x.DeliveryStatus.DeliveryStatusId == statusId && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            //    List<DeliveryStatusViewModel> modelList = Match(list);
            //    resultList = modelList.FindAll(x => x.Area == revenueUnitName);
            //}
            //else if (num == 3)
            //{
            //    list = deliveryStatusBL.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            //    List<DeliveryStatusViewModel> modelList = Match(list);
            //    resultList = modelList.FindAll(x => x.Area == revenueUnitName);
            //}
            //else if (num == 4)
            //{
            //    list = deliveryStatusBL.GetAll().Where(x => x.RecordStatus == 1 && x.DeliveredBy.EmployeeId == deliveredById && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            //    List<DeliveryStatusViewModel> modelList = Match(list);
            //    resultList = modelList.FindAll(x => x.Area == revenueUnitName);
            //}
            //else if (num == 5)
            //{
            //    list = deliveryStatusBL.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            //    List<DeliveryStatusViewModel> modelList = Match(list);
            //    resultList = modelList;
            //}



            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate No", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));

            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.Columns.Add(new DataColumn("DeliveredBy", typeof(string)));
            dt.Columns.Add(new DataColumn("ReceivedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            //foreach (DeliveryStatusViewModel item in resultList)
            foreach (DeliveryStatusViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.AirwayBillNo;
                row[2] = item.QTY.ToString();
                row[3] = item.Status;
                row[4] = item.Remarks;
                row[5] = item.Area;
                row[6] = item.Driver;
                row[7] = item.Checker;
                row[8] = item.PlateNo;
                row[9] = item.Batch;
                row[10] = item.BCO;
                row[11] = item.ScannedBy;
                row[12] = item.DeliveredBy;
                row[13] = item.ReceivedBy;

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
            width.Add(60);
            width.Add(120);
            width.Add(180);
            width.Add(200);
            width.Add(130);
            width.Add(110);
            width.Add(100);
            width.Add(110);
            width.Add(0);
            width.Add(110);
            width.Add(110);
            return width;
        }

        public List<DeliveryStatusViewModel> Match(List<Delivery> _deliveries)
        {
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
            DeliveryStatusBL status = new DeliveryStatusBL();
            DeliveryRemarkBL remark = new DeliveryRemarkBL();
            DistributionBL distributionService = new DistributionBL();
            DeliveryReceiptBL deliveryReceiptService = new DeliveryReceiptBL();
            ShipmentBL shipmentService = new ShipmentBL();            
            PackageNumberBL _packageNumberService = new PackageNumberBL();

            List<DeliveryStatusViewModel> _results = new List<DeliveryStatusViewModel>();
            
            List<Distribution> distributions = distributionService.GetAll().ToList();

            List<DeliveryReceipt> deliveryReceipt = deliveryReceiptService.GetAll().ToList();
            UserStore _userService = new UserStore();
            foreach (Delivery delivery in _deliveries)
            {               

                DeliveryStatusViewModel model = new DeliveryStatusViewModel();

                DeliveryStatusViewModel isExist = _results.Find(x => x.AirwayBillNo == delivery.Shipment.AirwayBillNo);
                

                if (isExist != null)
                {
                    isExist.QTY++;
                }
                else
                {
                    model.AirwayBillNo = delivery.Shipment.AirwayBillNo;
                    model.QTY = _packageNumberService.FilterActiveBy(x => x.Shipment.AirwayBillNo == delivery.Shipment.AirwayBillNo).Count;
                    model.Status = delivery.DeliveryStatus.DeliveryStatusName;
                    model.Remarks = "NA";
                    model.DeliveredBy = delivery.DeliveredBy.FullName;
                    if (delivery.DeliveryRemarkId != null)
                    {
                        model.Remarks = delivery.DeliveryRemark.DeliveryRemarkName;
                    }
                    Distribution dis = distributions.Find(x => x.ShipmentId == delivery.ShipmentId);
                    DeliveryReceipt dReceipt = deliveryReceipt.Find(x => x.DeliveryId == delivery.DeliveryId);
                    //List<Distribution> list = distributions.Where( x => x.ShipmentId == delivery.ShipmentId).Distinct().ToList();
                    //foreach(Distribution dis in list)
                    //{
                    //    //model.Area = dis.Area.RevenueUnitName;
                    //    model.Driver = dis.Driver;
                    //    model.Checker = dis.Checker;
                    //    model.Batch = dis.Batch.BatchName;
                    //    model.PlateNo = dis.PlateNo;
                    //    model.BCO = dis.Area.City.BranchCorpOffice.BranchCorpOfficeName;
                    //}
                    model.Area = dis.Area.RevenueUnitName;
                    model.Driver = dis.Driver;
                    model.Checker = dis.Checker;
                    model.Batch = dis.Batch.BatchName;
                    model.PlateNo = dis.PlateNo;
                    model.BCO = dis.Area.City.BranchCorpOffice.BranchCorpOfficeName;
                    //model.ScannedBy = AppUser.User.Employee.FullName;
                    model.ScannedBy = "N/A";
                    string employee = _userService.FindById(dis.CreatedBy).Employee.FullName;
                    if (employee != "")
                    {
                        model.ScannedBy = employee;
                    }
                    model.ReceivedBy = "NA";
                    if (dReceipt !=null)
                    {
                        model.ReceivedBy = dReceipt.ReceivedBy;
                    }
                    
                    _results.Add(model);
                }
            }

            //List<BranchCorpOffice> _bco= bcoService.GetAll().Where(x => x.RecordStatus == 1 && x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();
            //string bcoName = bcoService.GetAll().Where(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).Select(x => x.BranchCorpOfficeName).ToString();
            //string bcoName = _bco.Select(x => x.BranchCorpOfficeName).ToString();
            string bcoName = bcoService.GetAll().Find(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).BranchCorpOfficeName;
            List<DeliveryStatusViewModel> _resultsFilter = _results.FindAll(x => x.BCO == bcoName);
            //List<DeliveryStatusViewModel> resultList = modelList.FindAll(x => x.Area == revenueUnitName);
            //return _results;
            return _resultsFilter;

        }
    }
}
