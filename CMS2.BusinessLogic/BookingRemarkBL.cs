using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class BookingRemarkBL:BaseAPCargoBL<BookingRemark>
    {
         private ICmsUoW _unitOfWork; 

        public BookingRemarkBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BookingRemarkBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
