using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using CMS2.Common.Enums;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using CMS2.Common;
namespace CMS2.DataAccess
{
    // use internal since only the UnitOfWork will access this class
    public class CmsRepository<TEntity> : ICmsRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private CmsContext _context = null;
        private DbSet<TEntity> _dbSet = null;

        public CmsRepository()
        {
            this._context = new CmsContext();            
            this._dbSet = _context.Set<TEntity>();
        }

        public CmsRepository(CmsContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<TEntity>();
        }

        protected DbSet<TEntity> dBSet
        {
            get { return _dbSet ?? (_dbSet = _context.Set<TEntity>()); }
        }

        private IQueryable<TEntity> GetEntities()
        {
            IQueryable<TEntity> result = _dbSet.AsQueryable();
            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    result = result.Include(include);
                }
            }
            return result;
        }

        public Expression<Func<TEntity, object>>[] Includes { get; set; }

        // returns all records. includes inactive and deleted
        public List<TEntity> GetAll()
        {
            List<TEntity> entities = new List<TEntity>();
            try
            {
                entities = GetEntities().ToList();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository GetAll", ex );
                throw;
            }
            return entities;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            List<TEntity> entities = new List<TEntity>();
            try
            {
                entities = await GetEntities().ToListAsync();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository GetAllAsync", ex );
                throw;
            }
            return entities;
        }

        // returns all active records only
        public List<TEntity> FilterActive()
        {
            List<TEntity> entities = new List<TEntity>();
            try
            {
                entities = GetEntities().Where(x => x.RecordStatus == (int)RecordStatus.Active).ToList();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository FilterActive", ex );
                throw;
            }
            return entities;

        }

        public async Task<List<TEntity>> FilterActiveAsync()
        {

            List<TEntity> entities = new List<TEntity>();
            try
            {
                entities = await GetEntities().Where(x => x.RecordStatus == (int)RecordStatus.Active).ToListAsync();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository FilterActiveAsync", ex );
                throw;
            }
            return entities;
        }

        // parameter: filter
        // returns filtered records by field and value
        public List<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter)
        {

            List<TEntity> entities = new List<TEntity>();
            try
            {
                entities = GetEntities().Where(filter).ToList();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository FilterBy", ex);
                throw;
            }
            return entities;
        }

        public async Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filter)
        {
            List<TEntity> entities = new List<TEntity>();
            try
            {
                entities = await GetEntities().Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository FilterByAsync", ex);
                throw;
            }
            return entities;

        }

        // parameter: filter
        // returns active filtered records by field and value
        public List<TEntity> FilterActiveBy(Expression<Func<TEntity, bool>> filter)
        {
            List<TEntity> entities = new List<TEntity>();
            try
            {
                entities = GetEntities().Where(x => x.RecordStatus == (int)RecordStatus.Active).Where(filter).ToList();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository FilterAciveBy", ex  );
                throw;
            }
            return entities;

        }

        public async Task<List<TEntity>> FilterActiveByAsync(Expression<Func<TEntity, bool>> filter)
        {
            List<TEntity> entities = new List<TEntity>();
            try
            {
                entities = await GetEntities().Where(x => x.RecordStatus == (int)RecordStatus.Active).Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository FilterActiveByAsync", ex );
                throw;
            }
            return entities;

        }

        public bool IsExist(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                if (_dbSet.Where(filter).Any())
                    return true;

            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository isExist", ex );
                throw;
            }
            return false;
        }

        public TEntity GetById(Guid id)
        {
            TEntity entity = new TEntity();
            try
            {
                entity = _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository IsExist", ex );
                throw;
            }
            return entity;
        }

        public TEntity GetById(int id)
        {
            TEntity entity = new TEntity();
            try
            {
                entity = _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository IsExist", ex);
                throw;
            }
            return entity;
        }

        public TEntity GetById(long id)
        {
            TEntity entity = new TEntity();
            try
            {
                entity = _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository IsExist", ex);
                throw;
            }
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            TEntity entity = new TEntity();
            try
            {
                entity = await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository GetByIdAsync", ex );
                throw;
            }
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            TEntity entity = new TEntity();
            try
            {
                entity = await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository GetByIdAsync", ex);
                throw;
            }
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            TEntity entity = new TEntity();
            try
            {
                entity = await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            { 
                Logs.ErrorLogs("", "Cms Repository GetByIdAsync", ex);
            throw;
            }
            return entity;
        }

        public void Create(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository Create", ex );
                throw;
            }

        }

        public void Update(TEntity entity)
        {
            try
            {
                string primaryKey = GetPrimaryKey(entity);

                TEntity model = _dbSet.Find(GetEntityId(entity));
                string propertyName = "";
                foreach (PropertyInfo prop in model.GetType().GetProperties())
                {
                    propertyName = prop.Name;
                    var propertyAttributes = entity.GetType().GetProperty(propertyName).GetCustomAttributes(false);
                    bool isNotMapped = false;
                    foreach (var att in propertyAttributes)
                    {
                        if (att.GetType() == typeof(NotMappedAttribute))
                        {
                            isNotMapped = true;
                            break;
                        }
                    }
                    var propertyValue = entity.GetType().GetProperty(propertyName).GetValue(entity);
                    if (propertyName.Equals(primaryKey) || // specific properties to be excluded
                        propertyName.Contains("Created") ||
                        propertyName.Equals("Record_Status") ||
                        propertyName.Equals("RecordStatusString") ||
                        propertyName.Equals("FullName") ||
                        isNotMapped
                        )
                    {
                    }
                    else
                    {
                        prop.SetValue(model, propertyValue);
                    }
                }
                _context.Entry(model).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository Update", ex );
                throw;
            }

        }

        public void Delete(Guid id)
        {
            try
            {
                TEntity entity = _dbSet.Find(id);
                entity.RecordStatus = (int)RecordStatus.Deleted;
                Update(entity);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository Delete", ex );
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                TEntity entity = _dbSet.Find(id);
                entity.RecordStatus = (int)RecordStatus.Deleted;
                Update(entity);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository Delete", ex );
                throw;
            }

        }

        public void Delete(long id)
        {
            try
            {
                TEntity entity = _dbSet.Find(id);
                entity.RecordStatus = (int)RecordStatus.Deleted;
                Update(entity);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository Delete", ex );
                throw;
            }
        }

        public void DeletePhysically(Guid id)
        {

            try
            {
                TEntity entity = _dbSet.Find(id);
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository DeletePhysically", ex );
                throw;
            }
        }

        public void DeletePhysically(int id)
        {
            try
            {
                TEntity entity = _dbSet.Find(id);
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository DeletePhysically", ex );
                throw;
            }
        }

        public void DeletePhysically(long id)
        {
            try
            {
                TEntity entity = _dbSet.Find(id);
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository DeletePhysically", ex );
                throw;
            }
        }

        private dynamic GetEntityId(TEntity entity)
        {
            try
            {
                string primaryKey = GetPrimaryKey(entity);
                Type _type =
                    entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().PropertyType;

                if (_type == typeof(Int32))
                {
                    return Convert.ToInt32(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
                }

                if (_type == typeof(Int64))
                {
                    return Convert.ToInt32(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
                }

                if (_type == typeof(Guid))
                {
                    return new Guid(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
                }

            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository GetEntityId", ex );
                throw;
            }

            return null;
        }

        private string GetPrimaryKey(TEntity entity)
        {
            try
            {
                var key =
                              entity.GetType()
                                  .GetProperties()
                                  .FirstOrDefault(x => x.GetCustomAttributes(typeof(KeyAttribute)).Any());

                return key.Name;
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Cms Repository GetPrimaryKey", ex );
                throw;
            }
            return "";
        }
    }
}
