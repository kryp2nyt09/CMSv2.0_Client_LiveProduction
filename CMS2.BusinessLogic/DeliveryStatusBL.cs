using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class DeliveryStatusBL:BaseAPCargoBL<DeliveryStatus>
    {
        private ICmsUoW _unitOfWork; 

        public DeliveryStatusBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public DeliveryStatusBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
