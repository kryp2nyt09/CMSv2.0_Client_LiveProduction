using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using CMS2.Common.Enums;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using CMS2.Entities.Models;
using CMS2.Common;

namespace CMS2.BusinessLogic
{
    public class ShipmentBL : BaseAPCargoBL<Shipment>
    {
        private ICmsUoW _unitOfWork;
        private ClientBL clientService;
        private CompanyBL companyService;
        private PaymentBL paymentService;
        private ShipmentAdjustmentBL adjustmentService;
        private StatementOfAccountBL soaService;
        private FuelSurchargeBL fuelSurchargeService;
        private ExpressRateBL expressRateService;
        private PackageDimensionBL packageDimensionService;
        private CommodityTypeBL commodityTypeService;
        private CommodityBL commodityService;
        private ShipmentBasicFeeBL shipmentBasicFeeService;
        private CratingBL cratingService;
        private PackagingBL packagingService;
        private RateMatrixBL rateMatrixService;
        private TransShipmentRouteBL transShipmentRouteService;
        private TransShipmentLegBL transShipmentLegService;
        private RevenueUnitBL revenueUnitService;
        private BranchCorpOfficeBL bcoService;
        private ApplicableRateBL applicableRateService;

        public ShipmentBL()
        {
            _unitOfWork = GetUnitOfWork();
            clientService = new ClientBL(_unitOfWork);
            companyService = new CompanyBL(_unitOfWork);
            paymentService = new PaymentBL(_unitOfWork);
            adjustmentService = new ShipmentAdjustmentBL(_unitOfWork);
            soaService = new StatementOfAccountBL(_unitOfWork);
            clientService = new ClientBL(_unitOfWork);
            fuelSurchargeService = new FuelSurchargeBL(_unitOfWork);
            expressRateService = new ExpressRateBL(_unitOfWork);
            packageDimensionService = new PackageDimensionBL(_unitOfWork);
            commodityTypeService = new CommodityTypeBL(_unitOfWork);
            commodityService = new CommodityBL(_unitOfWork);
            shipmentBasicFeeService = new ShipmentBasicFeeBL(_unitOfWork);
            cratingService = new CratingBL(_unitOfWork);
            packagingService = new PackagingBL(_unitOfWork);
            rateMatrixService = new RateMatrixBL(_unitOfWork);
            transShipmentRouteService = new TransShipmentRouteBL(_unitOfWork);
            transShipmentLegService = new TransShipmentLegBL(_unitOfWork);
            revenueUnitService = new RevenueUnitBL(_unitOfWork);
            bcoService = new BranchCorpOfficeBL(_unitOfWork);
            applicableRateService = new ApplicableRateBL(_unitOfWork);
        }

        public ShipmentBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
            clientService = new ClientBL(unitOfWork);
            companyService = new CompanyBL(unitOfWork);
            paymentService = new PaymentBL(unitOfWork);
            adjustmentService = new ShipmentAdjustmentBL(unitOfWork);
            soaService = new StatementOfAccountBL(unitOfWork);
            expressRateService = new ExpressRateBL(unitOfWork);
            fuelSurchargeService = new FuelSurchargeBL(unitOfWork);
            packageDimensionService = new PackageDimensionBL(unitOfWork);
            commodityTypeService = new CommodityTypeBL(unitOfWork);
            commodityService = new CommodityBL(unitOfWork);
            shipmentBasicFeeService = new ShipmentBasicFeeBL(unitOfWork);
            cratingService = new CratingBL(unitOfWork);
            packagingService = new PackagingBL(unitOfWork);
            rateMatrixService = new RateMatrixBL(unitOfWork);
            transShipmentRouteService = new TransShipmentRouteBL(unitOfWork);
            transShipmentLegService = new TransShipmentLegBL(unitOfWork);
            revenueUnitService = new RevenueUnitBL(unitOfWork);
            bcoService = new BranchCorpOfficeBL(unitOfWork);
            applicableRateService = new ApplicableRateBL(unitOfWork);
        }

        public override Expression<Func<Shipment, object>>[] Includes()
        {
            return new Expression<Func<Shipment, object>>[]
                {
                    x => x.OriginCity,
                    x=>x.DestinationCity,
                    x=>x.CommodityType,
                    x=>x.Commodity,
                    x=>x.PackageNumbers,
                    x=>x.PackageDimensions,
                    x=>x.Booking,
                    x=>x.AcceptedBy,
                    x=>x.Deliveries,
                    x=>x.ShipMode,
                    x=>x.ServiceType,
                    x=>x.GoodsDescription
                };
        }

