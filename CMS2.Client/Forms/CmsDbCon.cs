using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using CMS2.BusinessLogic;
using CMS2.Client.Properties;
using CMS2.Entities;
using System.Data.SqlClient;
using CMS2.DataAccess;
using CMS2.Client.SyncHelper;
using System.Drawing;
using CMS2.Common;
using System.Threading;
using System.ComponentModel;
using System.ServiceProcess;
using System.IO;

namespace CMS2.Client
{
    public partial class CmsDbCon : Telerik.WinControls.UI.RadForm
    {

        #region Properties

        private XmlNodeList settings;

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
        private string _deviceCode { get; set; }
        public string _deviceRevenueUnitId { get; set; }

        public bool IsNeedDBSetup { get; set; }
        public bool IsFormClose { get; set; }
        public string _isSubserver { get; set; }

        private BranchCorpOfficeBL bcoService;
        private RevenueUnitTypeBL revenutUnitTypeService;
        private RevenueUnitBL revenueUnitService;

        private List<BranchCorpOffice> _branchCorpOffices;
        private List<RevenueUnitType> revenueUnitTypes;
        private List<RevenueUnit> revenueUnits;
        private BindingList<SyncTables> _entities;

        private bool isProvision = true;
        private bool isDeprovisionClient = true;
        private bool IsDeprovisionServer = true;

        private int DoneCount = 0;

        private bool isRenew = false;

        private string fileName = @"C:\Program Files (x86)\APCargo\APCargo\AP CARGO SERVICE.exe.config";

        #endregion

