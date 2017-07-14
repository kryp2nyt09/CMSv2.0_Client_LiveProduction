using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ClusterBL : BaseAPCargoBL<Cluster>
    {
        private ICmsUoW _unitOfWork;
        private BranchCorpOfficeBL branchCorpOfficeService;

        public ClusterBL()
        {
            _unitOfWork = GetUnitOfWork();
            branchCorpOfficeService = new BranchCorpOfficeBL(_unitOfWork);
        }

        public ClusterBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<Cluster, object>>[] Includes()
        {
            return new Expression<Func<Cluster, object>>[]
                {
                    x => x.BranchCorpOffice
                };
        }

        public List<BranchCorpOffice> GetBranchCorpOffices()
        {
            return branchCorpOfficeService.FilterActive();
        }

    }
}
