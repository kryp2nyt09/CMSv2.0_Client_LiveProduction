using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ManifestAwbBL : BaseAPCargoBL<ManifestAwb>
    {
        private ICmsUoW _unitOfWork;

        public ManifestAwbBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ManifestAwbBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