        public Shipment ModelToEntity(ShipmentModel model)
        {
            Shipment entity = new Shipment()
            {
                ShipmentId = model.ShipmentId,
                AirwayBillNo = model.AirwayBillNo,
                OriginCityId = model.OriginCityId,
                OriginCity = model.OriginCity,
                DestinationCityId = model.DestinationCityId,
                DestinationCity = model.DestinationCity,
                ConsigneeId = model.ConsigneeId,
                Consignee = model.Consignee,
                ShipperId = model.ShipperId,
                Shipper = model.Shipper,
                DateAccepted = model.DateAccepted,
                AcceptedById = model.AcceptedById,
                AcceptedBy = model.AcceptedBy,
                CommodityTypeId = model.CommodityTypeId,
                CommodityType = model.CommodityType,
                ServiceModeId = model.ServiceModeId,
                ServiceMode = model.ServiceMode,
                PaymentModeId = model.PaymentModeId,
                PaymentMode = model.PaymentMode,
                PaymentTermId = model.PaymentTermId,
                PaymentTerm = model.PaymentTerm,
                IsVatable = model.IsVatable,
                Remarks = model.Remarks,
                DeclaredValue = model.DeclaredValue,
                StatementOfAccountId = model.StatementOfAccountId,
                AwbFeeId = model.AwbFeeId,
                FreightCollectChargeId = model.FreightCollectChargeId,
                FuelSurchargeId = model.FuelSurchargeId,
                PeracFeeId = model.PeracFeeId,
                EvatId = model.EvatId,
                InsuranceId = model.InsuranceId,
                ValuationFactorId = model.ValuationFactorId,
                DateOfFullPayment = model.DateOfFullPayment,
                Payments = model.Payments,
                Adjustments = model.Adjustments,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                RecordStatus = model.RecordStatus,
                Notes = model.Notes,
                BookingId = model.BookingId,
                Booking = model.Booking,
                OriginAddress = model.OriginAddress,
                DestinationAddress = model.DestinationAddress,
                Quantity = model.Quantity,
                Weight = model.Weight,
                PackageNumbers = model.PackageNumbers,
                Deliveries = model.Deliveries,
                ServiceTypeId = model.ServiceTypeId,
                ServiceType = model.ServiceType,
                ShipModeId = model.ShipModeId,
                ShipMode = model.ShipMode,
                GoodsDescriptionId = model.GoodsDescriptionId,
                GoodsDescription = model.GoodsDescription,
                HandlingFee = model.HandlingFee,
                QuarantineFee = model.QuarantineFee,
                Discount = model.Discount,
                CommodityId = model.CommodityId,
                Commodity = model.Commodity,
                DeliveryFeeId = model.DeliveryFeeId,
                DangerousFeeId = model.DangerousFeeId,
                FreightCharge = model.FreightCharge,
                TotalAmount = model.ShipmentTotal,
                OriginBarangay = model.OriginBarangay,
                DestinationBarangay = model.DestinationBarangay,
                TransShipmentLegId = model.TransShipmentLegId,

            };

            entity.PackageDimensions = packageDimensionService.ModelsToEntities(model.PackageDimensions);// FilterActiveBy(x => x.ShipmentId == model.ShipmentId);
            //entity.PackageDimensions = packageDimensionService.FilterActiveBy(x => x.ShipmentId == model.ShipmentId);
            if (model.PeracFeeId != null)
                entity.PeracFee = model.PeracFee;
            if (model.DeliveryFeeId != null)
                entity.DeliveryFee = model.DeliveryFee;
            if (model.DangerousFeeId != null)
                entity.DangerousFee = model.DangerousFee;
            if (model.FreightCollectChargeId != null)
                entity.FreightCollectCharge = model.FreightCollectCharge;
            if (model.EvatId != null)
                entity.EVat = model.EVat;
            if (model.FuelSurchargeId != null)
                entity.FuelSurcharge = model.FuelSurcharge;
            if (model.ValuationFactorId != null)
                entity.ValuationFactor = model.ValuationFactor;
            if (model.AwbFeeId != null)
                entity.AwbFee = model.AwbFee;
            if (model.InsuranceId != null)
                entity.Insurance = model.Insurance;
            if (model.StatementOfAccountId != null)
            {
                entity.StatementOfAccount = soaService.GetById(Guid.Parse(model.StatementOfAccountId.ToString()));
            }

            if (model.TransShipmentLegId != null && model.TransShipmentLegId != Guid.Empty)
            {
                entity.TransShipmentLeg = model.TransShipmentLeg;
            }
            else
            {
                entity.TransShipmentLegId = null;
            }

            return entity;
        }

        public List<Shipment> ModelsToEntities(List<ShipmentModel> models)
        {
            List<Shipment> entities = new List<Shipment>();
            foreach (var item in models)
            {
                entities.Add(ModelToEntity(item));
            }
            return entities;
        }

        public ShipmentModel EntityToModel(Shipment entity)
        {
            ShipmentModel model = new ShipmentModel()
            {
                ShipmentId = entity.ShipmentId,
                AirwayBillNo = entity.AirwayBillNo,
                OriginCityId = entity.OriginCityId,
                OriginCity = entity.OriginCity,
                DestinationCityId = entity.DestinationCityId,
                DestinationCity = entity.DestinationCity,
                ConsigneeId = entity.ConsigneeId,
                Consignee = entity.Consignee,
                ShipperId = entity.ShipperId,
                Shipper = entity.Shipper,
                DateAccepted = entity.DateAccepted,
                AcceptedById = entity.AcceptedById,
                AcceptedBy = entity.AcceptedBy,
                CommodityTypeId = entity.CommodityTypeId,
                CommodityType = entity.CommodityType,
                ServiceModeId = entity.ServiceModeId,
                ServiceMode = entity.ServiceMode,
                PaymentModeId = entity.PaymentModeId,
                PaymentMode = entity.PaymentMode,
                PaymentTermId = entity.PaymentTermId,
                PaymentTerm = entity.PaymentTerm,
                IsVatable = entity.IsVatable,
                Remarks = entity.Remarks,
                DeclaredValue = entity.DeclaredValue,
                StatementOfAccountId = entity.StatementOfAccountId,
                AwbFeeId = entity.AwbFeeId,
                AwbFee = entity.AwbFee,
                FreightCollectChargeId = entity.FreightCollectChargeId,
                FreightCollectCharge = entity.FreightCollectCharge,
                FuelSurchargeId = entity.FuelSurchargeId,
                FuelSurcharge = entity.FuelSurcharge,
                PeracFeeId = entity.PeracFeeId,
                PeracFee = entity.PeracFee,
                EvatId = entity.EvatId,
                EVat = entity.EVat,
                InsuranceId = entity.InsuranceId,
                Insurance = entity.Insurance,
                ValuationFactorId = entity.ValuationFactorId,
                ValuationFactor = entity.ValuationFactor,
                DateOfFullPayment = entity.DateOfFullPayment,
                Payments = entity.Payments,
                Adjustments = entity.Adjustments,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                RecordStatus = entity.RecordStatus,
                Notes = entity.Notes,
                BookingId = entity.BookingId,
                Booking = entity.Booking,
                OriginAddress = entity.OriginAddress,
                DestinationAddress = entity.DestinationAddress,
                Quantity = entity.Quantity,
                Weight = entity.Weight,
                PackageNumbers = entity.PackageNumbers,
                ServiceTypeId = entity.ServiceTypeId,
                ShipModeId = entity.ShipModeId,
                GoodsDescriptionId = entity.GoodsDescriptionId,
                HandlingFee = entity.HandlingFee,
                QuarantineFee = entity.QuarantineFee,
                Discount = entity.Discount,
                CommodityId = entity.CommodityId,
                DeliveryFeeId = entity.DeliveryFeeId,
                DangerousFeeId = entity.DangerousFeeId

            };

            model.ShipperCompanyAccountNo = "na";
            if (entity.Shipper.Company != null)
            {
                model.Discount = entity.Shipper.Company.Discount;
                model.ShipperCompanyAccountNo = entity.Shipper.Company.AccountNo;
            }

            model.ConsigneeCompanyAccountNo = "na";
            if (entity.Consignee.Company != null)
                model.ConsigneeCompanyAccountNo = entity.Consignee.Company.AccountNo;

            if (entity.StatementOfAccountId != null)
                model.StatementOfAccount = soaService.EntityToModel(entity.StatementOfAccount);

            if (entity.TransShipmentLegId != null)
            {
                model.TransShipmentLeg = entity.TransShipmentLeg;
            }

            if (entity.CommodityType != null)
                model.CommodityType = entity.CommodityType;

            if (entity.Commodity != null)
                model.Commodity = entity.Commodity;

            if (entity.Deliveries != null)
                model.Deliveries = entity.Deliveries;

            if (entity.ServiceTypeId != null)
                model.ServiceType = entity.ServiceType;

            if (entity.ShipModeId != null)
                model.ShipMode = entity.ShipMode;

            if (entity.GoodsDescriptionId != null)
                model.GoodsDescription = entity.GoodsDescription;

            model.PackageDimensions = packageDimensionService.EntitiesToModels(entity.PackageDimensions);
            int _index = 0;
            foreach (var item in model.PackageDimensions.Where(x => x.RecordStatus == (int)RecordStatus.Active).ToList())
            {
                item.Index = _index;
                if (item.CommodityTypeId == null)
                    item.CommodityTypeId = model.CommodityTypeId;
                if (item.DrainingId != null)
                    item.ForDraining = true;
                if (item.PackagingId != null)
                    item.ForPackaging = true;
                _index++;
            }

            if (model.Payments == null)
                model.Payments = paymentService.FilterActiveBy(x => x.ShipmentId == model.ShipmentId);

            model = ComputePackageEvmCrating(model);
            model = ComputePackageWeightCharge(model);
            model = ComputeCharges(model);

            return model;
        }

