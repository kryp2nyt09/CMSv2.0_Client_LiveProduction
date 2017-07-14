using System;
using System.Linq;
using CMS2.Common.Enums;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ShipmentAdjustmentBL : BaseAPCargoBL<ShipmentAdjustment>
    {
        private ICmsUoW _unitOfWork;

        public ShipmentAdjustmentBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ShipmentAdjustmentBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public ShipmentAdjustment AddEdit(ShipmentAdjustment model)
        {
            if (IsExist(x => x.ShipmentId == model.ShipmentId && x.StatementOfAccountId == model.StatementOfAccountId))
            {
                var _model = FilterActiveBy(x => x.ShipmentId == model.ShipmentId && x.StatementOfAccountId == model.StatementOfAccountId).SingleOrDefault();
                _model.ModifiedBy = model.ModifiedBy;
                _model.ModifiedDate = model.ModifiedDate;
                _model.RecordStatus = (int)RecordStatus.Deleted;
                Edit(_model);
            }
            model.ShipmentAdjustmentId = Guid.NewGuid();
            model.CreatedBy = model.ModifiedBy;
            model.CreatedDate = model.ModifiedDate;
            Add(model);
            return null;
        }
    }
}
