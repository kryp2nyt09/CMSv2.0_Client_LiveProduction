using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.Common.Enums;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class TruckAreaMappingBL : BaseAPCargoBL<TruckAreaMapping>
    {
        private ICmsUoW _unitOfWork;
        private TruckBL truckService;
        private AreaBL areaService;
        public TruckAreaMappingBL()
        {
            _unitOfWork = GetUnitOfWork();
            truckService = new TruckBL(_unitOfWork);
            areaService = new AreaBL(_unitOfWork);
        }

        public TruckAreaMappingBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<TruckAreaMapping, object>>[] Includes()
        {
            return new Expression<Func<TruckAreaMapping, object>>[]
                {
                    x=>x.RevenueUnit,
                    x=>x.Truck
                };
        }

        public List<TruckAreaMapping> GetByDate(DateTime date)
        {
            DateTime _date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            List<TruckAreaMapping> models = new List<TruckAreaMapping>();
            var _models = GetAll().OrderByDescending(x => x.DateAssigned).ToList();
            if (_models != null)
            {
                foreach (var item in _models)
                {
                    if (_date >= item.DateAssigned)
                    {
                        models.Add(item);
                    }
                }
            }
            return models;
        }

        public List<Truck> GetTrucks()
        {
            return truckService.FilterActive();
        }

        public List<RevenueUnit> GetAreas()
        {
            return areaService.FilterActive();
        }

        public override void Edit(TruckAreaMapping entity)
        {
            var existing = GetById(entity.TruckAreaMappingId);
            existing.ModifiedBy = entity.ModifiedBy;
            existing.ModifiedDate = entity.ModifiedDate;
            existing.RecordStatus = (int)RecordStatus.Inactive;
            base.Edit(existing);

            TruckAreaMapping model = new TruckAreaMapping();
            model.TruckAreaMappingId = Guid.NewGuid();
            model.RevenueUnitId = entity.RevenueUnitId;
            model.TruckId = entity.TruckId;
            model.DateAssigned = entity.DateAssigned;
            model.CreatedBy = entity.ModifiedBy;
            model.CreatedDate = entity.ModifiedDate;
            model.ModifiedBy = entity.ModifiedBy;
            model.ModifiedDate = entity.ModifiedDate;
            model.RecordStatus = (int)RecordStatus.Active;
            base.Add(model);
        }
    }
}
