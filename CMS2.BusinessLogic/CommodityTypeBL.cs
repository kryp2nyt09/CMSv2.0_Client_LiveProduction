using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class CommodityTypeBL : BaseAPCargoBL<CommodityType>
    {
        private ICmsUoW _unitOfWork;
        
        public CommodityTypeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public CommodityTypeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<CommodityType, object>>[] Includes()
        {
            return new Expression<Func<CommodityType, object>>[]
                {
                    x => x.Commoditites
                };
        }
    }
}
