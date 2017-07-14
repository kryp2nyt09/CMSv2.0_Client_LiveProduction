using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class BusinessTypeBL : BaseAPCargoBL<BusinessType>
    {
        private ICmsUoW _unitOfWork; 

        public BusinessTypeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BusinessTypeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
