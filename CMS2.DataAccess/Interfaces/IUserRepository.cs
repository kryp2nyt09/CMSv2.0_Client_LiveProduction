using System.Threading;
using System.Threading.Tasks;
using CMS2.Entities;

namespace CMS2.DataAccess.Interfaces
{
    public interface IUserRepository : ICmsRepository<User>
    {
        User FindByUserName(string username);
        Task<User> FindByUserNameAsync(string username);
        Task<User> FindByUserNameAsync(CancellationToken cancellationToken, string username);
    }
}
