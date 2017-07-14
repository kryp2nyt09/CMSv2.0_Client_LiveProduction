using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ServiceModeBL : BaseAPCargoBL<ServiceMode>
    {
        private ICmsUoW _unitOfWork;

        public ServiceModeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ServiceModeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

    }
}
