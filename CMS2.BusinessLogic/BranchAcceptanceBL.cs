using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.BusinessLogic
{
    public class BranchAcceptanceBL : BaseAPCargoBL<BranchAcceptance>
    {
        private ICmsUoW _unitOfWork;

        public BranchAcceptanceBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BranchAcceptanceBL(ICmsUoW _unitOfWork)
            : base(_unitOfWork)
        {

        }
    }
}
