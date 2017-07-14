using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class AccountTypeBL: BaseAPCargoBL<AccountType>
    {
        private ICmsUoW _unitOfWork; 

        public AccountTypeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public AccountTypeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
