using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class GatewayTransmittalBL : BaseAPCargoBL<GatewayTransmittal>
    {
        private ICmsUoW _unitOfWork;

        public GatewayTransmittalBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public GatewayTransmittalBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
