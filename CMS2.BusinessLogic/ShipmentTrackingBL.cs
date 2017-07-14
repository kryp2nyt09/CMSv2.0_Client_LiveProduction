using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ShipmentTrackingBL : BaseAPCargoBL<ShipmentTracking>
    {
        private ICmsUoW _unitOfWork;

        public ShipmentTrackingBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ShipmentTrackingBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
