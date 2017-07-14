using System;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class AdjustmentReasonBL:BaseAPCargoBL<AdjustmentReason>
    {
        private ICmsUoW _unitOfWork; 

        public AdjustmentReasonBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public AdjustmentReasonBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
        public AdjustmentReason AddEdit(AdjustmentReason model)
        {

            var _model = GetByIdAsync(model.AdjustmentReasonId).Result;
            if (_model == null)
            {
                if (IsExist(x => x.Reason.Equals(model.Reason)))
                {
                    return model;
                }
                model.AdjustmentReasonId = Guid.NewGuid();
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
