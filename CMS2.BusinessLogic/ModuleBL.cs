using APCargo.DataAccess.Interfaces;
using APCargo.Entities;

namespace APCargo.BusinessLogic
{
    public class ModuleBL : BaseAPCargoBL<Module>
    {
        private IAPCargoUoW _unitOfWork;

        public ModuleBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ModuleBL(IAPCargoUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
