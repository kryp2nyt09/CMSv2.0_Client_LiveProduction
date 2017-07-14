using CMS2.DataAccess;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using CMS2.Common.Constants;
using CMS2.Common.Enums;

namespace CMS2.BusinessLogic
{
    public class TntMaintBL : BaseAPCargoBL<TntMaint>
    {
        //private ICmsUoW _unitOfWork;

        //public TntMaintBL()
        //{
        //    _unitOfWork = GetUnitOfWork();
        //}

        //public TntMaintBL(ICmsUoW unitOfWork)
        //    : base(unitOfWork)
        //{

        //}

        //public void UpdateCms()
        //{
        //    TrackNTraceContext _trackingContext = new TrackNTraceContext();
        //    PackageNumberBL packageNumberService = new PackageNumberBL();
        //    UserStore userService = new UserStore();

        //    #region TransmittalStatus
        //    TransmittalStatusBL transmittalStatusService = new TransmittalStatusBL();
        //    var _status = _trackingContext.status.OrderBy(x => x.nIdentity).ToList();
        //    if (_status != null)
        //    {
        //        var user = userService.FindByNameAsync("admin").Result;
        //        foreach (var item in _status)
        //        {
        //            if (
        //                transmittalStatusService.IsExist(
        //                    x =>
        //                        x.TransmittalStatusName.Equals(item.cStatusName) &&
        //                        x.TransmittalStatusCode.Equals(item.cStatusCode)))
        //            {
        //            }
        //            else
        //            {
        //                TransmittalStatus model = new TransmittalStatus();
        //                model.TransmittalStatusId = Guid.NewGuid();
        //                model.TransmittalStatusCode = item.cStatusCode;
        //                model.TransmittalStatusName = item.cStatusName;
        //                model.CreatedBy = user.Id;
        //                model.CreatedDate = DateTime.Now;
        //                model.ModifiedBy = user.Id;
        //                model.ModifiedDate = DateTime.Now;
        //                model.RecordStatus = (int) RecordStatus.Active;
        //                transmittalStatusService.Add(model);
        //            }
        //        }
        //    }
        //    #endregion

        //    #region Bundle
        //    //BundleBL bundleService = new BundleBL();
        //    //var _bundles = _trackingContext.bundles.Where(x=>x.dDateTime.Value.Year==2016).OrderBy(x => x.nIdentity).ToList();
        //    //if (_bundles != null)
        //    //{
        //    //    foreach (var item in _bundles)
        //    //    {
        //    //        var user = userService.FindByNameAsync(item.cUser).Result;
        //    //        var currentPackage = packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).FirstOrDefault();
        //    //        if (currentPackage != null && currentPackage.BundleId == null)
        //    //        {
        //    //            var packages =
        //    //                packageNumberService.FilterBy(x => x.ShipmentId == currentPackage.ShipmentId).ToList();
        //    //            if (packages.Any(x => x.BundleId != null))
        //    //            {
        //    //                currentPackage.BundleId = packages.FirstOrDefault(x => x.BundleId != null).BundleId;
        //    //                packageNumberService.Edit(currentPackage);
        //    //            }
        //    //            else
        //    //            {
        //    //                Bundle model = new Bundle();
        //    //                model.BundleId = Guid.NewGuid();
        //    //                model.BundleDate = item.dDateTime ?? DateTime.Now;
        //    //                model.BundleNo = item.cSackNo;
        //    //                model.BundleById = user.EmployeeId;
        //    //                model.CreatedBy = user.Id;
        //    //                model.CreatedDate = item.dDateTime ?? DateTime.Now;
        //    //                model.ModifiedBy = user.Id;
        //    //                model.ModifiedDate = item.dDateTime ?? DateTime.Now;
        //    //                model.RecordStatus = (int)RecordStatus.Active;
        //    //                bundleService.Add(model);
        //    //                currentPackage.BundleId = model.BundleId;
        //    //                packageNumberService.Add(currentPackage);
        //    //            }
        //    //        }
        //    //    }
        //    //}

        //    #endregion

        //}

