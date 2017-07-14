using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS2.BusinessLogic.Interfaces;
using CMS2.DataAccess;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System.Windows.Forms;
using System.Data.Entity.Validation;

namespace CMS2.BusinessLogic
{
    public abstract class BaseAPCargoBL<TEntity> : ICmsBL<TEntity> where TEntity : class
    {
        private ICmsUoW _unitOfWork = null;
        private ICmsRepository<TEntity> _entityRepository = null;
        public string LogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";

        public BaseAPCargoBL()
        {
            this._unitOfWork = new CmsUoW();
            this._entityRepository = _unitOfWork.Repository<TEntity>();
        }

        public BaseAPCargoBL(ICmsUoW unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._entityRepository = unitOfWork.Repository<TEntity>();
        }

        public ICmsUoW GetUnitOfWork()
        {
            return _unitOfWork;
        }

        public virtual List<TEntity> GetAll()
        {
            _entityRepository.Includes = Includes();
            return _entityRepository.GetAll();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            _entityRepository.Includes = Includes();
            return await _entityRepository.GetAllAsync();
        }
        
        public virtual List<TEntity> FilterActive()
        {
            _entityRepository.Includes = Includes();
            return _entityRepository.FilterActive();
        }

        public virtual async Task<List<TEntity>> FilterActiveAsync()
        {
            _entityRepository.Includes = Includes();
            return await _entityRepository.FilterActiveAsync();
        }

        // returns filtered by key and value
        public virtual List<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            _entityRepository.Includes = Includes();
            return _entityRepository.FilterBy(filter);
        }

        public virtual async Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filter)
        {
            _entityRepository.Includes = Includes();
            return await _entityRepository.FilterByAsync(filter);
        }
        // returns active and filtered by key and value
        public virtual List<TEntity> FilterActiveBy(Expression<Func<TEntity, bool>> filter)
        {
            _entityRepository.Includes = Includes();
            return _entityRepository.FilterActiveBy(filter);
        }

        public virtual async Task<List<TEntity>> FilterActiveByAsync(Expression<Func<TEntity, bool>> filter)
        {
            _entityRepository.Includes = Includes();
            return await _entityRepository.FilterActiveByAsync(filter);
        }

        public virtual Expression<Func<TEntity, object>>[] Includes()
        {
            return null;

            // use code below to specify entities
            //return new Expression<Func<TEnitty, object>>[]
            //    {
            //        x => x.entity
            //    };
        }

        public virtual bool IsExist(Expression<Func<TEntity, bool>> filter)
        {
            return _entityRepository.IsExist(filter);
        }

        public virtual TEntity GetById(Guid id)
        {
            return _entityRepository.GetById(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entityRepository.GetByIdAsync(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            return await  _entityRepository.GetByIdAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                _entityRepository.Create(entity);
                SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }

        public virtual void AddMultiple(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _entityRepository.Create(entity);
                SaveChanges();
            }
        }

        public virtual void Edit(TEntity entity)
        {
            _entityRepository.Update(entity);
            SaveChanges();
        }

        public virtual void EditMultiple(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _entityRepository.Update(entity);
                SaveChanges();
            }
        }

        public virtual void Delete(Guid id)
        {
            _entityRepository.Delete(id);
            SaveChanges();
        }

        public virtual void DeleteMultiple(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                _entityRepository.Delete(id);
                SaveChanges();
            }
        }

        public virtual void DeletePhysically(Guid id)
        {
            _entityRepository.DeletePhysically(id);
            SaveChanges();
        }

        public virtual void DeleteMultiplePhysically(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                _entityRepository.DeletePhysically(id);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public List<ApplicationSetting> ApplicationSetting
        {
            get
            {
                ApplicationSettingBL applicationSettingBL = new ApplicationSettingBL(this._unitOfWork);
                var result = applicationSettingBL.FilterActive();
                return result;
            }
        }

        public List<Role> Role
        {
            get
            {
                RoleStore roleBL = new RoleStore(this._unitOfWork);
                var result = roleBL.Roles;
                List<Role> roles = new List<Role>();
                foreach (var item in result)
                {
                    Role model = new Role()
                    {
                        RoleId = item.Id,
                        RoleName=item.Name
                    };
                    roles.Add(model);
                }
                return roles;
            }
        }

        //public List<Employee> GetByDateBcoName(DateTime date, string bcoName)
        //{
        //    DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        //    List<Employee> models = new List<Employee>();
        //    var _models =  this.FilterBy(x => !x..Equals(AssignLocationConstant.BCO))
        //            .OrderByDescending(x => x.DateAssigned)
        //            .ToList();
        //    if (_models != null)
        //    {
        //        foreach (var item in _models)
        //        {
        //            switch (item.LocationAssignment)
        //            {
        //                case AssignLocationConstant.Area:
        //                    item.AssignedLocation = areaService.GetById(item.AssignedLocationId);
        //                    break;
        //                case AssignLocationConstant.BSO:
        //                    item.AssignedLocation = bsoService.GetById(item.AssignedLocationId);
        //                    break;
        //                case AssignLocationConstant.GatewaySat:
        //                    item.AssignedLocation = gatewaySatService.GetById(item.AssignedLocationId);
        //                    break;
        //            }
        //            if (item.AssignedLocation.Cluster.BranchCorpOffice.BranchCorpOfficeName.Equals(bcoName))
        //            {
        //                if (_date >= item.DateAssigned)
        //                {
        //                    models.Add(item);
        //                }
        //            }
        //        }
        //    }
        //    return models;
        //}
        //private void RecordChange(TEntity entity, string changeType)
        //{
        //    RecordChangeBL recordChangeBL = new RecordChangeBL(this._unitOfWork);
        //    Entities.RecordChange change = new RecordChange();
        //    change.RecordChangeId = Guid.NewGuid();
        //    change.ChangeType = changeType;
        //    change.Entity = entity.GetType().FullName;
        //    change.EntityId = GetEntityId(entity);
        //    change.ChangeDate = DateTime.Now;
        //    recordChangeBL.Add(change);
        //}

        //private dynamic GetEntityId(TEntity entity)
        //{
        //    var key =entity.GetType().GetProperties().FirstOrDefault(x => x.GetCustomAttributes(typeof(KeyAttribute),false).Any());
        //    string primaryKey = key.Name;

        //    Type _type = entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().PropertyType;

        //    if (_type == typeof(Int32))
        //    {
        //        return Convert.ToInt32(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
        //    }

        //    if (_type == typeof(Int64))
        //    {
        //        return Convert.ToInt32(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
        //    }

        //    if (_type == typeof(Guid))
        //    {
        //        return new Guid(entity.GetType().GetProperties().Where(x => x.Name.Equals(primaryKey)).FirstOrDefault().GetValue(entity).ToString());
        //    }

        //    return null;
        //}

       
    }
}
