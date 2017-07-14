using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class DeliveryBL:BaseAPCargoBL<Delivery>
    {
        private ICmsUoW _unitOfWork; 

        public DeliveryBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public DeliveryBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<Delivery, object>>[] Includes()
        {
            return new Expression<Func<Delivery, object>>[]
                {
                    x => x.DeliveredPackages,
                        x=>x.DeliveryReceipts,
                        x=>x.DeliveredBy
                };
        }
    }
}
