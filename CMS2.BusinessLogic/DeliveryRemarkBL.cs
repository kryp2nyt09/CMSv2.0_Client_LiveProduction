using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class DeliveryRemarkBL:BaseAPCargoBL<DeliveryRemark>
    {
        private ICmsUoW _unitOfWork; 

        public DeliveryRemarkBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public DeliveryRemarkBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
