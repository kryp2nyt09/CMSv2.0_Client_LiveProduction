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
    public class CargoTransferReport
    {
        public DataTable getData(DateTime date)
        {

            CargoTransferBL cargoTransferBl = new CargoTransferBL();

            List<CargoTransfer> list = cargoTransferBl.GetAll().Where(x => x.RecordStatus == 1 && x.CreatedDate.ToShortDateString() == date.ToShortDateString()).GroupBy(x => x.Cargo).Select(y => y.First()).ToList();
         
            List<CargoTransferViewModel> modelList = Match(list);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("Destination", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));

            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));

            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("GATEWAY", typeof(string)));
            dt.Columns.Add(new DataColumn("SATELLITE", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (CargoTransferViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.OriginArea;
                row[2] = item.DestinationBco;
                row[3] = item.Driver;
                row[4] = item.Checker;
                row[5] = item.Pieces;
                row[6] = item.PlateNo;
                row[7] = item.Batch;
                row[8] = item.AirwayBillNo;
                row[9] = item.QTY;
                //row[10] = item.CreatedDate.ToShortDateString();

                row[11] = item.DestinationArea;
                //row[12] = item.GATEWAY;
                //row[13] = item.SATELLITE;

                row[14] = item.ScannedBy;

                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }


        public DataTable getCTDataByFilter(DateTime date, Guid? destinationId, Guid? revenueunitId, string plateno, Guid? batchId)
        {
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
            CargoTransferBL cargoTransferBl = new CargoTransferBL();

            List<CargoTransferViewModel> modelList = new List<CargoTransferViewModel>();
            List<CargoTransfer> list = cargoTransferBl.GetAll().Where
                (x => x.RecordStatus == 1
                && ((x.BranchCorpOfficeID == destinationId && x.BranchCorpOfficeID != Guid.Empty) || (x.BranchCorpOfficeID == x.BranchCorpOfficeID && destinationId == Guid.Empty))
                && ((x.RevenueUnitID == revenueunitId && x.RevenueUnitID != Guid.Empty) || (x.RevenueUnitID == x.RevenueUnitID && revenueunitId == Guid.Empty))
                && ((x.BatchID == batchId && x.BatchID != Guid.Empty) || (x.BatchID == x.BatchID && batchId == Guid.Empty))
                && ((x.PlateNo == plateno && x.PlateNo != "All") || (x.PlateNo == x.PlateNo && plateno == "All"))
                && x.CreatedDate.ToShortDateString() == date.ToShortDateString()
                ).GroupBy(x => x.Cargo).Select(y => y.First()).ToList();

            //List<CargoTransferViewModel> modelList = Match(list);

            string _bco = "";
            if (destinationId != Guid.Empty)
            {
                _bco = bcoService.GetAll().Find(x => x.BranchCorpOfficeId == destinationId).BranchCorpOfficeName;
                modelList = Match(list).FindAll(x => x.DestinationArea == _bco);
            }
            else
            {
                modelList = Match(list);
            }

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("Destination", typeof(string)));
            dt.Columns.Add(new DataColumn("Driver", typeof(string)));
            dt.Columns.Add(new DataColumn("Checker", typeof(string)));
            dt.Columns.Add(new DataColumn("Pieces", typeof(string)));
            dt.Columns.Add(new DataColumn("Plate #", typeof(string)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));

            dt.Columns.Add(new DataColumn("CreatedDate", typeof(string)));

            dt.Columns.Add(new DataColumn("BCO", typeof(string)));
            dt.Columns.Add(new DataColumn("GATEWAY", typeof(string)));
            dt.Columns.Add(new DataColumn("SATELLITE", typeof(string)));
            dt.Columns.Add(new DataColumn("ScannedBy", typeof(string)));
            dt.BeginLoadData();
            int ctr = 1;
            foreach (CargoTransferViewModel item in modelList)
            {
                DataRow row = dt.NewRow();
                row[0] = (ctr++).ToString();
                row[1] = item.OriginArea;
                row[2] = item.DestinationBco;
                row[3] = item.Driver;
                row[4] = item.Checker;
                row[5] = item.Pieces;
                row[6] = item.PlateNo;
                row[7] = item.Batch;
                row[8] = item.AirwayBillNo;
                row[9] = item.QTY;
                row[11] = item.DestinationArea;
                row[14] = item.ScannedBy;

                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            return dt;
        }

        public List<int> setWidth()
        {
            List<int> width = new List<int>();
            width.Add(25);
            width.Add(190);
            width.Add(190);
            width.Add(100);
            width.Add(100);
            width.Add(0); //Pieces
            width.Add(100); //PlateNo
            width.Add(90);//Batch

            width.Add(100);//AWB
            width.Add(100);//Qty
            width.Add(0);

            width.Add(0);
            width.Add(0);
            width.Add(0);
            width.Add(0);

            return width;
        }

        public List<CargoTransferViewModel> Match(List<CargoTransfer> _cargoTransfers) {

            PackageNumberBL _packageNumberService = new PackageNumberBL();
            ShipmentBL shipment = new ShipmentBL();
            List<CargoTransferViewModel> _results = new List<CargoTransferViewModel>();
            BundleBL bundleService = new BundleBL();
            PackageNumber _packageNumber = new PackageNumber();
            List<string> listCargo = new List<string>();
            UserStore _userService = new UserStore();
            foreach (CargoTransfer cargoTransfer in _cargoTransfers)
            {
                CargoTransferViewModel model = new CargoTransferViewModel();
                string _airwaybill = "";
                try {
                    // _airwaybill = _packageNumberService.GetAll().Find(x => x.PackageNo == cargoTransfer.Cargo).Shipment.AirwayBillNo;
                    _packageNumber = _packageNumberService.FilterActive().Where(x => x.PackageNo == cargoTransfer.Cargo).FirstOrDefault();
                    if(_packageNumber == null)
                    {
                        CargoTransferViewModel model1 = new CargoTransferViewModel();
                        listCargo = bundleService.GetAll().Where(x => x.SackNo == cargoTransfer.Cargo).Select(y => y.Cargo).ToList();
                        if (listCargo != null && listCargo.Count != 0)
                        {
                            CargoTransferViewModel isExist = _results.Find(x => x.AirwayBillNo == cargoTransfer.Cargo);
                            if (isExist != null)
                            {
                                isExist.QTY++;
                                model.Pieces++;
                            }
                            else
                            {
                                //List<Shipment> list = shipment.GetAll().Where(x => x.AirwayBillNo.Equals(_airwaybill)).ToList();
                                //model1.Origin = _airwaybill;
                                //foreach (Shipment x in list)
                                //{
                                //    model1.Origin = x.OriginCity.CityName;
                                //    model1.Destination = x.DestinationCity.CityName;
                                //}
                                model1.OriginArea = "N/A";
                                model1.DestinationBco = "N/A";
                                model1.Driver = cargoTransfer.Driver;
                                model1.Checker = cargoTransfer.Checker;
                                model1.Pieces= listCargo.Count;
                                model1.PlateNo = cargoTransfer.PlateNo;
                                model1.Batch = cargoTransfer.Batch.BatchName;
                                model1.AirwayBillNo = cargoTransfer.Cargo;
                                model1.QTY= listCargo.Count;
                                //model1.CreatedDate = cargoTransfer.CreatedDate;
                                model1.DestinationArea = cargoTransfer.DestinationBco.BranchCorpOfficeName;
                                //model1.GATEWAY = cargoTransfer.DestinationArea.RevenueUnitName;
                                //model1.SATELLITE = cargoTransfer.DestinationArea.RevenueUnitName;
                                //model1.ScannedBy = AppUser.User.Employee.FullName;
                                model1.ScannedBy = "N/A";
                                string employee = _userService.FindById(cargoTransfer.CreatedBy).Employee.FullName;
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
                        CargoTransferViewModel isExist = _results.Find(x => x.AirwayBillNo == _airwaybill);

                        if (isExist != null)
                        {
                            isExist.QTY++;
                            model.Pieces++;
                            //_results.Add(isExist);
                        }
                        else
                        {
                            List<Shipment> list = shipment.GetAll().Where(x => x.AirwayBillNo.Equals(_airwaybill)).ToList();
                            model.OriginArea = _airwaybill;
                            foreach (Shipment x in list)
                            {
                                model.OriginArea = x.OriginCity.CityName;
                                model.DestinationBco = x.DestinationCity.CityName;
                                model.DestinationArea = x.DestinationCity.BranchCorpOffice.BranchCorpOfficeName;
                            }
                            model.Driver = cargoTransfer.Driver;
                            model.Checker = cargoTransfer.Checker;
                            model.Pieces++;
                            model.PlateNo = cargoTransfer.PlateNo;
                            model.Batch = cargoTransfer.Batch.BatchName;
                            model.AirwayBillNo = _airwaybill;
                            model.QTY++;
                            //model.CreatedDate = cargoTransfer.CreatedDate;

                            // model.BCO = cargoTransfer.BranchCorpOffice.BranchCorpOfficeName;
                            //model.BCO = cargoTransfer.BranchCorpOffice.BranchCorpOfficeName;
                            //model.GATEWAY = cargoTransfer.TransferToRevenueUnitName;
                            //model.SATELLITE = cargoTransfer.TransferToRevenueUnitName;
                            //model.ScannedBy = AppUser.User.Employee.FullName;
                            model.ScannedBy = "N/A";
                            string employee = _userService.FindById(cargoTransfer.CreatedBy).Employee.FullName;
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
