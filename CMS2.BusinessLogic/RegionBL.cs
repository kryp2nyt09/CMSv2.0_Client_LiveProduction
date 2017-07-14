using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class RegionBL : BaseAPCargoBL<Region>
    {
        private ICmsUoW _unitOfWork;
        private GroupBL groupService;

        public RegionBL()
        {
            _unitOfWork = GetUnitOfWork();
            groupService = new GroupBL(_unitOfWork);
        }

        public RegionBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<Region, object>>[] Includes()
        {
            return new Expression<Func<Region, object>>[]
            {
                x => x.Group
            };
        }

        public List<Group> GetGroups()
        {
            return groupService.FilterActive();
        }
        
    }
}
