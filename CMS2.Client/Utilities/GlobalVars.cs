using System;
using System.Collections.Generic;
using CMS2.Client.SyncHelper;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.Client
{
    public static class GlobalVars
    {
        public static ICmsUoW UnitOfWork { get; set; }
        public static Guid DeviceRevenueUnitId { get; set; }
        public static Guid DeviceCityId { get; set; }
        public static Guid DeviceBcoId { get; set; }
        public static string DeviceCode { get; set; }
        public static bool IsSubserver { get; set; }
        public static bool StopFlag { get; set; }
        
        public static Synchronization Sync { get; set; }
        public static List<MenuAccess> MenuAccess { get; set; }



    }
}
