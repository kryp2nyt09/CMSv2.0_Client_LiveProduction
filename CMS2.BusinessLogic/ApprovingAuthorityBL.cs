using System.Collections.Generic;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ApprovingAuthorityBL: BaseAPCargoBL<ApprovingAuthority>
    {
        private ICmsUoW _unitOfWork;
        private CompanyBL companyService;

        public ApprovingAuthorityBL()
        {
            _unitOfWork = GetUnitOfWork();
            companyService = new CompanyBL(_unitOfWork);
        }

        public ApprovingAuthorityBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public List<Company> GetCompanies()
        {
            return companyService.FilterActiveBy(x => x.AccountStatus.AccountStatusName.Equals("Active"));
        }
    }
}