        //public void UpdateTransfer()
        //{
        //    TrackNTraceContext _trackingContext = new TrackNTraceContext();
        //    PackageNumberBL packageNumberService = new PackageNumberBL();
        //    PackageTransferBL packageTransferService = new PackageTransferBL();
        //    List<PackageTransfer> transferModels = new List<PackageTransfer>();
        //    PackageNumberTransferBL packageNumberTransferService = new PackageNumberTransferBL();
        //    UserStore userService = new UserStore();
        //    BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
        //    EmployeeBL employeeService = new EmployeeBL();
        //    //GatewayBL gatewayService = new GatewayBL();
        //    var _transfers = _trackingContext.transfers.OrderBy(x => x.nIdentity).ToList();
        //    if (_transfers != null)
        //    {
        //        Guid previousShipmentId = new Guid();
        //        Guid currentShipmentId = new Guid();
        //        Guid currentTransferId = new Guid();
        //        foreach (var item in _transfers)
        //        {
        //            if (!packageNumberTransferService.IsExistInTransfer(item.cCargo))
        //            {
        //                try
        //                {
        //                    currentShipmentId = packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).FirstOrDefault().ShipmentId;
        //                    if (previousShipmentId != currentShipmentId)
        //                    {
        //                        previousShipmentId = currentShipmentId;
        //                        PackageTransfer model = new PackageTransfer();
        //                        model.PackageTransferId = Guid.NewGuid();
        //                        model.TransferDate = item.dDateTime ?? DateTime.Now;
        //                        model.Remarks = item.cRemarks;
        //                        model.CreatedDate = item.dDateTime ?? DateTime.Now;
        //                        model.ModifiedDate = item.dDateTime ?? DateTime.Now;
        //                        model.RecordStatus = 1;

        //                        var user = userService.FindByNameAsync(item.cUser).Result;
        //                        model.CreatedBy = user.Id;
        //                        model.ModifiedBy = user.Id;
        //                        model.ScannedById = user.EmployeeId;

        //                        //EmployeePositionMapping userAssignment = employeePositionService.GetByEmployeeDate(user.EmployeeId, item.dDateTime ?? DateTime.Now);
        //                        Employee userAssignment = employeeService.GetById(user.EmployeeId);
        //                        if (userAssignment == null)
        //                            userAssignment = new Employee();
        //                        else
        //                        {
        //                            var revenueunit = userAssignment.AssignedToArea;
        //                            if (item.cTransferTo.Equals("Branch"))
        //                            {

