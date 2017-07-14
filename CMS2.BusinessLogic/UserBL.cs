using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.BusinessLogic
{
    public class UserBL : BaseAPCargoBL<User>
    {
        private ICmsUoW _unitOfWork;

        public UserBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public UserBL(ICmsUoW unitOfWork) :base(unitOfWork)
        {

        }
    }
}
