using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class StatusBL : BaseAPCargoBL<Status>
    {
        private ICmsUoW _unitOfWork;

        public StatusBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public StatusBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