        //                                model.TransferType = TransferTypeConstant.BsoToBco;
        //                                model.TransferFromId = revenueunit.RevenueUnitId;
        //                                model.TransferToId =
        //                                    bcoService.FilterBy(x => x.BranchCorpOfficeName.Equals(item.cBranchAirlines))
        //                                        .FirstOrDefault()
        //                                        .BranchCorpOfficeId;
        //                            }
        //                            else if (item.cTransferTo.Equals("Airlines"))
        //                            {
        //                                model.TransferType = TransferTypeConstant.BcoToGateWay;
        //                                model.TransferFromId =
        //                                    revenueunit.City.BranchCorpOffice.BranchCorpOfficeId;
        //                                //model.TransferToId =
        //                                //    gatewayService.FilterBy(x => x.GatewayName.Equals(item.cBranchAirlines))
        //                                //        .FirstOrDefault()
        //                                //        .GatewayId;
        //                            }
        //                            List<Employee> employeeAssignments = employeeService.GetAll().Where(x => x.Position.PositionName == "Driver").ToList();
        //                            if (employeeAssignments != null)
        //                            {
        //                                string employeeName = "";
        //                                string driverName = item.cDriver;
        //                                foreach (var assign in employeeAssignments)
        //                                {
        //                                    employeeName = (assign.FirstName.Substring(0, 1) + "." + assign.LastName).ToUpper().Trim();
        //                                    if (employeeName.Equals(driverName))
        //                                    {
        //                                        model.DriverId = assign.EmployeeId;
        //                                        model.PackageNumberTransfers = new List<PackageNumberTransfer>();
        //                                        PackageNumberTransfer _model = new PackageNumberTransfer();
        //                                        _model.PackageNumberTransferId = Guid.NewGuid();
        //                                        _model.PackageTransferId = model.PackageTransferId;
        //                                        _model.PackageNumberId =
        //                                            packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo))
        //                                                .FirstOrDefault()
        //                                                .PackageNumberId;
        //                                        _model.CreatedBy = model.CreatedBy;
        //                                        _model.CreatedDate = model.CreatedDate;
        //                                        _model.ModifiedBy = model.ModifiedBy;
        //                                        _model.ModifiedDate = model.ModifiedDate;
        //                                        _model.RecordStatus = model.RecordStatus;
        //                                        model.PackageNumberTransfers.Add(_model);
        //                                        transferModels.Add(model);
        //                                        currentTransferId = model.PackageTransferId;
        //                                        packageTransferService.Add(model);
        //                                    }
        //                                    // may not need this logic
        //                                    //if (employeeName.Equals(item.cChecker) &&
        //                                    //    assign.Position.PositionName.Equals("Field Rep"))
        //                                    //{
        //                                    //    model.ScannedById = assign.Employee.EmployeeId;
        //                                    //}
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        PackageTransfer model = transferModels.FirstOrDefault(x => x.PackageTransferId == currentTransferId);
        //                        PackageNumberTransfer _model = new PackageNumberTransfer();
        //                        _model.PackageNumberTransferId = Guid.NewGuid();
        //                        _model.PackageTransferId = model.PackageTransferId;
        //                        _model.PackageNumberId =
        //                            packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo))
        //                                .FirstOrDefault()
        //                                .PackageNumberId;
        //                        _model.CreatedBy = model.CreatedBy;
        //                        _model.CreatedDate = model.CreatedDate;
        //                        _model.ModifiedBy = model.ModifiedBy;
        //                        _model.ModifiedDate = model.ModifiedDate;
        //                        _model.RecordStatus = model.RecordStatus;
        //                        model.PackageNumberTransfers.Add(_model);
        //                        packageNumberTransferService.Add(_model);
        //                    }
        //                }
        //                catch (Exception ex)
        //                { }
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets Acceptance data from TNT and insert to CMS
        ///// </summary>
        ///// <param name="date"></param>
        //public void AcceptanceAreaBco(DateTime date)
        //{
        //    TrackNTraceContext _trackingContext = new TrackNTraceContext();
        //    //TransferAcceptanceBL transferAcceptanceService = new TransferAcceptanceBL();
        //    List<TransferAcceptance> acceptanceModels = new List<TransferAcceptance>();
        //    PackageNumberAcceptanceBL packageNumberAcceptanceService = new PackageNumberAcceptanceBL();
        //    PackageNumberBL packageNumberService = new PackageNumberBL();
        //    UserStore userService = new UserStore();
        //    EmployeeBL employeeService = new EmployeeBL();
        //    RevenueUnit revenueUnit;
        //    var _bcoAcceptance = _trackingContext.branchacceptances.Where(x => (x.dDateTime.Value.Year == date.Year && x.dDateTime.Value.Month == date.Month && x.dDateTime.Value.Day == date.Day)).OrderBy(x => x.nIdentity).ToList();
        //    if (_bcoAcceptance != null)
        //    {
        //        Guid previousShipmentId = new Guid();
        //        Guid currentShipmentId = new Guid();
        //        Guid currentAcceptanceId = new Guid();
        //        foreach (var item in _bcoAcceptance)
        //        {
        //            if (!packageNumberAcceptanceService.IsExistInAcceptance(item.cCargo, AcceptanceTypeConstant.AreaToBco))
        //            {
        //                try
        //                {
        //                    currentShipmentId = packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).FirstOrDefault().ShipmentId;
        //                    if (previousShipmentId != currentShipmentId)
        //                    {
        //                        previousShipmentId = currentShipmentId;
        //                        TransferAcceptance model = new TransferAcceptance();
        //                        model.TransferAcceptanceId = Guid.NewGuid();
        //                        model.AcceptanceDate = item.dDateTime ?? DateTime.Now;
        //                        model.Remarks = item.cRemarks;
        //                        model.CreatedDate = item.dDateTime ?? DateTime.Now;
        //                        model.ModifiedDate = item.dDateTime ?? DateTime.Now;
        //                        model.RecordStatus = 1;

