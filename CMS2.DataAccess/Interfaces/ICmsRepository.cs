using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CMS2.DataAccess.Interfaces
{
    public interface ICmsRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        List<TEntity> FilterActive();
        Task<List<TEntity>> FilterActiveAsync();
        List<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filter);
        List<TEntity> FilterActiveBy(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> FilterActiveByAsync(Expression<Func<TEntity, bool>> filter);
        Expression<Func<TEntity, object>>[] Includes { get; set; }
        bool IsExist(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(Guid id);
        TEntity GetById(int id);
        TEntity GetById(long id);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(long id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
        void Delete(int id);
        void Delete(long id);
        void DeletePhysically(Guid id);
        void DeletePhysically(int id);
        void DeletePhysically(long id);
    }
}
