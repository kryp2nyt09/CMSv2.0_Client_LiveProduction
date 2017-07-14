using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using CMS2.Common.Enums;
using CMS2.Common.Identity;
using CMS2.DataAccess;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class UserRoleBL
    {
        private ICmsUoW _unitOfWork;
        ICmsRepository<Role> roleService = null;
        UserStore userService = null;
        private EmployeeBL employeeInofService;
        
        public UserRoleBL()
        {
            _unitOfWork = new CmsUoW();
            roleService = _unitOfWork.Repository<Role>();
            userService = new UserStore(_unitOfWork);
            employeeInofService = new EmployeeBL(_unitOfWork);
        }

        public UserRoleBL(ICmsUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public override Expression<Func<RevenueUnit, object>>[] Includes()
        //{
        //    return new Expression<Func<RevenueUnit, object>>[]
        //        {
        //            x => x.Cluster,
        //            x=>x.City
        //        };
        //}

        #region RoleFunctions
        public List<Role> GetRoles()
        {
            return roleService.GetAll();
        }

        public List<Role> GetActiveRoles()
        {
            return roleService.FilterActive();
        }

        public Role GetRoleById(Guid id)
        {
            return roleService.GetById(id);
        }
        public void AddRole(Role model)
        {
            var roles = roleService.GetAll();
            if (roles.Exists(x => x.RoleName.Equals(model.RoleName)))
            {
                var role = roles.SingleOrDefault(x => x.RoleName.Equals(model.RoleName));
                role.Description = model.Description;
                role.ModifiedBy = model.ModifiedBy;
                role.ModifiedDate = DateTime.Now;
                role.RecordStatus = (int) RecordStatus.Active;
                roleService.Update(role);
            }
            else
            {
                roleService.Create(model);    
            }
            _unitOfWork.Save();
        }

        public void EditRole(Role model)
        {
            Role role = roleService.GetById(model.RoleId);
            role.RoleName = model.RoleName;
            role.Description = model.Description;
            role.ModifiedBy = model.ModifiedBy;
            role.ModifiedDate = model.ModifiedDate;
            roleService.Update(role);
            _unitOfWork.Save();
        }

        public void DeleteRole(Role model)
        {
            var role = roleService.GetById(model.RoleId);
            role.ModifiedBy = model.ModifiedBy;
            role.ModifiedDate = model.ModifiedDate;
            role.RecordStatus = (int)RecordStatus.Deleted;
            roleService.Update(role);
            _unitOfWork.Save();
        }
        #endregion

        #region UserFunctions
        public List<User> GetAllUsers()
        {
            return null;
        }

        public List<User> GetAllActiveUsers()
        {
            var users = userService.GetAllActiveUsers();
            return users;
        }

        public User GetUserById(Guid id)
        {
            return null;//userService.
        }

        public IdentityUser GetById(Guid id)
        {
            return userService.GetById(id);
        }

        public void EditUser(User model)
        {
            //User user = userService.GetById(model.UserId);
            //user.Description = model.Description;
            //user.ModifiedBy = model.ModifiedBy;
            //user.ModifiedDate = model.ModifiedDate;
            //userService.Update(role);
            //_unitOfWork.Save();
        }
        
        #endregion

        public List<KeyValuePair<string, Guid>> GetEmployeeNames()
        {
            var users = userService.GetAllUsers().Select(x=>x.EmployeeId);
            var employees=employeeInofService.GetEmployeeNames().Where(x=>!users.Contains(x.Value)).ToList();
            if (employees.Count>0)
            {
                return employees;
            }
            else
            {
                return new List<KeyValuePair<string, Guid>>();
            }
        }

        public void AddToRole(IdentityUser user, string roleName)
        {
            userService.AddToRoleAsync(user, roleName);
        }

        public void RemoveFromRole(IdentityUser user, string roleName)
        {
            userService.RemoveFromRoleAsync(user, roleName);
        }










        //public User CreateUser(User model)
        //{
        //    if (IsExist(x => x.UserName.Equals(model.UserName)))
        //    {
        //        return model;
        //    }
        //    try
        //    {
        //        string newSalt = CreateSalt();

        //        //model.Id = Guid.NewGuid();
        //        //model.Password = HashPassword(model.Password, newSalt);
        //        //model.Salt = newSalt;
        //        //model.LastPasswordChange = DateTime.Now;
        //        //model.OldPassword = "";
        //        //model.LastLogInDate = DateTime.Now;
        //        model.ModifiedBy = new Guid();//Guid.Parse(User.Identity.GetUserId()));
        //        model.ModifiedDate = DateTime.Now;
        //        model.CreatedBy = new Guid(); //Guid.Parse(User.Identity.GetUserId());
        //        model.CreatedDate = DateTime.Now;
        //        Add(model);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return model;
        //    }
        //}

        //public User AuthenticateUser(string username, string password)
        //{
        //    var user = FilterActiveByAsync(x => x.UserName.Equals(username)).Result.SingleOrDefault();
        //    if (user == null)
        //        return null;

        //    if (VerifyPassword(password, user.PasswordHash, ""))
        //    {
        //        //user.LastLogInDate = DateTime.Now;
        //        Edit(user);
        //        return user;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        private string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[20];
            rng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        public string HashPassword(string password, string salt)
        {
            HashAlgorithm hasher = new SHA256Managed();
            byte[] hashedPassword = hasher.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(salt, password)));
            return Convert.ToBase64String(hashedPassword);
        }

        public bool VerifyPassword(string password, string storedpassword, string storedsalt)
        {
            string hashedPassword = HashPassword(password, storedsalt);
            if (hashedPassword.Equals(storedpassword))
                return true;
            else
                return false;
        }

        public bool ChangePassword()
        {
            return false;
        }

        public void ResetPassword()
        {

        }
    }
}