        //                        var user = userService.FindByNameAsync(item.cUser).Result;
        //                        model.CreatedBy = user.Id;
        //                        model.ModifiedBy = user.Id;
        //                        model.TransferType = AcceptanceTypeConstant.AreaToBco;

        //                        revenueUnit = employeeService.GetById(user.EmployeeId).AssignedToArea;
        //                        model.TransferToId = revenueUnit.City.BranchCorpOffice.BranchCorpOfficeId;


        //                        List<Employee> driverAssignments = employeeService.GetAll().Where(x => x.Position.PositionName == "Driver").ToList();
        //                        if (driverAssignments != null)
        //                        {
        //                            string employeeName = "";
        //                            string driverName = item.cDriver;
        //                            foreach (var assign in driverAssignments)
        //                            {
        //                                employeeName =
        //                                    (assign.FirstName.Substring(0, 1) + "." + assign.LastName)
        //                                        .ToUpper().Trim();
        //                                if (employeeName.Equals(driverName))
        //                                {
        //                                    model.DriverId = assign.EmployeeId;
        //                                    revenueUnit = assign.AssignedToArea;
        //                                    model.TransferFromId = revenueUnit.RevenueUnitId;
        //                                }
        //                            }
        //                        }
        //                        List<Employee> fieldRepAssignments = employeeService.GetAll().Where(x => x.Position.PositionName == "Driver").ToList();
        //                        if (fieldRepAssignments != null)
        //                        {
        //                            string employeeName = "";
        //                            string checkerName = item.cChecker;
        //                            foreach (var assign in fieldRepAssignments)
        //                            {
        //                                employeeName =
        //                                    (assign.FirstName.Substring(0, 1) + "." + assign.LastName)
        //                                        .ToUpper().Trim();
        //                                if (employeeName.Equals(checkerName))
        //                                {
        //                                    model.ScannedById = assign.EmployeeId;
        //                                }
        //                            }
        //                        }
        //                        if (model.DriverId != Guid.Empty || model.ScannedById != Guid.Empty)
        //                        {
        //                            model.PackageNumberAcceptances = new List<PackageNumberAcceptance>();
        //                            PackageNumberAcceptance _model = new PackageNumberAcceptance();
        //                            _model.PackageNumberAcceptanceId = Guid.NewGuid();
        //                            _model.TransferAcceptanceId = model.TransferAcceptanceId;
        //                            _model.PackageNumberId =
        //                                packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo))
        //                                    .FirstOrDefault()
        //                                    .PackageNumberId;
        //                            _model.CreatedBy = model.CreatedBy;
        //                            _model.CreatedDate = model.CreatedDate;
        //                            _model.ModifiedBy = model.ModifiedBy;
        //                            _model.ModifiedDate = model.ModifiedDate;
        //                            _model.RecordStatus = model.RecordStatus;
        //                            model.PackageNumberAcceptances.Add(_model);
        //                            acceptanceModels.Add(model);
        //                            currentAcceptanceId = model.TransferAcceptanceId;
        //                            //transferAcceptanceService.Add(model);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        TransferAcceptance model = acceptanceModels.FirstOrDefault(x => x.TransferAcceptanceId == currentAcceptanceId);
        //                        PackageNumberAcceptance _model = new PackageNumberAcceptance();
        //                        _model.PackageNumberAcceptanceId = Guid.NewGuid();
        //                        _model.TransferAcceptanceId = model.TransferAcceptanceId;
        //                        _model.PackageNumberId =
        //                            packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo))
        //                                .FirstOrDefault()
        //                                .PackageNumberId;
        //                        _model.CreatedBy = model.CreatedBy;
        //                        _model.CreatedDate = model.CreatedDate;
        //                        _model.ModifiedBy = model.ModifiedBy;
        //                        _model.ModifiedDate = model.ModifiedDate;
        //                        _model.RecordStatus = model.RecordStatus;
        //                        model.PackageNumberAcceptances.Add(_model);
        //                        packageNumberAcceptanceService.Add(_model);
        //                    }
        //                }
        //                catch (Exception ex)
        //                { }
        //            }
        //        }
        //    }
        //}

