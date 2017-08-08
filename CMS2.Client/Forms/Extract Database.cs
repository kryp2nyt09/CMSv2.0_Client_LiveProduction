
using CMS2.Client.SyncHelper;
using CMS2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using CMS2.Client.Properties;
using System.Configuration;
using System.Threading;
using System.ServiceProcess;
using CMS2.Common;

namespace CMS2_Client
{
    public partial class Extract_Database : Telerik.WinControls.UI.RadForm
    {

        #region Properties
        private bool isSubServer { get; set; }
        private bool isLocalConnected { get; set; }
        private bool isMainConnected { get; set; }
        private string _localServer { get; set; }
        private string _localDbName { get; set; }
        private string _localUsername { get; set; }
        private string _localPassword { get; set; }

        private string _mainServer { get; set; }
        private string _mainDbName { get; set; }
        private string _mainUsername { get; set; }
        private string _mainPassword { get; set; }

        private string _localConnectionString { get; set; }
        private string _mainConnectionString { get; set; }
        private string _filter { get; set; }
        public string _branchCorpOfficeId { get; set; }
        private Synchronization _synchronization;
        private List<BranchCorpOffice> _branchCorpOffices;
        private List<SyncTables> _entities;

        private string fileName = @"C:\Program Files (x86)\APCargo\APCargo\AP CARGO SERVICE.exe.config";
        #endregion

        #region Constructor
        public Extract_Database()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void Extract_Database_Load(object sender, EventArgs e)
        {
            LocalServer.Text = "localhost";
            LocalServer.Enabled = false;

            this.isSubServer = true;
            this.isLocalConnected = false;
            this.isMainConnected = false;
            this.testMainConnection.Visible = false;
            this.testLocalConnection.Visible = false;
            this.dboBranchCoprOffice.Enabled = false;
            this.SetEntities();
            this.radProgressBar1.Value1 = 0;
            this.radProgressBar1.Maximum = _entities.Count + 5;
            this.lblProgressState.Text = "";
        }
        private void LocalTestConnection_Click(object sender, EventArgs e)
        {
            if (IsDataValid_Local())
            {
                GatherInputs();
                if (isSubServer)
                {
                    _localConnectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};Connect Timeout=180;Connection Lifetime=0;Pooling=true;max pool size=1000;multipleactiveresultsets=True;", _localServer, "master", _localUsername, _localPassword);
                }
                else
                {
                    _localConnectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};Connect Timeout=180;Connection Lifetime=0;Pooling=true;max pool size=1000;multipleactiveresultsets=True;", _localServer, _localDbName, _localUsername, _localPassword);

                }

