using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class AccountStatusBL : BaseAPCargoBL<AccountStatus>
    {
        private ICmsUoW _unitOfWork; 

        public AccountStatusBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public AccountStatusBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