        public List<ShipmentModel> EntitiesToModels(List<Shipment> entities)
        {
            List<ShipmentModel> models = new List<ShipmentModel>();
            foreach (var item in entities)
            {
                ShipmentModel model = EntityToModel(item);
                models.Add(model);
            }
            return models;
        }

        public ShipmentModel ComputeCharges(ShipmentModel model)
        {
            Logs.AppLogs(LogPath, "Shipment BL - ComputeCharges", model.ShipmentId.ToString());

            #region variables
            decimal freightCollectCharge = 0;
            decimal insuranceCharge = 0;
            decimal vat = 0;
            decimal fuelCharge = 0;
            decimal deliveryFee = 0;
            decimal valuation = 0;
            decimal peracFee = 0;
            decimal awbFee = 0;
            decimal dangerousFee = 0;
            decimal defaultFee = 0;
            model.DeliveryFeeId = null;
            model.DeliveryFee = null;
            #endregion

            model = ComputePackageWeightCharge(ComputePackageEvmCrating(model));

            dynamic matrix = null;
            //shipmentBasicFeeService = new ShipmentBasicFeeBL();
            List<ShipmentBasicFee> basicFees = shipmentBasicFeeService.FilterActive();

            model.FreightCollectChargeId = null;
            model.FreightCollectCharge = null;
            if (model.PaymentMode.PaymentModeCode.Equals("FC") || model.PaymentMode.PaymentModeCode.Equals("PP"))
            {
                ApplicableRate _applicableRate = applicableRateService.GetApplicableRate(model.CommodityTypeId, model.ServiceModeId, model.ServiceTypeId);

                if (_applicableRate != null)
                {
                    matrix = rateMatrixService.GetMatrix(_applicableRate.ApplicableRateId);
                }
                //matrix = rateMatrixService.FilterActiveBy(x => x.CommodityTypeId == model.a && x.ServiceTypeId == model.ServiceTypeId && x.ServiceModeId == model.ServiceModeId).FirstOrDefault();
                if (model.PaymentMode.PaymentModeCode.Equals("FC"))
                {
                    model.FreightCollectChargeId = basicFees.FirstOrDefault(x => x.ShipmentFeeName.Equals("FC")).ShipmentBasicFeeId;
                    model.FreightCollectCharge = basicFees.FirstOrDefault(x => x.ShipmentBasicFeeId == model.FreightCollectChargeId);
                    if (model.FreightCollectChargeId != null)
                        freightCollectCharge = model.FreightCollectCharge.Amount;
                }
            }
            else
            {
                if (model.Shipper.CompanyId != null && model.PaymentMode.PaymentModeCode.Equals("CAS"))
                {
                    matrix = companyService.GetById(model.Shipper.CompanyId ?? new Guid());
                }
                else if (model.Consignee.CompanyId != null && model.PaymentMode.PaymentModeCode.Equals("CAC"))
                {
                    matrix = companyService.GetById(model.Consignee.CompanyId ?? new Guid());
                }

                if (matrix != null)
                {
                    //if (matrix.HasFreightCollectCharge)
                    //{
                    //    model.FreightCollectChargeId = basicFees.FirstOrDefault(x => x.ShipmentFeeName.Equals("FC")).ShipmentBasicFeeId;
                    //    model.FreightCollectCharge = basicFees.FirstOrDefault(x => x.ShipmentBasicFeeId == model.FreightCollectChargeId);
                    //    if (model.FreightCollectChargeId != null)
                    //        freightCollectCharge = model.FreightCollectCharge.Amount;
                    //}
                }
                else
                {
                    ApplicableRate _applicableRate = applicableRateService.GetApplicableRate(model.CommodityTypeId, model.ServiceModeId, model.ServiceTypeId);

                    matrix = rateMatrixService.GetMatrix(_applicableRate.ApplicableRateId);

                }
            }

            #region AddOnCharges
            if (matrix != null)
            {
                if (matrix.HasAwbFee)
                {
                    model.AwbFeeId = null;
                    model.AwbFee = null;
                    var _awbfee = basicFees.FirstOrDefault(x => x.ShipmentFeeName.Equals("AWB Fee"));
                    if (_awbfee != null)
                    {
                        model.AwbFeeId = _awbfee.ShipmentBasicFeeId;
                        model.AwbFee = _awbfee;
                        awbFee = model.AwbFee.Amount;
                    }
                }
                if (matrix.HasInsurance)
                {
                    model.InsuranceId = null;
                    model.Insurance = null;
                    var _insurance = basicFees.FirstOrDefault(x => x.ShipmentFeeName.Equals("Insurance Fee"));
                    if (_insurance != null)
                    {
                        model.InsuranceId = _insurance.ShipmentBasicFeeId;
                        model.Insurance = _insurance;
                    }
                    if (model.Weight > model.PackageDimensions.Where(x => x.RecordStatus == 1).Sum(x => x.Evm))
                    {
                        insuranceCharge = model.Weight * model.Insurance.Amount;
                    }
                    else
                    {
                        insuranceCharge = model.PackageDimensions.Where(x=>x.RecordStatus==1).Sum(x => x.Evm) * model.Insurance.Amount;
                    }
                }
                if (matrix.HasFuelCharge)
                {
                    if (model.Weight > 5)
                    {
                        var fuelSurcharge =
                            fuelSurchargeService.FilterActiveBy(x => x.OriginGroupId == model.OriginCity.BranchCorpOffice.Province.Region.Group.GroupId && x.DestinationGroupId == model.DestinationCity.BranchCorpOffice.Province.Region.Group.GroupId);
                        if (fuelSurcharge != null && fuelSurcharge.Count > 0)
                        {
                            fuelCharge = model.Weight * fuelSurcharge.FirstOrDefault().Amount;
                            model.FuelSurchargeAmount = fuelCharge;
                        }
                    }
                }
                if (matrix.HasValuationCharge && model.DeclaredValue > 0)
                {
                    model.ValuationFactorId = null;
                    model.ValuationFactor = null;
                    var _valuationFee = basicFees.FirstOrDefault(x => x.ShipmentFeeName.Equals("Valuation Fee"));
                    if (_valuationFee != null)
                    {
                        model.ValuationFactorId = _valuationFee.ShipmentBasicFeeId;
                        model.ValuationFactor = _valuationFee;
                        model.ValuationAmount = model.DeclaredValue * (model.ValuationFactor.Amount / 100);
                        valuation = model.ValuationAmount;
                    }
                }
                if (matrix.HasDeliveryFee)
                {
                    ShipmentBasicFee _deliveryfee = basicFees.Where(x => x.ShipmentFeeName.Equals("Delivery Fee")).FirstOrDefault();

                    if (_deliveryfee != null)
                    {
                        ShipmentBasicFee Fee = new ShipmentBasicFee();
                        model.DeliveryFee = Fee;
                        model.DeliveryFeeId = _deliveryfee.ShipmentBasicFeeId;
                        defaultFee = _deliveryfee.Amount;


                        if (model.ChargeableWeight <= 5)
                        {
                            deliveryFee = defaultFee * 5;
                        }
                        else
                        {
                            deliveryFee = model.ChargeableWeight * defaultFee;
                        }
                        model.DeliveryFee.Amount = deliveryFee;
                    }

                }
                if (matrix.HasPerishableFee)
                {
                    model.PeracFeeId = null;
                    model.PeracFee = null;
                    var _perac = basicFees.FirstOrDefault(x => x.ShipmentFeeName.Equals("Perac Fee"));
                    if (_perac != null)
                    {
                        model.PeracFeeId = _perac.ShipmentBasicFeeId;
                        model.PeracFee = _perac;
                        peracFee = model.PeracFee.Amount;
                    }
                }
                if (matrix.HasDangerousFee)
                {
                    model.DangerousFeeId = null;
                    model.DangerousFee = null;
                    var _dangerousfee = basicFees.FirstOrDefault(x => x.ShipmentFeeName.Equals("Dangerous Fee"));
                    if (_dangerousfee != null)
                    {
                        model.DangerousFeeId = _dangerousfee.ShipmentBasicFeeId;
                        model.DangerousFee = _dangerousfee;
                        dangerousFee = model.DangerousFee.Amount;
                    }
                }

                // get discount
                if (model.Discount > 0)
                {
                    model.DiscountAmount = model.WeightCharge * (model.Discount / 100);
                }


                model.ShipmentSubTotal = model.WeightCharge + awbFee + insuranceCharge + fuelCharge + valuation + deliveryFee + peracFee + dangerousFee + freightCollectCharge + model.DrainingFee + model.CratingFee + model.QuarantineFee + model.HandlingFee - model.DiscountAmount;

                // Compute for VAT
                if (matrix.IsVatable)
                {
                    model.EvatId = null;
                    model.EVat = null;
                    if (basicFees.Exists(x => x.ShipmentFeeName.Equals("Vat")))
                    {
                        var _vat = basicFees.FirstOrDefault(x => x.ShipmentFeeName.Equals("Vat"));
                        model.EvatId = _vat.ShipmentBasicFeeId;
                        model.EVat = _vat;
                    }
                    if (model.EVat == null)
                    { vat = 12 / 100; }
                    else
                    {
                        vat = model.EVat.Amount / 100;
                    }
                    if (model.IsVatable)
                    {
                        model.ShipmentVatAmount = (model.ShipmentSubTotal * vat);
                    }
                    else
                    {
                        model.ShipmentVatAmount = 0;
                    }

                }
            }

            model.InsuranceAmount = insuranceCharge;
            model.ValuationAmount = valuation;
            model.FreightCharge = model.ShipmentSubTotal;
            model.ShipmentTotal = (model.ShipmentSubTotal + model.ShipmentVatAmount);
            #endregion

            Logs.AppLogs(LogPath, "Shipment BL - ComputeCharges - Done");
            return model;
        }

