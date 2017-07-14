using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ReasonBL : BaseAPCargoBL<Reason>
    {
        private ICmsUoW _unitOfWork;

        public ReasonBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ReasonBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
