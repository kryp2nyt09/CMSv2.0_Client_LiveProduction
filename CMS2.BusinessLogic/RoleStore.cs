using System;
using System.Linq;
using System.Threading.Tasks;
using CMS2.Common.Identity;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using Microsoft.AspNet.Identity;

namespace CMS2.BusinessLogic
{
    public class RoleStore : IRoleStore<IdentityRole, Guid>, IQueryableRoleStore<IdentityRole, Guid>, IDisposable
    {
        private readonly ICmsUoW _unitOfWork;

        public RoleStore(ICmsUoW unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region IRoleStore<IdentityRole, Guid> Members
        public Task CreateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r = GetRole(role);

            _unitOfWork.RoleRepository.Create(r);
            return _unitOfWork.SaveChangesAsync();
        }
        
        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            var r = GetRole(role);

            _unitOfWork.RoleRepository.Delete(r.RoleId);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task<IdentityRole> FindByIdAsync(Guid roleId)
        {
            var role = _unitOfWork.RoleRepository.GetByIdAsync(roleId).Result;
            return Task.FromResult<IdentityRole>(GetIdentityRole(role));
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            var role = _unitOfWork.RoleRepository.FindByName(roleName);
            return Task.FromResult<IdentityRole>(GetIdentityRole(role));
        }

        public Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
                throw new ArgumentNullException("role");
            var r = GetRole(role);
            
            _unitOfWork.RoleRepository.Update(r);
            return _unitOfWork.SaveChangesAsync();
        }
        
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            // Dispose does nothing since we want Unity to manage the lifecycle of our Unit of Work
        }
        #endregion

        #region IQueryableRoleStore<IdentityRole, Guid> Members
        public IQueryable<IdentityRole> Roles
        {
            get
            {
                return _unitOfWork.RoleRepository
                    .GetAllAsync().Result
                    .Select(x => GetIdentityRole(x))
                    .AsQueryable();
            }
        }
        #endregion

        #region Private Methods

        private Role GetRole(IdentityRole identityRole)
        {
            if (identityRole == null)
                return null;
            return new Role
            {
                RoleId = identityRole.Id,
                RoleName = identityRole.Name
            };
        }

        private IdentityRole GetIdentityRole(Role role)
        {
            if (role == null)
                return null;
            return new IdentityRole
            {
                Id = role.RoleId,
                Name = role.RoleName
            };
        }
        #endregion
    }
}