using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CMS2.DataAccess;
using CMS2.Entities;
using CMS2.Entities.Models;
//using CMS2.Entities.TrackingEntities;
//using CMS2.Entities.TrackingEntities.Models;

namespace CMS2.BusinessLogic
{
    public class TrackingBL
    {
        //private TrackNTraceContext trackingContext;
        //private Database database;
        //private DbSet<acceptance> acceptanceSet;
        //private DbSet<airline> airlineSet;
        //private DbSet<bundle> bundleSet;
        ////private DbSet<destination> destinationSet;
        //private DbSet<distribution2> distributionSet;
        //private DbSet<gateway> gatewaySet;
        //private DbSet<inbound> inboundSet;
        //private DbSet<status> statusSet;
        //private DbSet<gatewaytransmittal> gatewayTransmittalSet;
        //private DbSet<origin> originSet;
        //private DbSet<destination> destinationSet;
        ////private DbSet<users> userSet;

        //private CmsContext cmsContext;
        ////private DbSet<Shipment> shipmentSet;
        //private ShipmentBL shipmentService;
        //private EmployeeBL employeeService;
        ////private DbSet<RevenueUnit> branchSet;
        ////private DbSet<CargoReceived> cargoReceivedSet;
        ////private EmployeePositionMappingBL employeePositionService;
        //private UserStore userService;

        //public TrackingBL()
        //{
        //    trackingContext = new TrackNTraceContext();
        //    database = trackingContext.Database;
        //    acceptanceSet = trackingContext.acceptances;
        //    bundleSet = trackingContext.bundles;
        //    distributionSet = trackingContext.distribution2s;
        //    inboundSet = trackingContext.inbounds;
        //    statusSet = trackingContext.status;
        //    gatewayTransmittalSet = trackingContext.gatewaytransmittals;
        //    gatewaySet = trackingContext.gateways;
        //    airlineSet = trackingContext.airlines;
        //    originSet = trackingContext.origins;
        //    destinationSet = trackingContext.destinations;

        //    cmsContext = new CmsContext();
        //    shipmentService = new ShipmentBL();
        //    //cargoReceivedSet = cmsContext.CargoReceived;
        //    //employeePositionService = new EmployeePositionMappingBL();
        //    userService = new UserStore();
        //}

        //#region Bundle/Insack
        //public List<BundleViewModel> GetBundlesByDateOriginBCO(DateTime date, string originBCO)
        //{
        //    List<string> usernames = new List<string>();
        //    List<Guid> employeeIds = employeeService.GetAll().Where(x => x.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeName == originBCO).Select(x => x.EmployeeId).ToList();
        //    if (employeeIds != null)
        //    {
        //        var users = userService.GetAllUsers();
        //        foreach (var item in employeeIds)
        //        {
        //            var user = users.FirstOrDefault(x => x.EmployeeId == item);
        //            if (user != null)
        //            {
        //                usernames.Add(user.UserName);
        //            }
        //        }
        //    }
            
        //    var bundles = bundleSet.AsEnumerable()
        //        .Where(
        //            x => (x.dDateTime.GetValueOrDefault().Year == date.Year &&
        //                x.dDateTime.GetValueOrDefault().Month == date.Month &&
        //                x.dDateTime.GetValueOrDefault().Day == date.Day) && usernames.Contains(x.cUser)).ToList();
             
        //    if (bundles != null)
        //    {
        //        var _bundles = BundleModelsToViewModels(bundles);
        //        return _bundles;
        //    }
        //    return null;
        //}

        //public List<BundleViewModel> GetBundlesBySackNo(string sackNo)
        //{
        //    var _bundle = bundleSet.Where(x => x.cSackNo.Equals(sackNo)).ToList();
        //    if (_bundle != null && _bundle.Count > 0)
        //    {
        //        return BundleModelsToViewModels(_bundle);
        //    }
        //    return null;
        //}

        //public int UpdateSackWeight(string sackNo, decimal weight)
        //{
        //    int affectedRecords = 0;
        //    var models = bundleSet.Where(x => x.cSackNo.Equals(sackNo)).ToList();
        //    if (models != null && models.Count > 0)
        //    {
        //        foreach (var item in models)
        //        {
        //            item.cWeight = weight.ToString();
        //        }
        //        affectedRecords = trackingContext.SaveChanges();
        //    }
        //    return affectedRecords;
        //}
        //#endregion

