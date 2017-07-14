using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.Entities;
using CMS2.DataAccess.Interfaces;

namespace CMS2.BusinessLogic
{
    public class BundleBL : BaseAPCargoBL<Bundle>
    {
        private ICmsUoW _unitOfWork;

        public BundleBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BundleBL(ICmsUoW unitOfWork) :base(unitOfWork)
        {

        }

    }
}
