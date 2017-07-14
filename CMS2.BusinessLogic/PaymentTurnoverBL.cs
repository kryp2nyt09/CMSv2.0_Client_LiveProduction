using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PaymentTurnoverBL : BaseAPCargoBL<PaymentTurnover>
    {
        private ICmsUoW _unitOfWork;

        public PaymentTurnoverBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PaymentTurnoverBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        //public override Expression<Func<PaymentTurnover, object>>[] Includes()
        //{
        //    return new Expression<Func<PaymentTurnover, object>>[]
        //        {
                    
        //        };
        //}

        public void AddEdit(PaymentTurnover entity)
        {
            var model = FilterActiveBy(x => x.PaymentTurnOverId == entity.PaymentTurnOverId);
            if (model == null || model.Count <= 0)
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