        // Compute for each Package EVM, Crating, Packaging, Draining           
        public ShipmentModel ComputePackageEvmCrating(ShipmentModel model)
        {
            CommodityType commodityType;
            List<PackageDimensionModel> packageDimensions = model.PackageDimensions.Where(x=>x.RecordStatus ==1).ToList();
            if (packageDimensions != null)
            {
                if (packageDimensions.Count > 0)
                {
                    decimal itemVolume = 0;
                    var cratings = cratingService.FilterActive();
                    var packagings = packagingService.FilterActive();
                    commodityType = model.CommodityType;
                    if (model.CommodityType == null)
                    {
                        commodityType = commodityTypeService.GetById(model.CommodityTypeId);
                    }

                    decimal drainingFee = 0;
                    Guid drainingId = new Guid();
                    var basicFees = shipmentBasicFeeService.FilterActiveBy(x => x.ShipmentFeeName.Equals("Draining Fee")).FirstOrDefault();
                    if (basicFees != null)
                    {
                        drainingFee = basicFees.Amount;
                        drainingId = basicFees.ShipmentBasicFeeId;
                    }

                    foreach (var packageDimension in packageDimensions)
                    {
                        itemVolume = packageDimension.Length * packageDimension.Width * packageDimension.Height;
                        if (packageDimension.Length > 0 && packageDimension.Width > 0 && packageDimension.Height > 0)
                        {
                            packageDimension.Evm = Decimal.Round((itemVolume / commodityType.EvmDivisor), 0);
                        }

                        packageDimension.DrainingFee = 0;
                        if (packageDimension.ForDraining)
                        {
                            packageDimension.DrainingId = drainingId;
                            packageDimension.DrainingFee = drainingFee;
                        }

                        packageDimension.CratingFee = 0;
                        if (packageDimension.CratingId != null)
                        {
                            if (cratings != null)
                            {
                                var crating = cratings.FirstOrDefault(x => x.CratingId == packageDimension.CratingId);
                                if (crating != null)
                                {
                                    decimal result = itemVolume * crating.Multiplier;
                                    if (result <= crating.BaseMaximum)
                                    {
                                        packageDimension.CratingFee = crating.BaseFee;
                                        model.CratingFee = crating.BaseFee;
                                    }
                                    else
                                    {
                                        packageDimension.CratingFee = result * crating.ExcessCost;
                                        model.CratingFee = result * crating.ExcessCost;
                                    }
                                }
                            }
                        }

                        packageDimension.PackagingFee = 0;
                        if (packageDimension.ForPackaging)
                        {
                            if (packagings != null)
                            {
                                decimal result = itemVolume * (decimal)0.000035315;
                                if (packagings.Exists(x => result <= x.BaseMaximum && result >= x.BaseMinimum))
                                {
                                    var packaging = packagings.Find(x => result <= x.BaseMaximum && result >= x.BaseMinimum);
                                    packageDimension.PackagingId = packaging.PackagingId;
                                    packageDimension.PackagingFee = packaging.BaseFee;
                                    model.PackagingFee = packaging.BaseFee;
                                }
                            }
                        }
                    }
                }
            }

            model.DrainingFee = model.PackageDimensions.Where(x => x.RecordStatus == 1).Sum(x => x.DrainingFee);
            model.CratingFee = model.PackageDimensions.Where(x => x.RecordStatus == 1).Sum(x => x.CratingFee);
            model.PackagingFee = model.PackageDimensions.Where(x => x.RecordStatus == 1).Sum(x => x.PackagingFee);

            return model;
        }

