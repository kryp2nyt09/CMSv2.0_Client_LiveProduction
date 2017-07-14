using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class TerminalBL : BaseAPCargoBL<Terminal>
    {
        private ICmsUoW _unitOfWork;

        public TerminalBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public TerminalBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
