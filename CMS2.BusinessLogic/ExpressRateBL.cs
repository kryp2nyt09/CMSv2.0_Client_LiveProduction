using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.Common.Enums;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ExpressRateBL : BaseAPCargoBL<ExpressRate>
    {
        private ICmsUoW _unitOfWork;
        private CityBL cityService;
        private CommodityTypeBL commodityService;

        public ExpressRateBL()
        {
            _unitOfWork = GetUnitOfWork();
            cityService = new CityBL(_unitOfWork);
            commodityService = new CommodityTypeBL(_unitOfWork);
        }

        public ExpressRateBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<ExpressRate, object>>[] Includes()
        {
            return new Expression<Func<ExpressRate, object>>[]
            {
                x=>x.OriginCity,
                x => x.DestinationCity,
                x=>x.RateMatrix
            };
        }

        public List<City> GetCities()
        {
            return cityService.FilterActive();
        }

        public List<CommodityType> GetCommodities()
        {
            return commodityService.FilterActive();
        }
        
        public ExpressRate GetExpressRatesByMatrix(Guid MatrixId, Guid OriginCityId, Guid DestinationCityId)
        {
            ExpressRate result = null;
            result = FilterActiveBy( x => x.OriginCityId == OriginCityId && x.DestinationCityId == DestinationCityId && x.RateMatrixId == MatrixId).FirstOrDefault();
            if (result == null)
            {
                result = FilterActiveBy(x => x.OriginCityId == DestinationCityId && x.DestinationCityId == OriginCityId && x.RateMatrixId == MatrixId).FirstOrDefault();
            }

            if (result==null)
                result = null;

            return result;
        }

        public List<City> GetOriginCitiesByMatrixId(Guid id)
        {
            return FilterActiveBy(x => x.RateMatrixId == id).Select(x => x.OriginCity).Distinct().ToList();
        }

        public List<City> GetDestinationCitiesByMatrixId(Guid id)
        {
            return FilterActiveBy(x => x.RateMatrixId == id).Select(x => x.DestinationCity).Distinct().ToList();
        }
    }
}
