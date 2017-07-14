using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CMS2.Common.Constants;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class TransferAcceptanceBL : BaseAPCargoBL<TransferAcceptance>
    {
        private ICmsUoW _unitOfWork;

        public TransferAcceptanceBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public TransferAcceptanceBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Expression<Func<TransferAcceptance, object>>[] Includes()
        {
            return new Expression<Func<TransferAcceptance, object>>[]
                {
                    x=>x.PackageNumberAcceptances,
                    x=>x.Driver,
                    x=>x.ScannedBy
                };
        }

        public List<TransferAcceptance> GetByDateAcceptanceTypeBcoId(DateTime date, string acceptanceType, Guid bcoId, Guid? gatewayId)
        {
            List<TransferAcceptance> models = new List<TransferAcceptance>();
            models = base.FilterBy(x => (x.AcceptanceDate.Year == date.Year && x.AcceptanceDate.Month == date.Month && x.AcceptanceDate.Day == date.Day));
            BranchCorpOfficeBL bcoService = new BranchCorpOfficeBL();
            switch (acceptanceType)
            {
                case AcceptanceTypeConstant.AreaToBco:
                    AreaBL areaService = new AreaBL();
                    var bsos = areaService.FilterBy(x => x.Cluster.BranchCorpOffice.BranchCorpOfficeId == bcoId).Select(x => x.RevenueUnitId).ToList();
                    models =
                        models.Where(
                            x => x.TransferType.Equals(TransferTypeConstant.AreaToBco) && bsos.Contains(x.TransferFromId))
                            .ToList();
                    if (models != null)
                    {
                        foreach (var item in models)
                        {
                            item.TransferFrom = areaService.GetById(item.TransferFromId);
                            item.TransferTo = bcoService.GetById(item.TransferToId);
                        }
                    }
                    break;
                case AcceptanceTypeConstant.BcoToGateWay:
                    GatewayBL gatewayService = new GatewayBL();
                    models =
                        models.Where(
                            x => x.TransferType.Equals(TransferTypeConstant.BcoToGateWay) && x.TransferFromId==bcoId && x.TransferToId==gatewayId)
                            .ToList();
                    if (models != null)
                    {
                        foreach (var item in models)
                        {
                            item.TransferFrom = bcoService.GetById(item.TransferFromId);
                            item.TransferTo = gatewayService.GetById(item.TransferToId);
                        }
                    }
                    break;
            }

            return models;
        }

        public List<TransferAcceptance> GetByDateFromToDriver(DateTime acceptanceDate, Guid transferFrom, Guid transferTo,
            Guid driver)
        {
            List<TransferAcceptance> models = new List<TransferAcceptance>();
            models = FilterBy(x => (x.AcceptanceDate.Year == acceptanceDate.Year && x.AcceptanceDate.Month == acceptanceDate.Month && x.AcceptanceDate.Day == acceptanceDate.Day) && x.TransferFromId == transferFrom && x.TransferToId == transferTo && x.DriverId == driver);

            return models;
        }
    }
}
