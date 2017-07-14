using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class CratingBL : BaseAPCargoBL<Crating>
    {
        private ICmsUoW _unitOfWork;

        public CratingBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public CratingBL(ICmsUoW unitOfWork)
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