        //public void AcceptanceBcoGateway(DateTime date)
        //{
        //    TrackNTraceContext _trackingContext = new TrackNTraceContext();
        //    //TransferAcceptanceBL transferAcceptanceService = new TransferAcceptanceBL();
        //    List<TransferAcceptance> acceptanceModels = new List<TransferAcceptance>();
        //    PackageNumberAcceptanceBL packageNumberAcceptanceService = new PackageNumberAcceptanceBL();
        //    PackageNumberBL packageNumberService = new PackageNumberBL();
        //    UserStore userService = new UserStore();
        //    EmployeeBL employeeService = new EmployeeBL();
        //    //GatewayBL gatewayService = new GatewayBL();

        //    var _gatewayAcceptance = _trackingContext.gatewayacceptances.Where(x => (x.dDateTime.Value.Year == date.Year && x.dDateTime.Value.Month == date.Month && x.dDateTime.Value.Day == date.Day)).OrderBy(x => x.nIdentity).ToList();
        //    if (_gatewayAcceptance != null)
        //    {
        //        Guid previousShipmentId = new Guid();
        //        Guid currentShipmentId = new Guid();
        //        Guid currentAcceptanceId = new Guid();
        //        foreach (var item in _gatewayAcceptance)
        //        {
        //            if (!packageNumberAcceptanceService.IsExistInAcceptance(item.cCargo, AcceptanceTypeConstant.BcoToGateWay))
        //            {
        //                try
        //                {
        //                    currentShipmentId = packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).FirstOrDefault().ShipmentId;
        //                    if (previousShipmentId != currentShipmentId)
        //                    {
        //                        previousShipmentId = currentShipmentId;
        //                        TransferAcceptance model = new TransferAcceptance();
        //                        model.TransferAcceptanceId = Guid.NewGuid();
        //                        model.AcceptanceDate = item.dDateTime ?? DateTime.Now;
        //                        model.Remarks = item.cRemarks;
        //                        model.CreatedDate = item.dDateTime ?? DateTime.Now;
        //                        model.ModifiedDate = item.dDateTime ?? DateTime.Now;
        //                        model.RecordStatus = 1;

        //                        var user = userService.FindByNameAsync(item.cUser).Result;
        //                        model.CreatedBy = user.Id;
        //                        model.ModifiedBy = user.Id;
        //                        model.ScannedById = user.EmployeeId;
        //                        model.TransferType = AcceptanceTypeConstant.BcoToGateWay;

