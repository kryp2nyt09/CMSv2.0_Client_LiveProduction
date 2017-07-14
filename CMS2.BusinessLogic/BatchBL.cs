using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.Entities;
using CMS2.DataAccess.Interfaces;

namespace CMS2.BusinessLogic
{
   public class BatchBL : BaseAPCargoBL<Batch>
    {
        private ICmsUoW _unitOfWork;

        public BatchBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BatchBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

    }
}
