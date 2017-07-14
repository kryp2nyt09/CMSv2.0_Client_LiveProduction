using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class RevenueUnitTypeBL : BaseAPCargoBL<RevenueUnitType>
    {
        private ICmsUoW _unitOfWork;

        public RevenueUnitTypeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public RevenueUnitTypeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
