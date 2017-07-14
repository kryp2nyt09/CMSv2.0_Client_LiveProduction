using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class CargoTransferBL : BaseAPCargoBL<CargoTransfer>
    {
        private ICmsUoW _unitOfWork;

        public CargoTransferBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public CargoTransferBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
