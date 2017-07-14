using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PaymentTermBL : BaseAPCargoBL<PaymentTerm>
    {
        private ICmsUoW _unitOfWork;
        
        public PaymentTermBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PaymentTermBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }
        
    }
}
