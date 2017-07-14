using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class WeightBreakBL : BaseAPCargoBL<WeightBreak>
    {
        private ICmsUoW _unitOfWork;

        public WeightBreakBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public WeightBreakBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<WeightBreak, object>>[] Includes()
        {
            return new Expression<Func<WeightBreak, object>>[]
                {
                    x => x.CommodityType
                };
        }
    }
}
