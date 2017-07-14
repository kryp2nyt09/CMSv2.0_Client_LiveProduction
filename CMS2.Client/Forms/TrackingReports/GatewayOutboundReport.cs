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
    class GatewayOutboundReport
    {
        public DataTable getData(DateTime date)
        {

            GatewayOutboundBL gatewayOutboundBl = new GatewayOutboundBL();
            GatewayInboundBL gatewayInboundBl = new GatewayInboundBL();

            List<GatewayOutbound> Outboundlist = gatewayOutboundBl.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).GroupBy(x => x.Cargo).Select(y => y.First()).ToList();
            List<GatewayInbound> Inboundlist = gatewayInboundBl.GetAll().GroupBy(x => x.Cargo).Select(y => y.First()).ToList();

            List<GatewayOutboundViewModel> modelList = Match(Inboundlist, Outboundlist);

            //modelList.GroupBy(x => x.AirwayBillNo).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Gateway", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));

            dt.Columns.Add(new DataColumn("AWB", typeof(string)));

            dt.Columns.Add(new DataColumn("Recieved(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Discrepancy(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Total Qty", typeof(string)));

            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Branch", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));

            dt.BeginLoadData();

            int ctr = 1;
            foreach (GatewayOutboundViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.Gateway.ToString();
                row[2] = item.Driver.ToString();
                row[3] = item.PlateNo.ToString();
                row[4] = item.Batch;
                row[5] = item.AirwayBillNo.ToString();
                row[6] = item.TotalRecieved.ToString();
                row[7] = item.TotalDiscrepency.ToString();
                row[8] = item.Total.ToString();
                row[10] = item.Destination;
                row[11] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public DataTable getGODatabyFilter(DateTime date, Guid? bcoid, string driver, string gateway, Guid? batchid, Guid? commodityTypeId, string mawb, int num)
        {

            GatewayOutboundBL gatewayOutboundBl = new GatewayOutboundBL();
            GatewayInboundBL gatewayInboundBl = new GatewayInboundBL();
            CommodityTypeBL comtypeService = new CommodityTypeBL();
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();


            List<GatewayInbound> Inboundlist = gatewayInboundBl.GetAll().GroupBy(x => x.Cargo).Select(y => y.First()).ToList();
            List<GatewayOutbound> Outboundlist = new List<GatewayOutbound>();
            List<GatewayOutboundViewModel> modelList = new List<GatewayOutboundViewModel>();
            if (num == 0)
            {
                Outboundlist = gatewayOutboundBl.GetAll().Where
                    (x => x.RecordStatus == 1 
                    //&& x.CreatedDate.ToShortDateString() == date.ToShortDateString() 
                    && x.MasterAirwayBill == mawb).GroupBy(x => x.Cargo).Select(y => y.First()).ToList();

            }
            else if(num == 1)
            {
                Outboundlist = gatewayOutboundBl.GetAll().Where
               (x => x.RecordStatus == 1
               //&& ((x.PackageNumber.Shipment.DestinationCity.BranchCorpOfficeId == bcoid && x.PackageNumber.Shipment.DestinationCity.BranchCorpOfficeId != null) || (x.PackageNumber.Shipment.DestinationCity.BranchCorpOfficeId == x.PackageNumber.Shipment.DestinationCity.BranchCorpOfficeId && x.PackageNumber.Shipment.DestinationCity.BranchCorpOfficeId == null))
               && ((x.Driver == driver && x.Driver != "All") || (x.Driver == x.Driver && driver == "All"))
               && ((x.Gateway == gateway && x.Gateway != "All") || (x.Gateway == x.Gateway && gateway == "All"))
               && ((x.BatchID == batchid && x.BatchID != Guid.Empty) || (x.BatchID == x.BatchID && batchid == Guid.Empty))
               && x.CreatedDate.ToShortDateString() == date.ToShortDateString()
               && ((x.MasterAirwayBill == mawb && x.MasterAirwayBill != "") || (x.MasterAirwayBill == x.MasterAirwayBill && mawb == ""))
               ).GroupBy(x => x.Cargo).Select(y => y.First()).ToList();
            }
            string comType = "";
            string _bco = "";

            if (commodityTypeId != Guid.Empty && bcoid != Guid.Empty)
            {
                //string comType = comtypeService.GetAll().Where(x => x.RecordStatus == 1 && x.CommodityTypeId == commodityTypeId).Select(x => x.CommodityTypeName).ToString();
                //string _bco = bcoService.GetAll().Where(x => x.RecordStatus == 1 && x.BranchCorpOfficeId == bcoid).Select(x => x.BranchCorpOfficeName).ToString();
                //List<CommodityType> _ctype = comtypeService.FilterActiveBy(x => x.CommodityTypeId == commodityTypeId).ToList();
                //string comType = _ctype.Select(x => x.CommodityTypeName).ToString();

                //List<BranchCorpOffice> _branch = bcoService.FilterActiveBy(x => x.BranchCorpOfficeId == bcoid).ToList();
                //string _bco = _branch.Select(x => x.BranchCorpOfficeName).ToString();

                comType = comtypeService.GetAll().Find(x => x.CommodityTypeId == commodityTypeId).CommodityTypeName;
                _bco = bcoService.GetAll().Find(x => x.BranchCorpOfficeId == bcoid).BranchCorpOfficeName;

                modelList = Match(Inboundlist, Outboundlist).FindAll(x => x.CommodityType == comType && x.Destination == _bco);
            }
            else if (commodityTypeId != Guid.Empty && bcoid == Guid.Empty)
            {
                //string comType = comtypeService.GetAll().Where(x => x.RecordStatus == 1 && x.CommodityTypeId == commodityTypeId).Select(x => x.CommodityTypeName).ToString();
                //List<CommodityType> _ctype = comtypeService.FilterActiveBy(x => x.CommodityTypeId == commodityTypeId).ToList();
                //string comType = _ctype.Select(x => x.CommodityTypeName).ToString();
                comType = comtypeService.GetAll().Find(x => x.CommodityTypeId == commodityTypeId).CommodityTypeName;
                modelList = Match(Inboundlist, Outboundlist).FindAll(x => x.CommodityType == comType);
            }
            else if(commodityTypeId == Guid.Empty && bcoid != Guid.Empty)
            {
                //string _bco = bcoService.GetAll().Where(x => x.RecordStatus == 1 && x.BranchCorpOfficeId == bcoid).Select(x => x.BranchCorpOfficeName).ToString();
                //List<BranchCorpOffice> _branch = bcoService.FilterActiveBy(x => x.BranchCorpOfficeId == bcoid).ToList();
                //string _bco = _branch.Select(x => x.BranchCorpOfficeName).ToString();
                _bco = bcoService.GetAll().Find(x => x.BranchCorpOfficeId == bcoid).BranchCorpOfficeName;
                modelList = Match(Inboundlist, Outboundlist).FindAll(x => x.Destination == _bco);
            }
            else
            {
                modelList = Match(Inboundlist, Outboundlist);
            }

            

            //modelList.GroupBy(x => x.AirwayBillNo).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Gateway", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));

            dt.Columns.Add(new DataColumn("AWB", typeof(string)));

            dt.Columns.Add(new DataColumn("Recieved(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Discrepancy(Qty)", typeof(string)));
            dt.Columns.Add(new DataColumn("Total Qty", typeof(string)));

            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));
            dt.Columns.Add(new DataColumn("Branch", typeof(string)));

            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));

            dt.BeginLoadData();

            int ctr = 1;
            foreach (GatewayOutboundViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.Gateway.ToString();
                row[2] = item.Driver.ToString();
                row[3] = item.PlateNo.ToString();
                row[4] = item.Batch;
                row[5] = item.AirwayBillNo.ToString();
                row[6] = item.TotalRecieved.ToString();
                row[7] = item.TotalDiscrepency.ToString();
                row[8] = item.Total.ToString();
                row[10] = item.Destination;
                row[11] = item.ScannedBy;
                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }




        public List<int> setWidth()
        {
            List<int> width = new List<int>();
            width.Add(25);
            width.Add(220);
            width.Add(150);
            width.Add(110);
            width.Add(110);
            width.Add(150);
            width.Add(120);

            width.Add(120);
            width.Add(120);
            width.Add(0);
            width.Add(0);
            width.Add(120);
            return width;
        }

        public List<GatewayOutboundViewModel> Match(List<GatewayInbound> _inbound , List<GatewayOutbound> _outbound) {

            PackageNumberBL _packageNumberService = new PackageNumberBL();
            List<GatewayOutboundViewModel> _results = new List<GatewayOutboundViewModel>();
            PackageNumber _packageNumber = new PackageNumber();
            List<string> listCargo = new List<string>();
            BundleBL bundleService = new BundleBL();
            UserStore _userService = new UserStore();
            foreach (GatewayOutbound outbound in _outbound)
            {

                GatewayOutboundViewModel model = new GatewayOutboundViewModel();
                string _airwaybill = "";
                try {
                    //_airwaybill = _packageNumberService.GetAll().Find(x => x.PackageNo == outbound.Cargo).Shipment.AirwayBillNo;
                    _packageNumber = _packageNumberService.FilterActive().Where(x => x.PackageNo == outbound.Cargo).FirstOrDefault();
                    if (_packageNumber == null)
                    {
                        GatewayOutboundViewModel model1 = new GatewayOutboundViewModel();
                        listCargo = bundleService.GetAll().Where(x => x.SackNo == outbound.Cargo).Select(y => y.Cargo).ToList();
                        if (listCargo != null && listCargo.Count != 0)
                        {
                            GatewayOutboundViewModel isExist = _results.Find(x => x.AirwayBillNo == outbound.Cargo);
                            if (_inbound.Exists(x => x.Cargo == outbound.Cargo))
                            {
                                if (isExist != null)
                                {
                                    isExist.TotalRecieved++;
                                    isExist.Total = isExist.TotalRecieved;
                                }
                                else
                                {
                                    model1.AirwayBillNo = outbound.Cargo;
                                    model1.Gateway = outbound.Gateway;
                                    model1.Driver = outbound.Driver;
                                    model1.PlateNo = outbound.PlateNo;
                                    model1.Batch = outbound.Batch.BatchName;
                                    model1.TotalRecieved = listCargo.Count;
                                    model1.Total = model1.TotalRecieved;
                                    model1.Destination = outbound.BranchCorpOffice.BranchCorpOfficeName;
                                    // model1.ScannedBy = AppUser.User.Employee.FullName;
                                    model1.ScannedBy = "N/A";
                                    //string employee = _userService.FilterActive().Find(x => x.UserId == _bundle.CreatedBy).Employee.FullName;
                                    string employee = _userService.FindById(outbound.CreatedBy).Employee.FullName;
                                    if (employee != "")
                                    {
                                        model1.ScannedBy = employee;
                                    }
                                    model1.CommodityType = "N/A";
                                    //model1.CommodityTypeName = _packageNumberService.FilterActiveBy(x => x.PackageNo == outbound.Cargo).First().Shipment.CommodityType.CommodityTypeName;
                                    _results.Add(model1);
                                }
                            }
                            else
                            {
                                if (isExist != null)
                                {
                                    isExist.TotalDiscrepency++;
                                    isExist.Total = isExist.TotalDiscrepency;
                                }
                                else
                                {
                                    model1.AirwayBillNo = outbound.Cargo;
                                    model1.Gateway = outbound.Gateway;
                                    model1.Driver = outbound.Driver;
                                    model1.PlateNo = outbound.PlateNo;
                                    model1.Batch = outbound.Batch.BatchName;
                                    model1.TotalDiscrepency = listCargo.Count;
                                    model1.Total = model1.TotalDiscrepency;
                                    model1.Destination = outbound.BranchCorpOffice.BranchCorpOfficeName;
                                    //model1.ScannedBy = "N/A";
                                    string employee = _userService.FindById(outbound.CreatedBy).Employee.FullName;
                                    if (employee != "")
                                    {
                                        model1.ScannedBy = employee;
                                    }
                                    model1.CommodityType = "N/A";
                                    //model1.CommodityTypeName = _packageNumberService.FilterActiveBy(x => x.PackageNo == outbound.Cargo).First().Shipment.CommodityType.CommodityTypeName;
                                    _results.Add(model1);
                                }
                            }
                        }
                    }
                    else
                    {
                        _airwaybill = _packageNumber.Shipment.AirwayBillNo;
                        GatewayOutboundViewModel isExist = _results.Find(x => x.AirwayBillNo == _airwaybill);
                        if (_inbound.Exists(x => x.Cargo == outbound.Cargo))
                        {
                            if (isExist != null)
                            {
                                isExist.TotalRecieved++;
                                isExist.Total = isExist.TotalRecieved;
                                //model.Total = model.TotalRecieved;
                                //_results.Add(isExist);
                            }
                            else
                            {
                                model.AirwayBillNo = _airwaybill;
                                model.Gateway = outbound.Gateway;
                                model.Driver = outbound.Driver;
                                model.PlateNo = outbound.PlateNo;
                                model.Batch = outbound.Batch.BatchName;
                                model.TotalRecieved++;
                                model.Total = model.TotalRecieved;
                                model.Destination = outbound.BranchCorpOffice.BranchCorpOfficeName;
                                //model.ScannedBy = AppUser.User.Employee.FullName;
                                string employee = _userService.FindById(outbound.CreatedBy).Employee.FullName;
                                if (employee != "")
                                {
                                    model.ScannedBy = employee;
                                }
                                //model.CommodityTypeName = _inbound.Where(x => x.Cargo == outbound.Cargo).Select(x => x.CommodityType.CommodityTypeName).ToString();
                                // model.CommodityTypeName = _inbound.Find(x => x.Cargo == outbound.Cargo).CommodityType.CommodityTypeName;
                                model.CommodityType = _packageNumberService.FilterActiveBy(x => x.PackageNo == outbound.Cargo).First().Shipment.CommodityType.CommodityTypeName;
                                _results.Add(model);

                            }
                        }
                        else
                        {
                            if (isExist != null)
                            {
                                isExist.TotalDiscrepency++;
                                isExist.Total = isExist.TotalDiscrepency;
                                //model.Total = model.TotalDiscrepency;
                                //_results.Add(isExist);
                            }
                            else
                            {
                                model.AirwayBillNo = _airwaybill;
                                model.Gateway = outbound.Gateway;
                                model.Driver = outbound.Driver;
                                model.PlateNo = outbound.PlateNo;
                                model.Batch = outbound.Batch.BatchName;
                                model.TotalDiscrepency++;
                                model.Total = model.TotalDiscrepency;
                                model.Destination = outbound.BranchCorpOffice.BranchCorpOfficeName;
                                string employee = _userService.FindById(outbound.CreatedBy).Employee.FullName;
                                if (employee != "")
                                {
                                    model.ScannedBy = employee;
                                }
                                //model.CommodityTypeName = _inbound.Find(x => x.Cargo == outbound.Cargo).CommodityType.CommodityTypeName;
                                // model.CommodityTypeName = _inbound.Where(x => x.Cargo == outbound.Cargo).Select(x => x.CommodityType.CommodityTypeName).ToString();
                                model.CommodityType = _packageNumberService.FilterActiveBy(x => x.PackageNo == outbound.Cargo).First().Shipment.CommodityType.CommodityTypeName;
                                _results.Add(model);

                            }
                        }
                    }
                }
                catch (Exception) { continue;  }
               

            }


            return _results;
        }
    }
}