        //#region AirlineTransmittal
        //public List<TransmittalViewModel> GetTransmittalByDateAirlineOriginStatusMAwb(DateTime date, string airline, string originBco, string status, string masterAwb)
        //{
        //    var models = TransmittalModelsToViewModels(GetTransmittal(date, airline, originBco, "").ToList()).ToList();
        //    List<TransmittalViewModel> list = new List<TransmittalViewModel>();
        //    foreach (var item in models)
        //    {
        //        if (!list.Exists(x => x.DestinationBranchCode == item.DestinationBranchCode))
        //        {
        //            item.OriginBranch = originBco;
        //            item.TranmittalStatus = "no data";
        //            list.Add(item);
        //        }
        //    }
        //    return list;
        //}

        //public List<TransmittalViewModel> GetTransmittalByMasterAwb(string masterAwb)
        //{
        //    var models = TransmittalModelsToViewModels(gatewayTransmittalSet.Where(x => x.MasterAwb.Equals(masterAwb)).ToList());
        //    return models;
        //}

        //public int UpdateTransmittal(DateTime date, string airline, string originBco, string masterAwb, string destinationBranch)//string status, 
        //{
        //    int affectedRecords = 0;
        //    var models = GetTransmittal(date, airline, originBco, destinationBranch).ToList();
        //    if (models != null && models.Count > 0)
        //    {
        //        foreach (var item in models)
        //        {
        //            item.MasterAwb = masterAwb;
        //        }
        //        affectedRecords = trackingContext.SaveChanges();
        //    }
        //    return affectedRecords;
        //}

        //private IEnumerable<gatewaytransmittal> GetTransmittal(DateTime date, string airline, string originBco, string destinationBranch = "", string masterAwb="")// string status, 
        //{
        //    List<string> usernames = new List<string>();
        //    List<Guid> employeeIds = employeeService.GetAll().Where(x => x.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeName == originBco).Select(x => x.EmployeeId).ToList();
        //    if (employeeIds != null)
        //    {
        //        var users = userService.GetAllUsers();
        //        foreach (var item in employeeIds)
        //        {
        //            var user = users.FirstOrDefault(x => x.EmployeeId == item);
        //            if (user != null)
        //            {
        //                usernames.Add(user.UserName);
        //            }
        //        }
        //    }

        //    var _transmittals =
        //        gatewayTransmittalSet.AsEnumerable()
        //            .Where(
        //                x =>
        //                    x.cGateway == airline && usernames.Contains(x.cUser) &&
        //                    (x.dDateTime.GetValueOrDefault().Year == date.Year &&
        //                     x.dDateTime.GetValueOrDefault().Month == date.Month &&
        //                     x.dDateTime.GetValueOrDefault().Day == date.Day));

        //    if (!string.IsNullOrEmpty(destinationBranch))
        //        _transmittals = _transmittals.Where(x => x.cDestination.Equals(destinationBranch));

        //    if (!string.IsNullOrEmpty(masterAwb))
        //        _transmittals = _transmittals.Where(x => x.MasterAwb.Equals(masterAwb));

        //    return _transmittals;
        //}
        //#endregion

        //#region InboundTransmittal
        //public List<InboundAwbViewModel> GetInboundByDateOriginAirlineMAwb(DateTime date, string originBco, string gateway,string masterAwb)
        //{
        //    List<InboundAwbViewModel> list = new List<InboundAwbViewModel>();

        //    var _inbound = GetInbound(date, originBco, gateway,  masterAwb).ToList();
        //    var _transmittal = GetTransmittal(date, originBco, gateway,  "",masterAwb).ToList();
        //    List<string> awbs = new List<string>();

