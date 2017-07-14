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
    public class DeliveredPackageBL:BaseAPCargoBL<DeliveredPackage>
    {
        private ICmsUoW _unitOfWork; 

        public DeliveredPackageBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public DeliveredPackageBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<DeliveredPackage, object>>[] Includes()
        {
            return new Expression<Func<DeliveredPackage, object>>[]
                {
                    x => x.Delivery,
                        x=>x.PackageNumber
                };
        }
    }
}
