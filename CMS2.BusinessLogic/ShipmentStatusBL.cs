using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ShipmentStatusBL : BaseAPCargoBL<ShipmentStatus>
    {
        private ICmsUoW _unitOfWork;

        public ShipmentStatusBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ShipmentStatusBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