        //                        List<Employee> driverAssignments = employeeService.GetAll().Where(x => x.Position.PositionName == "Driver").ToList();
        //                        if (driverAssignments != null)
        //                        {
        //                            string employeeName = "";
        //                            string driverName = item.cDriver;
        //                            foreach (var assign in driverAssignments)
        //                            {
        //                                employeeName = (assign.FirstName.Substring(0, 1) + "." + assign.LastName).ToUpper().Trim();
        //                                if (employeeName.Equals(driverName))
        //                                {
        //                                    var revenueunit = assign.AssignedToArea;
        //                                    model.DriverId = assign.EmployeeId;
        //                                    model.TransferFromId = revenueunit.City.BranchCorpOffice.BranchCorpOfficeId;
        //                                    //model.TransferToId = gatewayService.FilterBy(x => x.GatewayName.Equals(item.cGateway)).FirstOrDefault().GatewayId;
        //                                    model.PackageNumberAcceptances = new List<PackageNumberAcceptance>();
        //                                    PackageNumberAcceptance _model = new PackageNumberAcceptance();
        //                                    _model.PackageNumberAcceptanceId = Guid.NewGuid();
        //                                    _model.TransferAcceptanceId = model.TransferAcceptanceId;
        //                                    _model.PackageNumberId =
        //                                        packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo))
        //                                            .FirstOrDefault()
        //                                            .PackageNumberId;
        //                                    _model.CreatedBy = model.CreatedBy;
        //                                    _model.CreatedDate = model.CreatedDate;
        //                                    _model.ModifiedBy = model.ModifiedBy;
        //                                    _model.ModifiedDate = model.ModifiedDate;
        //                                    _model.RecordStatus = model.RecordStatus;
        //                                    model.PackageNumberAcceptances.Add(_model);
        //                                    acceptanceModels.Add(model);
        //                                    currentAcceptanceId = model.TransferAcceptanceId;
        //                                    //transferAcceptanceService.Add(model);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        TransferAcceptance model = acceptanceModels.FirstOrDefault(x => x.TransferAcceptanceId == currentAcceptanceId);
        //                        PackageNumberAcceptance _model = new PackageNumberAcceptance();
        //                        _model.PackageNumberAcceptanceId = Guid.NewGuid();
        //                        _model.TransferAcceptanceId = model.TransferAcceptanceId;
        //                        _model.PackageNumberId =
        //                            packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo))
        //                                .FirstOrDefault()
        //                                .PackageNumberId;
        //                        _model.CreatedBy = model.CreatedBy;
        //                        _model.CreatedDate = model.CreatedDate;
        //                        _model.ModifiedBy = model.ModifiedBy;
        //                        _model.ModifiedDate = model.ModifiedDate;
        //                        _model.RecordStatus = model.RecordStatus;
        //                        model.PackageNumberAcceptances.Add(_model);
        //                        packageNumberAcceptanceService.Add(_model);
        //                    }
        //                }
        //                catch (Exception ex)
        //                { }
        //            }
        //        }
        //    }
        //}

        //public void AcceptanceGatewayBco(DateTime date) // TODO: not yet implemented fully
        //{
        //    TrackNTraceContext _trackingContext = new TrackNTraceContext();
        //    //TransferAcceptanceBL transferAcceptanceService = new TransferAcceptanceBL();
        //    List<TransferAcceptance> acceptanceModels = new List<TransferAcceptance>();
        //    PackageNumberAcceptanceBL packageNumberAcceptanceService = new PackageNumberAcceptanceBL();
        //    PackageNumberBL packageNumberService = new PackageNumberBL();
        //    UserStore userService = new UserStore();
        //    EmployeeBL employeeService = new EmployeeBL();
        //    RevenueUnit revenueUnit;

        //    var _bcoAcceptance = _trackingContext.branchacceptances.Where(x => (x.dDateTime.Value.Year == date.Year && x.dDateTime.Value.Month == date.Month && x.dDateTime.Value.Day == date.Day)).OrderBy(x => x.nIdentity).ToList();
        //    if (_bcoAcceptance != null)
        //    {
        //        Guid previousShipmentId = new Guid();
        //        Guid currentShipmentId = new Guid();
        //        Guid currentAcceptanceId = new Guid();
        //        foreach (var item in _bcoAcceptance)
        //        {
        //            if (!packageNumberAcceptanceService.IsExistInAcceptance(item.cCargo, AcceptanceTypeConstant.GatewayToBco))
        //            {
        //                try
        //                {
        //                    currentShipmentId = packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo)).FirstOrDefault().ShipmentId;
        //                    if (previousShipmentId != currentShipmentId)
        //                    {
        //                        previousShipmentId = currentShipmentId;
        //                        TransferAcceptance model = new TransferAcceptance();
        //                        model.TransferAcceptanceId = Guid.NewGuid();
        //                        model.AcceptanceDate = item.dDateTime ?? DateTime.Now;
        //                        model.Remarks = item.cRemarks;
        //                        model.CreatedDate = item.dDateTime ?? DateTime.Now;
        //                        model.ModifiedDate = item.dDateTime ?? DateTime.Now;
        //                        model.RecordStatus = 1;

        //                        var user = userService.FindByNameAsync(item.cUser).Result;
        //                        model.CreatedBy = user.Id;
        //                        model.ModifiedBy = user.Id;
        //                        model.TransferType = AcceptanceTypeConstant.AreaToBco;

