using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class DeliveryReceiptBL:BaseAPCargoBL<DeliveryReceipt>
    {
        private ICmsUoW _unitOfWork; 

        public DeliveryReceiptBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public DeliveryReceiptBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
