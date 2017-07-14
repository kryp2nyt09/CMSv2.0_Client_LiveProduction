using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ShipModeBL : BaseAPCargoBL<ShipMode>
    {
        private ICmsUoW _unitOfWork;

        public ShipModeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ShipModeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<ShipMode, object>>[] Includes()
        {
            return new Expression<Func<ShipMode, object>>[]
                {
                    
                };
        }

    }
}
