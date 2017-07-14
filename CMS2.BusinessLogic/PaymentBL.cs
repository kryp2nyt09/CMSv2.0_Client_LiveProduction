using System;
using System.Collections.Generic;
using System.Linq;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PaymentBL: BaseAPCargoBL<Payment>
    {
        private ICmsUoW _unitOfWork; 

        public PaymentBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PaymentBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public Payment AddEdit(Payment model)
        {
            var _model = GetByIdAsync(model.PaymentId).Result;
            if (_model == null)
            {
                model.PaymentId = Guid.NewGuid();
                model.CreatedBy = model.ModifiedBy;
                model.CreatedDate = model.ModifiedDate;
                Add(model);
                return null;
            }
            else
            {
                model.CreatedBy = _model.CreatedBy;
                model.CreatedDate = _model.CreatedDate;
                Edit(model);
                return null;
            }
        }

        public List<Payment> GetByDate(DateTime date)
        {
            return FilterActiveBy(x => x.PaymentDate == date).ToList();
        }
    }
}
