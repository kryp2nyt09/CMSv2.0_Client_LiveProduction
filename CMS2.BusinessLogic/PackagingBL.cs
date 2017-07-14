using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PackagingBL : BaseAPCargoBL<Packaging>
    {
        private ICmsUoW _unitOfWork;

        public PackagingBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PackagingBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        //public override Expression<Func<Crating, object>>[] Includes()
        //{
        //    return new Expression<Func<Crating, object>>[]
        //        {

        //        };
        //}
    }
}