        //    foreach (var item in _inbound)
        //    {
        //        if (list.Exists(x => x.AirwayBill == item.cAwb))
        //        {
        //            var _item = list.Find(x => x.AirwayBill == item.cAwb);
        //            var _count = _item.CountInbound;
        //            _item.CountInbound = _count + 1;
        //        }
        //        else
        //        {
        //            InboundAwbViewModel vm = new InboundAwbViewModel();
        //            vm.AirwayBill = item.cAwb;
        //            vm.CountInbound = 1;
        //            vm.User = item.cUser;
        //            list.Add(vm);
        //            awbs.Add(item.cAwb);
        //        }
        //    }
        //    foreach (var item in _transmittal)
        //    {
        //        if (list.Exists(x => x.AirwayBill == item.cAwb))
        //        {
        //            var _item = list.Find(x => x.AirwayBill == item.cAwb);
        //            var _count = _item.CountOutbound;
        //            _item.CountOutbound = _count + 1;
        //            _item.DestinationBranchCode = item.cDestination;
        //            //_item.Status = item.cStatus;
        //        }
        //        else
        //        {
        //            InboundAwbViewModel vm = new InboundAwbViewModel();
        //            vm.AirwayBill = item.cAwb;
        //            vm.DestinationBranchCode = item.cDestination;
        //            //vm.Status = item.cStatus;
        //            vm.CountOutbound = 1;
        //            vm.User = item.cUser;
        //            list.Add(vm);
        //        }
        //    }
        //    var _shipments = shipmentService.FilterActiveBy(x => awbs.Contains(x.AirwayBillNo));
        //    var shipments = shipmentService.ComputeCharges(shipmentService.EntitiesToModels(_shipments));
        //    foreach (var item in shipments)
        //    {
        //        var _item = list.Find(x => x.AirwayBill == item.AirwayBillNo);
        //        _item.ActualWeight = item.Weight;
        //        _item.TotalAmount = item.ShipmentTotal;
        //    }
        //    return list;
        //}

        //public int UpdateInbound(List<string> awbs, string masterAwb)
        //{
        //    int affectedRecords = 0;
        //    var models = GetInbound(new DateTime(), "", "", "", awbs).ToList();
        //    if (models != null && models.Count > 0)
        //    {
        //        foreach (var item in models)
        //        {
        //            item.cMawb = masterAwb;
        //        }
        //        affectedRecords = trackingContext.SaveChanges();
        //    }
        //    return affectedRecords;
        //}

        //private IEnumerable<inbound> GetInbound(DateTime date, string originBco, string gateway, string masterAwb, List<string> awbs = null)
        //{
        //    List<string> usernames = new List<string>();
        //    List<Guid> employeeIds = employeeService.GetAll().Where(x => x.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeName == originBco).Select(x => x.EmployeeId).ToList();
        //    if (employeeIds != null)
        //    {
        //        var users = userService.GetAllUsers();
        //        foreach (var item in employeeIds)
        //        {
        //            var user = users.FirstOrDefault(x => x.EmployeeId == item);
        //            if (user != null)
        //            {
        //                usernames.Add(user.UserName);
        //            }
        //        }
        //    }
            
        //    var _inbound = inboundSet.AsEnumerable().Where(x =>
        //           x.cAirline.Equals(gateway) && usernames.Contains(x.cUser) &&
        //           (x.dDateTime.GetValueOrDefault().Year == date.Year &&
        //            x.dDateTime.GetValueOrDefault().Month == date.Month &&
        //            x.dDateTime.GetValueOrDefault().Day == date.Day));

        //    if (awbs != null)
        //    {
        //        _inbound = _inbound.Where(x => awbs.Contains(x.cAwb));
        //    }

        //    if (!string.IsNullOrEmpty(masterAwb))
        //    {
        //        _inbound = _inbound.Where(x => x.cMawb.Equals(masterAwb));
        //    }

        //    return _inbound;
        //}

        //#endregion

        //#region Retrieval

        //public List<RetrievalViewModel> GetRetrievaByDateDestination(DateTime date, string city)
        //{
        //    List<RetrievalViewModel> list = new List<RetrievalViewModel>();
        //    var models = GetRetrieval(date, city).ToList();
        //    if (models != null && models.Count > 0)
        //    {
        //        foreach (var item in models)
        //        {
        //            if (!list.Exists(x => x.OriginBranch.Equals(item.cBranch)))
        //            {
        //                RetrievalViewModel vm = new RetrievalViewModel();
        //                vm.TransactionDate = item.dDateTime ?? DateTime.Now.Date;
        //                vm.OriginBranch = item.cBranch;
        //                vm.DestinationBranchCode = item.cDestination;
        //                list.Add(vm);
        //            }
        //        }
        //    }

