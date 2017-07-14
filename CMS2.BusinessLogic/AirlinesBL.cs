using System;
using System.Collections.Generic;
using System.Linq;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic 
{
    public class AirlinesBL : BaseAPCargoBL<Airlines>
    {
        private ICmsUoW _unitOfWork;
        public AirlinesBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public AirlinesBL(ICmsUoW unitOfWork):base(unitOfWork)
        {

        }
    }
}
