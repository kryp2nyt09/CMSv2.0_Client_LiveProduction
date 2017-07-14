using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMS2.DataAccess.Interfaces
{
    public interface ICmsUoW : IDisposable
    {
        ICmsRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void Save();
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
