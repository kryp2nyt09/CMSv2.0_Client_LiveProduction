using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;


namespace CMS2.BusinessLogic
{

    public class GatewayInboundBL : BaseAPCargoBL<GatewayInbound>

    {
        private ICmsUoW _unitOfWork;

        public GatewayInboundBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public GatewayInboundBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
