using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class TruckBL : BaseAPCargoBL<Truck>
    {
        private ICmsUoW _unitOfWork;

        public TruckBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public TruckBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

    }
}
