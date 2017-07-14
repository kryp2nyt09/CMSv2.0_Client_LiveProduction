using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS2.Common.Constants;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class GatewaySatOfficeBL : BaseAPCargoBL<RevenueUnit>
    {
        private ICmsUoW _unitOfWork;
        private CityBL cityService;
        private ClusterBL clusterService;

        public GatewaySatOfficeBL()
        {
            _unitOfWork = GetUnitOfWork();
            cityService = new CityBL(_unitOfWork);
            clusterService = new ClusterBL(_unitOfWork);
        }

        public GatewaySatOfficeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<RevenueUnit, object>>[] Includes()
        {
            return new Expression<Func<RevenueUnit, object>>[]
                {
                    x=>x.City
                };
        }

        public List<City> GetCities()
        {
            return cityService.FilterActive();
        }

        public List<Cluster> GetClusters()
        {
            return clusterService.FilterActive();
        }

        public override List<RevenueUnit> FilterActive()
        {
            Guid id = Guid.Parse(RevenueUnitTypeId.GatewaySat);
            return base.FilterActiveBy(x => x.RevenueUnitTypeId == id);
        }
    }
}
