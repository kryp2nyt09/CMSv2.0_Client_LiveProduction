using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System.Collections.Generic;

namespace CMS2.BusinessLogic
{
    public class RevenueUnitBL : BaseAPCargoBL<RevenueUnit>
    {
        private ICmsUoW _unitOfWork;

        public RevenueUnitBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public RevenueUnitBL(ICmsUoW unitOfWork)
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
    }
}
