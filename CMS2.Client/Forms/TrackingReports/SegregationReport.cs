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
    public class SegregationReport
    {
        public DataTable getData(DateTime date)
        {
            SegregationBL segregationBL = new SegregationBL();

            List<Segregation> _segregation = segregationBL.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

            List<SegregationViewModel> modelList = Macth(_segregation);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Branch Corp Office", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));

            dt.BeginLoadData();
            int ctr = 1;
            foreach (SegregationViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = ctr++.ToString();
                row[1] = item.OriginBco;
                row[2] = item.Driver;
                row[3] = item.Checker;
                row[4] = item.PlateNo;
                row[5] = item.Batch;
                row[6] = item.AirwayBillNo;
                row[7] = item.Qty.ToString();
                row[8] = item.Area;
                row[10] = item.ScannedBy;

                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public DataTable getSGDatabyFilter(DateTime date, Guid? originbcoid, string driver, string plateno, Guid? batchid)
        {

            SegregationBL segregationBL = new SegregationBL();
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();

            List<SegregationViewModel> modelList = new List<SegregationViewModel>();

            List<Segregation> _segregation = segregationBL.GetAll().Where
                (x => x.RecordStatus == 1
                //&& ((x.PackageNumber.Shipment.OriginCity.BranchCorpOfficeId == originbcoid && x.PackageNumber.Shipment.OriginCity.BranchCorpOfficeId != null) || (x.PackageNumber.Shipment.OriginCity.BranchCorpOfficeId == x.PackageNumber.Shipment.OriginCity.BranchCorpOfficeId && x.PackageNumber.Shipment.OriginCity.BranchCorpOfficeId == null))
                && ((x.Driver == driver && x.Driver != "All") || (x.Driver == x.Driver && driver == "All"))
                && ((x.PlateNo == plateno && x.PlateNo != "All") || (x.PlateNo == x.PlateNo && plateno == "All"))
                && ((x.BatchID == batchid && x.BatchID != null) || (x.BatchID == x.BatchID && batchid == null))
                && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

            string bcoName = "";
            if(originbcoid != null)
            {

                //List<BranchCorpOffice> _bco = bcoService.GetAll().Where(x => x.RecordStatus == 1 && x.BranchCorpOfficeId == originbcoid).ToList();
                //string bcoName = bcoService.GetAll().Where(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).Select(x => x.BranchCorpOfficeName).ToString();
                //string bcoName = _bco.Select(x => x.BranchCorpOfficeName).ToString();
                //string _bco = bcoService.GetAll().Where(x => x.RecordStatus == 1 && x.BranchCorpOfficeId == originbcoid).Select(x => x.BranchCorpOfficeName).ToString();
                bcoName = bcoService.GetAll().Find(x => x.BranchCorpOfficeId == originbcoid).BranchCorpOfficeName;
                modelList = Macth(_segregation).FindAll(x => x.OriginBco == bcoName);
            }
            else
            {
                modelList = Macth(_segregation);
            }

           // List<SegregationViewModel> modelList = Macth(_segregation);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Branch Corp Office", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));

            dt.BeginLoadData();
            int ctr = 1;
            foreach (SegregationViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = ctr++.ToString();
                row[1] = item.OriginBco;
                row[2] = item.Driver;
                row[3] = item.Checker;
                row[4] = item.PlateNo;
                row[5] = item.Batch;
                row[6] = item.AirwayBillNo;
                row[7] = item.Qty.ToString();
                row[8] = item.Area;
                //row[9] = item.CreatedDate.ToShortDateString();
                row[10] = item.ScannedBy;

                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }


        public List<int> setWidth()
        {
            List<int> width = new List<int>();
            width.Add(30); 
            width.Add(180);
            width.Add(150);
            width.Add(150);
            width.Add(150);
            width.Add(120);
            width.Add(120);
            width.Add(100);
            width.Add(200);
            width.Add(0);
            width.Add(100);
            return width;
        }

        public List<SegregationViewModel> Macth(List<Segregation> _segregation)
        {
            List<SegregationViewModel> _results = new List<SegregationViewModel>();
            
            PackageNumberBL _packageNumberService = new PackageNumberBL();

            ShipmentBL shipmentService = new ShipmentBL();
            UserStore _userService = new UserStore();
            foreach (Segregation segregation in _segregation)
            {
                SegregationViewModel model = new SegregationViewModel();
                string _airwaybill = "";
                try {
                    _airwaybill = _packageNumberService.GetAll().Find(x => x.PackageNo == segregation.Cargo).Shipment.AirwayBillNo;
                }
                catch (Exception) { continue; }
                SegregationViewModel isExist = _results.Find(x => x.AirwayBillNo == _airwaybill);

                if (isExist != null)
                {
                    isExist.Qty++;
                }
                else
                {
                    model.OriginBco = segregation.BranchCorpOffice.BranchCorpOfficeName;
                    model.Driver = segregation.Driver;
                    model.Checker = segregation.Checker;
                    model.PlateNo = segregation.PlateNo;
                    model.Batch = segregation.Batch.BatchName;
                    model.AirwayBillNo = _airwaybill;
                    model.Qty++;
                    model.Area = shipmentService.GetAll().Find(x => x.AirwayBillNo == _airwaybill).DestinationCity.CityName;
                    //model.CreatedDate = segregation.CreatedDate;
                    //model.ScannedBy = AppUser.User.Employee.FullName;
                    model.ScannedBy = "N/A";
                    string employee = _userService.FindById(segregation.CreatedBy).Employee.FullName;
                    if (employee != "")
                    {
                        model.ScannedBy = employee;
                    }
                    _results.Add(model);
                }
            }

            return _results;
        }

    }
}