                SqlConnection localConnection = new SqlConnection(_localConnectionString);
                try
                {
                    localConnection.Open();
                    isLocalConnected = true;
                    testLocalConnection.Text = "Success";
                    testLocalConnection.Visible = true;
                    testLocalConnection.ForeColor = Color.Green;
                    if (!isSubServer)
                    {
                        loadbranchCorp(_localConnectionString.Replace("master", ""));
                        dboBranchCoprOffice.Enabled = true;
                    }

                }
                catch (Exception)
                {
                    isLocalConnected = false;
                    testLocalConnection.Text = "Failed";
                    testLocalConnection.Visible = true;
                    testLocalConnection.ForeColor = Color.Red;
                }
                finally
                {
                    localConnection.Dispose();
                }
            }
        }
        private void MainTestConnection_Click(object sender, EventArgs e)
        {
            if (IsDataValid_Main())
            {
                GatherInputs();
                _mainConnectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};Connect Timeout=180;Connection Lifetime=0;Pooling=true;max pool size=1000;multipleactiveresultsets=True;", _mainServer, _mainDbName, _mainUsername, _mainPassword);
                SqlConnection mainConnection = new SqlConnection(_mainConnectionString);
                try
                {
                    mainConnection.Open();
                    isMainConnected = true;
                    testMainConnection.Text = "Success";
                    testMainConnection.Visible = true;
                    testMainConnection.ForeColor = Color.Green;
                    loadbranchCorp(_mainConnectionString);
                    dboBranchCoprOffice.Enabled = true;
                }
                catch (Exception)
                {
                    isMainConnected = false;
                    testMainConnection.Text = "Failed";
                    testMainConnection.Visible = true;
                    testMainConnection.ForeColor = Color.Red;
                }
                finally
                {
                    mainConnection.Dispose();
                }
            }
        }
        private void SubServer_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

            ResetAll();
            ToggleEnableDisableMainServer(true);
            isSubServer = true;

            LocalServer.Text = "localhost";
            LocalServer.Enabled = false;

        }
        private void ClientApp_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {

            ResetAll();
            ToggleEnableDisableMainServer(false);
            isSubServer = false;

            LocalServer.Clear();
            LocalServer.Enabled = true;

        }
        private void Extract_Click(object sender, EventArgs e)
        {
            //Use to clean all synchronization data in selected database
           //StartDeprovisionWholeServer();

            int index = dboBranchCoprOffice.SelectedItem.ToString().IndexOf(" ");
            _filter = dboBranchCoprOffice.SelectedItem.ToString().Substring(0, index);
            _branchCorpOfficeId = dboBranchCoprOffice.SelectedValue.ToString();

            if (isSubServer && isLocalConnected && isMainConnected)
            {
                Extract.Enabled = false;
                Worker.RunWorkerAsync();
            }
            else if (!isSubServer && isLocalConnected)
            {
                Extract.Enabled = false;
                WriteToConfig();
                radProgressBar1.Value1 = radProgressBar1.Maximum;
                radButton1.Enabled = true;
            }
        }
        private void radButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void Extract_Work(object sender, DoWorkEventArgs e)
        {
            DropDatabaseIfExist();
            CreateDatabase();

            //Deprovision Server if theres an update on table
            //StartDeprovisionServer();
            //Provisioning Server and Local for synchronization
            radProgressBar1.Value1 = 0;
            StartProvision();

            //synchronizing databases
            radProgressBar1.Value1 = 0;
            StartSynchronization(Worker);

            WriteToConfig();

            if (File.Exists(fileName))
            {
                SaveSyncServiceSettings(Worker);
                StartService(Worker);
            }


        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            radProgressBar1.Value1 += e.ProgressPercentage;
            lblProgressState.Text = e.UserState.ToString();
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!File.Exists(fileName))
            {
                if (OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = OpenFile.FileName;
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;
                    SaveSyncServiceSettings(worker);
                    StartService(worker);
                }
            }

            lblProgressState.Text = "Completed.";
            radProgressBar1.Value1 = radProgressBar1.Maximum;
            System.Threading.Thread.Sleep(5000);
            radButton1.Enabled = true;
        }
        #endregion

        #region Methods
        private void ToggleEnableDisableMainServer(bool Flag)
        {
            MainServer.Enabled = Flag;
            MainDbName.Enabled = Flag;
            MainUsername.Enabled = Flag;
            MainPassword.Enabled = Flag;
            MainTestConnection.Enabled = Flag;
        }
        private void WriteToConfig()
        {
            Worker.ReportProgress(0, "Saving application settings.");
            var appConfigDoc = new XmlDocument();
            appConfigDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            foreach (XmlElement xElement in appConfigDoc.DocumentElement)
            {
                if (xElement.Name == "connectionStrings")
                {
                    var nodes = xElement.ChildNodes;
                    foreach (XmlElement item in nodes)
                    {
                        if (item.Attributes["name"].Value.Equals("Cms"))
                        {
                            item.Attributes["connectionString"].Value = _localConnectionString.Replace("master", _localDbName);
                        }
                        else if (item.Attributes["name"].Value.Equals("CmsCentral"))
                        {
                            if (isSubServer)
                            {
                                item.Attributes["connectionString"].Value = _mainConnectionString;
                            }
                        }
                    }
                }
            }
            appConfigDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            Settings.Default.LocalDbServer = _localServer;
            Settings.Default.LocalDbName = _localDbName;
            Settings.Default.LocalDbUsername = _localUsername;
            Settings.Default.LocalDbPassword = _localPassword;
            Settings.Default.CentralServerIp = _mainServer;
            Settings.Default.CentralDbName = _mainDbName;
            Settings.Default.CentralUsername = _mainUsername;
            Settings.Default.CentralPassword = _mainPassword;
            Settings.Default.Filter = _filter;
            Settings.Default.DeviceBcoId = _branchCorpOfficeId;
            Settings.Default.Save();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["isSync"].Value = "true";
            config.AppSettings.Settings["Filter"].Value = _filter;
            config.AppSettings.Settings["BcoId"].Value = _branchCorpOfficeId;
            config.AppSettings.Settings["isSubserver"].Value = isSubServer.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            Worker.ReportProgress(0, "Application settings was saved.");
        }
        private void GatherInputs()
        {
            _localServer = LocalServer.Text;
            _localDbName = LocalDbName.Text;
            _localUsername = LocalUsername.Text;
            _localPassword = LocalPassword.Text;

            _mainServer = MainServer.Text;
            _mainDbName = MainDbName.Text;
            _mainUsername = MainUsername.Text;
            _mainPassword = MainPassword.Text;
        }
        private void ResetAll()
        {
            _localServer = "";
            _localDbName = "";
            _localUsername = "";
            _localPassword = "";

            _mainServer = "";
            _mainDbName = "";
            _mainUsername = "";
            _mainPassword = "";

            LocalServer.Text = _localServer;
            LocalDbName.Text = _localDbName;
            LocalUsername.Text = _localUsername;
            LocalPassword.Text = _localPassword;

            MainServer.Text = _mainServer;
            MainDbName.Text = _mainDbName;
            MainUsername.Text = _mainUsername;
            MainPassword.Text = _mainPassword;

            testLocalConnection.Visible = false;
            testMainConnection.Visible = false;

            dboBranchCoprOffice.Enabled = false;
        }
        private void DropDatabaseIfExist()
        {
            using (SqlConnection connection = new SqlConnection(_localConnectionString))
            {
                using (SqlCommand command = new SqlCommand("Select COUNT(*) from master.dbo.sysdatabases where name = '" + _localDbName + "'", connection))
                {
                    try
                    {
                        connection.Open();
                        int count = (Int32)command.ExecuteScalar();

                        if (count == 1)
                        {
                            command.CommandText = "Use master alter database[" + _localDbName + "] set single_user with rollback immediate; DROP DATABASE [" + _localDbName + "]";
                            command.ExecuteNonQuery();
                            Worker.ReportProgress(0, "Database was dropped.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Worker.ReportProgress(0, "Database was unable to drop.");
                    }
                }
            }

        }
        private void CreateDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_localConnectionString))
            {
                using (SqlCommand command = new SqlCommand("Create Database " + _localDbName, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Worker.ReportProgress(0, "Database " + _localDbName + " was created.");
                    }
                    catch (Exception ex)
                    {
                        Worker.ReportProgress(0, "Unable to create database " + _localDbName + ".");
                    }
                }
            }

        }
        private void loadbranchCorp(string conString)
        {
            using (SqlConnection connectionString = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM BranchCorpOffice ORDER BY BranchCorpOfficeName ASC", connectionString);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    dboBranchCoprOffice.ValueMember = "BranchCorpOfficeId";
                    dboBranchCoprOffice.DisplayMember = "BranchCorpOfficeName";
                    dboBranchCoprOffice.DataSource = dt;

                    connectionString.Close();
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Error occured!");
                }
            }
        }
        private bool IsDataValid_Local()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(LocalServer.Text) || string.IsNullOrEmpty(LocalDbName.Text) || string.IsNullOrEmpty(LocalUsername.Text) || string.IsNullOrEmpty(LocalPassword.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Extraction Error", MessageBoxButtons.OK);
                isValid = false;
            }
            else if (LocalServer.Text != "localhost")
            {
                if (isSubServer)
                {
                    MessageBox.Show("Please enter another ip address.", "Data Error", MessageBoxButtons.OK);
                    LocalServer.Focus();
                    isValid = false;
                }
              
            }
            return isValid;
        }
        private bool IsDataValid_Main()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(MainServer.Text) || string.IsNullOrEmpty(MainDbName.Text) || string.IsNullOrEmpty(MainUsername.Text) || string.IsNullOrEmpty(MainPassword.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Data Error", MessageBoxButtons.OK);
                isValid = false;
            }

            return isValid;
        }
        private void SetEntities()
        {
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
                "TruckAreaMapping", "Truck", "Unbundle", "RoleUser", "MenuAccess", "SubMenu"};

            _entities = new List<SyncTables>();
            foreach (string item in list)
            {
                SyncTables table = new SyncTables();
                table.TableName = item;
                _entities.Add(table);
            }

        }
        private void ReplicateDatabase()
        {
            List<CMS2.Client.SyncHelper.ThreadState> listOfThread = new List<CMS2.Client.SyncHelper.ThreadState>();
            foreach (SyncTables table in _entities)
            {
                CMS2.Client.SyncHelper.ThreadState _threadState = new CMS2.Client.SyncHelper.ThreadState();
                _threadState.table = table;
                _threadState.worker = Worker;
                listOfThread.Add(_threadState);

                try
                {
                    Synchronize sync = new Synchronize(table.TableName, _filter, _threadState._event, new SqlConnection(_localConnectionString.Replace("master", _localDbName)), new SqlConnection(_mainConnectionString));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(sync.RepicateDatabase), _threadState);
                    _threadState._event.WaitOne();
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void ProvisionForReplication()
        {
            List<CMS2.Client.SyncHelper.ThreadState> listOfState = new List<CMS2.Client.SyncHelper.ThreadState>();
            SqlConnection localConnection = new SqlConnection(_localConnectionString.Replace("master", _localDbName));
            SqlConnection mainConnection = new SqlConnection(_mainConnectionString);

            foreach (SyncTables table in _entities)
            {
                CMS2.Client.SyncHelper.ThreadState state = new CMS2.Client.SyncHelper.ThreadState();
                state.table = table;
                state.worker = Worker;
                listOfState.Add(state);

                try
                {
                    Provision provision = new Provision(table.TableName, localConnection, mainConnection, state._event, _filter, _branchCorpOfficeId);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(provision.PrepareDatabaseForReplication), state);
                    state._event.WaitOne();

                }
                catch (Exception ex)
                {

                }
            }
        }
        private void StartSynchronization(BackgroundWorker worker)
        {
            List<CMS2.Client.SyncHelper.ThreadState> listOfThread = new List<CMS2.Client.SyncHelper.ThreadState>();
            foreach (SyncTables table in _entities)
            {
                CMS2.Client.SyncHelper.ThreadState _threadState = new CMS2.Client.SyncHelper.ThreadState();
                _threadState.table = table;
                _threadState.worker = worker;
                listOfThread.Add(_threadState);

                try
                {
                    Synchronize sync = new Synchronize(table.TableName, _filter, _threadState._event, new SqlConnection(_localConnectionString.Replace("master", _localDbName)), new SqlConnection(_mainConnectionString));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(sync.PerformSync), _threadState);
                    _threadState._event.WaitOne();
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void StartProvision()
        {
            List<CMS2.Client.SyncHelper.ThreadState> listOfState = new List<CMS2.Client.SyncHelper.ThreadState>();
            SqlConnection localConnection = new SqlConnection(_localConnectionString.Replace("master", _localDbName));
            SqlConnection mainConnection = new SqlConnection(_mainConnectionString);

            foreach (SyncTables table in _entities)
            {
                CMS2.Client.SyncHelper.ThreadState state = new CMS2.Client.SyncHelper.ThreadState();
                state.table = table;
                state.worker = Worker;
                listOfState.Add(state);

                try
                {
                    Provision provision = new Provision(table.TableName, localConnection, mainConnection, state._event, _filter, _branchCorpOfficeId);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(provision.PrepareDatabaseForSynchronization), state);
                    state._event.WaitOne();

                }
                catch (Exception ex)
                {

                }
            }
        }
        private void StartDeprovisionServer()
        {
            List<CMS2.Client.SyncHelper.ThreadState> listOfState = new List<CMS2.Client.SyncHelper.ThreadState>();
            SqlConnection mainConnection = new SqlConnection(_mainConnectionString);

            foreach (SyncTables table in _entities)
            {
                CMS2.Client.SyncHelper.ThreadState state = new CMS2.Client.SyncHelper.ThreadState();
                state.table = table;
                state.worker = Worker;
                listOfState.Add(state);

                try
                {
                    Deprovision deprovision = new Deprovision(mainConnection, state._event, _filter, table.TableName);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(deprovision.PerformDeprovisionTable), state);
                    state._event.WaitOne();
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void StartDeprovisionWholeServer()
        {
            SqlConnection mainConnection = new SqlConnection(_mainConnectionString);
            ManualResetEvent _event = new ManualResetEvent(false);
            Deprovision deprovision = new Deprovision(mainConnection, _event, "", "");
            ThreadPool.QueueUserWorkItem(new WaitCallback(deprovision.PerformDeprovisionDatabase), _event);
            _event.WaitOne();
        }
        private void SaveSyncServiceSettings(BackgroundWorker worker)
        {
            try
            {
                worker.ReportProgress(1, "Saving synchronization service settings.");
                XmlDocument appConfigDoc = new XmlDocument();
                appConfigDoc.Load(fileName);
                foreach (XmlElement xElement in appConfigDoc.DocumentElement)
                {
                    if (xElement.Name == "connectionStrings")
                    {
                        var nodes = xElement.ChildNodes;
                        foreach (XmlElement item in nodes)
                        {
                            if (item.Attributes["name"].Value.Equals("AP_CARGO_SERVICE.Properties.Settings.LocalConnectionString"))
                            {
                                item.Attributes["connectionString"].Value = _localConnectionString.Replace("master", _localDbName);
                            }
                            else if (item.Attributes["name"].Value.Equals("AP_CARGO_SERVICE.Properties.Settings.ServerConnectionString"))
                            {
                                item.Attributes["connectionString"].Value = _mainConnectionString;
                            }
                        }
                    }
                    if (xElement.Name == "applicationSettings")
                    {
                        XmlNodeList nodes = xElement.ChildNodes;

                        foreach (XmlElement item in nodes)
                        {
                            if (item.Name.Equals("AP_CARGO_SERVICE.Properties.Settings"))
                            {
                                XmlNodeList settings = item.ChildNodes;
                                foreach (XmlElement xitem in settings)
                                {
                                    switch (xitem.Attributes["name"].Value)
                                    {
                                        case "BranchCorpOfficeId":
                                            foreach (XmlElement xNode in xitem)
                                            {
                                                xNode.InnerText = _branchCorpOfficeId;
                                            }
                                            break;
                                        case "Filter":
                                            foreach (XmlElement xNode in xitem)
                                            {
                                                xNode.InnerText = _filter;
                                            }
                                            break;
                                        case "Provision":
                                            foreach (XmlElement xNode in xitem)
                                            {
                                                xNode.InnerText = "false";
                                            }
                                            break;
                                        case "DeprovisionServer":
                                            foreach (XmlElement xNode in xitem)
                                            {
                                                xNode.InnerText = "false";
                                            }
                                            break;
                                        case "DeprovisionClient":
                                            foreach (XmlElement xNode in xitem)
                                            {
                                                xNode.InnerText = "false";
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                appConfigDoc.Save(fileName);
                worker.ReportProgress(2, "Synchronization service settings was saved.");
            }
            catch (Exception ex)
            {
                Log.WriteErrorLogs(ex);
            }

        }
        private void StartService(BackgroundWorker worker)
        {
            try
            {
                worker.ReportProgress(0, "Starting synchronization service.");
                System.Threading.Thread.Sleep(2000);
                ServiceController service = new ServiceController("Synchronization Service", Environment.MachineName);
                TimeSpan timeout = TimeSpan.FromMilliseconds(300000);

                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    lblProgressState.Text = "Synchronization Service started.";
                    System.Threading.Thread.Sleep(2000);
                    worker.ReportProgress(1, "Synchronization Service started.");
                    Log.WriteLogs("Synchronization Service started.");
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("Starting Service", "StartService", ex);
            }

        }
        private void StopService()
        {
            try
            {
                ServiceController service = new ServiceController("Synchronization Service", Environment.MachineName);
                TimeSpan timeout = TimeSpan.FromMilliseconds(60000);
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    lblProgressState.Text = "Stopping Synchronization Service...";
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    Log.WriteLogs("Synchronization Service was stop.");
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("Stopping Service", "StopService", ex);
            }

        }
        #endregion
    }
}
