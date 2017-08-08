using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Synchronization;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using CMS2.Client.Properties;
using CMS2.DataAccess;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Metadata.Edm;
using System.ComponentModel;
using CMS2.Entities;
using CMS2.Entities.ReportModel;

namespace CMS2.Client.SyncHelper
{
    public class Synchronization
    {

        private SqlConnection ServerConnection;
        private SqlConnection ClientConnection;

        private string DeviceBranchCorpOfficeID;
        private string Filter;
        private bool isProvision;
        private bool isDeprovisionServer;
        private bool isDeprovisionClient;
        private List<SyncTables> Entities = new List<SyncTables>();

        List<ManualResetEvent> ProvisionEvents = new List<ManualResetEvent>();
        List<ManualResetEvent> ProvisionEvents1 = new List<ManualResetEvent>();
        List<ManualResetEvent> DeprovisionEvents = new List<ManualResetEvent>();
        List<ManualResetEvent> DeprovisionEvents1 = new List<ManualResetEvent>();
        List<ManualResetEvent> SynchronizationEvents = new List<ManualResetEvent>();
        List<ManualResetEvent> SynchronizationEvents1 = new List<ManualResetEvent>();

        public Synchronization()
        {
            this.ClientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Cms"].ConnectionString);
            this.ServerConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CmsCentral"].ConnectionString);
        }

        public Synchronization(List<SyncTables> tables, bool isProvision, bool isDeprovisionClient, bool isDeprovisionServer, string BranchCorpOfficeId, string Filter)
        {
            if (ValidateConnectionStrings(ConfigurationManager.ConnectionStrings["Cms"].ConnectionString, ConfigurationManager.ConnectionStrings["CmsCentral"].ConnectionString))
            {
                this.ClientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Cms"].ConnectionString);
                this.ServerConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["CmsCentral"].ConnectionString);
                this.Entities = tables;
                this.DeviceBranchCorpOfficeID = BranchCorpOfficeId;
                this.Filter = Filter;
                this.isProvision = isProvision;
                this.isDeprovisionServer = isDeprovisionServer;
                this.isDeprovisionClient = isDeprovisionClient;
                this.DeProvision_Server();
                this.DeProvision_Client();
                this.Prepare_Database_For_Synchronization();

            }

        }

        public Synchronization(string clientConnection, string serverConnection, bool isProvision, bool isDeprovisionClient, bool isDeprovisionServer, string BranchCorpOfficeId, string Filter)
        {
            if (ValidateConnectionStrings(clientConnection, serverConnection))
            {
                this.ClientConnection = new SqlConnection(clientConnection);
                this.ServerConnection = new SqlConnection(serverConnection);
                this.SetTables();
                this.DeviceBranchCorpOfficeID = BranchCorpOfficeId;
                this.Filter = Filter;
                this.isProvision = isProvision;
                this.isDeprovisionServer = isDeprovisionServer;
                this.isDeprovisionClient = isDeprovisionClient;
                this.DeProvision_Server();
                this.DeProvision_Client();
                this.Prepare_Database_For_Synchronization();
            }
        }

        private bool ValidateConnectionStrings(string _local, string _server)
        {
            if (_local == _server)
            {
                return false;
            }
            return true;
        }

        private void Prepare_Database_For_Synchronization()
        {

            if (!isProvision)
            {
                return;
            }
            for (int i = 0; i < Entities.Count - 1; i++)
            {
                ManualResetEvent _newEvent = new ManualResetEvent(false);

                if (i <= 60)
                {
                    ProvisionEvents.Add(_newEvent);
                }
                else
                {
                    ProvisionEvents1.Add(_newEvent);
                }

                Provision _provision = new Provision(Entities[i].TableName, ClientConnection, ServerConnection, _newEvent, Filter, DeviceBranchCorpOfficeID);
                ThreadPool.QueueUserWorkItem(new WaitCallback(_provision.PrepareDatabaseForSynchronization), Entities[i]);
            }
        }

