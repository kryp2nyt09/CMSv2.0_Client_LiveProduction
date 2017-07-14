using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class TransShipmentRouteBL : BaseAPCargoBL<TransShipmentRoute>
    {
        private ICmsUoW _unitOfWork;

        public TransShipmentRouteBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public TransShipmentRouteBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        //public override Expression<Func<TransShipmentRoute, object>>[] Includes()
        //{
        //    return new Expression<Func<TransShipmentRoute, object>>[]
        //        {
        //            x => x.Legs,
        //            x=>x.OriginCity,
        //            x=>x.DestinationCity
        //        };
        //}
    }
}
