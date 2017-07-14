using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS2.Common.Enums;
using CMS2.Common.Identity;
using CMS2.DataAccess;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using Microsoft.AspNet.Identity;
using System.Linq.Expressions;
using System.Text;

namespace CMS2.BusinessLogic
{
    public class UserStore : IUserLoginStore<IdentityUser, Guid>,IUserRoleStore<IdentityUser, Guid>, IUserPasswordStore<IdentityUser, Guid>, IUserSecurityStampStore<IdentityUser, Guid>, IUserStore<IdentityUser, Guid>, IDisposable
    {
        private readonly ICmsUoW _unitOfWork;
        private EmployeeBL employeeAssignmentService;
        public UserStore()
        {
            _unitOfWork = new CmsUoW();
            employeeAssignmentService = new EmployeeBL(_unitOfWork);
        }

        public UserStore(ICmsUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Expression<Func<User, object>>[] Includes()
        {
            return new Expression<Func<User, object>>[]
                {
                    x => x.Employee
                };
        }

        #region IUserStore<IdentityUser, Guid> Members
        public Task CreateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = getUser(user);

            _unitOfWork.UserRepository.Create(u);
            return _unitOfWork.SaveChangesAsync();
        }
        
        public Task DeleteAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = getUser(user);

            _unitOfWork.UserRepository.Delete(u.UserId);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            var user = _unitOfWork.UserRepository.GetByIdAsync(userId).Result;
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }

        public IdentityUser GetById(Guid id)
        {
            var user = _unitOfWork.UserRepository.GetById(id);
            return getIdentityUser(user);
        }

        public User FindById(Guid userId)
        {
            return _unitOfWork.UserRepository.GetById(userId);
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            var user = _unitOfWork.UserRepository.FindByUserName(userName);
            return Task.FromResult<IdentityUser>(getIdentityUser(user));
        }

        public User FindByName(string userName)
        {
            return _unitOfWork.UserRepository.FindByUserName(userName);
        }

        public Task UpdateAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentException("user");

            var u = _unitOfWork.UserRepository.GetByIdAsync(user.Id).Result;
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            populateUser(u, user);

