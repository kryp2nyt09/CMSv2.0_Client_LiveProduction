using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class EmployeeBL : BaseAPCargoBL<Employee>
    {
        private ICmsUoW _unitOfWork;

        public EmployeeBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public EmployeeBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public override Expression<Func<Employee, object>>[] Includes()
        //{
        //    return new Expression<Func<Employee, object>>[]
        //        {
        //            x => x.EmployeePositionMappings
        //        };
        //}

        public List<KeyValuePair<string, Guid>> GetEmployeeNames()
        {
            List<KeyValuePair<string, Guid>> employeeNames = new List<KeyValuePair<string, Guid>>();
            var employees = FilterActive().OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            foreach (var item in employees)
            {
                employeeNames.Add(new KeyValuePair<string, Guid>(item.LastName + ", " + item.FirstName + " " + item.MiddleName, item.EmployeeId));
            }
            return employeeNames;
        }

        public List<KeyValuePair<string, Guid>> GetByPosition(string position)
        {
            //EmployeePositionMappingBL mappingService = new EmployeePositionMappingBL(_unitOfWork);
            //PositionBL positionService = new PositionBL(_unitOfWork);

            List<KeyValuePair<string, Guid>> employeeNames = new List<KeyValuePair<string, Guid>>();
            //var employees = GetAll().Join
            //                    (mappingService.FilterActive()
            //                        .Join
            //                            (positionService.FilterActive().Where(z => z.PositionName.Equals(position)),
            //                                map => map.PositionId,
            //                                pos => pos.PositionId,
            //                                (map, pos) => new { Map = map }),
            //                    emp => emp.EmployeeId,
            //                    map => map.Map.EmployeeId,
            //                    (emp, map) => new { Emp = emp })
            //                    .OrderBy(y => y.Emp.LastName).ThenBy(y => y.Emp.FirstName);

            List<Employee> employees = GetAll().Where(x => x.Position.PositionName == position).ToList();
            if (employees!=null)
            {
                foreach (var item in employees)
                {
                    employeeNames.Add(new KeyValuePair<string, Guid>(item.LastName + ", " + item.FirstName + " " + item.MiddleName, item.EmployeeId));
                }
            }
            return employeeNames;
        }

        public new Employee Add(Employee model)
        {
            try
            {
                base.Add(model);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetFullNameByShortcut(string shorcutName)
        {
            string fullname = "";
            foreach (var item in GetAll())
            {
                var shortcut = (item.FirstName.Substring(0, 1) + "." + item.LastName).ToUpper();
                if (shortcut.Equals(shorcutName))
                {
                    fullname = item.FullName;
                    break;
                }
            }
            return fullname;
        }
    }
}
