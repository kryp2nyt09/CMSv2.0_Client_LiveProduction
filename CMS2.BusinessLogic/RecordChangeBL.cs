using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class RecordChangeBL:BaseAPCargoBL<RecordChange>
    {
        private ICmsUoW _unitOfWork; 

        public RecordChangeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public RecordChangeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
