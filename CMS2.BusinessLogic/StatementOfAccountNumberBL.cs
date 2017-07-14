using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class StatementOfAccountNumberBL : BaseAPCargoBL<StatementOfAccountNumber>
    {
        private ICmsUoW _unitOfWork;

        public StatementOfAccountNumberBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public StatementOfAccountNumberBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
