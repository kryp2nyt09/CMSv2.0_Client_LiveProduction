using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.Entities;
using CMS2.DataAccess.Interfaces;

namespace CMS2.BusinessLogic
{
    class RemarkBL : BaseAPCargoBL<Remarks>
    {
        private ICmsUoW _unitOfWork;

        public RemarkBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public RemarkBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
