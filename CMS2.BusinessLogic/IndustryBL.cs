using System;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class IndustryBL: BaseAPCargoBL<Industry>
    {
        private ICmsUoW _unitOfWork;

        public IndustryBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public IndustryBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public Industry AddEdit(Industry model)
        {

            var _model = GetByIdAsync(model.IndustryId).Result;
            if (_model == null)
            {
                if (IsExist(x => x.IndustryName.Equals(model.IndustryName)))
                {
                    return model;
                }
                model.IndustryId = Guid.NewGuid();
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
