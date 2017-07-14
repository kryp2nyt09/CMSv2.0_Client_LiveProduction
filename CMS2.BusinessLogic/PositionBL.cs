using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PositionBL : BaseAPCargoBL<Position>
    {
        private ICmsUoW _unitOfWork;

        public PositionBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PositionBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
