using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using CMS2.DataAccess.Interfaces;

namespace CMS2.BusinessLogic
{
    public class MenuAccessBL : BaseAPCargoBL<MenuAccess>
    {
        private ICmsUoW _unitOfWork;
        public MenuAccessBL()
        {
            _unitOfWork = GetUnitOfWork();
        }
        public MenuAccessBL(ICmsUoW unitOfWork)
        :base(unitOfWork)
        {

        }
    }
}
