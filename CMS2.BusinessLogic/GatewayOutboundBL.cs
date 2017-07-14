using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class GatewayOutboundBL : BaseAPCargoBL<GatewayOutbound>
    {
        private ICmsUoW _unitOfWork;

        public GatewayOutboundBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public GatewayOutboundBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
