using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class OrganizationTypeBL: BaseAPCargoBL<OrganizationType>
    {
        private ICmsUoW _unitOfWork;

        public OrganizationTypeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public OrganizationTypeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

    }
}
