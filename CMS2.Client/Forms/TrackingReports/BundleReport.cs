
using CMS2.BusinessLogic;
using CMS2.Entities;
using CMS2.Entities.ReportModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace CMS2.Client.Forms.TrackingReports
{
    public class BundleReport
    {
        public DataTable getBundleData(DateTime date)
        {
            BundleBL bundleBl = new BundleBL();

            List<Bundle> list = bundleBl.FilterActive().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();                        
            List<BundleViewModel> bundleList = Match(list);
            
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipper", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Commodity Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Commodity", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("AGW", typeof(string)));
            dt.Columns.Add(new DataColumn("Service Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));
            dt.Columns.Add(new DataColumn("SackNo", typeof(string)));

            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Destination", typeof(string)));

            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("BSO", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));

            dt.BeginLoadData();
            int ctr = 1;
            foreach (BundleViewModel item in bundleList)
            {
                DataRow row = dt.NewRow();
                row[0] = "" + ctr++;
                row[1] = item.AirwayBillNo;
                row[2] = item.Shipper;
                row[3] = item.Consignee;
                row[4] = item.Address;
                row[5] = item.CommodityType;
                row[6] = item.Commodity;
                row[7] = item.Qty.ToString();
                row[8] = item.AGW.ToString();
                row[9] = item.ServiceMode;
                row[10] = item.PaymentMode;
                row[12] = item.SackNo;

                row[15] = item.DestinationBCO;
                row[17] = item.Scannedby;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public DataTable getBundleDataByFilter(DateTime date, Guid? bcoid, string sackNo, int num)
        {
            BundleBL bundleBl = new BundleBL();

            List<Bundle> list = new List<Bundle>();

            if(num == 0)
            {
               list = bundleBl.GetAll().Where(x => x.RecordStatus == 1 && x.SackNo == sackNo).ToList();
            }
            else if (num == 1)
            {
                list = bundleBl.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 2)
            {
                // list = bundleBl.GetAll().Where(x => x.RecordStatus == 1 && x.User.Employee.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == bcoid && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
                list = bundleBl.GetAll().Where(x => x.RecordStatus == 1 && x.DestinationBcoId == bcoid && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

            }

            List<BundleViewModel> bundleList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipper", typeof(string)));
            dt.Columns.Add(new DataColumn("Consignee", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Commodity Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Commodity", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("AGW", typeof(string)));
            dt.Columns.Add(new DataColumn("Service Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Mode", typeof(string)));
            dt.Columns.Add(new DataColumn("Area", typeof(string)));
            dt.Columns.Add(new DataColumn("SackNo", typeof(string)));

            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Destination", typeof(string)));

            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("BSO", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));

            dt.BeginLoadData();
            int ctr = 1;
            foreach (BundleViewModel item in bundleList)
            {
                DataRow row = dt.NewRow();
                row[0] = "" + ctr++;
                row[1] = item.AirwayBillNo;
                row[2] = item.Shipper;
                row[3] = item.Consignee;
                row[4] = item.Address;
                row[5] = item.CommodityType;
                row[6] = item.Commodity;
                row[7] = item.Qty.ToString();
                row[8] = item.AGW.ToString();
                row[9] = item.ServiceMode;
                row[10] = item.PaymentMode;
                row[12] = item.SackNo;
                row[15] = item.DestinationBCO;

                row[17] = item.Scannedby;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public List<int> setBundleWidth()
        {
            List<int> width = new List<int>();
            width.Add(30);  // No
            width.Add(110); // AWB
            width.Add(110); // Shipper
            width.Add(110); // Consignee
            width.Add(210); // Address
            width.Add(110); // Com Type
            width.Add(110); // Com
            width.Add(60);  // Qty
            width.Add(80);  // AGW
            width.Add(110); // Service Mode
            width.Add(110); // Payment Mode
                            
            width.Add(0);   // Area
            width.Add(110);   // Sack No
            width.Add(0);   // Create Date
            width.Add(0);   // Destination
                            
            width.Add(0);   // BCO
            width.Add(0);   // BSO

            width.Add(110); // Scanned By

            return width;
        }

        public List<BundleViewModel> Match(List<Bundle> bundle) {

            PackageNumberBL _packageNumberService = new PackageNumberBL();
            List<BundleViewModel> _results = new List<BundleViewModel>();
            ShipmentBL shipment = new ShipmentBL();
            //UserBL _userService = new UserBL();
            UserStore _userService = new UserStore();
            foreach (Bundle _bundle in bundle) {
                BundleViewModel model = new BundleViewModel();
                string _airwaybill = "";
                try {
                    _airwaybill = _packageNumberService.FilterActive().Find(x => x.PackageNo == _bundle.Cargo).Shipment.AirwayBillNo;
                }catch(Exception) { continue; }

                BundleViewModel isExist = _results.Find(x => x.AirwayBillNo == _airwaybill);
                
                if (isExist != null)
                {
                    isExist.Qty++;
                    //_results.Add(isExist);
                }
                else
                {
                    model.AirwayBillNo = _airwaybill;
                    List<Shipment> list = shipment.FilterActive().Where(x => x.AirwayBillNo == _airwaybill).ToList();                    
                    foreach (Shipment ship in list)
                    {
                        model.Shipper = ship.Shipper.FullName;
                        model.Consignee = ship.Consignee.FullName;
                        model.Address = ship.Consignee.Address1;
                        model.CommodityType = ship.Commodity.CommodityType.CommodityTypeName;
                        model.Commodity = ship.Commodity.CommodityName;
                        model.Qty++;
                        model.AGW += ship.Weight;
                        model.ServiceMode = ship.ServiceMode.ServiceModeName;
                        model.PaymentMode = ship.PaymentMode.PaymentModeName;                 
              
                    }
                    model.SackNo = _bundle.SackNo;
                    model.DestinationBCO = _bundle.DestinationBco;
                    //model.Scannedby = AppUser.User.Employee.FullName;
                    model.Scannedby = "N/A";
                    //string employee = _userService.FilterActive().Find(x => x.UserId == _bundle.CreatedBy).Employee.FullName;
                    string employee = _userService.FindById(_bundle.CreatedBy).Employee.FullName;
                    if (employee != "")
                    {
                        model.Scannedby = employee;
                    }
                    _results.Add(model);
                }
           }

            return _results;
        }
    }
}
