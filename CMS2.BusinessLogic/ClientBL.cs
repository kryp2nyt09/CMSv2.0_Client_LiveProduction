using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class ClientBL : BaseAPCargoBL<Client>
    {
        private ICmsUoW _unitOfWork;
        private CityBL cityService;
        private CompanyBL companyService;
        
        public ClientBL()
        {
            _unitOfWork = GetUnitOfWork();
            cityService = new CityBL(_unitOfWork);
            companyService = new CompanyBL(_unitOfWork);
        }

        public ClientBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
            
        }

        public override Expression<Func<Client, object>>[] Includes()
        {
            return new Expression<Func<Client, object>>[]
                {
                    x => x.Company
                };
        }

        public List<Company> GetCompanies()
        {
            return companyService.FilterActiveBy(x => x.AccountStatus.AccountStatusName.Equals("Active"));
        }
        
        public List<City> GetCities()
        {
            return cityService.FilterActive();
        }

        public string GetNewAccountNo(string cityCode, Boolean isSecond)
        {
            string date = DateTime.Now.ToString("yyMMdd");
            int codeLength = cityCode.Length;
            var lastClient =
                FilterBy(
                    x => x.AccountNo.Substring(1, codeLength).Equals(cityCode) && x.AccountNo.Substring(codeLength+1, 6).Equals(date)).Max(x => x.AccountNo.Substring(codeLength + 7, 3));
            if (lastClient != null)
            {
                int lastSequence = Convert.ToInt32(lastClient);
                if (isSecond)
                    lastSequence = lastSequence + 1;
                return cityCode + date + (lastSequence + 1).ToString("000");
            }
            else
            {
                return cityCode + date + "001";
            }
        }

        public List<Client> GetByCompanyId(Guid companyId)
        {
            CompanyBL companyService = new CompanyBL(_unitOfWork);
            return GetAll()
                        .Join(companyService.FilterBy(x => x.CompanyId == companyId), 
                            cln => cln.CompanyId,
                            com => com.CompanyId,
                            (cln, com) => new { clients = cln })
                        .Select(y=>y.clients)
                        .ToList();
        }

        public List<Client> GetByCompanyAccountNo(string companyAccountNo)
        {
            CompanyBL companyService = new CompanyBL(_unitOfWork);
            return GetAll()
                        .Join(companyService.FilterBy(x => x.AccountNo.Equals(companyAccountNo)),
                            cln => cln.CompanyId,
                            com => com.CompanyId,
                            (cln, com) => new { clients = cln })
                        .Select(y => y.clients)
                        .ToList();
        }

        public Client GetByAccountNo(string clientAccountNo)
        {
            return FilterActiveBy(x => x.AccountNo.Equals(clientAccountNo)).SingleOrDefault();
        }

    }
}