        //    return list;
        //}

        //public List<AirwayBillViewModel> GetRetrievaByDateDestinationOrigin(DateTime date, string city, string branchName)
        //{
        //    List<AirwayBillViewModel> list = new List<AirwayBillViewModel>();
        //    var models = GetRetrieval(date, city, branchName).ToList();
        //    if (models != null && models.Count > 0)
        //    {
        //        foreach (var item in models)
        //        {
        //            if (list.Exists(x => x.AirwayBillNo == item.cAwb))
        //            {
        //                list.Find(x => x.AirwayBillNo == item.cAwb).CargoNos.Add(item.cCargo);
        //            }
        //            else
        //            {
        //                AirwayBillViewModel vm = new AirwayBillViewModel();
        //                vm.AirwayBillNo = item.cAwb;
        //                vm.CargoNos = new List<string>() { item.cCargo };
        //                ////vm.Airline = item.cAirline;
        //                ////vm.Status = item.cStatus;
        //                list.Add(vm);
        //            }

        //        }
        //    }

        //    return list;
        //}

        //private IEnumerable<gatewaytransmittal> GetRetrieval(DateTime date, string city,string branchName = "")
        //{
        //    var models =
        //        gatewayTransmittalSet.AsEnumerable()
        //            .Where(
        //                x => x.cDestination.Equals(city) &&
        //                    (x.dDateTime.GetValueOrDefault().Year == date.Year &&
        //                     x.dDateTime.GetValueOrDefault().Month == date.Month &&
        //                     x.dDateTime.GetValueOrDefault().Day == date.Day));

        //    if (!string.IsNullOrEmpty(branchName))
        //        models = models.Where(x => x.cBranch == branchName);

        //    return models;
        //}

        //#endregion

        //#region DailyTrip
        //public List<DistributionViewModel> GetDistributionByDateBranch(DateTime date, string bco)
        //{
        //    List<string> usernames = new List<string>();
        //    List<Guid> employeeIds = employeeService.GetAll().Where(x => x.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeName == bco).Select(x => x.EmployeeId).ToList();
        //    if (employeeIds != null)
        //    {
        //        var users = userService.GetAllUsers();
        //        foreach (var item in employeeIds)
        //        {
        //            var user = users.FirstOrDefault(x => x.EmployeeId == item);
        //            if (user != null)
        //            {
        //                usernames.Add(user.UserName);
        //            }
        //        }
        //    }

        //    List<DistributionViewModel> list = new List<DistributionViewModel>();
        //    var models = distributionSet.AsEnumerable().Where(x => usernames.Contains(x.cUser) &&
        //                                            (x.dDateTime.GetValueOrDefault().Year == date.Year &&
        //                                             x.dDateTime.GetValueOrDefault().Month == date.Month &&
        //                                             x.dDateTime.GetValueOrDefault().Day == date.Day)).OrderBy(x => x.cPlateNo).ToList();
        //    if (models != null && models.Count > 0)
        //    {
        //        foreach (var item in models)
        //        {
        //            if (!list.Exists(x => x.PlateNo == item.cPlateNo))
        //            {
        //                DistributionViewModel vm = new DistributionViewModel();
        //                vm.TripDate = item.dDateTime.GetValueOrDefault().Date;
        //                vm.PlateNo = item.cPlateNo;
        //                vm.FieldRep = item.cChecker;
        //                vm.Driver = item.cDriver;
        //                list.Add(vm);
        //            }
        //        }
        //    }

        //    return list;
        //}

