using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS2.BusinessLogic
{
    public class PackageNumberAcceptanceBL : BaseAPCargoBL<PackageNumberAcceptance>
    {
        private ICmsUoW _unitOfWork;

        public PackageNumberAcceptanceBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PackageNumberAcceptanceBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<PackageNumberAcceptance, object>>[] Includes()
        {
            return new Expression<Func<PackageNumberAcceptance, object>>[]
                {
                    x => x.PackageNumber,
                    x=>x.TransferAcceptance
                };
        }

        public bool IsExistInAcceptance(string cargoNo,string acceptanceType)
        {
            var acceptance = FilterBy(x => x.PackageNumber.PackageNo.Equals(cargoNo) && x.TransferAcceptance.TransferType.Equals(acceptanceType));
            if (acceptance == null || acceptance.Count == 0)
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
