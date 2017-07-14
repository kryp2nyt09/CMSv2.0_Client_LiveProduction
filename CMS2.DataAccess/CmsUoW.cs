using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CMS2.DataAccess.Interfaces;
using System.Windows.Forms;
using System.Data.Entity.Validation;
using CMS2.Common;

namespace CMS2.DataAccess
{
    public class CmsUoW : ICmsUoW
    {
        private bool disposed = false;

        private CmsContext _context = null;
        private Hashtable _repositories;
        private object _lockedObject = new object();

        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;

        public CmsUoW()
        {
            this._context = new CmsContext();
            try
            {
                if (_context.Database.Connection.State == ConnectionState.Closed)
                    _context.Database.Connection.Open();
            }
            catch (Exception ex)
            {
                Logs.AppLogs("", "CmsUoW constructor", ex.Message);
            }
           
        }

        public CmsUoW(string connectionString)
        {
            this._context = new CmsContext(connectionString);
            try
            {
                if (_context.Database.Connection.State == ConnectionState.Closed)
                    _context.Database.Connection.Open();
            }
            catch (Exception ex)
            {
            }
          
        }

        public ICmsRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(CmsRepository<>);

                    var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                    lock (_lockedObject)
                    {
                        _repositories.Add(type, repositoryInstance);
                    }
            }

            return (ICmsRepository<TEntity>)_repositories[type];
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.InnerException.ToString());
            }
           
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _roleRepository = null;
                    _userRepository = null;
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRoleRepository RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(this._context)); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(this._context)); }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