        public ShipmentModel ComputePackageWeightCharge(ShipmentModel model)
        {
            //List<List<ExpressRate>> expressRates = new List<List<ExpressRate>>();
            List<ExpressRate> expressRates = new List<ExpressRate>();
            RateMatrix matrix = new RateMatrix();

            ApplicableRate _applicableRate = applicableRateService.GetApplicableRate(model.CommodityTypeId, model.ServiceModeId, model.ServiceTypeId);

            if (_applicableRate != null)
            {
                matrix = rateMatrixService.GetMatrix(_applicableRate.ApplicableRateId);
            }
            //else
            //{
            //    matrix = rateMatrixService.GetMatrix(model.CommodityTypeId, model.ServiceTypeId,
            //    model.ServiceModeId);
            //}


            ExpressRate rates = null;

            if (matrix != null)
            {
                if (model.ShipMode == null)
                {
                }
                else
                {

                    if (model.ShipMode.ShipModeName.Equals("Transhipment"))
                    {
                        #region Transhipment

                        //'get the Hub/leg - temporarily Manila is the leg
                        //'get the first rate
                        //'get the second rate
                        //'add the two combination
                        ///'multiply to chargeable weight
                        //'it will be the weight charge

                        ExpressRate rate1 = new ExpressRate();
                        ExpressRate rate2 = new ExpressRate();

                        if (model.TransShipmentLeg == null)
                        {
                            model.TransShipmentLeg = transShipmentLegService.GetAll().FirstOrDefault();
                            model.TransShipmentLegId = model.TransShipmentLeg.TransShipmentLegId;
                        }

                        rate1 = expressRateService.GetExpressRatesByMatrix(matrix.RateMatrixId, model.DestinationCityId, model.TransShipmentLeg.CityId);
                        rate2 = expressRateService.GetExpressRatesByMatrix(matrix.RateMatrixId, model.OriginCityId, model.TransShipmentLeg.CityId);

                        //if( rate1 == null)
                        //{
                        //    rate1 = expressRateService.GetExpressRatesByMatrix(matrix.RateMatrixId,model.TransShipmentLeg.CityId,model.DestinationCityId);
                        //}

                        //if (rate2 == null)
                        //{
                        //    rate2 = expressRateService.GetExpressRatesByMatrix(matrix.RateMatrixId, model.TransShipmentLeg.CityId, model.OriginCityId );
                        //}

                        if (rate1 != null)
                        {
                            expressRates.Add(rate1);
                        }
                        if (rate2 != null)
                        {
                            expressRates.Add(rate2);
                        }


                        #endregion
                    }
                    else
                    {
                        rates = expressRateService.GetExpressRatesByMatrix(matrix.RateMatrixId, model.OriginCityId, model.DestinationCityId);
                        if (rates != null)
                        {
                            expressRates.Add(rates);
                        }
                        else
                        {

                            ExpressRate rate1 = new ExpressRate();
                            ExpressRate rate2 = new ExpressRate();

                            if (model.TransShipmentLeg == null)
                            {
                                model.TransShipmentLeg = transShipmentLegService.GetAll().FirstOrDefault();
                                model.TransShipmentLegId = model.TransShipmentLeg.TransShipmentLegId;
                            }

                            rate1 = expressRateService.GetExpressRatesByMatrix(matrix.RateMatrixId, model.DestinationCityId, model.TransShipmentLeg.CityId);
                            rate2 = expressRateService.GetExpressRatesByMatrix(matrix.RateMatrixId, model.OriginCityId, model.TransShipmentLeg.CityId);

                            if (rate1 != null)
                            {
                                expressRates.Add(rate1);
                            }
                            if (rate2 != null)
                            {
                                expressRates.Add(rate2);
                            }

                        }
                    }
                }

                if (matrix.ApplyEvm && !matrix.ApplyWeight)
                {
                    model.ChargeableWeight = model.PackageDimensions.Where(x=>x.RecordStatus ==1).Sum(x => x.Evm);
                }
                else if (!matrix.ApplyEvm && matrix.ApplyWeight)
                {
                    model.ChargeableWeight = model.Weight;
                }
                else
                {
                    model.ChargeableWeight = model.Weight;
                    if (model.Weight < model.PackageDimensions.Where(x => x.RecordStatus == 1).Sum(x => x.Evm))
                        model.ChargeableWeight = Math.Round(model.PackageDimensions.Where(x => x.RecordStatus == 1).Sum(x => x.Evm));
                }
            }
            #region GetExpressRates

            #endregion

            decimal expressRateAmount = 1;
            model.WeightCharge = 0;
            if (expressRates.Count > 0)
            {
                foreach (var expressRate in expressRates)
                {
                    if (model.ChargeableWeight >= 1 && model.ChargeableWeight < 6)
                    {
                        expressRateAmount = expressRate.C1to5Cost;
                        //model.WeightCharge = model.WeightCharge + (model.ChargeableWeight * expressRateAmount);
                        model.WeightCharge += expressRateAmount;
                    }
                    else if (model.ChargeableWeight >= 6 && model.ChargeableWeight < 49)
                    {
                        expressRateAmount = expressRate.C6to49Cost;
                        model.WeightCharge += model.ChargeableWeight * expressRateAmount;
                    }
                    else if (model.ChargeableWeight >= 50 && model.ChargeableWeight < 249)
                    {
                        expressRateAmount = expressRate.C50to249Cost;
                        model.WeightCharge += model.ChargeableWeight * expressRateAmount;
                    }
                    else if (model.ChargeableWeight >= 250 && model.ChargeableWeight < 999)
                    {
                        expressRateAmount = expressRate.C250to999Cost;
                        model.WeightCharge += model.ChargeableWeight * expressRateAmount;
                    }
                    else if (model.ChargeableWeight >= 1000 && model.ChargeableWeight < 10000)
                    {
                        expressRateAmount = expressRate.C1000_10000Cost;
                        model.WeightCharge += model.ChargeableWeight * expressRateAmount;
                    }
                }
            }

            return model;
        }

