using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.BusinessLogic
{
    public class PaymentSummaryStatusBL : BaseAPCargoBL<PaymentSummaryStatus>
    {
        private ICmsUoW _unitOfWork;
        public PaymentSummaryStatusBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PaymentSummaryStatusBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
    
}
