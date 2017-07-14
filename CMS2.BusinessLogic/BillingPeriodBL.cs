using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class BillingPeriodBL : BaseAPCargoBL<BillingPeriod>
    {
        private ICmsUoW _unitOfWork;

        public BillingPeriodBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BillingPeriodBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