            _unitOfWork.UserRepository.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Dispose does nothing since we want Unity to manage the lifecycle of our Unit of Work
        }
        #endregion

        #region IUserClaimStore<IdentityUser, Guid> Members
        //public Task AddClaimAsync(IdentityUser user, Claim claim)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException("user");
        //    if (claim == null)
        //        throw new ArgumentNullException("claim");

        //    var u = _unitOfWork.UserRepository.GetById(user.Id);
        //    if (u == null)
        //        throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

        //    var c = new Claim()
        //    {
        //        ClaimType = claim.ClaimType,
        //        ClaimValue = claim.ClaimValue,
        //        User = u
        //    };
        //    u.Claims.Add(c);

        //    _unitOfWork.UserRepository.Update(u);
        //    return _unitOfWork.SaveChangesAsync();
        //}

        //public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException("user");

        //    var u = _unitOfWork.UserRepository.GetById(user.Id);
        //    if (u == null)
        //        throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

        //    return Task.FromResult<IList<Claim>>(u.Claims.Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList());
        //}

        //public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException("user");
        //    if (claim == null)
        //        throw new ArgumentNullException("claim");

        //    var u = _unitOfWork.UserRepository.GetById(user.Id);
        //    if (u == null)
        //        throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

        //    var c = u.Claims.FirstOrDefault(x => x.ClaimType == claim.ClaimType && x.ClaimValue == claim.ClaimValue);
        //    u.Claims.Remove(c);

        //    _unitOfWork.UserRepository.Update(u);
        //    return _unitOfWork.SaveChangesAsync();
        //}
        #endregion

        #region IUserLoginStore<IdentityUser, Guid> Members
        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var u = _unitOfWork.UserRepository.GetByIdAsync(user.Id).Result;
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            //var l = new global::Entities.ExternalLogin
            //{
            //    LoginProvider = login.LoginProvider,
            //    ProviderKey = login.ProviderKey,
            //    User = u
            //};
            //u.Logins.Add(l);

            _unitOfWork.UserRepository.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            var identityUser = default(IdentityUser);

            //var l = _unitOfWork.ExternalLoginRepository.GetByProviderAndKey(login.LoginProvider, login.ProviderKey);
            //if (l != null)
            //    identityUser = getIdentityUser(l.User);

            return Task.FromResult<IdentityUser>(identityUser);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _unitOfWork.UserRepository.GetByIdAsync(user.Id).Result;
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return null;//return Task.FromResult<IList<UserLoginInfo>>(u.Logins.Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey)).ToList());
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (login == null)
                throw new ArgumentNullException("login");

            var u = _unitOfWork.UserRepository.GetByIdAsync(user.Id).Result;
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            //var l = u.Logins.FirstOrDefault(x => x.LoginProvider == login.LoginProvider && x.ProviderKey == login.ProviderKey);
            //u.Logins.Remove(l);

            _unitOfWork.UserRepository.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region IUserRoleStore<IdentityUser, Guid> Members
        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: roleName.");

            var u = _unitOfWork.UserRepository.GetByIdAsync(user.Id).Result;
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");
            var r = _unitOfWork.RoleRepository.FindByName(roleName);
            if (r == null)
                throw new ArgumentException("roleName does not correspond to a Role entity.", "roleName");

            u.Roles.Add(r);
            _unitOfWork.UserRepository.Update(u);

            return _unitOfWork.SaveChangesAsync();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var u = _unitOfWork.UserRepository.GetByIdAsync(user.Id).Result;
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<IList<string>>(u.Roles.Select(x => x.RoleName).ToList());
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var u = _unitOfWork.UserRepository.GetByIdAsync(user.Id).Result;
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            return Task.FromResult<bool>(u.Roles.Any(x => x.RoleName == roleName));
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Argument cannot be null, empty, or whitespace: role.");

            var u = _unitOfWork.UserRepository.GetByIdAsync(user.Id).Result;
            if (u == null)
                throw new ArgumentException("IdentityUser does not correspond to a User entity.", "user");

            var r = u.Roles.FirstOrDefault(x => x.RoleName == roleName);
            u.Roles.Remove(r);

            _unitOfWork.UserRepository.Update(u);
            return _unitOfWork.SaveChangesAsync();
        }
        #endregion

        #region IUserPasswordStore<IdentityUser, Guid> Members
        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.PasswordHash.ToString());
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.PasswordHash.ToString()));
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = Encoding.Default.GetBytes( passwordHash);
            return Task.FromResult(0);
        }
        #endregion

        #region IUserSecurityStampStore<IdentityUser, Guid> Members
        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult<string>(user.SaltHash.ToString());

        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SaltHash = Encoding.Default.GetBytes( stamp);
            return Task.FromResult(0);
        }
        #endregion

        #region Private Methods
        private User getUser(IdentityUser identityUser)
        {
            if (identityUser == null)
                return null;

            var user = new User();
            populateUser(user, identityUser);

            return user;
        }

        private void populateUser(User user, IdentityUser identityUser)
        {
            EmployeeBL employeeService = new EmployeeBL(_unitOfWork);
            user.UserId = identityUser.Id;
            user.UserName = identityUser.UserName;
            user.PasswordHash = identityUser.PasswordHash;
            user.SaltHash = identityUser.SaltHash;
            user.EmployeeId = identityUser.EmployeeId;
            user.Employee = employeeService.GetById(identityUser.EmployeeId);
            user.CreatedBy = identityUser.CreatedBy;
            user.CreatedDate = identityUser.CreatedDate;
            user.ModifiedBy = identityUser.ModifiedBy;
            user.ModifiedDate = identityUser.ModifiedDate;
            user.RecordStatus = identityUser.RecordStatus;
            user.LastLogInDate = identityUser.LastLogInDate;
            user.LastPasswordChange = identityUser.LastPasswordChange;
            user.OldPassword = user.PasswordHash;
        }

        private IdentityUser getIdentityUser(User user)
        {
            if (user == null)
                return null;

            var identityUser = new IdentityUser();
            populateIdentityUser(identityUser, user);

            return identityUser;
        }

        private void populateIdentityUser(IdentityUser identityUser,User user)
        {
            identityUser.Id = user.UserId;
            identityUser.UserName = user.UserName;
            identityUser.PasswordHash = user.PasswordHash;
            identityUser.SaltHash = user.SaltHash;
            identityUser.EmployeeId = user.EmployeeId;
            identityUser.CreatedBy = user.CreatedBy;
            identityUser.CreatedDate = user.CreatedDate;
            identityUser.ModifiedBy = user.ModifiedBy;
            identityUser.ModifiedDate = user.ModifiedDate;
            identityUser.RecordStatus = user.RecordStatus;
            identityUser.LastLogInDate = user.LastLogInDate;
            identityUser.LastPasswordChange = user.LastPasswordChange;
            identityUser.OldPassword = user.OldPassword;
        }


        #endregion

        public List<User> GetAllUsers()
        {
            // TODO exclude SuperAdmin users from the list
            return _unitOfWork.UserRepository.GetAll();
        }

        public List<User> GetAllActiveUsers()
        {
            return GetAllUsers().Where(x => x.RecordStatus != (int)RecordStatus.Deleted).OrderBy(x => x.UserName).ToList();
        }

        public User GetUserById(Guid id)
        {
            return  GetAllUsers().Where(x => x.UserId == id).FirstOrDefault();
        }

        public User GetUserByUsername(string name)
        {
            //return GetAllUsers().Where(x => x.UserName == name && x.RecordStatus == 1).FirstOrDefault();
            return _unitOfWork.UserRepository.FilterActiveBy(x => x.UserName == name && x.RecordStatus == 1).FirstOrDefault();
        }
    }
}