        public List<ShipmentModel> ComputeCharges(List<ShipmentModel> models)
        {
            Logs.AppLogs(LogPath, "Shipment BL - ComputeCharges", "Shipment count: " + models.Count.ToString());
            List<ShipmentModel> _models = new List<ShipmentModel>();
            foreach (var item in models)
            {
                _models.Add(ComputeCharges(item));
            }
            return _models;
        }

        public List<ShipmentModel> GetByDate(DateTime date)
        {
            var models = EntitiesToModels(FilterActiveBy(x => x.DateAccepted == date)).ToList();
            models = ComputeCharges(models);
            return models;
        }

        public List<ShipmentModel> GetShipments()
        {
            var entities = FilterActive();
            var models = EntitiesToModels(entities);

            return models;
        }

        //public List<ExpressRate> GetExpressRatesBy(Expression<Func<ExpressRate, bool>> filter)
        //{
        //    return expressRateService.FilterActiveBy(filter);
        //}

        public void AddEdit(ShipmentModel model)
        {
            if (!IsExist(x => x.ShipmentId == model.ShipmentId))
            {
                foreach (var item in model.PackageDimensions)
                {
                    item.PackageDimensionId = Guid.NewGuid();
                }
                Add(ModelToEntity(model));
            }
            else
            {
                foreach (var modelDimension in model.PackageDimensions)
                {
                    if (packageDimensionService.IsExist(x => x.PackageDimensionId == modelDimension.PackageDimensionId))
                    {
                        var dim = packageDimensionService.FilterBy(x => x.PackageDimensionId == modelDimension.PackageDimensionId).FirstOrDefault();
                        dim.ModifiedBy = modelDimension.ModifiedBy;
                        dim.ModifiedDate = modelDimension.ModifiedDate;
                        dim.RecordStatus = modelDimension.RecordStatus;
                        packageDimensionService.Edit(dim);

                    }
                    else
                    {
                        PackageDimension newDim = new PackageDimension();
                        newDim.PackageDimensionId = Guid.NewGuid();
                        newDim.ShipmentId = model.ShipmentId;
                        newDim.Length = modelDimension.Length;
                        newDim.Width = modelDimension.Width;
                        newDim.Height = modelDimension.Height;
                        newDim.CratingId = modelDimension.CratingId;
                        newDim.Crating = modelDimension.Crating;
                        newDim.DrainingId = modelDimension.DrainingId;
                        newDim.DrainingFee = modelDimension.Draining;
                        newDim.PackagingId = modelDimension.PackagingId;
                        newDim.Packaging = modelDimension.Packaging;
                        newDim.CreatedBy = model.ModifiedBy;
                        newDim.CreatedDate = model.ModifiedDate;
                        newDim.ModifiedBy = model.ModifiedBy;
                        newDim.ModifiedDate = model.ModifiedDate;
                        newDim.RecordStatus = model.RecordStatus;
                        packageDimensionService.Add(newDim);
                    }
                }

                Shipment shipment = new Shipment();
                List<PackageDimension> _dimensions = packageDimensionService.FilterBy(x => x.ShipmentId == model.ShipmentId).ToList();
                shipment = this.ModelToEntity(model);
                shipment.PackageDimensions = _dimensions;
                this.Edit(shipment);

                //Edit(this.ModelToEntity(shipment));
            }
        }

        public void AddEdit(Shipment entity)
        {
            if (!IsExist(x => x.ShipmentId == entity.ShipmentId))
            {
                foreach (var item in entity.PackageDimensions)
                {
                    item.PackageDimensionId = Guid.NewGuid();
                }
                Add(entity);
            }
            else
            {
                var dimensions = packageDimensionService.FilterBy(x => x.ShipmentId == entity.ShipmentId);
                foreach (var entityDimension in dimensions)
                {
                    entityDimension.ModifiedBy = entity.ModifiedBy;
                    entityDimension.ModifiedDate = entity.ModifiedDate;
                    entityDimension.RecordStatus = (int)RecordStatus.Deleted;
                    packageDimensionService.Edit(entityDimension);
                }
                foreach (var modelDimension in entity.PackageDimensions)
                {
                    if (packageDimensionService.IsExist(x => x.ShipmentId == entity.ShipmentId && x.Length == modelDimension.Length && x.Width == modelDimension.Width && x.Height == modelDimension.Height))
                    {
                        var dim = packageDimensionService.FilterBy(x => x.ShipmentId == entity.ShipmentId && x.Length == modelDimension.Length && x.Width == modelDimension.Width && x.Height == modelDimension.Height).FirstOrDefault();
                        dim.ModifiedBy = entity.ModifiedBy;
                        dim.ModifiedDate = entity.ModifiedDate;
                        dim.RecordStatus = entity.RecordStatus;
                        packageDimensionService.Edit(dim);
                    }
                    else
                    {
                        PackageDimension newDim = new PackageDimension();
                        newDim.PackageDimensionId = Guid.NewGuid();
                        newDim.ShipmentId = entity.ShipmentId;
                        newDim.Length = modelDimension.Length;
                        newDim.Width = modelDimension.Width;
                        newDim.Height = modelDimension.Height;
                        newDim.CratingId = modelDimension.CratingId;
                        newDim.Crating = modelDimension.Crating;
                        newDim.PackagingId = modelDimension.PackagingId;
                        newDim.Packaging = modelDimension.Packaging;
                        newDim.DrainingId = modelDimension.DrainingId;
                        newDim.DrainingFee = modelDimension.DrainingFee;
                        newDim.CreatedDate = entity.ModifiedDate;
                        newDim.CreatedBy = entity.ModifiedBy;
                        newDim.ModifiedBy = entity.ModifiedBy;
                        newDim.ModifiedDate = entity.ModifiedDate;
                        newDim.RecordStatus = entity.RecordStatus;
                        packageDimensionService.Add(newDim);
                    }
                }

                Edit(entity);
            }
        }

