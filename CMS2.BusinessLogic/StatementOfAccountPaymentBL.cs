using System;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class StatementOfAccountPaymentBL : BaseAPCargoBL<StatementOfAccountPayment>
    {
        private ICmsUoW _unitOfWork; 

        public StatementOfAccountPaymentBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public StatementOfAccountPaymentBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
        public StatementOfAccountPayment AddEdit(StatementOfAccountPayment model)
        {
            var _model = GetByIdAsync(model.StatementOfAccountPaymentId).Result;
            if (_model == null)
            {
                model.StatementOfAccountPaymentId = Guid.NewGuid();
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
    }
}
