using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PaymentTypeBL : BaseAPCargoBL<PaymentType>
    {
        private ICmsUoW _unitOfWork;

        public PaymentTypeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PaymentTypeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

    }
}
