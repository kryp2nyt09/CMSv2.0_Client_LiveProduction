using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class CityBL : BaseAPCargoBL<City>
    {
        private ICmsUoW _unitOfWork;
        private BranchCorpOfficeBL bcoService;

        public CityBL()
        {
            _unitOfWork = GetUnitOfWork();
            bcoService = new BranchCorpOfficeBL(_unitOfWork);
        }

        public CityBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<City, object>>[] Includes()
        {
            return new Expression<Func<City, object>>[]
                {
                    x => x.RevenueUnits,
                };
        }
        
        public List<BranchCorpOffice> GetBranchCorpOffices()
        {
            return bcoService.FilterActive();
        }

    }
}