        //public List<ShipmentModel> GetDailyTripByDateBranchFieldRepDriverPlateNo(DateTime date, string bco, string fieldRep,string driver, string plateNo)
        //{
        //    List<string> usernames = new List<string>();
        //    List<Guid> employeeIds = employeeService.GetAll().Where(x => x.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeName == bco).Select(x => x.EmployeeId).ToList();
        //    if (employeeIds != null)
        //    {
        //        var users = userService.GetAllUsers();
        //        foreach (var item in employeeIds)
        //        {
        //            var user = users.FirstOrDefault(x => x.EmployeeId == item);
        //            if (user != null)
        //            {
        //                usernames.Add(user.UserName);
        //            }
        //        }
        //    }
        //    List<ShipmentModel> list = new List<ShipmentModel>();
        //    var shipments = shipmentService.FilterActive()
        //        .Join(
        //            distributionSet.AsEnumerable()
        //                .Where(
        //                    x =>
        //                        usernames.Contains(x.cUser) && x.cChecker == fieldRep && x.cDriver == driver &&
        //                        x.cPlateNo == plateNo &&
        //                        (x.dDateTime.GetValueOrDefault().Year == date.Year &&
        //                         x.dDateTime.GetValueOrDefault().Month == date.Month &&
        //                         x.dDateTime.GetValueOrDefault().Day == date.Day))
        //                .GroupBy(x=>x.cAwb).Select(x=>new {cAwb=x.Key})
        //                .ToList(),
        //            ship => ship.AirwayBillNo, dist => dist.cAwb, (ship, dist) => new {ship}).Select(z=>z.ship).ToList();

        //    if (shipments != null && shipments.Count > 0)
        //    {
        //        list = shipmentService.ComputeCharges(shipmentService.EntitiesToModels(shipments));
        //    }

        //    return list;
        //}
        //#endregion

        //#region DeliveryStatus

        //public List<DeliveryStatusViewModel> GetShippingStatusByAwb(string awb)
        //{
        //    List<DeliveryStatusViewModel> list = new List<DeliveryStatusViewModel>();
        //    DeliveryStatusViewModel vm;
        //    List<string> cargoNos = new List<string>();
        //    Guid shipmentId = new Guid();
        //    User user = new User();
        //    string city = "";
        //    List<branchacceptance> acceptancePackages = new List<branchacceptance>();
        //    branchacceptance acceptance = new branchacceptance();

        //    var shipments = shipmentService.FilterBy(x => x.AirwayBillNo.Equals(awb));
        //    if (shipments != null && shipments.Count > 0)
        //    {
        //        #region Received
        //        Shipment shipment = shipments.FirstOrDefault();
        //        shipmentId = shipment.ShipmentId;
        //        cargoNos = shipment.PackageNumbers.Select(x => x.PackageNo).ToList();

        //        user = userService.GetAllUsers().FirstOrDefault(x => x.EmployeeId == shipment.AcceptedById);
        //        city = employeeService.GetById(user.EmployeeId).AssignedToArea.City.CityName;

        //        vm = new DeliveryStatusViewModel()
        //        {
        //            StatusDate = shipment.DateAccepted,
        //            Location = city,
        //            Status = "Shipment Picked-up/Received",
        //            User = user.Employee.FullName,
        //            Airline = ""
        //        };
        //        list.Add(vm);
                
        //        #endregion

        //        acceptancePackages = trackingContext.branchacceptances.Where(x => cargoNos.Contains(x.cCargo)).ToList();
        //        if (acceptancePackages != null && acceptancePackages.Count > 0)
        //        {
        //            #region BranchAcceptanceFromArea
        //            acceptance = acceptancePackages.OrderBy(x => x.dDateTime).FirstOrDefault();
        //            //trackDate = acceptance.dDateTime ?? DateTime.Now;
        //            user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(acceptance.cUser));
        //            city = employeeService.GetById(user.EmployeeId).AssignedToArea.City.CityName;

        //            vm = new DeliveryStatusViewModel()
        //            {
        //                StatusDate = acceptance.dDateTime ?? DateTime.Now,
        //                Location = city,
        //                Status = "Arrived at APCargo Hub",
        //                User = user.Employee.FullName,
        //                Airline = ""
        //            };
        //            list.Add(vm);
                    
        //            #endregion

