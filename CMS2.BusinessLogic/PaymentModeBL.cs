using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PaymentModeBL : BaseAPCargoBL<PaymentMode>
    {
        private ICmsUoW _unitOfWork;

        public PaymentModeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PaymentModeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
