using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class TransShipmentLegBL : BaseAPCargoBL<TransShipmentLeg>
    {
        private ICmsUoW _unitOfWork;

        public TransShipmentLegBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public TransShipmentLegBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        //public override Expression<Func<TransShipmentLeg, object>>[] Includes()
        //{
        //    return new Expression<Func<TransShipmentLeg, object>>[]
        //        {
        //            x => x.TransShipmentRoute,
        //            x=>x.Leg
        //        };
        //}
    }

}
