using CMS2.BusinessLogic;
using CMS2.Common;
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
    public class UnbundleReport
    {
        public DataTable getUnBundleData(DateTime date)
        {
            UnbundleBL unbundlebl = new UnbundleBL();
            BundleBL bundlebl = new BundleBL();

            List<Unbundle> unbundleList = unbundlebl.FilterActive().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            List<Bundle> bundleList = bundlebl.FilterActive().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

            List<UnbundleViewModel> modelList = Match(unbundleList , bundleList);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Sack No", typeof(string)));
            dt.Columns.Add(new DataColumn("Total Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("Scanned Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("Discrepancy Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("Weight", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Branch", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (UnbundleViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = "" + ctr++;
                row[1] = item.SackNo;
                row[2] = item.TotalPcs;
                row[3] = item.ScannedPcs;
                row[4] = item.TotalDiscrepency;
                row[5] = item.Origin;
                row[6] = item.Weight;
                row[7] = item.AirwayBillNo;
                row[10] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }
       
        public DataTable getUnbundleDataByFilter(DateTime date, Guid? bcoid, string sackNo, int num)
        {
            UnbundleBL unbundlebl = new UnbundleBL();
            BundleBL bundlebl = new BundleBL();
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
            List<Unbundle> unbundleList = new List<Unbundle>();
            List<Bundle> bundleList = bundlebl.FilterActive().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            List<UnbundleViewModel> modelList = new List<UnbundleViewModel>();

            string sackNum = "";
            sackNum = sackNo;
            string bcoName = "";
            if (bcoid !=null && bcoid != Guid.Empty)
            {
                bcoName = bcoService.FilterActive().Find(x => x.BranchCorpOfficeId == bcoid).BranchCorpOfficeName;
            }

            if (num == 0)
            {
                //unbundleList = unbundlebl.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString() && x.SackNo == sackNo).ToList();
                unbundleList = unbundlebl.FilterActive().Where(x => x.RecordStatus == 1 && x.SackNo == sackNo).ToList();
                modelList = Match(unbundleList, bundleList).FindAll(x => x.SackNo == sackNum);
                //bundleList = bundlebl.GetAll().Where(x => x.RecordStatus == 1 && x.BranchCorpOfficeID == GlobalVars.DeviceBcoId && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if(num ==1)
            {
                unbundleList = unbundlebl.FilterActive().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
                modelList = Match(unbundleList, bundleList);
            }
            else if(num == 2)
            {
                unbundleList = unbundlebl.FilterActive().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
                modelList = Match(unbundleList, bundleList).FindAll(x => x.Origin == bcoName);
            }
            
           

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Sack No", typeof(string)));
            dt.Columns.Add(new DataColumn("Total Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("Scanned Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("Discrepancy Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("Weight", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Branch", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (UnbundleViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = "" + ctr++;
                row[1] = item.SackNo;
                row[2] = item.TotalPcs;
                row[3] = item.ScannedPcs;
                row[4] = item.TotalDiscrepency;
                row[5] = item.Origin;
                row[6] = item.Weight;
                //row[7] = item.AirwayBillNo;
                //row[8] = item.CreatedDate;
                //row[9] = item.Branch;
                row[10] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public List<int> setBundleWidth()
        {
            List<int> width = new List<int>();

            width.Add(25);
            width.Add(115);
            width.Add(110);
            width.Add(110);
            width.Add(110);
            width.Add(190);
            width.Add(100);            
            width.Add(90);
            width.Add(0);
            width.Add(260);
            width.Add(260);

            return width;
        }
        public List<UnbundleViewModel> Match(List<Unbundle> _unbundle , List<Bundle> _bundle)
        {
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
            PackageNumberBL _packageNumberService = new PackageNumberBL();
            List<UnbundleViewModel> _results = new List<UnbundleViewModel>();
            ShipmentBL shipment = new ShipmentBL();
            Shipment _shipment = new Shipment();
            UserStore _userService = new UserStore();
            //string bcoName = bcoService.FilterActive().Find(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).BranchCorpOfficeName;
            string bcoName = "";
            Guid bcoid;
            Guid userid;
            foreach (Bundle bundle in _bundle){
                UnbundleViewModel model = new UnbundleViewModel();
                try
                {
                    _shipment = _packageNumberService.FilterActive().Find(x => x.PackageNo == bundle.Cargo).Shipment;
                }
                catch (Exception ex)
                {
                    Logs.ErrorLogs("", "Unbundle Match", ex.Message);
                    continue;
                }
                UnbundleViewModel isExist = _results.Find(x => x.AirwayBillNo == _shipment.AirwayBillNo);

                if (_unbundle.Exists(x => x.Cargo == bundle.Cargo))
                {
                    if (isExist != null)
                    {
                        isExist.ScannedPcs++;
                        //isExist.TotalPcs += isExist.ScannedPcs;
                        isExist.TotalPcs = isExist.ScannedPcs;
                    }

                    else
                    {
                        model.AirwayBillNo = _shipment.AirwayBillNo;
                        model.SackNo = bundle.SackNo;
                        model.ScannedPcs++;
                        model.Weight += bundle.Weight;
                        model.TotalPcs = model.ScannedPcs;
                        model.Origin = _shipment.OriginCity.CityName;
                        //model.CreatedDate = bundle.CreatedDate;
                        //model.Branch = "N/A";
                        bcoid = _unbundle.Find(x => x.Cargo == bundle.Cargo).OriginBcoID;
                        if(bcoid != null && bcoid != Guid.Empty)
                        {
                            bcoName = bcoService.FilterActive().Find(x => x.BranchCorpOfficeId == bcoid).BranchCorpOfficeName;
                            //model.Branch = bcoName;
                        }
                        //model.ScannedBy = AppUser.User.Employee.FullName;
                        model.ScannedBy = "N/A";
                        userid = _unbundle.Find(x => x.Cargo == bundle.Cargo).CreatedBy;
                        string employee = _userService.FindById(userid).Employee.FullName;
                        if (employee != "")
                        {
                            model.ScannedBy = employee;
                        }
                        _results.Add(model);

                    }
                }
                else
                {

                    if (isExist != null)
                    {
                        isExist.TotalDiscrepency++;
                        //isExist.TotalPcs += isExist.TotalDiscrepency;
                        isExist.TotalPcs = isExist.TotalDiscrepency;
                    }

                    else
                    {
                        model.AirwayBillNo = _shipment.AirwayBillNo;
                        model.SackNo = bundle.SackNo;
                        model.TotalDiscrepency++;
                        model.TotalPcs = model.TotalDiscrepency;
                        model.Weight += bundle.Weight;

                        model.Origin = _shipment.OriginCity.CityName;
                        //model.CreatedDate = bundle.CreatedDate;
                        //model.Branch = "N/A";
                        bcoName = _packageNumberService.FilterActive().Find(x => x.PackageNo == bundle.Cargo).Shipment.OriginCity.BranchCorpOffice.BranchCorpOfficeName;
                        if (bcoName != "")
                        {
                            //bcoName = bcoService.FilterActive().Find(x => x.BranchCorpOfficeId == bcoid).BranchCorpOfficeName;
                            //model.Branch = bcoName;
                        }
                        //model.Branch = bundle.BranchCorpOffice.BranchCorpOfficeName;
                        model.ScannedBy = "N/A";
                        _results.Add(model);
                    }
                }
            }
            return _results;
        }





    }
}
