using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PackageNumberBL : BaseAPCargoBL<PackageNumber>
    {
        private ICmsUoW _unitOfWork;
        private EmployeeBL employeeService;

        public PackageNumberBL()
        {
            _unitOfWork = GetUnitOfWork();
            
        }

        public PackageNumberBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
            employeeService = new EmployeeBL(unitOfWork);
        }

        public override Expression<Func<PackageNumber, object>>[] Includes()
        {
            return new Expression<Func<PackageNumber, object>>[]
                {
                    x => x.DeliveredPackages
                };
        }

        public List<Employee> GetEmployees()
        {
            return employeeService.FilterActive().ToList();
        }
    }
}
