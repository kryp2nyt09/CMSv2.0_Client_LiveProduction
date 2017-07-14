using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class CommodityBL : BaseAPCargoBL<Commodity>
    {
        private ICmsUoW _unitOfWork;

        public CommodityBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public CommodityBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<Commodity, object>>[] Includes()
        {
            return new Expression<Func<Commodity, object>>[]
                {

                };
        }
    }
}
