using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class GatewayBL:BaseAPCargoBL<Gateway>
    {
        private ICmsUoW _unitOfWork;
        private GatewayTypeBL gatewayTypeService;

        public GatewayBL()
        {
            _unitOfWork = GetUnitOfWork();
            gatewayTypeService = new GatewayTypeBL(_unitOfWork);
        }

        public GatewayBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<Gateway, object>>[] Includes()
        {
            return new Expression<Func<Gateway, object>>[]
                {
                    x => x.GatewayType
                };
        }
        

        public List<GatewayType> GetGatewayTypes()
        {
            return gatewayTypeService.FilterActive();
        }
    }
}
