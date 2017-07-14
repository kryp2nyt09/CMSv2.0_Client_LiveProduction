using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using CMS2.Entities.Models;

namespace CMS2.BusinessLogic
{
    public class PackageDimensionBL : BaseAPCargoBL<PackageDimension>
    {
        private ICmsUoW _unitOfWork;

        public PackageDimensionBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PackageDimensionBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }

        public override Expression<Func<PackageDimension, object>>[] Includes()
        {
            return new Expression<Func<PackageDimension, object>>[]
                {
                    x => x.Crating
                };
        }

        public PackageDimension ModelToEntity(PackageDimensionModel model)
        {
            PackageDimension entity = new PackageDimension();
            entity.PackageDimensionId = model.PackageDimensionId;
            entity.ShipmentId = model.ShipmentId;
            entity.Length = model.Length;
            entity.Width = model.Width;
            entity.Height = model.Height;
            entity.CratingId = model.CratingId;
            entity.DrainingId = model.DrainingId;
            entity.CreatedBy = model.CreatedBy;
            entity.CreatedDate = model.CreatedDate;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = model.ModifiedDate;
            entity.RecordStatus = model.RecordStatus;
            return entity;
        }

        public List<PackageDimension> ModelsToEntities(List<PackageDimensionModel> models)
        {
            List<PackageDimension> entities = new List<PackageDimension>();
            foreach (var item in models)
            {
                entities.Add(ModelToEntity(item));
            }
            return entities;
        }

        public PackageDimensionModel EntityToModel(PackageDimension entity)
        {
            PackageDimensionModel model = new PackageDimensionModel();
            model.PackageDimensionId = entity.PackageDimensionId;
            model.ShipmentId = entity.ShipmentId;
            model.Length = entity.Length;
            model.Width = entity.Width;
            model.Height = entity.Height;
            model.CratingId = entity.CratingId;
            model.DrainingId = entity.DrainingId;
            model.CreatedBy = entity.CreatedBy;
            model.CreatedDate = entity.CreatedDate;
            model.ModifiedBy = entity.ModifiedBy;
            model.ModifiedDate = entity.ModifiedDate;
            model.RecordStatus = entity.RecordStatus;
            return model;
        }

        public List<PackageDimensionModel> EntitiesToModels(List<PackageDimension> entities)
        {
            List<PackageDimensionModel> models = new List<PackageDimensionModel>();
            foreach (var item in entities)
            {
                models.Add(EntityToModel(item));
            }
            return models;
        }
    }
}
