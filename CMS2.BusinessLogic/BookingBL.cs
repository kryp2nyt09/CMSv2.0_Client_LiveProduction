using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class BookingBL : BaseAPCargoBL<Booking>
    {
        private ICmsUoW _unitOfWork;

        public BookingBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BookingBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<Booking, object>>[] Includes()
        {
            return new Expression<Func<Booking, object>>[]
                {
                    x => x.BookedBy,
                    x=>x.BookingStatus,
                    x=>x.BookingRemark,
                    x=>x.AssignedToArea,
                    x=>x.Consignee,
                    x=>x.Shipper,
                    x=>x.DestinationCity,
                    x=>x.OriginCity
                };
        }

        public void AddEdit(Booking entity)
        {
            var booking = FilterActiveBy(x => x.BookingId == entity.BookingId);
            if (booking == null || booking.Count<=0)
            {
                Add(entity);
            }
            else
            {
                Edit(entity);
            }
        }
    }
}
