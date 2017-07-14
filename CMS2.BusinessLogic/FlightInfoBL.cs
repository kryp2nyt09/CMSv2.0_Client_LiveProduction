using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class FlightInfoBL:BaseAPCargoBL<FlightInformation>
    {
        private ICmsUoW _unitOfWork;

        public FlightInfoBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public FlightInfoBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

    }
}
