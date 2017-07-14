using System;
using System.Collections.Generic;
using CMS2.Common.Enums;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class FuelSurchargeBL : BaseAPCargoBL<FuelSurcharge>
    {
        private ICmsUoW _unitOfWork;
        private GroupBL groupService;

        public FuelSurchargeBL()
        {
            _unitOfWork = GetUnitOfWork();
            groupService = new GroupBL(_unitOfWork);
        }

        public FuelSurchargeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Group> GetGroups()
        {
            return groupService.FilterActive();
        }

        public CommodityType AddEdit(FuelSurcharge model)
        {
            var _model = GetByIdAsync(model.FuelSurchargeId).Result;
            if (_model == null)
            {
                model.FuelSurchargeId = Guid.NewGuid();
                model.RecordStatus = (int)RecordStatus.Active;
                Add(model);
                return null;
            }
            else
            {
                _model.RecordStatus = (int) RecordStatus.Inactive;
                _model.ModifiedBy = model.ModifiedBy;
                _model.ModifiedDate = model.ModifiedDate;
                Edit(_model);

                model.FuelSurchargeId = Guid.NewGuid();
                model.RecordStatus = (int)RecordStatus.Active;
                Add(model);

                return null;
            }
        }
    }
}
