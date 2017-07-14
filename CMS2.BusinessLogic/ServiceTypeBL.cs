using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ServiceTypeBL : BaseAPCargoBL<ServiceType>
    {
        private ICmsUoW _unitOfWork;

        public ServiceTypeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public ServiceTypeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<ServiceType, object>>[] Includes()
        {
            return new Expression<Func<ServiceType, object>>[]
                {

                };
        }
    }
}