        public List<ShipmentByDateBcoModel> GetPickupCargoByDateBco(DateTime pickupDate, Guid bcoId)
        {
            TruckAreaMappingBL truckAreaService = new TruckAreaMappingBL(_unitOfWork);
            EmployeeBL employeeService = new EmployeeBL();
            //var employeeAssignments = employeeAssignmentService.GetByDateBco(pickupDate, bcoId).Select(x => x.EmployeeId).ToList();
            List<Guid> employeeids = employeeService.GetAll().Where(x => x.AssignedToArea.City.BranchCorpOfficeId == bcoId).Select(x => x.EmployeeId).ToList();
            var shipments = base.FilterActiveBy(x =>
                    (x.DateAccepted.Year == pickupDate.Year && x.DateAccepted.Month == pickupDate.Month && x.DateAccepted.Day == pickupDate.Day) && employeeids.Contains(x.AcceptedById));

            var truckarea = truckAreaService.FilterBy(x => x.DateAssigned <= pickupDate && x.RevenueUnit.City.BranchCorpOffice.BranchCorpOfficeId == bcoId).OrderByDescending(x => x.DateAssigned).FirstOrDefault();

            List<ShipmentByDateBcoModel> models = new List<ShipmentByDateBcoModel>();
            if (shipments != null)
            {
                foreach (var item in shipments)
                {
                    ShipmentByDateBcoModel model = new ShipmentByDateBcoModel();
                    model.Shipment = item;
                    var revenueunit = revenueUnitService.GetById(item.AcceptedBy.AssignedToAreaId);
                    if (revenueunit == null)
                    {
                        var bco = bcoService.GetById(item.AcceptedBy.AssignedToAreaId);
                        model.BranchCorpOfficeId = item.AcceptedBy.AssignedToAreaId;
                        model.BranchCorpOffice = bco.BranchCorpOfficeName;
                    }
                    else
                    {
                        model.AreaId = revenueunit.RevenueUnitId;
                        model.Area = revenueunit;
                        model.BranchCorpOfficeId = revenueunit.City.BranchCorpOffice.BranchCorpOfficeId;
                        model.BranchCorpOffice = revenueunit.City.BranchCorpOffice.BranchCorpOfficeName;
                    }

                    if (truckarea != null)
                    {
                        model.TruckId = truckarea.TruckId;
                        model.Truck = truckarea.Truck;
                    }
                    else
                    {
                        model.TruckId = new Guid();
                        model.Truck = new Truck();
                    }
                    model.DriverId = new Guid();
                    model.Driver = new Employee();
                    models.Add(model);
                }
            }

            return models;
        }

        // The following methods as SOA specific
        public ShipmentModel ComputeBalances(ShipmentModel model, DateTime? dueDate)
        {
            Logs.AppLogs(LogPath, "Shipment BL - ComputeBalances");
            model.CurrentBalance = model.ShipmentTotal;
            model.PreviousAmountDue = model.CurrentBalance;
            if (model.StatementOfAccount == null)
            {
                #region Shipment No SOA
                Logs.AppLogs(LogPath, "Shipment BL - ComputeBalances - SOA is null");
                try
                {
                    if (model.Payments != null && model.Payments.Count > 0)
                    {
                        foreach (var item in model.Payments.OrderBy(x => x.PaymentDate))
                        {
                            model.CurrentPayments = model.CurrentPayments + item.Amount;
                            model.CurrentBalance = model.CurrentBalance - item.Amount;
                        }
                        model.LastPaymentDate = model.Payments.Max(x => x.PaymentDate);
                    }
                }
                catch (Exception ex)
                {
                    Logs.ErrorLogs(LogPath, "Shipment BL - ComputeBalances - SOA is null", ex);
                }
                #endregion
            }
            else
            {
                #region Shipment with SOA
                Logs.AppLogs(LogPath, "Shipment BL - ComputeBalances - SOA is not null");
                try
                {
                    var soa = model.StatementOfAccount;
                    DateTime soaStart = new DateTime(soa.StatementOfAccountPeriodFrom.Year, soa.StatementOfAccountPeriodFrom.Month, soa.StatementOfAccountPeriodFrom.Day, 0, 0, 0);
                    DateTime soaEnd = new DateTime(soa.StatementOfAccountPeriodUntil.Year, soa.StatementOfAccountPeriodUntil.Month, soa.StatementOfAccountPeriodUntil.Day, 11, 59, 59);
                    DateTime soaDueDate = new DateTime(model.StatementOfAccount.SoaDueDate.Year, model.StatementOfAccount.SoaDueDate.Month, model.StatementOfAccount.SoaDueDate.Day, 11, 59, 59);
                    DateTime _soaDueDate = soaDueDate;
                    double period = ((soaEnd.Date - soaStart.Date).TotalDays) + 1;

                    DateTime currentDate = soaStart;
                    if (dueDate == null)
                        dueDate = soaDueDate;

                    model.LastPaymentDate = model.Payments.Max(x => x.PaymentDate);

                    List<ShipmentAdjustment> _adjustments = new List<ShipmentAdjustment>();
                    if (model.Adjustments != null && model.Adjustments.Count > 0)
                    {
                        // TODO Bug not getting record if DateAdjusted is equal to dueDate
                        _adjustments = adjustmentService.FilterActiveBy(x => x.ShipmentId == model.ShipmentId && x.DateAdjusted <= dueDate);
                        model.TotalAdjustments = _adjustments.Sum(x => x.AdjustmentAmount);
                    }

                    decimal tempPreviousAmountDue = model.PreviousAmountDue;
                    while (currentDate <= dueDate)
                    {
                        // data from previous SOA for Details and Report
                        // this is ok
                        DateTime _dueDate = dueDate.GetValueOrDefault().AddDays(-period);
                        if ((currentDate.Date == _dueDate.Date && model.CurrentBalance > 0) && dueDate.GetValueOrDefault().Date >= soaDueDate.Date)
                        {
                            DateTime _currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 11, 59, 59);
                            var payments = model.Payments.Where(x => x.StatementOfAccountPaymentId != null && x.PaymentDate <= _currentDate);
                            model.PreviousPayments = 0;
                            if (payments != null && payments.Count() > 0)
                            {
                                model.PreviousPayments = payments.Sum(x => x.Amount);
                            }

                            var adjustment = _adjustments.Where(x => x.StatementOfAccount.StatementOfAccountPeriodFrom > _currentDate.AddDays(-(7 + period)) && x.StatementOfAccount.SoaDueDate <= _currentDate);
                            model.PreviousAdjustments = 0;
                            if (adjustment != null && adjustment.Count() > 0)
                            {
                                model.PreviousAdjustments = adjustment.Sum(x => x.AdjustmentAmount);
                            }
                        }

                        // data for current SOA
                        if ((model.Payments != null && model.Payments.Count > 0) && currentDate == _soaDueDate)
                        {
                            var payments = model.Payments.Where(x => x.StatementOfAccountPayment.StatementOfAccount.StatementOfAccountPeriodFrom >= soaStart &&
                               x.StatementOfAccountPayment.StatementOfAccount.SoaDueDate <= _soaDueDate);
                            model.CurrentPayments = 0;
                            if (payments != null && payments.Count() > 0)
                            {
                                model.CurrentPayments = payments.Sum(x => x.Amount);
                                model.CurrentBalance = model.CurrentBalance - payments.Sum(x => x.Amount);
                            }
                            soaStart = soaStart.AddDays(period);
                            _soaDueDate = _soaDueDate.AddDays(period);
                        }

                        if ((_adjustments != null && _adjustments.Count > 0) && currentDate < dueDate)
                        {
                            var adjustment = _adjustments.SingleOrDefault(x => x.ShipmentId == model.ShipmentId && x.DateAdjusted == currentDate);
                            model.Adjustment = 0;
                            if (adjustment != null)
                            {
                                model.Adjustment = adjustment.AdjustmentAmount;
                                model.CurrentBalance = model.CurrentBalance + model.Adjustment;
                            }
                        }

                        // compute for surcharge
                        // this is ok
                        if ((currentDate.Date == soaDueDate.Date && model.CurrentBalance <= 0) && dueDate > soaDueDate)
                        {
                            model.PreviousAmountDue = tempPreviousAmountDue;
                            model.PreviousBalance = model.PreviousAmountDue - model.PreviousPayments + model.PreviousAdjustments;
                            model.Surcharge = 0;
                            tempPreviousAmountDue = model.PreviousBalance + model.Surcharge;
                            model.CurrentBalance = tempPreviousAmountDue - model.CurrentPayments + model.Adjustment;
                            soaDueDate = soaDueDate.AddDays(period);
                        }
                        else if ((currentDate.Date == soaDueDate.Date && model.CurrentBalance > 0) && dueDate > soaDueDate)
                        {
                            model.PreviousAmountDue = tempPreviousAmountDue;
                            model.PreviousBalance = model.PreviousAmountDue - model.PreviousPayments + model.PreviousAdjustments;
                            model.Surcharge = (model.PreviousBalance * (Convert.ToDecimal(3) / Convert.ToDecimal(100))); // Surcharge is 3%
                            tempPreviousAmountDue = model.PreviousBalance + model.Surcharge;
                            model.CurrentBalance = tempPreviousAmountDue - model.CurrentPayments + model.Adjustment;
                            soaDueDate = soaDueDate.AddDays(period);
                        }

                        currentDate = currentDate.AddDays(1);
                    }
                }
                catch (Exception ex)
                {
                    Logs.ErrorLogs(LogPath, "Shipment BL - ComputeBalances - SOA is not null", ex);
                }
                #endregion
            }

