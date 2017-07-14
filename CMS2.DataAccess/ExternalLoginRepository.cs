using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using APCargo.DataAccess.Interfaces;
using System.Windows.Forms;
using System;

namespace DataAccess
{
    internal class ExternalLoginRepository : Repository<ExternalLogin>, IExternalLoginRepository
    {
        internal ExternalLoginRepository(ApplicationContext context)
            : base(context)
        {
        }

        public ExternalLogin GetByProviderAndKey(string loginProvider, string providerKey)
        {
            return Set.FirstOrDefault(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
        }

        public Task<ExternalLogin> GetByProviderAndKeyAsync(string loginProvider, string providerKey)
        {
            return Set.FirstOrDefaultAsync(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);
        }

        public Task<ExternalLogin> GetByProviderAndKeyAsync(CancellationToken cancellationToken, string loginProvider, string providerKey)
        {
            return Set.FirstOrDefaultAsync(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey, cancellationToken);
        }

        ExternalLogin IExternalLoginRepository.GetByProviderAndKey(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        Task<ExternalLogin> IExternalLoginRepository.GetByProviderAndKeyAsync(string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }

        Task<ExternalLogin> IExternalLoginRepository.GetByProviderAndKeyAsync(CancellationToken cancellationToken, string loginProvider, string providerKey)
        {
            throw new NotImplementedException();
        }
    }
}