        //            var transmittals = trackingContext.gatewaytransmittals.Where(x => cargoNos.Contains(x.cCargo)).ToList();
        //            if (transmittals != null && transmittals.Count > 0)
        //            {
        //                #region Transmittals
        //                gatewaytransmittal transmittal = transmittals.FirstOrDefault();
        //                //trackDate = transmittal.dDateTime ?? new DateTime();
        //                user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(transmittal.cUser));
        //                city = employeeService.GetById(user.EmployeeId).AssignedToArea.City.CityName;

        //                vm = new DeliveryStatusViewModel()
        //                {
        //                    StatusDate = transmittal.dDateTime ?? DateTime.Now,
        //                    Location = city,
        //                    Status = "Departed APCargo Facility",
        //                    User = user.Employee.FullName,
        //                    Airline = ""
        //                };
        //                list.Add(vm);
        //                #endregion

        //                var inbounds = trackingContext.inbounds.Where(x => cargoNos.Contains(x.cCargo)).ToList();
        //                if (inbounds != null && inbounds.Count > 0)
        //                {
        //                    #region Inbounds
        //                    inbound inbound = inbounds.FirstOrDefault();
        //                    //trackDate = inbound.dDateTime ?? new DateTime();
        //                    user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(inbound.cUser));
        //                    city = employeeService.GetById(user.EmployeeId).AssignedToArea.City.CityName;

        //                    vm = new DeliveryStatusViewModel()
        //                    {
        //                        StatusDate = inbound.dDateTime ?? DateTime.Now,
        //                        Location = city,
        //                        Status = "Arrived At Destination Gateway",
        //                        User = user.Employee.FullName,
        //                        Airline = ""
        //                    };
        //                    list.Add(vm);
                            
        //                    #endregion

        //                    if (acceptancePackages != null && acceptancePackages.Count > 0)
        //                    {
        //                        #region BranchAcceptanceFromGateway
        //                        acceptance = acceptancePackages.OrderByDescending(x => x.dDateTime).FirstOrDefault();
        //                        if (acceptance != null)
        //                        {
        //                            //trackDate = acceptance.dDateTime ?? DateTime.Now;
        //                            user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(acceptance.cUser));
        //                            city = employeeService.GetById(user.EmployeeId).AssignedToArea.City.CityName;

        //                            vm = new DeliveryStatusViewModel()
        //                            {
        //                                StatusDate = acceptance.dDateTime ?? DateTime.Now,
        //                                Location = city,
        //                                Status = "Arrived at APCargo Facility",
        //                                User = user.Employee.FullName,
        //                                Airline = ""
        //                            };
        //                            list.Add(vm);
        //                        }
        //                        #endregion

        //                        var distributions = trackingContext.distribution2s.Where(x => cargoNos.Contains(x.cCargo)).ToList();
        //                        if (distributions != null || distributions.Count > 0)
        //                        {
        //                            #region Distribution
        //                            distribution2 distribution = distributions.FirstOrDefault();
        //                            //trackDate = distribution.dDateTime ?? new DateTime();
        //                            user = userService.GetAllUsers().FirstOrDefault(x => x.UserName.Equals(distribution.cUser));
        //                            city = employeeService.GetById(user.EmployeeId).AssignedToArea.City.CityName;

        //                            vm = new DeliveryStatusViewModel()
        //                            {
        //                                StatusDate = distribution.dDateTime ?? DateTime.Now,
        //                                Location = city,
        //                                Status = "Out For Delivery",
        //                                User = user.Employee.FullName,
        //                                Airline = ""
        //                            };
        //                            list.Add(vm);
        //                            #endregion

        //                            DeliveryBL deliveryService = new DeliveryBL();
        //                            var deliveries = deliveryService.FilterBy(x => x.ShipmentId == shipmentId && x.DeliveryStatus.DeliveryStatusName.Equals("Delivered"));
        //                            if (deliveries != null && deliveries.Count > 0)
        //                            {
        //                                #region Delivery
        //                                var delivery = deliveries.FirstOrDefault();
        //                                //trackDate = delivery.DateDelivered;
        //                                user = userService.GetAllUsers().FirstOrDefault(x => x.EmployeeId == delivery.DeliveredById);
        //                                city = employeeService.GetById(user.EmployeeId).AssignedToArea.City.CityName;