        #region Constructors
        public CmsDbCon()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void CmsDbCon_Load(object sender, EventArgs e)
        {
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            radProgressBar1.Value1 = 0;
            lblProgressState.Text = "";
            btnSaveSync.Enabled = false;

            this.isLocalConnected = false;
            this.isMainConnected = false;
            testMainConnection.Visible = false;
            testLocalConnection.Visible = false;

            bcoService = new BranchCorpOfficeBL(GlobalVars.UnitOfWork);
            revenutUnitTypeService = new RevenueUnitTypeBL(GlobalVars.UnitOfWork);
            revenueUnitService = new RevenueUnitBL(GlobalVars.UnitOfWork);

            _branchCorpOffices = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
            revenueUnitTypes = revenutUnitTypeService.FilterActive().OrderBy(x => x.RevenueUnitTypeName).ToList();
            revenueUnits = revenueUnitService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();

            // Load BranchCorpOffices
            lstBco.DataSource = _branchCorpOffices;
            lstBco.DisplayMember = "BranchCorpOfficeName";
            lstBco.ValueMember = "BranchCorpOfficeId";

            // Load RevenueUnitTypes
            lstRevenueUnitType.DataSource = revenueUnitTypes;
            lstRevenueUnitType.DisplayMember = "RevenueUnitTypeName";
            lstRevenueUnitType.ValueMember = "RevenueUnitTypeId";

            // Load RevenueUnits
            lstRevenueUnit.DataSource = revenueUnits;
            lstRevenueUnit.DisplayMember = "RevenueUnitName";
            lstRevenueUnit.ValueMember = "RevenueUnitId";

            _branchCorpOfficeId = GlobalVars.DeviceBcoId.ToString();
            _filter = ConfigurationManager.AppSettings["Filter"].ToString();
            _localConnectionString = ConfigurationManager.ConnectionStrings["Cms"].ConnectionString;
            _mainConnectionString = ConfigurationManager.ConnectionStrings["CmsCentral"].ConnectionString;
            _isSubserver = ConfigurationManager.AppSettings["isSubserver"].ToString();

            SetChekcBoxes();
            SetEntities();
            gridTables.DataSource = _entities;
            radProgressBar1.Maximum = _entities.Count() + 5;

            if (_isSubserver.ToLower() == "false")
            {
                txtServerIP.Enabled = false;
                txtServerDbName.Enabled = false;
                txtServerUsername.Enabled = false;
                txtServerPassword.Enabled = false;
                isMainConnected = true;
                radPageViewPage2.Enabled = false;
            }



        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isLocalConnected && isMainConnected && GatherInputs())
            {
                // AppUSerSettings
                Settings.Default["LocalDbServer"] = _localServer;
                Settings.Default["LocalDbName"] = _localDbName;
                Settings.Default["LocalDbUsername"] = _localUsername;
                Settings.Default["LocalDbPassword"] = _localPassword;
                Settings.Default["CentralServerIp"] = _mainServer;
                Settings.Default["CentralDbName"] = _mainDbName;
                Settings.Default["CentralUsername"] = _mainUsername;
                Settings.Default["CentralPassword"] = _mainPassword;
                Settings.Default["DeviceCode"] = _deviceCode;
                Settings.Default["DeviceRevenueUnitId"] = _deviceRevenueUnitId;
                Settings.Default["DeviceBcoId"] = _branchCorpOfficeId;
                Settings.Default.Save();

                //app.config-appSettings
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["DeviceCode"].Value = txtDeviceCode.Text;
                config.AppSettings.Settings["RUId"].Value = _deviceRevenueUnitId;
                config.AppSettings.Settings["BcoId"].Value = _branchCorpOfficeId;
                config.Save(ConfigurationSaveMode.Modified);

                XmlDocument appConfigDoc = new XmlDocument();
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
                                item.Attributes["connectionString"].Value = _localConnectionString;
                            }
                            else if (item.Attributes["name"].Value.Equals("CmsCentral"))
                            {
                                item.Attributes["connectionString"].Value = _mainConnectionString;
                            }
                        }
                    }
                }
                appConfigDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                Application.Restart();

            }
        }
        private void CmsDbCon_Shown(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            txtLocalIP.Text = Settings.Default.LocalDbServer;
            txtLocalDbName.Text = Settings.Default.LocalDbName;
            txtLocalDbUsername.Text = Settings.Default.LocalDbUsername;
            txtLocalDbPassword.Text = Settings.Default.LocalDbPassword;
            txtServerIP.Text = Settings.Default.CentralServerIp;
            txtServerDbName.Text = Settings.Default.CentralDbName;
            txtServerUsername.Text = Settings.Default.CentralUsername;
            txtServerPassword.Text = Settings.Default.CentralPassword;
            txtDeviceCode.Text = Settings.Default.DeviceCode;


            try
            {
                lstBco.SelectedValue = _branchCorpOffices.Find(x => x.BranchCorpOfficeId == Guid.Parse(_branchCorpOfficeId)).BranchCorpOfficeId;
                lstRevenueUnit.SelectedValue = revenueUnits.Find(x => x.RevenueUnitId == Guid.Parse(Settings.Default.DeviceRevenueUnitId.ToString())).RevenueUnitId;
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("CMS Settings", "CmsDBConShow", ex);
            }

            //CheckTableState(worker);
            Worker.RunWorkerAsync();
        }
        private void lstBco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBco.SelectedIndex > -1 && lstRevenueUnitType.SelectedIndex > -1)
            {
                List<RevenueUnit> list = revenueUnitService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
                list = list.Where(x => x.City.BranchCorpOffice.BranchCorpOfficeName == lstBco.SelectedItem.ToString() && x.RevenueUnitType.RevenueUnitTypeName == lstRevenueUnitType.SelectedItem.ToString()).ToList();
                PopulateRevenueUnit(list);
            }

        }
        private void PopulateRevenueUnit(List<RevenueUnit> list)
        {
            lstRevenueUnit.Items.Clear();
            lstRevenueUnit.DataSource = list;
            lstRevenueUnit.DisplayMember = "RevenueUnitName";
            lstRevenueUnit.ValueMember = "RevenueUnitId";
        }
        private void CmsDbCon_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsFormClose = true;
        }
        private void lstBco_Validated(object sender, EventArgs e)
        {
            if (lstBco.SelectedIndex > -1)
            {
                _branchCorpOfficeId = lstBco.SelectedValue.ToString();
            }
        }
        private void lstRevenueUnitType_Validated(object sender, EventArgs e)
        {
            if (lstRevenueUnitType.SelectedIndex > -1)
            {
                _deviceRevenueUnitId = lstRevenueUnitType.SelectedValue.ToString();
            }
        }
        private void btnLocalTest_Click(object sender, EventArgs e)
        {
            if (IsDataValid_Local())
            {
                GatherInputs();
                _localConnectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};Connect Timeout=180;Connection Lifetime=0;Pooling=true;", _localServer, _localDbName, _localUsername, _localPassword);

                SqlConnection localConnection = new SqlConnection(_localConnectionString);
                try
                {
                    localConnection.Open();
                    isLocalConnected = true;
                    testLocalConnection.Text = "Success";
                    testLocalConnection.Visible = true;
                    testLocalConnection.ForeColor = Color.Green;

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
        private void btnServerTest_Click(object sender, EventArgs e)
        {
            if (IsDataValid_Main())
            {
                GatherInputs();
                _mainConnectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};;Connect Timeout=180;Pooling=true;", _mainServer, _mainDbName, _mainUsername, _mainPassword);
                SqlConnection mainConnection = new SqlConnection(_mainConnectionString);
                try
                {
                    mainConnection.Open();
                    isMainConnected = true;
                    testMainConnection.Text = "Success";
                    testMainConnection.Visible = true;
                    testMainConnection.ForeColor = Color.Green;
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
        private void lstRevenueUnitType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            if (lstRevenueUnitType.SelectedIndex > -1 && lstBco.SelectedIndex > -1)
            {
                List<RevenueUnit> list = revenueUnitService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
                list = list.Where(x => x.City.BranchCorpOffice.BranchCorpOfficeId == Guid.Parse(lstBco.SelectedValue.ToString()) && x.RevenueUnitType.RevenueUnitTypeName == lstRevenueUnitType.SelectedItem.ToString()).ToList();
                PopulateRevenueUnit(list);
            }


        }
        private void LocalTestConnection_Click(object sender, EventArgs e)
        {
            if (IsDataValid_Local())
            {
                GatherInputs();

                _localConnectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};Connect Timeout=180;Connection Lifetime=0;Pooling=true;", _localServer, _localDbName, _localUsername, _localPassword);


                SqlConnection localConnection = new SqlConnection(_localConnectionString);
                try
                {
                    localConnection.Open();
                    isLocalConnected = true;
                    testLocalConnection.Text = "Success";
                    testLocalConnection.Visible = true;
                    testLocalConnection.ForeColor = Color.Green;

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
                _mainConnectionString = String.Format("Server={0};Database={1};User Id={2};Password={3};", _mainServer, _mainDbName, _mainUsername, _mainPassword);
                SqlConnection mainConnection = new SqlConnection(_mainConnectionString);
                try
                {
                    mainConnection.Open();
                    isMainConnected = true;
                    testMainConnection.Text = "Success";
                    testMainConnection.Visible = true;
                    testMainConnection.ForeColor = Color.Green;
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
        private void chkDeprovisionClient_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                isDeprovisionClient = true;
            }
            else
            {
                isDeprovisionClient = false;
            }
        }
        private void chkDeprovisionServer_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                IsDeprovisionServer = true;
            }
            else
            {
                IsDeprovisionServer = false;
            }

        }
        private void chkProvision_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                isProvision = true;
            }
            else
            {
                isProvision = false;
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnCancel.Enabled = true;
            isRenew = true;
            Worker.RunWorkerAsync();            
        }
        private void Renew_Work(object sender, DoWorkEventArgs e)
        {
            if (radPageView1.SelectedPage == radPageViewPage2)
            {
                if (isRenew)
                {

                    if (isDeprovisionClient)
                    {
                        radProgressBar1.Value1 = 0;
                        StartDeprovisionLocal();
                    }
                    if (IsDeprovisionServer)
                    {
                        radProgressBar1.Value1 = 0;
                        //StartDeprovisionServer();
                    }
                    if (isProvision)
                    {
                        radProgressBar1.Value1 = 0;
                        StartProvision();
                    }
                }

                radProgressBar1.Value1 = 0;
                CheckTableState(Worker);
            }
            else
            {
                radProgressBar1.Value1 = 0;
                CheckTableState(Worker);
            }





        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Worker.IsBusy)
            {
                Worker.CancelAsync();
            }
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            radProgressBar1.Value1 += e.ProgressPercentage;
            lblProgressState.Text = e.UserState.ToString();
        }
        private void Worker_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {            
            radProgressBar1.Value1 = radProgressBar1.Maximum;
            btnStart.Enabled = true;
            btnSaveSync.Enabled = true;
            btnCancel.Enabled = false;
            isRenew = false;
            lblProgressState.Text = "Completed.";
        }
        private void gridTables_CellValueChanged(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            IEditableObject editableObject = e.Row.DataBoundItem as IEditableObject;
            if (editableObject != null)
            {
                editableObject.EndEdit();
            }

            SyncTables table = e.Row.DataBoundItem as SyncTables;
            if (table != null)
            {
                _entities.Remove(table);
                _entities.Add(table);
            }
        }
        private void btnSaveSync_Click(object sender, EventArgs e)
        {

            if (File.Exists(fileName))
            {
                SaveSyncServiceSettings();

                StopService();
                StartService();
            }
            else
            {
                if (OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = OpenFile.FileName;
                    SaveSyncServiceSettings();

                    StopService();
                    StartService();
                }
            }

        }
        #endregion

        #region Methods
        private void SetEntities()
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

                _entities = new BindingList<SyncTables>();
                foreach (var item in list)
                {
                    SyncTables table = new SyncTables();
                    table.TableName = item;
                    _entities.Add(table);
                }
            }
        }
        private bool IsDataValid_Local()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtLocalIP.Text) || string.IsNullOrEmpty(txtLocalDbName.Text) || string.IsNullOrEmpty(txtLocalDbUsername.Text) || string.IsNullOrEmpty(txtLocalDbPassword.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Data Error.", MessageBoxButtons.OK);
                isValid = false;
            }

            return isValid;
        }
        private bool IsDataValid_Main()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(txtServerIP.Text) || string.IsNullOrEmpty(txtServerDbName.Text) || string.IsNullOrEmpty(txtServerUsername.Text) || string.IsNullOrEmpty(txtServerPassword.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Data Error", MessageBoxButtons.OK);
                isValid = false;
            }
            return isValid;
        }
        private bool GatherInputs()
        {
            _localServer = txtLocalIP.Text;
            _localDbName = txtLocalDbName.Text;
            _localUsername = txtLocalDbUsername.Text;
            _localPassword = txtLocalDbPassword.Text;

            _mainServer = txtServerIP.Text;
            _mainDbName = txtServerDbName.Text;
            _mainUsername = txtServerUsername.Text;
            _mainPassword = txtServerPassword.Text;

            _deviceCode = txtDeviceCode.Text;

            try
            {
                if (lstBco.SelectedValue != null)
                {
                    _branchCorpOfficeId = lstBco.SelectedValue.ToString();
                }
                else
                {
                    lstBco.Focus();
                    return false;
                }

                if (lstRevenueUnit.SelectedValue != null)
                {
                    _deviceRevenueUnitId = lstRevenueUnit.SelectedValue.ToString();
                }
                else
                {
                    lstRevenueUnit.Focus();
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        private void CheckTableState(BackgroundWorker worker)
        {
            List<SyncHelper.ThreadState> listOfThread = new List<SyncHelper.ThreadState>();
            foreach (SyncTables table in _entities)
            {
                SyncHelper.ThreadState _threadState = new SyncHelper.ThreadState();
                _threadState.table = table;
                _threadState.worker = worker;

                try
                {
                    Synchronize sync = new Synchronize(table.TableName, _filter, _threadState._event, new SqlConnection(_localConnectionString), new SqlConnection(_mainConnectionString));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(sync.PerformSync), _threadState);
                    _threadState._event.WaitOne();
                }
                catch (Exception ex)
                {
                    Logs.ErrorLogs("CheckTableState", "ChecktableState", ex);
                }
            }
            //while (DoneCount != _entities.Count - 5)
            //{

            //}
        }
        private void StartProvision()
        {
            List<SyncHelper.ThreadState> listOfState = new List<SyncHelper.ThreadState>();
            SqlConnection localConnection = new SqlConnection(_localConnectionString);
            SqlConnection mainConnection = new SqlConnection(_mainConnectionString);
            foreach (SyncTables table in _entities)
            {
                SyncHelper.ThreadState state = new SyncHelper.ThreadState();
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
        private void StartDeprovisionClient()
        {
            List<SyncHelper.ThreadState> listOfState = new List<SyncHelper.ThreadState>();
            List<ManualResetEvent> deprovisionEvents = new List<ManualResetEvent>();
            List<ManualResetEvent> deprovisionEvents1 = new List<ManualResetEvent>();
            SqlConnection localConnection = new SqlConnection(_localConnectionString);

            foreach (SyncTables table in _entities)
            {
                SyncHelper.ThreadState state = new SyncHelper.ThreadState();
                state.table = table;
                state.worker = Worker;
                listOfState.Add(state);

                try
                {
                    Deprovision deprovision = new Deprovision(localConnection, state._event, _filter, table.TableName);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(deprovision.PerformDeprovisionTable), state);
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void StartDeprovisionLocal()
        {
            SqlConnection localConnection = new SqlConnection(_localConnectionString);
            ManualResetEvent _event = new ManualResetEvent(false);
            Deprovision deprovision = new Deprovision(localConnection, _event, "", "");
            ThreadPool.QueueUserWorkItem(new WaitCallback(deprovision.PerformDeprovisionDatabase), _event);
            _event.WaitOne();
        }
        private void StartDeprovisionWholeServer()
        {
            SqlConnection mainConnection = new SqlConnection(_mainConnectionString);
            ManualResetEvent _event = new ManualResetEvent(false);
            Deprovision deprovision = new Deprovision(mainConnection, _event, "", "");
            ThreadPool.QueueUserWorkItem(new WaitCallback(deprovision.PerformDeprovisionDatabase), _event);
            _event.WaitOne();
        }
        private void StartDeprovisionServer()
        {
            List<SyncHelper.ThreadState> listOfState = new List<SyncHelper.ThreadState>();
            SqlConnection mainConnection = new SqlConnection(_mainConnectionString);

            foreach (SyncTables table in _entities)
            {
                SyncHelper.ThreadState state = new SyncHelper.ThreadState();
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
        private void SetChekcBoxes()
        {
            this.chkDeprovisionClient.Checked = isDeprovisionClient;
            this.chkDeprovisionServer.Checked = IsDeprovisionServer;
            this.chkProvision.Checked = isProvision;
        }
        private void SaveSyncServiceSettings()
        {
            try
            {
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
                                item.Attributes["connectionString"].Value = _localConnectionString;
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
                                                xNode.InnerText = isProvision.ToString();
                                            }
                                            break;
                                        case "DeprovisionServer":
                                            foreach (XmlElement xNode in xitem)
                                            {
                                                xNode.InnerText = IsDeprovisionServer.ToString();
                                            }
                                            break;
                                        case "DeprovisionClient":
                                            foreach (XmlElement xNode in xitem)
                                            {
                                                xNode.InnerText = isDeprovisionClient.ToString();
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
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("Saving Sync Settings configuration", "SaveSyncSettings", ex);
            }

        }
        private void StartService()
        {
            try
            {
                ServiceController service = new ServiceController("Synchronization Service", Environment.MachineName);
                TimeSpan timeout = TimeSpan.FromMilliseconds(300000);

                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                    lblProgressState.Text = "Starting Synchronization Service...";
                    service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                    lblProgressState.Text = "Synchronization Service started.";
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

        private void lstRevenueUnit_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (lstRevenueUnit.SelectedValue != null)
            {
                _deviceRevenueUnitId = lstRevenueUnit.SelectedValue.ToString();
            }
        }

        private void lstBco_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void lstRevenueUnit_Validated(object sender, EventArgs e)
        {
            if (lstRevenueUnit.SelectedIndex > -1)
            {
                _deviceRevenueUnitId = lstRevenueUnit.SelectedValue.ToString();
            }
        }
    }
}
