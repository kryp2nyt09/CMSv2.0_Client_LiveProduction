using System;
using APCargo.DataAccess.Interfaces;
using APCargo.Entities;

namespace APCargo.BusinessLogic
{
    public class RoleMenuMappingBL : BaseAPCargoBL<RoleMenuMapping>
    {
        private IAPCargoUoW _unitOfWork;

        public RoleMenuMappingBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public RoleMenuMappingBL(IAPCargoUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public RoleMenuMapping AddEdit(RoleMenuMapping model)
        {

            model.ModifiedBy = new Guid();//Guid.Parse(User.Identity.GetUserId()));
            model.ModifiedDate = DateTime.UtcNow;

            var _model = GetById(model.RoleMenuMappingId);
            if (_model == null)
            {
                if (IsExist(x => x.RoleId==model.RoleId && x.MenuId==model.MenuId))
                {
                    return model;
                }
                model.RoleMenuMappingId = Guid.NewGuid();
                model.CreatedBy = new Guid(); //Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.UtcNow;
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
