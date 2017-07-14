using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class BookingStatusBL : BaseAPCargoBL<BookingStatus>
    {
         private ICmsUoW _unitOfWork; 

        public BookingStatusBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BookingStatusBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