        //                                vm = new DeliveryStatusViewModel()
        //                                {
        //                                    StatusDate = delivery.DateDelivered,
        //                                    Location = city,
        //                                    Status = "Delivered",
        //                                    User = user.Employee.FullName,
        //                                    Airline = ""
        //                                };
        //                                list.Add(vm);
        //                                #endregion
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return list.OrderBy(x => x.StatusDate).ToList();
        //}

        //#endregion

        //#region CargoMonitoring

        //public AirwayBillViewModel GetCargoStatusByAirwayBill(string airwaybill)
        //{
        //    AirwayBillViewModel vm = null;
        //    var shipment = shipmentService.FilterActiveBy(x => x.AirwayBillNo == airwaybill).First();
        //    //var cargo = cargoReceivedSet.AsEnumerable().SingleOrDefault(x=>x.AirwayBill.GetValueOrDefault().ToString()==airwaybill);

        //    if (shipment != null)
        //    {
        //        vm = new AirwayBillViewModel();
        //        ShipmentModel shipmentModel = shipmentService.ComputeCharges(shipmentService.EntityToModel(shipment));
        //        vm.AirwayBillNo = shipmentModel.AirwayBillNo;
        //        vm.ShipmentDate = shipmentModel.DateAccepted;
        //        vm.ShipperName = shipmentModel.Shipper.FullName;
        //        vm.ConsigneeName = shipmentModel.Consignee.FullName;
        //        vm.OriginCity = shipmentModel.OriginCity.CityName;
        //        vm.DestinationCity = shipmentModel.DestinationCity.CityName;
        //        vm.ServiceMode = shipmentModel.ServiceMode.ServiceModeName;
        //        vm.PaymentMode = shipmentModel.PaymentMode.PaymentModeName;
        //        vm.ShipmentTotalCharge = shipmentModel.ShipmentTotal;
        //        //if (cargo != null)
        //        //{
        //        //    vm.Status = cargo.Status;
        //        //    vm.DeliveredBy = cargo.DeliveredBy;
        //        //    vm.DeliveredOn = cargo.CreatedDate ?? DateTime.Now;
        //        //    vm.ReceivedBy = cargo.ReceivedBy;
        //        //    vm.ReceivedOn = cargo.DateReceived ?? DateTime.Now;
        //        //    vm.Remarks = cargo.Remarks;
        //        //    vm.Notes = cargo.Notes;
        //        //}
        //    }
            
        //    return vm;
        //}

        //public int UpdateDeliveryStatus(AirwayBillViewModel model)
        //{
        //    //var _model = cargoReceivedSet.Single(x => x.AirwayBill.ToString() == model.AirwayBillNo);
        //    //if (_model == null)
        //    //{
        //    //    CargoReceived entity = new CargoReceived();
        //    //    entity.AirwayBill = long.Parse(model.AirwayBillNo);
        //    //    entity.DeliveredBy = model.DeliveredBy;
        //    //    entity.ReceivedBy = model.ReceivedBy;
        //    //    entity.DateReceived = model.ReceivedOn;
        //    //    entity.Remarks = model.Remarks;
        //    //    entity.Status = model.Status;
        //    //    entity.Notes = model.Notes;
        //    //    entity.CreatedBy = model.CreatedBy;
        //    //    entity.CreatedDate = model.CreatedDate;
        //    //    entity.ModifiedBy = model.ModifiedBy;
        //    //    entity.ModifiedDate = model.ModifiedDate;
        //    //    cmsContext.CargoReceived.Add(entity);
        //    //}
        //    //else
        //    //{
        //    //    _model.DeliveredBy = model.DeliveredBy;
        //    //    _model.ReceivedBy = model.ReceivedBy;
        //    //    _model.DateReceived = model.ReceivedOn;
        //    //    _model.Remarks = model.Remarks;
        //    //    _model.Status = model.Status;
        //    //    _model.Notes = model.Notes;
        //    //    _model.ModifiedBy = model.ModifiedBy;
        //    //    _model.ModifiedDate = model.ModifiedDate;
        //    //    cmsContext.Entry(_model).State = EntityState.Modified; 
        //    //}
        //    //return cmsContext.SaveChanges();
        //    return 0;

