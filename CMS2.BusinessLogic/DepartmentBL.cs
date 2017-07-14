using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class DepartmentBL : BaseAPCargoBL<Department>
    {
        private ICmsUoW _unitOfWork;

        public DepartmentBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public DepartmentBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

    }
}
