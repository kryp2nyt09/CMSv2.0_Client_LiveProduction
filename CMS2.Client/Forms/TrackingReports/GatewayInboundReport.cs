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
    public class GatewayInboundReport
    {
        public DataTable getData(DateTime date)
        {
            GatewayInboundBL gatewayInboundBl = new GatewayInboundBL();
            List<GatewayInbound> list = list = gatewayInboundBl.GetAll().Where(x => x.OriginBco.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            //List<GatewayInbound> list = list = gatewayInboundBl.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();

            List<GatewayInboundViewModel> modelList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Gateway", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("MAWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Flight #", typeof(string)));
            dt.Columns.Add(new DataColumn("Commodity Type", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (GatewayInboundViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.Gateway;
                row[2] = item.Origin;
                row[3] = item.Pieces;
                row[4] = item.MAWB;
                row[5] = item.FlightNo;
                row[6] = item.CommodityType;
                row[7] = item.AirwayBillNo;
                row[8] = item.CreatedDate.ToShortDateString();
                row[9] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public DataTable getDatabyFilter(DateTime date, string gatewayName, Guid? bcoid, Guid? commodityTypeId, string flightNumber, string mawb, int num)
        {
            GatewayInboundBL gatewayInboundBl = new GatewayInboundBL();
            //List<GatewayInbound> list = list = gatewayInboundBl.GetAll().Where(x => x.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            List<GatewayInbound> list = new List<GatewayInbound>();
            if (num == 0)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.MasterAirwayBill == mawb && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if(num == 1)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.Gateway == gatewayName && x.OriginBcoID == bcoid && x.CommodityID == commodityTypeId && x.FlightNumber == flightNumber && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 2)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.Gateway == gatewayName && x.CommodityID == commodityTypeId && x.FlightNumber == flightNumber && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 3)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.Gateway == gatewayName && x.FlightNumber == flightNumber && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 4)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.Gateway == gatewayName && x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 5)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.RecordStatus == 1 && x.OriginBcoID == bcoid && x.CommodityID == commodityTypeId && x.FlightNumber == flightNumber && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 6)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.RecordStatus == 1 && x.OriginBcoID == bcoid && x.FlightNumber == flightNumber && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 7)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.RecordStatus == 1 && x.OriginBcoID == bcoid && x.CommodityID == commodityTypeId && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 8)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.RecordStatus == 1 && x.OriginBcoID == bcoid && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 9)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.RecordStatus == 1 && x.Gateway == gatewayName && x.OriginBcoID == bcoid && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            else if (num == 10)
            {
                list = gatewayInboundBl.GetAll().Where(x => x.RecordStatus == 1 && x.Gateway == gatewayName && x.OriginBcoID == bcoid && x.FlightNumber == flightNumber && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }

            List<GatewayInboundViewModel> modelList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Gateway", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("MAWB", typeof(string)));
            dt.Columns.Add(new DataColumn("Flight #", typeof(string)));
            dt.Columns.Add(new DataColumn("Commodity Type", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (GatewayInboundViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.Gateway;
                row[2] = item.Origin;
                row[3] = item.Pieces;
                row[4] = item.MAWB;
                row[5] = item.FlightNo;
                row[6] = item.CommodityType;
                row[7] = item.AirwayBillNo;
                row[8] = item.CreatedDate.ToShortDateString();
                row[9] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public List<int> setWidth()
        {
            List<int> width = new List<int>();
            width.Add(25);
            width.Add(200);
            width.Add(200);
            width.Add(100);
            width.Add(100);
            width.Add(100);
            width.Add(200);
            width.Add(100);
            width.Add(0);
            width.Add(100);
            return width;
        }

        public List<GatewayInboundViewModel> Match(List<GatewayInbound> _inbound)
        {
            
            PackageNumberBL _packageNumberService = new PackageNumberBL();
            List<GatewayInboundViewModel> _results = new List<GatewayInboundViewModel>();
            PackageNumber _packageNumber = new PackageNumber();
            BundleBL bundleService = new BundleBL();
            List<string> listCargo = new List<string>();
            UserStore _userService = new UserStore();

            foreach (GatewayInbound inbound in _inbound)
            {
                GatewayInboundViewModel model = new GatewayInboundViewModel();
                string _airwaybill = "";
                try {
                    // _airwaybill = _packageNumberService.GetAll().Find(x => x.PackageNo == inbound.Cargo).Shipment.AirwayBillNo;
                    _packageNumber = _packageNumberService.FilterActive().Where(x => x.PackageNo == inbound.Cargo).FirstOrDefault();
                    if (_packageNumber == null)
                    {
                        GatewayInboundViewModel model1 = new GatewayInboundViewModel();
                        listCargo = bundleService.GetAll().Where(x => x.SackNo == inbound.Cargo).Select(y => y.Cargo).ToList();
                        if(listCargo !=null && listCargo.Count != 0)
                        {
                            GatewayInboundViewModel isExist = _results.Find(x => x.AirwayBillNo == inbound.Cargo);
                            if (isExist != null)
                            {
                                isExist.Pieces++;
                            }
                            else
                            {
                                model1.AirwayBillNo = inbound.Cargo;
                                model1.Gateway = inbound.Gateway;
                                model1.Origin = inbound.OriginBco.BranchCorpOfficeName;
                                model1.Pieces= listCargo.Count;
                                model1.MAWB = inbound.MasterAirwayBill;
                                model1.FlightNo = inbound.FlightNumber;
                                model1.CommodityType = inbound.CommodityType.CommodityTypeName;
                                model1.CreatedDate = inbound.CreatedDate;
                                //model1.ScannedBy = AppUser.User.Employee.FullName;
                                model1.ScannedBy = "N/A";
                                string employee = _userService.FindById(inbound.CreatedBy).Employee.FullName;
                                if (employee != "")
                                {
                                    model1.ScannedBy = employee;
                                }
                                _results.Add(model1);

                            }
                        }
                    }
                    else
                    {
                        _airwaybill = _packageNumber.Shipment.AirwayBillNo;

                        GatewayInboundViewModel isExist = _results.Find(x => x.AirwayBillNo == _airwaybill);
                        if (isExist != null)
                        {
                            isExist.Pieces++;
                        }
                        else
                        {
                            model.AirwayBillNo = _airwaybill;
                            model.Gateway = inbound.Gateway;
                            model.Origin = inbound.OriginBco.BranchCorpOfficeName;
                            model.Pieces++;
                            model.MAWB = inbound.MasterAirwayBill;
                            model.FlightNo = inbound.FlightNumber;
                            model.CommodityType = inbound.CommodityType.CommodityTypeName;
                            model.CreatedDate = inbound.CreatedDate;
                            //model.ScannedBy = AppUser.User.Employee.FullName;
                            model.ScannedBy = "N/A";
                            string employee = _userService.FindById(inbound.CreatedBy).Employee.FullName;
                            if (employee != "")
                            {
                                model.ScannedBy = employee;
                            }
                            _results.Add(model);

                        }
                    }

                }
                catch (Exception) { continue; }
                
            }


            return _results;
        }
    }
}