            return model;

        }

        public List<ShipmentModel> ComputeBalances(List<ShipmentModel> models, DateTime? dueDate)
        {
            Logs.AppLogs(LogPath, "Shipment BL - ComputeBalances", "Shipment count: " + models.Count.ToString());
            List<ShipmentModel> computedShipments = new List<ShipmentModel>();
            foreach (var item in models)
            {
                if (item.StatementOfAccount == null)
                { computedShipments.Add(ComputeBalances(item, dueDate)); }
                else
                {
                    computedShipments.Add(ComputeBalances(item, dueDate));
                }
            }
            return computedShipments;
        }

        public List<ShipmentModel> GetByCompanyAccountNoByPeriod(string companyAccountNo, DateTime dateFrom, DateTime dateUntil)
        {
            Logs.AppLogs(LogPath, "Shipment BL - GetByCompanyAccountNoByPeriod");
            var clients = clientService.FilterActiveBy(x => x.Company.AccountNo.Equals(companyAccountNo) || x.Company.MotherCompany.AccountNo.Equals(companyAccountNo));
            //var shipments = GetByPeriod(dateFrom, dateUntil);
            var entities = (from shp in FilterActiveBy(x => x.DateAccepted > dateFrom && x.DateAccepted < dateUntil)
                            from cln in clients
                            where ((shp.ConsigneeId == cln.ClientId && shp.Consignee.Company.PaymentMode.PaymentModeCode.Equals("CAC")) || (shp.ShipperId == cln.ClientId && shp.Shipper.Company.PaymentMode.PaymentModeCode.Equals("CAS")))
                            select shp).Distinct().ToList();

            if (entities != null)
            {
                Logs.AppLogs(LogPath, "Shipment BL - GetByCompanyAccountNoByPeriod", "return: _shipments- " + entities.Count.ToString());
                var models = ComputeCharges(EntitiesToModels(entities));
                return models;
            }
            Logs.AppLogs(LogPath, "Shipment BL - GetByCompanyAccountNoByPeriod", "return: _shipments- null");
            return null;
        }

        public void LinkToSoa(Guid statementOfAccountId, string companyAccountNo, DateTime periodStart, DateTime periodEnd)
        {
            DateTime _periodStart = new DateTime(periodStart.Year, periodStart.Month, periodStart.Day, 0, 0, 0);
            DateTime _periodEnd = new DateTime(periodEnd.Year, periodEnd.Month, periodEnd.Day, 11, 59, 59);
            var clients = clientService.FilterActiveBy(x => x.Company.AccountNo.Equals(companyAccountNo) || x.Company.MotherCompany.AccountNo.Equals(companyAccountNo));
            var entities = (from shp in FilterActiveBy(x => x.DateAccepted > _periodStart && x.DateAccepted < _periodEnd)
                            from cln in clients
                            where ((shp.ConsigneeId == cln.ClientId && shp.PaymentMode.PaymentModeCode.Equals("CAC")) || (shp.ShipperId == cln.ClientId && shp.PaymentMode.PaymentModeCode.Equals("CAS")))
                            select shp).Distinct().ToList();

            if (entities != null)
            {
                foreach (var item in entities)
                {
                    if (item.StatementOfAccountId == null)
                    {
                        item.StatementOfAccountId = statementOfAccountId;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedBy = new Guid();
                        Edit(item);
                    }
                }
            }
        }

        public List<ShipmentModel> GetSoaShipmentsByCompanyId(Guid companyId)
        {
            Logs.AppLogs(LogPath, "Shipment BL - GetUnpaidShipmentsByCompanyId", companyId.ToString());
            List<ShipmentModel> models = new List<ShipmentModel>();
            var clients = companyService.GetById(companyId).Clients;
            foreach (var item in clients)
            {
                models.AddRange(GetSoaShipmentsByClientId(item.ClientId));
            }
            return models.OrderByDescending(x => x.DateAccepted).ToList();
        }

        public List<ShipmentModel> GetSoaShipmentsByClientId(Guid clientId)
        {
            Logs.AppLogs(LogPath, "Shipment BL - GetUnpaidShipmentsByClientId", clientId.ToString());
            var unpaidShipments = FilterActiveBy(x => ((x.ConsigneeId == clientId && x.PaymentMode.PaymentModeCode.Equals("CAC")) || (x.ShipperId == clientId && x.PaymentMode.PaymentModeCode.Equals("CAS")))).ToList().Distinct().OrderByDescending(x => x.DateAccepted).ToList();

            List<ShipmentModel> models = new List<ShipmentModel>();
            if (unpaidShipments != null)
            {
                models = EntitiesToModels(unpaidShipments);
            }

            return models;
        }
    }
}