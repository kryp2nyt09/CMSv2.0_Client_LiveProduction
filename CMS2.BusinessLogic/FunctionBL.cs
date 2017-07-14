using APCargo.DataAccess.Interfaces;
using APCargo.Entities;

namespace APCargo.BusinessLogic
{
    public class FunctionBL : BaseAPCargoBL<Function>
    {
        private IAPCargoUoW _unitOfWork;

        public FunctionBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public FunctionBL(IAPCargoUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
