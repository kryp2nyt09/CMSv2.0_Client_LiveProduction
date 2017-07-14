using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class CompanyBL : BaseAPCargoBL<Company>
    {
        private ICmsUoW _unitOfWork;
        private CityBL cityService;
        private EmployeeBL employeeService;
        private PaymentTermBL paymentTermService;
        private PaymentModeBL paymentModeService;
        private AccountStatusBL accountStatusService;
        private AccountTypeBL accountTypeService;
        private BusinessTypeBL businessTypeService;
        private IndustryBL industryService;
        private OrganizationTypeBL organizationTypeService;
        private BillingPeriodBL billingPeriodService;

        public CompanyBL()
        {
            _unitOfWork = GetUnitOfWork();
            cityService = new CityBL(_unitOfWork);
            employeeService = new EmployeeBL(_unitOfWork);
            paymentTermService = new PaymentTermBL(_unitOfWork);
            paymentModeService = new PaymentModeBL(_unitOfWork);
            accountStatusService = new AccountStatusBL(_unitOfWork);
            accountTypeService = new AccountTypeBL(_unitOfWork);
            businessTypeService = new BusinessTypeBL(_unitOfWork);
            industryService = new IndustryBL(_unitOfWork);
            organizationTypeService = new OrganizationTypeBL(_unitOfWork);
            billingPeriodService = new BillingPeriodBL(_unitOfWork);
        }

        public CompanyBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<Company, object>>[] Includes()
        {
            return new Expression<Func<Company, object>>[]
                {
                    x => x.AccountStatus
                };
        }

        public List<Employee> GetEmployees()
        {
            return employeeService.FilterActive().OrderBy(x => x.FullName).ToList();
        }

        public List<BillingPeriod> GetBillingPeriods()
        {
            return billingPeriodService.FilterActive().OrderBy(x=>x.BillingPeriodName).ToList();
        }
        
        public List<AccountStatus> GetAccountStatus()
        {
            return accountStatusService.FilterActive().OrderBy(x=>x.AccountStatusName).ToList();
        }

        public List<AccountType> GetAccountTypes()
        {
            return accountTypeService.FilterActive().OrderBy(x=>x.AccountTypeName).ToList();
        }

        public List<BusinessType> GetBusinessTypes()
        {
            return businessTypeService.FilterActive().OrderBy(x=>x.BusinessTypeName).ToList();
        }

        public List<Industry> GetIndustries()
        {
            return industryService.FilterActive().OrderBy(x=>x.IndustryName).ToList();
        }

        public List<OrganizationType> GetOrganizationTypes()
        {
            return organizationTypeService.FilterActive().OrderBy(x=>x.OrganizationTypeName).ToList();
        }
        
        public List<City> GetCities()
        {
            return cityService.FilterActive().OrderBy(x=>x.CityName).ToList();
        }

        public List<PaymentTerm> GetPaymentTerms()
        {
            return paymentTermService.FilterActive().OrderBy(x=>x.PaymentTermId).ToList();
        }

        public Company GetByAccountNo(string accountNo)
        {
            return FilterActiveBy(x => x.AccountNo.Equals(accountNo)).FirstOrDefault();
        }
        
        public string GetNewAccountNo(Guid cityId, DateTime dateApproved)
        {
            // format: CityCode+mmddyy+3-digit (XXX 000000 000)
            int nextSequence = 1;
            string cityCode = cityService.FilterActiveBy(x => x.CityId == cityId).Select(x => x.CityCode).FirstOrDefault();
            string dateCode = dateApproved.ToString("yyMMdd");
            string temp = "0" + cityCode + dateCode;
            string lastAccountNo =
                FilterBy(x => x.AccountNo.StartsWith(temp))
                    .OrderByDescending(x => x.ApprovedDate)
                    .Select(x => x.AccountNo)
                    .FirstOrDefault();
            if (lastAccountNo!=null)
                nextSequence = Convert.ToInt32(lastAccountNo.Substring((lastAccountNo.Length - 3), 3))+1;

            return cityCode + dateCode + nextSequence.ToString("000");
        }
    }
}
