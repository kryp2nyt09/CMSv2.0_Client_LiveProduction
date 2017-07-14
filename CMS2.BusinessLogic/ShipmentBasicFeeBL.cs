using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ShipmentBasicFeeBL : BaseAPCargoBL<ShipmentBasicFee>
    {
        private ICmsUoW _unitOfWork;

        public ShipmentBasicFeeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ShipmentBasicFeeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

    }
}
