using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.DataAccess
{
    internal class UserRepository : CmsRepository<User>, IUserRepository
    {
        internal UserRepository(CmsContext context)
            : base(context)
        {
        }

                public User FindByUserName(string username)
        {
            return dBSet.FirstOrDefault(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(string username)
        {
            return dBSet.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public Task<User> FindByUserNameAsync(System.Threading.CancellationToken cancellationToken, string username)
        {
            return dBSet.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }
    }
}