        //}
        //#endregion

        //#region Lookup
       
        //public List<status> GetStatus()
        //{
        //    var status = statusSet.ToList();
        //    return status;
        //}
        //#endregion

        //#region Transmittal
        //private List<TransmittalViewModel> TransmittalModelsToViewModels(List<gatewaytransmittal> models)
        //{
        //    List<TransmittalViewModel> list = new List<TransmittalViewModel>();

        //    foreach (var item in models)
        //    {
        //        list.Add(TransmittalModelToViewModel(item));
        //    }
        //    return list;
        //}

        //private TransmittalViewModel TransmittalModelToViewModel(gatewaytransmittal model)
        //{
        //    DateTime date = DateTime.Now;
        //    if (model.dDateTime.HasValue)
        //        date = model.dDateTime ?? DateTime.Now;

        //    TransmittalViewModel vm = new TransmittalViewModel();
        //    vm.Identity = Convert.ToInt32(model.nIdentity);
        //    //vm.TranmittalStatus = model.cStatus;
        //    vm.OriginBranch = model.cBranch;
        //    vm.DestinationBranchCode = model.cDestination;
        //    vm.Airline = model.cGateway;
        //    vm.Cargo = model.cCargo;
        //    vm.TransmittalDate = date.Date;
        //    vm.User = model.cUser;
        //    //vm.TransNo = Convert.ToInt32(model.cTransNo);
        //    vm.AirwayBill = model.cAwb;
        //    vm.MasterAirwayBill = model.MasterAwb;
        //    return vm;
        //}

        
        //#endregion

        //#region Bundle
        //private BundleViewModel BundleModelToViewModel(bundle model)
        //{
        //    BundleViewModel vm = new BundleViewModel();
        //    vm.SackNo = model.cSackNo;
        //    vm.DestinationCityCode = model.cDestination;
        //    if (string.IsNullOrEmpty(model.cWeight))
        //        model.cWeight = "0";
        //    vm.Weight = decimal.Parse(model.cWeight);
        //    vm.CargoNos = new List<CargoViewModel>() { new CargoViewModel() { AirwayBill = model.cAwb, CargoNo = model.cCargo } };
        //    vm.TransactionDate = model.dDateTime ?? DateTime.Now.Date;
        //    vm.User = model.cUser;
        //    return vm;
        //}
        //private List<BundleViewModel> BundleModelsToViewModels(List<bundle> models)
        //{
        //    List<BundleViewModel> list = new List<BundleViewModel>();

        //    foreach (var item in models)
        //    {
        //        if (list.Exists(x => x.SackNo.Equals(item.cSackNo)))
        //        {
        //            var bundle = list.FirstOrDefault(x => x.SackNo.Equals(item.cSackNo));
        //            var cargoNo=new CargoViewModel() { AirwayBill = item.cAwb, CargoNo = item.cCargo } ;
        //            bundle.CargoNos.Add(cargoNo);
        //        }
        //        else
        //        {
        //            list.Add(BundleModelToViewModel(item));
        //        }
        //    }
        //    return list;
        //}
        //#endregion

        //#region Inbound
        //private InboundViewModel InboundModelToViewModel(inbound model)
        //{
        //    InboundViewModel vm = new InboundViewModel();
        //    vm.Identity = Convert.ToInt32(model.nIdentity);
        //    vm.Cargo = model.cCargo;
        //    vm.DestinationBranch = model.cBranch;
        //    vm.TransactionDate = model.dDateTime ?? DateTime.Now;
        //    vm.User = model.cUser;
        //    vm.Airline = model.cAirline;
        //    vm.AirwayBill = model.cAwb;
        //    vm.MasterAirwayBill = model.cMawb;
        //    return vm;
        //}
        //private List<InboundViewModel> InboundModelsToViewModels(List<inbound> models)
        //{
        //    List<InboundViewModel> list = new List<InboundViewModel>();

        //    foreach (var item in models)
        //    {
        //        list.Add(InboundModelToViewModel(item));
        //    }
        //    return list;
        //}
        //#endregion
    }
}
