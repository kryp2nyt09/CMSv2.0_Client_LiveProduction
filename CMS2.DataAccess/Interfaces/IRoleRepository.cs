using System.Threading;
using System.Threading.Tasks;
using CMS2.Entities;

namespace CMS2.DataAccess.Interfaces
{
    public interface IRoleRepository : ICmsRepository<Role>
    {
        Role FindByName(string roleName);
        Task<Role> FindByNameAsync(string roleName);
        Task<Role> FindByNameAsync(CancellationToken cancellationToken, string roleName);
    }
}
