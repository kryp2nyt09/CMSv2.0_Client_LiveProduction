using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class HoldCargoBL : BaseAPCargoBL<HoldCargo>
    {
        private ICmsUoW _unitOfWork;

        public HoldCargoBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public HoldCargoBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
