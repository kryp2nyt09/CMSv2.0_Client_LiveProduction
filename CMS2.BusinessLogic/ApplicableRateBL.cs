using System.Linq;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;
using System;

namespace CMS2.BusinessLogic
{
    public class ApplicableRateBL : BaseAPCargoBL<ApplicableRate>
    {
        private ICmsUoW _unitOfWork;
        
        public ApplicableRateBL()
        {
            _unitOfWork = GetUnitOfWork();           
        }

        public ApplicableRateBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {
            
        }

        public ApplicableRate GetApplicableRate(Guid CommodityTypeId, Guid ServiceModeId, Guid ServiceTypeId)
        {
            ApplicableRate result = null;
            result = FilterActiveBy(x => x.CommodityTypeId == CommodityTypeId && x.ServiceModeId == ServiceModeId
                && x.ServiceTypeId == ServiceTypeId).FirstOrDefault();
            return result;
        }

    }
}
