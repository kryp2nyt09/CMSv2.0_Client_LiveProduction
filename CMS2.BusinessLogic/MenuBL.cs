using System;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class MenuBL : BaseAPCargoBL<Menu>
    {
        private ICmsUoW _unitOfWork;

        public MenuBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public MenuBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        //public List<Menu> GetMenuByRoleName(string roleName)
        //{
        //    RoleMenuMappingBL mappingService = new RoleMenuMappingBL(_unitOfWork);
        //    return null;
        //}

        public Menu AddEdit(Menu model)
        {

            model.ModifiedBy = new Guid();//Guid.Parse(User.Identity.GetUserId()));
            model.ModifiedDate = DateTime.Now;

            var _model = GetByIdAsync(model.MenuId).Result;
            if (_model == null)
            {
                if (IsExist(x => x.MenuName.Equals(model.MenuName)))
                {
                    return model;
                }
                model.MenuId = Guid.NewGuid();
                model.CreatedBy = new Guid(); //Guid.Parse(User.Identity.GetUserId());
                model.CreatedDate = DateTime.Now;
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
