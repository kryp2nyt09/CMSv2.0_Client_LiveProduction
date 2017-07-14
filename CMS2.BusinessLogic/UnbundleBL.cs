using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.BusinessLogic
{
    public class UnbundleBL : BaseAPCargoBL<Unbundle>
    {
        private ICmsUoW _unitOfWork;

        public UnbundleBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public UnbundleBL(ICmsUoW unitOfWork) :base(unitOfWork)
        {

        }
    }
}
