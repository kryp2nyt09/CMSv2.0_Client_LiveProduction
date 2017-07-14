using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.Common.Enums;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class RateMatrixBL : BaseAPCargoBL<RateMatrix>
    {
        private ICmsUoW _unitOfWork;
        private ExpressRateBL expressRateService;

        public RateMatrixBL()
        {
            _unitOfWork = GetUnitOfWork();
            expressRateService = new ExpressRateBL(_unitOfWork);
        }

        public RateMatrixBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<RateMatrix, object>>[] Includes()
        {
            return new Expression<Func<RateMatrix, object>>[]
            {
                x=>x.ApplicableRate,
                //x => x.CommodityType,
                //x=>x.ServiceType,
                //x=>x.ServiceMode,
                x=>x.ExpressRates
            };
        }

        public List<Guid> GetApplicableRates()
        {
            return GetAll().Select(x => x.ApplicableRateId).Distinct().ToList();
        }
        
        /// <summary>
        /// Copy matrix from another Applicable Rate
        /// </summary>
        /// <param name="sourceId">Source Applicable Rate</param>
        /// <param name="targetId">Target Applicable Rate</param>
        public void CopyCreate(Guid sourceId, Guid targetId, Guid userId)
        {
            var existingMatrix = FilterActiveBy(x => x.ApplicableRateId == sourceId).FirstOrDefault();
            if (existingMatrix != null)
            {
                var existingExpressRates =
                    expressRateService.FilterActiveBy(x => x.RateMatrixId == existingMatrix.RateMatrixId);

                RateMatrix newEntity = new RateMatrix();
                newEntity.RateMatrixId = Guid.NewGuid();
                newEntity.ApplicableRateId = targetId;
                //newEntity.CommodityTypeId = existingMatrix.CommodityTypeId;
                //newEntity.ServiceTypeId = existingMatrix.ServiceTypeId;
                //newEntity.ServiceModeId = existingMatrix.ServiceModeId;
                newEntity.HasAwbFee = existingMatrix.HasAwbFee;
                newEntity.HasInsurance = existingMatrix.HasInsurance;
                newEntity.ApplyEvm = existingMatrix.ApplyEvm;
                newEntity.ApplyWeight = existingMatrix.ApplyWeight;
                newEntity.HasFuelCharge = existingMatrix.HasFuelCharge;
                newEntity.IsVatable = existingMatrix.IsVatable;
                newEntity.HasValuationCharge = existingMatrix.HasValuationCharge;
                newEntity.HasDeliveryFee = existingMatrix.HasDeliveryFee;
                newEntity.HasPerishableFee = existingMatrix.HasPerishableFee;
                newEntity.HasDeliveryFee = existingMatrix.HasDeliveryFee;
                newEntity.CreatedBy = userId;
                newEntity.CreatedDate = DateTime.Now;
                newEntity.ModifiedBy = userId;
                newEntity.ModifiedDate = DateTime.Now;
                newEntity.RecordStatus = (int)RecordStatus.Active;

                if (existingExpressRates != null)
                {
                    newEntity.ExpressRates = new List<ExpressRate>();
                    foreach (var item in existingExpressRates)
                    {
                        ExpressRate newRate = new ExpressRate();
                        newRate.ExpressRateId = Guid.NewGuid();
                        newRate.OriginCityId = item.OriginCityId;
                        newRate.DestinationCityId = item.DestinationCityId;
                        newRate.C1to5Cost = item.C1to5Cost;
                        newRate.C6to49Cost = item.C6to49Cost;
                        newRate.C50to249Cost = item.C50to249Cost;
                        newRate.C250to999Cost = item.C250to999Cost;
                        newRate.C1000_10000Cost = item.C1000_10000Cost;
                        newRate.EffectiveDate = item.EffectiveDate;
                        newRate.CreatedBy = userId;
                        newRate.CreatedDate = DateTime.Now;
                        newRate.ModifiedBy = userId;
                        newRate.ModifiedDate = DateTime.Now;
                        newEntity.RecordStatus = (int)RecordStatus.Active;
                        newEntity.ExpressRates.Add(newRate);
                    }
                }

                Add(newEntity);
            }
        }

        //public RateMatrix GetMatrix(Guid CommodityTypeId, Guid ServiceTypeId, Guid ServiceModeId)
        //{
        //    RateMatrix result = null;
        //    result = FilterActiveBy(x => x.CommodityTypeId == CommodityTypeId && x.ServiceTypeId == ServiceTypeId &&
        //                 x.ServiceModeId == ServiceModeId).FirstOrDefault();

        //    return result;
        //}

        public RateMatrix GetMatrix(Guid ApplicableRateId)
        {
            RateMatrix result = null;
            result = FilterActiveBy(x => x.ApplicableRateId == ApplicableRateId).FirstOrDefault();

            return result;
        }
    }
}
