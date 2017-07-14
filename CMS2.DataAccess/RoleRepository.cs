using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.DataAccess
{
    internal class RoleRepository : CmsRepository<Role>, IRoleRepository
    {
        internal RoleRepository(CmsContext context)
            : base(context)
        {
        }

        public Role FindByName(string roleName)
        {
            return dBSet.FirstOrDefault(x => x.RoleName == roleName);
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return dBSet.FirstOrDefaultAsync(x => x.RoleName == roleName);
        }

        public Task<Role> FindByNameAsync(System.Threading.CancellationToken cancellationToken, string roleName)
        {
            return dBSet.FirstOrDefaultAsync(x => x.RoleName == roleName, cancellationToken);
        }
    }
}