        public void Synchronize()
        {
            List<ThreadState> listOfThread = new List<ThreadState>();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            foreach (SyncTables table in Entities)
            {
                ThreadState _threadState = new ThreadState();
                _threadState.table = table;
                _threadState.worker = worker;
                listOfThread.Add(_threadState);

                try
                {
                    Synchronize sync = new Synchronize(table.TableName, Filter, _threadState._event, ClientConnection, ServerConnection);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(sync.PerformSync), _threadState);
                    _threadState._event.WaitOne();
                }
                catch (Exception ex)
                {
                }
            }

        }

        /// <summary>
        /// Remove the "_Filter_template" template from the server database.
        /// This also removes all of the scopes that depend on the template.            
        /// </summary>
        /// <param name="tableName"></param>
        public void DeProvision_Template(string tableName)
        {
            try
            {
                SqlSyncScopeDeprovisioning templateDeprovisioning = new SqlSyncScopeDeprovisioning(ServerConnection);
                templateDeprovisioning.DeprovisionTemplate(tableName);
                Console.Write(tableName + " template was Deprovision.");
            }
            catch (Exception)
            {
                Console.Write(tableName + " template was already Deprovision.");
            }

        }

        public void Deprovision_Server(string tableName)
        {
            try
            {
                ManualResetEvent _newEvent = new ManualResetEvent(false);
                DeprovisionEvents.Add(_newEvent);
                Deprovision _deprovision = new Deprovision(ServerConnection, _newEvent, Filter, tableName);
                ThreadPool.QueueUserWorkItem(_deprovision.PerformDeprovisionTable, tableName);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(tableName, ex);
            }
        }

        public void DeProvision_Server()
        {
            if (!this.isDeprovisionServer)
            {
                return;
            }

            try
            {
                for (int i = 0; i < Entities.Count - 1; i++)
                {
                    ManualResetEvent _newEvent = new ManualResetEvent(false);
                    if (i < 60)
                    {
                        DeprovisionEvents.Add(_newEvent);
                    }
                    else
                    {
                        DeprovisionEvents1.Add(_newEvent);
                    }
                    Deprovision _deprovision = new Deprovision(ServerConnection, _newEvent, Filter, Entities[i].TableName);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(_deprovision.PerformDeprovisionTable), Entities[i]);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(ex);
            }

        }

        public void DeProvision_Client()
        {
            if (!this.isDeprovisionClient)
            {
                return;
            }

            try
            {
                for (int i = 0; i < Entities.Count - 1; i++)
                {
                    ManualResetEvent _newEvent = new ManualResetEvent(false);
                    if (i < 60)
                    {
                        DeprovisionEvents.Add(_newEvent);
                    }
                    else
                    {
                        DeprovisionEvents1.Add(_newEvent);
                    }
                    Deprovision _deprovision = new Deprovision(ClientConnection, _newEvent, Filter, Entities[i].TableName);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(_deprovision.PerformDeprovisionTable), Entities[i]);
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(ex);
            }

        }

        public void DeProvision_Client(string tableName)
        {
            try
            {
                ManualResetEvent _newEvent = new ManualResetEvent(false);
                Deprovision _deprovision = new Deprovision(ServerConnection, _newEvent, Filter, tableName);
                ThreadPool.QueueUserWorkItem(_deprovision.PerformDeprovisionTable, tableName);
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(tableName, ex);
            }

        }

        public void SetTables()
        {
            using (CmsContext context = new CmsContext())
            {
                //ObjectContext objContext = ((IObjectContextAdapter)context).ObjectContext;
                //MetadataWorkspace workspace = objContext.MetadataWorkspace;
                
                //IEnumerable<EntityType> tables = workspace.GetItems<EntityType>(DataSpace.SSpace);

                List<string> list = new List<string>(){"AccountStatus", "AccountType", "AdjustmentReason", "ApplicableRate", "ApplicationSetting",
                "ApprovingAuthority", "Company", "Employee", "RevenueUnit", "City", "BranchCorpOffice", "Cluster", "Province", "Region",
                "Group", "RevenueUnitType", "Department", "Position", "BillingPeriod", "BusinessType", "Client", "Industry", 
                "OrganizationType", "PaymentMode", "PaymentTerm", "AwbIssuance", "Batch", "BookingRemark", "Booking", "BookingStatus",
                "BranchAcceptance", "Remarks", "Bundle", "CargoTransfer", "Reason", "Status", "User", "Claim", "Role", "Commodity",
                "CommodityType", "WeightBreak", "DeliveredPackage", "Delivery", "DeliveryReceipt", "DeliveryRemark", "DeliveryStatus",
                "Shipment", "ShipmentAdjustment", "StatementOfAccount", "ShipmentBasicFee", "FuelSurcharge", "GoodsDescription",
                "PackageDimension", "Crating", "Packaging", "PackageNumber", "Payment", "PaymentType", "StatementOfAccountPayment",
                "ServiceMode", "ServiceType", "ShipMode", "Distribution", "ExpressRate", "RateMatrix", "FlightInfo", "GatewayInbound",
                "GatewayOutbound", "GatewayTransmittal", "HoldCargo", "Menu", "PackageNumberAcceptance", "TransferAcceptance",
                "PackageNumberTransfer", "PackageTransfer", "PaymentSummary", "PaymentSummaryStatus", "PaymentTurnover", "RecordChange",
                "Segregation", "ShipmentStatus", "ShipmentTracking", "StatementOfAccountNumber", "StatementOfAccountPrint",
                "TntMaint", "TransmittalStatus", "TransShipmentLeg", "TransShipmentRoute",
                "TruckAreaMapping", "Truck", "Unbundle", "RoleUser", "MenuAccess",  "SubMenu"};

                foreach (var item in list)
                {
                    SyncTables table = new SyncTables();
                    table.TableName = item;
                    Entities.Add(table);
                }
            }
        }

    }

    public class Synchronize
    {

        private ManualResetEvent _currentEvent;
        private string _tableName;
        private SqlConnection _localConnection;
        private SqlConnection _serverConnection;
        private string _filter;
        
        ThreadState State;

        public Synchronize(string tableName, string filter, ManualResetEvent currentEvent, SqlConnection localConnection, SqlConnection serverConnection)
        {
            this._tableName = tableName;
            this._filter = filter;
            this._currentEvent = currentEvent;
            this._localConnection = localConnection;
            this._serverConnection = serverConnection;
        }
        public void PerformSync(Object obj)
        {

            State = (ThreadState)obj;
            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
            syncOrchestrator.LocalProvider = new SqlSyncProvider(_tableName + _filter, _localConnection);
            syncOrchestrator.RemoteProvider = new SqlSyncProvider(_tableName + _filter, _serverConnection);
            SyncOperationStatistics syncStats;
            
            State.worker.ReportProgress(0, _tableName + " was synchronizing.");
            
            switch (_tableName)
            {
                case "Shipment":
                case "PackageNumber":
                case "PackageDimension":
                case "Payment":
                case "StatementOfAccountPayment":
                case "PaymentTurnOver":
                case "ShipmentAdjustment":
                case "Delivery":
                case "DeliveryPackage":
                case "DeliveryReceipt":
                case "Booking":
                case "Client":
                case "Bundle":                    
                    

                    try
                    {
                        syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
                        syncStats = syncOrchestrator.Synchronize();
                        Log.WriteLogs(_tableName + " Total Changes Uploaded: " + syncStats.UploadChangesTotal + " Total Changes Downloaded: " + syncStats.DownloadChangesTotal + " Total Changes applied: " + syncStats.DownloadChangesApplied + " Total Changes failed: " + syncStats.DownloadChangesFailed);
                        State.table.Status = TableStatus.Working;
                        State.table.isSelected = false;
                        State.worker.ReportProgress(1, _tableName + " was synchronized.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Log.WriteErrorLogs(ex);
                            State.table.Status = TableStatus.Error;
                            State.table.isSelected = true;
                            State.worker.ReportProgress(1, _tableName + " synchronize error.");
                        }
                        catch (Exception)
                        {
                        }

                    }
                    break;

                case "PaymentSummary":
                case "BranchAcceptance":
                case "GatewayTransmittal":
                case "GatewayOutbound":
                case "GatewayInbound":                
                case "Unbundle":
                case "HoldCargo":
                case "Distribution":
                case "Segregation":
                case "CargoTransfer":


                    try
                    {
                        syncOrchestrator.Direction = SyncDirectionOrder.Upload;
                        syncStats = syncOrchestrator.Synchronize();
                        Log.WriteLogs(_tableName + " Total Changes Uploaded: " + syncStats.UploadChangesTotal + " Total Changes Downloaded: " + syncStats.DownloadChangesTotal + " Total Changes applied: " + syncStats.DownloadChangesApplied + " Total Changes failed: " + syncStats.DownloadChangesFailed);
                        State.table.Status = TableStatus.Working;
                        State.table.isSelected = false;
                        State.worker.ReportProgress(1, _tableName + " was synchronized.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Log.WriteErrorLogs(ex);
                            State.table.Status = TableStatus.Error;
                            State.table.isSelected = true;
                            State.worker.ReportProgress(1, _tableName + " synchronize error.");
                        }
                        catch (Exception)
                        {
                        }

                    }
                    break;
                default:
                    syncOrchestrator.Direction = SyncDirectionOrder.Download;
                    try
                    {
                        syncStats = syncOrchestrator.Synchronize();

                        Log.WriteLogs(_tableName + " Total Changes Uploaded: " + syncStats.UploadChangesTotal + " Total Changes Downloaded: " + syncStats.DownloadChangesTotal + " Total Changes applied: " + syncStats.DownloadChangesApplied + " Total Changes failed: " + syncStats.DownloadChangesFailed);

                        State.table.Status = TableStatus.Working;
                        State.table.isSelected = false;
                        State.worker.ReportProgress(1, _tableName + " was synchronized.");
                        //AddCount();
                        break;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Log.WriteErrorLogs(ex);
                            State.table.Status = TableStatus.Error;
                            State.table.isSelected = true;
                            State.worker.ReportProgress(1, _tableName + " synchronize error.");
                            //AddCount();
                        }
                        catch (Exception)
                        {
                        }

                    }
                    break;
            }

            State._event.Set();

        }
        public void RepicateDatabase(Object obj)
        {
            State = (ThreadState)obj;
            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
            syncOrchestrator.LocalProvider = new SqlSyncProvider(_tableName + _filter, _localConnection);
            syncOrchestrator.RemoteProvider = new SqlSyncProvider(_tableName + _filter, _serverConnection);
            SyncOperationStatistics syncStats;

            State.worker.ReportProgress(0, _tableName + " was synchronizing.");
            syncOrchestrator.Direction = SyncDirectionOrder.Download;
            try
            {
                syncStats = syncOrchestrator.Synchronize();

                Log.WriteLogs(_tableName + " Total Changes Uploaded: " + syncStats.UploadChangesTotal + " Total Changes Downloaded: " + syncStats.DownloadChangesTotal + " Total Changes applied: " + syncStats.DownloadChangesApplied + " Total Changes failed: " + syncStats.DownloadChangesFailed);
                State.table.Status = TableStatus.Working;
                State.table.isSelected = false;
                State.worker.ReportProgress(1, _tableName + " was synchronized.");
            }
            catch (Exception ex)
            {
                try
                {
                    Log.WriteErrorLogs(ex);
                    State.table.Status = TableStatus.Error;
                    State.table.isSelected = true;
                    State.worker.ReportProgress(1, _tableName + " synchronize error.");
                }
                catch (Exception)
                {
                }

            }
            State._event.Set();
        }

    }

    public class Provision
    {

        SqlConnection _serverConnection;
        SqlConnection _localConnection;

        private string _tableName;
        private string _filter;
        private string _branchCorpOfficeId;

        private ManualResetEvent _currentEvent;

        static DbSyncScopeDescription scopeDesc;
        static DbSyncTableDescription tableDescription;
        static SqlSyncScopeProvisioning serverTemplate;

        bool flag = false;
        public Provision() { }
        public Provision(string tableName, SqlConnection localConnection, SqlConnection serverConnection, ManualResetEvent currentEvent, string filter, string branchCorpOfficeId)
        {
            this._tableName = tableName;
            this._localConnection = new SqlConnection(localConnection.ConnectionString);
            this._serverConnection = new SqlConnection(serverConnection.ConnectionString);
            this._currentEvent = currentEvent;
            this._filter = filter;
            this._branchCorpOfficeId = branchCorpOfficeId;
        }

        public void PrepareDatabaseForSynchronization(Object obj)
        {

            SqlParameter param;
            ThreadState state = (ThreadState)obj;

            try
            {

                string filterColumn = "";
                string filterClause = "";
                switch (_tableName)
                {
                    case "Booking":

                        filterColumn = "BookingId";
                        filterClause = "[side].[BookingId] IN (SELECT c.BookingId FROM " +
                                        "((SELECT book.BookingId  FROM Booking as book " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = book.AssignedToAreaId " +
                                        "left join City as city on city.CityId = ru.CityId " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId) " +
                                        "UNION " +
                                        "(SELECT b.BookingId FROM Booking b " +
                                        "LEFT JOIN City c ON c.CityId = b.DestinationCityId " +
                                        "LEFT JOIN BranchCorpOffice bco ON bco.BranchCorpOfficeId = c.BranchCorpOfficeId " +
                                        "WHERE bco.BranchCorpOfficeId = @BranchCorpOfficeId)) c )";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);
                        
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        
                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                    case "Shipment":

                        filterColumn = "ShipmentId";
                        filterClause = "[side].[ShipmentId] In (SELECT c.ShipmentId FROM " +
                                        "((SELECT ship.ShipmentId FROM Shipment AS ship " +
                                        "left join Booking AS book ON book.BookingId = ship.BookingId  " +
                                        "left join RevenueUnit AS ru ON ru.RevenueUnitId = book.AssignedToAreaId  " +
                                        "left join City AS city ON city.CityId = ru.CityId  " +
                                        "WHERE city.BranchCorpOfficeId = @BranchCorpOfficeId) " +
                                        "UNION " +
                                        "(SELECT SHIP.ShipmentId FROM Shipment SHIP " +
                                        "LEFT JOIN Booking book ON book.BookingId = SHIP.BookingId " +
                                        "LEFT JOIN City c ON c.CityId = book.DestinationCityId  " +
                                        "LEFT JOIN BranchCorpOffice bco ON bco.BranchCorpOfficeId = c.BranchCorpOfficeId  " +
                                        "WHERE bco.BranchCorpOfficeId = @BranchCorpOfficeId)) c)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);
                        
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        
                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;

                    case "PackageNumber":

                        filterColumn = "PackageNumberId";
                        filterClause = "[side].[PackageNumberId] In (SELECT c.PackageNumberId FROM " +
                                        "((SELECT pack.PackageNumberId FROM PackageNumber as pack  " +
                                        "left join   Shipment as ship on ship.ShipmentId = pack.ShipmentId  " +
                                        "left join Booking as book on book.BookingId = ship.BookingId  " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = book.AssignedToAreaId  " +
                                        "left join City as city on city.CityId = ru.CityId  " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId) " +
                                        "UNION  " +
                                        "(SELECT pack.PackageNumberId FROM PackageNumber as pack  " +
                                        "LEFT JOIN Shipment SHIP ON SHIP.ShipmentId = pack.ShipmentId " +
                                        "LEFT JOIN Booking book ON book.BookingId = SHIP.BookingId " +
                                        "LEFT JOIN City c ON c.CityId = book.DestinationCityId  " +
                                        "LEFT JOIN BranchCorpOffice bco ON bco.BranchCorpOfficeId = c.BranchCorpOfficeId  " +
                                        "WHERE bco.BranchCorpOfficeId = @BranchCorpOfficeId)) c)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);
                        
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        
                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;

                    case "PackageDimension":

                        filterColumn = "PackageDimensionId";
                        filterClause = "[side].[PackageDimensionId] In (SELECT  c.PackageDimensionId FROM " +
                                        "((SELECT pack.PackageDimensionId FROM PackageDimension as pack " +
                                        "left join   Shipment as ship on ship.ShipmentId = pack.ShipmentId " +
                                        "left join Booking as book on book.BookingId = ship.BookingId " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = book.AssignedToAreaId " +
                                        "left join City as city on city.CityId = ru.CityId " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId)" +
                                        "UNION " +
                                        "(SELECT pack.PackageDimensionId FROM PackageDimension as pack " +
                                        "LEFT JOIN Shipment SHIP ON SHIP.ShipmentId = pack.ShipmentId " +
                                        "LEFT JOIN Booking book ON book.BookingId = SHIP.BookingId " +
                                        "LEFT JOIN City c ON c.CityId = book.DestinationCityId " +
                                        "LEFT JOIN BranchCorpOffice bco ON bco.BranchCorpOfficeId = c.BranchCorpOfficeId " +
                                        "WHERE bco.BranchCorpOfficeId = @BranchCorpOfficeId)) c)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);
                        
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        
                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;

                    case "StatementOfAccountPayment":

                        filterColumn = "StatementOfAccountId";
                        filterClause = "[side].[StatementOfAccountId] In (Select soaPayment.StatementOfAccountId from StatementOfAccountPayment as soaPayment " +
                                        "left join StatementOfAccount as soa on soa.StatementOfAccountId = soaPayment.StatementOfAccountId " +
                                        "left join Company as company on company.CompanyId = soa.CompanyId " +
                                        "left join City as city on city.CityId = company.CityId " +
                                        "left join BranchCorpOffice as bco on bco.BranchCorpOfficeId = city.BranchCorpOfficeId " +
                                        "where bco.BranchCorpOfficeId = @BranchCorpOfficeId)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);
                        
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        
                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                    case "Payment":

                        filterColumn = "PaymentId";
                        filterClause = "[side].[PaymentId] In (SELECT  c.PaymentId FROM " +
                                        "((SELECT pack.PaymentId FROM Payment as pack " +
                                        "left join   Shipment as ship on ship.ShipmentId = pack.ShipmentId " +
                                        "left join Booking as book on book.BookingId = ship.BookingId " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = book.AssignedToAreaId " +
                                        "left join City as city on city.CityId = ru.CityId " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId)" +
                                        "UNION " +
                                        "(SELECT pack.PaymentId FROM Payment as pack " +
                                        "LEFT JOIN Shipment SHIP ON SHIP.ShipmentId = pack.ShipmentId " +
                                        "LEFT JOIN Booking book ON book.BookingId = SHIP.BookingId " +
                                        "LEFT JOIN City c ON c.CityId = book.DestinationCityId " +
                                        "LEFT JOIN BranchCorpOffice bco ON bco.BranchCorpOfficeId = c.BranchCorpOfficeId " +
                                        "WHERE bco.BranchCorpOfficeId = @BranchCorpOfficeId)) c)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);
                        
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        
                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                    case "PaymenTurnOver":

                        filterColumn = "CollectedById";
                        filterClause = "[side].[CollectedById] In (Select turnOver.CollectedById From PaymentTurnOver as turnOver " +
                                        "left join Employee as emp on emp.EmployeeId = turnOver.CollectedById " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = emp.AssignedToAreaId " +
                                        "left join City as city on city.CityId = ru.CityId " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);

                        
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        

                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                    case "ShipmentAdjustment":

                        filterColumn = "ShipmentId";
                        filterClause = "[side].[ShipmentId] In (Select adjustment.ShipmentId from ShipmentAdjustment as adjustment " +
                                        "left join Shipment as shipment on shipment.ShipmentId = adjustment.ShipmentId " +
                                        "left join Booking as book on book.BookingId = shipment.BookingId " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = book.AssignedToAreaId " +
                                        "left join City as city on city.CityId = ru.CityId " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);

                        
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        

                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                    case "Delivery":

                        filterColumn = "DeliveryId";
                        filterClause = "[side].[DeliveryId] In (SELECT  c.DeliveryId FROM " +
                                        "((SELECT delivery.DeliveryId FROM Delivery as delivery " +
                                        "left join   Shipment as ship on ship.ShipmentId = delivery.ShipmentId " +
                                        "left join Booking as book on book.BookingId = ship.BookingId " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = book.AssignedToAreaId " +
                                        "left join City as city on city.CityId = ru.CityId " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId)" +
                                        "UNION " +
                                        "(SELECT delivery.DeliveryId FROM Delivery as delivery " +
                                        "LEFT JOIN Shipment SHIP ON SHIP.ShipmentId = delivery.ShipmentId " +
                                        "LEFT JOIN Booking book ON book.BookingId = SHIP.BookingId " +
                                        "LEFT JOIN City c ON c.CityId = book.DestinationCityId " +
                                        "LEFT JOIN BranchCorpOffice bco ON bco.BranchCorpOfficeId = c.BranchCorpOfficeId " +
                                        "WHERE bco.BranchCorpOfficeId = @BranchCorpOfficeId)) c)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        

                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                    case "DeliveryPackage":

                        filterColumn = "DeliveredPackageId";
                        filterClause = "[side].[DeliveredPackageId] In (SELECT c.DeliveredPackageId FROM DeliveryPackage package" +
                                        "LEFT JOIN Delivery delivery on delivery.DeliveryId = package.DeliveryId " +
                                        "LEFT JOIN Shipment as ship on ship.ShipmentId = delivery.ShipmentId " +
                                        "left join Booking as book on book.BookingId = ship.BookingId  " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = book.AssignedToAreaId  " +
                                        "left join City as city on city.CityId = ru.CityId  " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId) " +
                                        "UNION  " +
                                        "(SELECT package.DeliveredPackageId from DeliveredPackage as package " +
                                        "LEFT JOIN Delivery delivery on delivery.DeliveryId = package.DeliveryId " +
                                        "LEFT JOIN   Shipment as ship on ship.ShipmentId = delivery.DeliveryId " +
                                        "LEFT JOIN Booking book ON book.BookingId = SHIP.BookingId " +
                                        "LEFT JOIN City c ON c.CityId = book.DestinationCityId  " +
                                        "LEFT JOIN BranchCorpOffice bco ON bco.BranchCorpOfficeId = c.BranchCorpOfficeId  " +
                                        "WHERE bco.BranchCorpOfficeId = @BranchCorpOfficeId)) c)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);
                                               
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }

                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                    case "DeliveryReceipt":

                        filterColumn = "DeliveryReceiptId";
                        filterClause = "[side].[DeliveryReceiptId] In (SELECT  c.DeliveryReceiptId FROM " +
                                        "((SELECT reciept.DeliveryReceiptId from DeliveryReceipt as reciept " +
                                        "left join Delivery delivery on delivery.DeliveryId = reciept.DeliveryId " +
                                        "left join   Shipment as ship on ship.ShipmentId = delivery.ShipmentId  " +
                                        "left join Booking as book on book.BookingId = ship.BookingId  " +
                                        "left join RevenueUnit as ru on ru.RevenueUnitId = book.AssignedToAreaId  " +
                                        "left join City as city on city.CityId = ru.CityId  " +
                                        "where city.BranchCorpOfficeId = @BranchCorpOfficeId) " +
                                        "UNION  " +
                                        "(SELECT reciept.DeliveryReceiptId from DeliveryReceipt as reciept " +
                                        "LEFT JOIN Delivery delivery on delivery.DeliveryId = reciept.DeliveryId " +
                                        "LEFT JOIN   Shipment as ship on ship.ShipmentId = delivery.DeliveryId  " +
                                        "LEFT JOIN Booking book ON book.BookingId = SHIP.BookingId " +
                                        "LEFT JOIN City c ON c.CityId = book.DestinationCityId  " +
                                        "LEFT JOIN BranchCorpOffice bco ON bco.BranchCorpOfficeId = c.BranchCorpOfficeId  " +
                                        "WHERE bco.BranchCorpOfficeId = @BranchCorpOfficeId)) c)";
                        param = new SqlParameter("@BranchCorpOfficeId", SqlDbType.UniqueIdentifier);

                        CreateTemplate(_tableName, filterColumn, filterClause, param);

                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName, param, _branchCorpOfficeId);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }
                        

                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                    default:
                       
                        while (!flag)
                        {
                            flag = ProvisionServer(_tableName);
                        }
                        flag = false;
                        while (!flag)
                        {
                            flag = ProvisionClient(_tableName);
                        }

                        state.table.Status = TableStatus.Provisioned;
                        state.worker.ReportProgress(1, _tableName + " was provisioned.");
                        Log.WriteLogs(_tableName + " was provisioned.");
                        break;
                }
            }
            catch (Exception ex)
            {
                state.table.Status = TableStatus.ErrorProvision;
                state.worker.ReportProgress(1, _tableName + " has provision error.");
                Log.WriteLogs(_tableName + " has provision error.");
            }
            state._event.Set();
        }

        public void PrepareDatabaseForReplication(Object obj)
        {
            SqlParameter param;
            ThreadState state = (ThreadState)obj;
            try
            {
                ProvisionServer(_tableName);
                ProvisionClient(_tableName);

                state._event.Set();
                state.table.Status = TableStatus.Provisioned;
                state.worker.ReportProgress(1, _tableName + " was provisioned.");
                Log.WriteLogs(_tableName + " was provisioned.");
            }
            catch (Exception ex)
            {
                state._event.Set();
                state.table.Status = TableStatus.ErrorProvision;
                state.worker.ReportProgress(1, _tableName + " has provision error.");
                Log.WriteLogs(_tableName + " has provision error.");
            }
        }

        private bool ProvisionServer(string TableName)
        {
            try
            {
                // define a new scope named tableNameScope
                DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(TableName + _filter);
                // get the description of the tableName
                DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(TableName, _serverConnection);

                // add the table description to the sync scope definition
                scopeDesc.Tables.Add(tableDesc);

                // create a server scope provisioning object based on the tableNameScope
                SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(_serverConnection, scopeDesc);
                
                // start the provisioning process
                if (!serverProvision.ScopeExists(scopeDesc.ScopeName))
                {
                    serverProvision.Apply();
                    Console.WriteLine("Server " + TableName + " was provisioned.");
                    Log.WriteLogs("Server " + TableName + " was provisioned.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Server " + TableName + " was already provisioned.");
                    Log.WriteLogs("Server " + TableName + " was already provisioned.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(ex);
            }
            return false;

        }

        private bool ProvisionServer(string TableName, SqlParameter Parameter, string ParamValue)
        {
            try
            {
                // Create a synchronization scope for OriginState=WA.
                SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(_serverConnection);

                // populate the scope description using the template
                serverProvision.PopulateFromTemplate(TableName + _filter, TableName + "_Filter_Template");

                // specify the value we want to pass in the filter parameter, in this case we want only orders from WA
                serverProvision.Tables[TableName].FilterParameters[Parameter.ParameterName].Value = Guid.Parse(ParamValue);

                // Set a friendly description of the template.
                serverProvision.UserComment = TableName + " data includes only " + ParamValue;

                // Create the new filtered scope in the database.
                if (!serverProvision.ScopeExists(serverProvision.ScopeName))
                {
                    serverProvision.Apply();
                    Log.WriteLogs("Server " + TableName + " was provisioned.");
                }
                else
                {
                    Log.WriteLogs("Server " + TableName + " was provisioned.");
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(ex);
                return false;
            }
        }

        public bool ProvisionClient(string TableName)
        {
            try
            {
                DbSyncScopeDescription scopeDescription = SqlSyncDescriptionBuilder.GetDescriptionForScope(TableName + _filter, _serverConnection);

                SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(_localConnection, scopeDescription);

                if (!clientProvision.ScopeExists(scopeDescription.ScopeName))
                {
                    clientProvision.Apply();
                    Log.WriteLogs("Client " + TableName + " was provisioned.");
                }
                else
                {
                    Log.WriteLogs("Client " + TableName + " was already provisioned.");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }   
           
        }

        private void CreateTemplate(string TableName, string filterColumn, string filterClause, SqlParameter param)
        {
            try
            {
                // Create a scope named tableName + _Filter_Template
                scopeDesc = new DbSyncScopeDescription(TableName + "_Filter_Template");

                // Set a friendly description of the template.
                scopeDesc.UserComment = "Filter template for " + TableName + ".";

                // Definition for tables.
                tableDescription = SqlSyncDescriptionBuilder.GetDescriptionForTable(TableName, _serverConnection);
                scopeDesc.Tables.Add(tableDescription);

                // Create a provisioning object for "tableName_Filter_template" that can be used to create a template
                // from which filtered synchronization scopes can be created.
                serverTemplate = new SqlSyncScopeProvisioning(_serverConnection, scopeDesc, SqlSyncScopeProvisioningType.Template);

                AddFilter(TableName, filterColumn, filterClause, param);

                // create a new select changes stored proc for this scope
                serverTemplate.SetCreateProceduresForAdditionalScopeDefault(DbSyncCreationOption.Create);

                // Create the tableName_Filter_template" template in the database.
                if (!serverTemplate.TemplateExists(TableName + "_Filter_Template"))
                {
                    serverTemplate.Apply();
                    Console.WriteLine(TableName + " filter template was created.");
                    Log.WriteLogs(TableName + " filter template was created.");
                }
                else
                {
                    Console.WriteLine(TableName + " filter template was already exist.");
                    Log.WriteLogs(TableName + " filter template was already exist.");
                }
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(ex);
            }

        }

        /// <summary>
        /// Add Filter to use for filtering data,
        /// and the filtering clause to use against the tracking table.
        /// "[side]" is an alias for the tracking table.
        /// An actual filter will be specified when the synchronization scope is created.
        /// </summary>
        /// <param name="TableName"> Name of table subjected to synch.</param>
        /// <param name="FilterColumn"> Table Column included in synch.</param>
        /// <param name="FilterClause"> Filte clause to filter data.</param>
        /// <param name="param">Parameter included in filter clause.</param>
        private void AddFilter(string TableName, string FilterColumn, string FilterClause, SqlParameter param)
        {
            serverTemplate.Tables[TableName].AddFilterColumn(FilterColumn);
            serverTemplate.Tables[TableName].FilterClause = FilterClause;
            serverTemplate.Tables[TableName].FilterParameters.Add(param);
        }


    }

    class Deprovision
    {
        private SqlConnection _connection;
        private ManualResetEvent _currentEvent;
        private string _filter;
        private string _tableName;

        public Deprovision(SqlConnection connection, ManualResetEvent currentEvent, string filter, string tableName)
        {
            this._connection = new SqlConnection(connection.ConnectionString);
            this._currentEvent = currentEvent;
            this._filter = filter;
            this._tableName = tableName;
        }

        public void PerformDeprovisionTable(Object obj)
        {
            ThreadState state = (ThreadState)obj;
            try
            {
                SqlSyncScopeDeprovisioning storeClientDeprovision = new SqlSyncScopeDeprovisioning(_connection);
                storeClientDeprovision.DeprovisionScope(this._tableName + this._filter);                
                state.table.Status = TableStatus.Deprovisioned;
                Log.WriteLogs(_tableName + " was deprovisioned.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Log.WriteErrorLogs(_tableName, ex);
                state.table.Status = TableStatus.ErrorDeprovision;
                Log.WriteLogs(_tableName + " has deprovision error.");
            }
            state.worker.ReportProgress(1, _tableName + " was deprovisioned.");
            state._event.Set();
        }

        public void PerformDeprovisionDatabase(object obj)
        {
            ManualResetEvent _event = (ManualResetEvent)obj;
            try
            {
                SqlSyncScopeDeprovisioning storeClientDeprovision = new SqlSyncScopeDeprovisioning(_connection);
                storeClientDeprovision.DeprovisionStore();

                Log.WriteLogs("Database was Deprovisioned.");
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(ex);
            }
            _event.Set();
        }

        public void PerformDeprovisionTemplate(object obj)
        {
            try
            {
                SqlSyncScopeDeprovisioning templateDeprovision = new SqlSyncScopeDeprovisioning(_connection);
                templateDeprovision.DeprovisionTemplate(_tableName + "Filter");

                Log.WriteLogs("Template was Deprovisioned.");
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(ex);
            }
        }

    }

    static class Log
    {
        private static string _fileName = "\\" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".txt";
        public static void WriteLogs(string Logs)
        {
            string _filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\APCARGO\\Logs\\SyncTransactionLogs";
            System.IO.Directory.CreateDirectory(_filepath);
            System.IO.File.AppendAllText(_filepath + _fileName, Environment.NewLine + DateTime.Now.ToString() + " :: " + Logs);
        }

        public static void WriteErrorLogs(Exception ex)
        {
            string _filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\APCARGO\\Logs\\SyncErrorLogs";
            System.IO.Directory.CreateDirectory(_filepath);
            System.IO.File.AppendAllText(_filepath + _fileName, Environment.NewLine + DateTime.Now.ToString() + " :: " + ex.Message.ToString());
            System.IO.File.AppendAllText(_filepath + _fileName, Environment.NewLine + DateTime.Now.ToString() + " :: " + ex.StackTrace.ToString());
        }

        public static void WriteErrorLogs(string Location, Exception ex)
        {
            string _filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\APCARGO\\Logs\\SyncErrorLogs";
            System.IO.Directory.CreateDirectory(_filepath);
            System.IO.File.AppendAllText(_filepath + _fileName, Environment.NewLine + DateTime.Now.ToString() + " :: " + Location);
            System.IO.File.AppendAllText(_filepath + _fileName, Environment.NewLine + DateTime.Now.ToString() + " :: " + ex.Message.ToString());
            System.IO.File.AppendAllText(_filepath + _fileName, Environment.NewLine + DateTime.Now.ToString() + " :: " + ex.StackTrace.ToString());
        }
    }

    public class ThreadState
    {
        public ManualResetEvent _event = new ManualResetEvent(false);

        public SyncTables table;

        public BackgroundWorker worker;

        public BindingList<Booking> bindingList;

        public int count;
    }
}

