using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using CrystalDecisions.CrystalReports.Engine;

namespace CMS2.BusinessLogic
{
    public class BranchCorpOfficeBL: BaseAPCargoBL<BranchCorpOffice>
    {
        private ICmsUoW _unitOfWork;
        private RegionBL regionService;
        public BranchCorpOfficeBL()
        {
            _unitOfWork = GetUnitOfWork();
            regionService= new RegionBL(_unitOfWork);
        }

        public BranchCorpOfficeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<BranchCorpOffice, object>>[] Includes()
        {
            return new Expression<Func<BranchCorpOffice, object>>[]
                {
                    //x => x.Region,
                    //x=>x.Clusters
                };
        }

        public List<Region> GetRegions()
        {
            return regionService.FilterActive();
        }
    }
}
