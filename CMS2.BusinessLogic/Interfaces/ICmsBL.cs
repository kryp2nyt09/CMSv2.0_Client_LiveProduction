using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS2.Entities;

namespace CMS2.BusinessLogic.Interfaces
{
    public interface ICmsBL<TEntity>
    {
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        List<TEntity> FilterActive();
        Task<List<TEntity>> FilterActiveAsync();
        List<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filter);
        List<TEntity> FilterActiveBy(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> FilterActiveByAsync(Expression<Func<TEntity, bool>> filter);
        Expression<Func<TEntity, object>>[] Includes();
        bool IsExist(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        void Add(TEntity entity);
        void AddMultiple(List<TEntity> entities);
        void Edit(TEntity entity);
        void EditMultiple(List<TEntity> entities);
        void Delete(Guid id);
        void DeleteMultiple(List<Guid> ids);
        void DeletePhysically(Guid id);
        void DeleteMultiplePhysically(List<Guid> ids);
        void SaveChanges();
        List<ApplicationSetting> ApplicationSetting { get; }
        List<Role> Role { get; }
    }
}
