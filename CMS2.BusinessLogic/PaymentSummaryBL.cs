using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.BusinessLogic
{
    public class PaymentSummaryBL : BaseAPCargoBL<PaymentSummary>
    {
        private ICmsUoW _unitOfWork;
        public PaymentSummaryBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PaymentSummaryBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

       
    }
}
