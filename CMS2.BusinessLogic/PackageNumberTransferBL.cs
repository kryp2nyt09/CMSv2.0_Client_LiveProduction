using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PackageNumberTransferBL : BaseAPCargoBL<PackageNumberTransfer>
    {
        private ICmsUoW _unitOfWork;

        public PackageNumberTransferBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PackageNumberTransferBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<PackageNumberTransfer, object>>[] Includes()
        {
            return new Expression<Func<PackageNumberTransfer, object>>[]
                {
                    x => x.PackageNumber,
                    x=>x.PackageTransfer
                };
        }

        public bool IsExistInTransfer(string cargoNo)
        {
            var transfer = FilterBy(x => x.PackageNumber.PackageNo.Equals(cargoNo));
            if (transfer == null || transfer.Count==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
