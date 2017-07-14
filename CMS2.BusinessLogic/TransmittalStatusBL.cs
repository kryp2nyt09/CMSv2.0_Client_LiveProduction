using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class TransmittalStatusBL:BaseAPCargoBL<TransmittalStatus>
    {
        private ICmsUoW _unitOfWork; 

        public TransmittalStatusBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public TransmittalStatusBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