        //                        Employee userAssignment = employeeService.GetById(user.EmployeeId);
        //                        if (userAssignment != null)
        //                        {
        //                            revenueUnit = userAssignment.AssignedToArea;
        //                            model.TransferToId = revenueUnit.City.BranchCorpOffice.BranchCorpOfficeId;
        //                        }

        //                        List<Employee> driverAssignments = employeeService.GetAll().Where(x => x.Position.PositionName == "Driver").ToList();
        //                        if (driverAssignments != null)
        //                        {
        //                            string employeeName = "";
        //                            string driverName = item.cDriver;
        //                            foreach (var assign in driverAssignments)
        //                            {
        //                                employeeName =
        //                                    (assign.FirstName.Substring(0, 1) + "." + assign.LastName)
        //                                        .ToUpper().Trim();
        //                                if (employeeName.Equals(driverName))
        //                                {
        //                                    model.DriverId = assign.EmployeeId;
        //                                    revenueUnit =  assign.AssignedToArea;
        //                                    model.TransferFromId = revenueUnit.RevenueUnitId;
        //                                }
        //                            }
        //                        }
        //                        List<Employee> fieldRepAssignments = employeeService.GetAll().Where(x => x.Position.PositionName == "Field Rep").ToList();
        //                        if (fieldRepAssignments != null)
        //                        {
        //                            string employeeName = "";
        //                            string checkerName = item.cChecker;
        //                            foreach (var assign in fieldRepAssignments)
        //                            {
        //                                employeeName =
        //                                    (assign.FirstName.Substring(0, 1) + "." + assign.LastName)
        //                                        .ToUpper().Trim();
        //                                if (employeeName.Equals(checkerName))
        //                                {
        //                                    model.ScannedById = assign.EmployeeId;
        //                                }
        //                            }
        //                        }
        //                        if (model.DriverId != Guid.Empty || model.ScannedById != Guid.Empty)
        //                        {
        //                            model.PackageNumberAcceptances = new List<PackageNumberAcceptance>();
        //                            PackageNumberAcceptance _model = new PackageNumberAcceptance();
        //                            _model.PackageNumberAcceptanceId = Guid.NewGuid();
        //                            _model.TransferAcceptanceId = model.TransferAcceptanceId;
        //                            _model.PackageNumberId =
        //                                packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo))
        //                                    .FirstOrDefault()
        //                                    .PackageNumberId;
        //                            _model.CreatedBy = model.CreatedBy;
        //                            _model.CreatedDate = model.CreatedDate;
        //                            _model.ModifiedBy = model.ModifiedBy;
        //                            _model.ModifiedDate = model.ModifiedDate;
        //                            _model.RecordStatus = model.RecordStatus;
        //                            model.PackageNumberAcceptances.Add(_model);
        //                            acceptanceModels.Add(model);
        //                            currentAcceptanceId = model.TransferAcceptanceId;
        //                            //transferAcceptanceService.Add(model);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        TransferAcceptance model = acceptanceModels.FirstOrDefault(x => x.TransferAcceptanceId == currentAcceptanceId);
        //                        PackageNumberAcceptance _model = new PackageNumberAcceptance();
        //                        _model.PackageNumberAcceptanceId = Guid.NewGuid();
        //                        _model.TransferAcceptanceId = model.TransferAcceptanceId;
        //                        _model.PackageNumberId =
        //                            packageNumberService.FilterBy(x => x.PackageNo.Equals(item.cCargo))
        //                                .FirstOrDefault()
        //                                .PackageNumberId;
        //                        _model.CreatedBy = model.CreatedBy;
        //                        _model.CreatedDate = model.CreatedDate;
        //                        _model.ModifiedBy = model.ModifiedBy;
        //                        _model.ModifiedDate = model.ModifiedDate;
        //                        _model.RecordStatus = model.RecordStatus;
        //                        model.PackageNumberAcceptances.Add(_model);
        //                        packageNumberAcceptanceService.Add(_model);
        //                    }
        //                }
        //                catch (Exception ex)
        //                { }
        //            }
        //        }
        //    }
        //}
    }
}
