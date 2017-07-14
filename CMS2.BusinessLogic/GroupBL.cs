using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class GroupBL : BaseAPCargoBL<Group>
    {
        private ICmsUoW _unitOfWork;

        public GroupBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public GroupBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
