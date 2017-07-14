using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic 
{
    public class DistributionBL : BaseAPCargoBL<Distribution>
    {
        private ICmsUoW _unitOfWork;

        public DistributionBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public DistributionBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
