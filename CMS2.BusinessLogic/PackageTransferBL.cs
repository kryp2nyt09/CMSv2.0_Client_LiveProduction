using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.Common.Constants;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class PackageTransferBL :BaseAPCargoBL<PackageTransfer>
    {
         private ICmsUoW _unitOfWork;

        public PackageTransferBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public PackageTransferBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<PackageTransfer, object>>[] Includes()
        {
            return new Expression<Func<PackageTransfer, object>>[]
                {
                    x=>x.PackageNumberTransfers
                };
        }

        public List<PackageTransfer> GetByDateTransferTypeBcoId(DateTime date, string transferType, Guid bcoId)
        {
            List<PackageTransfer> models = new List<PackageTransfer>();
            models = base.FilterBy(x => (x.TransferDate.Year == date.Year && x.TransferDate.Month==date.Month && x.TransferDate.Day==date.Day));
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
            switch (transferType)
            {
                case TransferTypeConstant.BsoToBco:
                    BranchSatOfficeBL bsoService = new BranchSatOfficeBL();
                    var bsos = bsoService.FilterBy(x => x.RevenueUnitType.RevenueUnitTypeName.Equals(RevenueUnitTypes.BSO) && x.City.BranchCorpOffice.BranchCorpOfficeId == bcoId).Select(x=>x.RevenueUnitId).ToList();
                    models =
                        models.Where(
                            x => x.TransferType.Equals(TransferTypeConstant.BsoToBco) && bsos.Contains(x.TransferFromId))
                            .ToList();
                    if (models != null)
                    {
                        foreach (var item in models)
                        {
                            item.TransferFrom = bsoService.GetById(item.TransferFromId);
                            item.TransferTo = bcoService.GetById(item.TransferToId);
                        }
                    }
                    break;
                case TransferTypeConstant.BcoToGateWay:
                    var bcos = bcoService.FilterBy(x => x.BranchCorpOfficeId == bcoId).Select(x => x.BranchCorpOfficeId).ToList();
                    models =
                        models.Where(
                            x => x.TransferType.Equals(TransferTypeConstant.BsoToBco) && bcos.Contains(x.TransferFromId))
                            .ToList();
                    break;
            }

            return models;
        }

        public List<PackageTransfer> GetByDateFromToDriver(DateTime transferDate, Guid transferFrom, Guid transferTo,
            Guid driver)
        {
            List<PackageTransfer> models = new List<PackageTransfer>();
            models = FilterBy(x => (x.TransferDate.Year == transferDate.Year && x.TransferDate.Month == transferDate.Month && x.TransferDate.Day == transferDate.Day) && x.TransferFromId==transferFrom && x.TransferToId==transferTo && x.DriverId==driver);
            return models;
        }
    }
}
