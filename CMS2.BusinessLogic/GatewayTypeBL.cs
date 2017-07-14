using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class GatewayTypeBL:BaseAPCargoBL<GatewayType>
    {
        private ICmsUoW _unitOfWork; 

        public GatewayTypeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public GatewayTypeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
