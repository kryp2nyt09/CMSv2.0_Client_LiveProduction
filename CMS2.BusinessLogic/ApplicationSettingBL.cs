using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ApplicationSettingBL : BaseAPCargoBL<ApplicationSetting>
    {
        private ICmsUoW _unitOfWork;

        public ApplicationSettingBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ApplicationSettingBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
