using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class GoodsDescriptionBL : BaseAPCargoBL<GoodsDescription>
    {
        private ICmsUoW _unitOfWork;

        public GoodsDescriptionBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public GoodsDescriptionBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<GoodsDescription, object>>[] Includes()
        {
            return new Expression<Func<GoodsDescription, object>>[]
                {
                    
                };
        }

    }
}
