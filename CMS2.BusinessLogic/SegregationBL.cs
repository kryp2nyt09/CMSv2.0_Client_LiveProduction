using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class SegregationBL : BaseAPCargoBL<Segregation>
    {
        private ICmsUoW _unitOfWork;

        public SegregationBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public SegregationBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
