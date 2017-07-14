using System;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class TerminalRevenueUnitMappingBL : BaseAPCargoBL<TerminalRevenueUnitMapping>
    {
        private ICmsUoW _unitOfWork;

        public TerminalRevenueUnitMappingBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public TerminalRevenueUnitMappingBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<TerminalRevenueUnitMapping, object>>[] Includes()
        {
            return new Expression<Func<TerminalRevenueUnitMapping, object>>[]
                {
                    x => x.Terminal,
                    x=>x.RevenueUnit,
                    x=>x.AssignedBy
                };
        }

    }
}
