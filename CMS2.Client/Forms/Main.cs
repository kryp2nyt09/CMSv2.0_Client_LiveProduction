
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CMS2.BusinessLogic;
using CMS2.Common.Constants;
using CMS2.Common.Enums;
using CMS2.Entities;
using CMS2.Entities.Models;
using System.Net.Mail;
using System.Data;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Microsoft.AspNet.Identity;
using CMS2.Common.Identity;
using CMS2.Client.ViewModels;
using CMS2.DataAccess;
using System.Security.Principal;
using CMS2.Common;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CMS2.Client.Reports;
using System.Drawing.Printing;
using CMS2.Client.SyncHelper;
using System.Threading.Tasks;
using System.Threading;
using Tools = CMS2.Common.Utilities;
using CMS2.Client.Forms;
using System.IO;
using Telerik.WinControls.Data;
using CMS2.Client.Forms.TrackingReportsView;
using System.Drawing;
using System.Drawing.Drawing2D;
using CMS2.Entities.ReportModel;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using CMS2.Client.Reports;
//using CMS2.Client.ReportModels;

namespace CMS2.Client
{
    public partial class Main : Telerik.WinControls.UI.RadForm
    {

        #region Properties

        #region Main


        private UserManager<IdentityUser, Guid> _userManager;
        private AreaBL areaService;
        private BranchSatOfficeBL bsoService;
        private GatewaySatOfficeBL gatewaySatService;
        private BranchCorpOfficeBL bcoService;
        private RevenueUnitBL revenueUnitService;
        private UserStore userService;
        private string LoaderUser = "Booking";
        //private Resizer rs = new Resizer();
        #endregion

        #region Booking

        Booking booking;
        Entities.Client shipper;
        Entities.Client consignee;
        private AutoCompleteStringCollection shipperLastNames;
        private AutoCompleteStringCollection shipperFirstNames;
        private AutoCompleteStringCollection shipperCompany;
        private AutoCompleteStringCollection clientConsigneeLastNames;
        private AutoCompleteStringCollection clientConsigneeFirstNames;
        private AutoCompleteStringCollection consigneeCompany;
        private AutoCompleteStringCollection shipperBco;
        private AutoCompleteStringCollection shipperCity;
        private AutoCompleteStringCollection consgineeBco;
        private AutoCompleteStringCollection consgineeCity;
        private AutoCompleteStringCollection assignedTo;
        private BindingSource bsBookingStatus;
        private BindingSource bsBookingRemark;
        private BindingSource bsAreas;
        private BindingSource bsOriginBco;
        private BindingSource bsDestinationBco;

        private BookingStatusBL bookingStatusService;
        private BookingRemarkBL bookingRemarkService;
        private BookingBL bookingService;
        private ClientBL clientService;
        private CityBL cityService;
        private CompanyBL companyService;

        private List<BookingStatus> bookingStatus;
        private List<BookingRemark> bookingRemarks;
        private List<BranchCorpOffice> branchCorpOffices;
        private List<RevenueUnit> areas;
        private List<Entities.Client> clients;
        private List<RevenueUnit> revenueUnits;
        private List<City> cities;
        private List<Company> companies;

        private BindingList<Booking> _bookingBindingList;
        private BindingList<Booking> _manifestBindingList;

        private bool isBookingPage = false;

        #endregion

        #region Tracking
        private BindingSource bsBCO1;
        private BindingSource bsDriver;
        private BindingSource bsBatch;
        private BindingSource bsEmployee;
        private BindingSource bsStatus;
        private BindingSource bsGTCommodityType;
        private BindingSource bstrackBARevenueUnitName;
        private BindingSource bsBundleBSO;
        private BindingSource bsUnbundleBCO;
        private BindingSource bsGTDestinationBCO;
        private BindingSource bsGTBatch;
        private BindingSource bsGODestinationBCO;
        private BindingSource bsGOBatch;
        private BindingSource bsGOCommodityType;
        private BindingSource bsGICommodityType;
        private BindingSource bsCTBatch;
        private BindingSource bsSGBCO;
        private BindingSource bsSGBatch;
        private BindingSource bsAirlines;
        private BindingSource bsFlightNumber;

        private BranchAcceptanceBL branchAcceptanceService;
        private BatchBL batchService;
        private DeliveryStatusBL deliveryStatusService;
        private ReasonBL reasonService;
        private GatewayInboundBL gatewayInboundService;
        private GatewayTransmittalBL gatewayTransmittalService;
        private GatewayOutboundBL gatewayOutboundService;
        private CargoTransferBL cargotransferService;
        private SegregationBL segregationService;
        private StatusBL statusService;
        private AirlinesBL airlineService;
        private PackageNumberBL packageNumberService;
        private FlightInfoBL flightnumberService;
        private BundleBL bundleService;
        private UnbundleBL unbundleService;
        private DistributionBL distributionService;
        private HoldCargoBL holdCargoService;
        private DeliveryBL deliveryService;
        private DeliveryReceiptBL deliveryReceiptService;
        private DeliveredPackageBL deliveredPackageService;

        private List<RevenueUnitType> revenueUnitTypes;
        private List<Airlines> airlines;
        private List<Batch> batches;
        private List<Status> statuses;
        private List<Reason> reasons;
        private List<FlightInformation> flightnumbers;
        private List<DeliveryStatus> deliveryStatuses;

        #endregion

        #region Acceptance

        private ShipmentModel shipment;
        private PackageDimensionModel packageDimensionModel;
        private BindingSource bsCommodityType;
        private BindingSource bsCommodity;
        private BindingSource bsServiceType;
        private BindingSource bsServiceMode;
        private BindingSource bsPaymentMode;
        private BindingSource bsCrating;
        private BindingSource bsPackaging;
        private BindingSource bsGoodsDescription;
        private BindingSource bsShipMode;
        private BindingSource bsTranshipmentLeg;
        private BindingSource bsRevenueUnitType;
        private BindingSource bsRevenueUnits;
        private AutoCompleteStringCollection commodityTypeCollection;
        private AutoCompleteStringCollection commodityCollection;
        private AutoCompleteStringCollection serviceTypeCollection;
        private AutoCompleteStringCollection serviceModeCollection;
        private AutoCompleteStringCollection shipModeCollection;
        private AutoCompleteStringCollection goodsDescCollection;
        private AutoCompleteStringCollection paymentModeCollection;
        private AutoCompleteStringCollection AirwayBill;

        private ApplicableRateBL applicableRateService;
        private CommodityTypeBL commodityTypeService;
        private CommodityBL commodityService;
        private ServiceTypeBL serviceTypeService;
        private ServiceModeBL serviceModeService;
        private PaymentModeBL paymentModeService;
        private ShipmentBL shipmentService;
        private ShipmentBasicFeeBL shipmentBasicFeeService;
        private CratingBL cratingService;
        private PackagingBL packagingService;
        private GoodsDescriptionBL goodsDescriptionService;
        private ShipModeBL shipModeService;
        private RateMatrixBL rateMatrixService;
        private PaymentTermBL paymentTermService;
        private TransShipmentLegBL transShipmentLegService;

        private CommodityType commodityType;
        private List<CommodityType> commodityTypes;
        private List<Commodity> commodities;
        private List<ServiceType> serviceTypes;
        private List<ServiceMode> serviceModes;
        private List<PaymentMode> paymentModes;
        private List<ShipmentBasicFee> shipmentBasicFees;
        private List<Crating> cratings;
        private List<Packaging> packagings;
        private List<GoodsDescription> goodsDescriptions;
        private List<ShipMode> shipModes;
        private List<TransShipmentLeg> transShipmentLegs;
        private List<PaymentTerm> paymentTerms;


        public string LogPath;

        public bool isNewShipment { get; set; }

        public ShipmentModel shipmentModel { get; set; }

        #endregion

        #region Payment

        private StatementOfAccountBL soaService;
        private PaymentBL paymentService;
        private PaymentTypeBL paymentTypeService;

        private StatementOfAccountPayment soaPayment;
        private Payment payment;
        private StatementOfAccount soa;
        private BindingSource bsPaymentType;
        public PaymentDetailsViewModel NewPayment;

        #endregion

        #region PaymentSummary
        private AutoCompleteStringCollection autoComprevenueUnitName;
        private AutoCompleteStringCollection autoComp_empName;
        private AutoCompleteStringCollection autoComp_revenueUnitType;

        Entities.PaymentSummary paymentSummary;
        PaymentSummaryStatus paymentSummaryStatus;

        private List<Employee> employees;
        private List<Payment> paymentPrepaid;
        private List<Payment> paymentFreightCollect;
        private List<Payment> paymentCorpAcctConsignee;
        private List<RevenueUnit> paymentSummary_revenueUnits;
        private List<Employee> paymentSummary_employee;
        private List<Employee> paymentSummary_remittedBy;
        private List<RevenueUnitType> paymentSummary_revenueUnitType;

        public List<PaymentSummaryModel> listPaymentSummary = new List<PaymentSummaryModel>();
        public List<PaymentSummaryDetails> listpaymentSummaryDetails = new List<PaymentSummaryDetails>();
        public List<PaymentSummary_MainDetailsModel> listMainDetails = new List<PaymentSummary_MainDetailsModel>();
        public List<PaymentSummaryModel> passListofPaymentSummary = new List<PaymentSummaryModel>();

        private EmployeeBL employeeService;
        private RevenueUnitTypeBL revenueUnitTypeService;
        private RevenueUnitBL revenueUnitservice;
        private PaymentSummaryStatusBL paymentSummaryStatusService;
        private PaymentSummaryBL paymentSummaryService;

        public int ctrPrepaid = 0;
        public int ctrfreight = 0;
        public int ctrcorpAcct = 0;
        //public decimal totalCashReceived = 0;
        //public decimal totalCheckReceived = 0;
        //public decimal totalAmountReceived = 0;
        //public decimal difference = 0;
        public decimal totalCollection = 0;
        private Point? _Previous = null;
        Image signatureImage;



        #endregion

        #endregion

        #region Constructors
        public Main()
        {
            LoadInit();
            AcceptanceLoadInit();
            PaymentInit();
            PaymentSummaryLoadInit();
            TrackingLoadInit();

            InitializeComponent();
            int style = NativeWinAPI.GetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE);
            style |= NativeWinAPI.WS_EX_COMPOSITED;
            NativeWinAPI.SetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE, style);

        }

        #endregion

        #region Events

        #region Main
        private void Main_Shown(object sender, EventArgs e)
        {
            Login();


            try
            {
                if (AppUser.User.UserName == "admin")
                {
                    btnSettings.Enabled = true;
                }
                else
                {
                    btnSettings.Enabled = false;
                }
            }
            catch (Exception)
            {

            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            //rs.FindAllControls(this);
            timer1.Start();

            LoadBookingComponents();
            BookingResetAll();
            PopulateGrid();
            AddDailyBooking();

        }
        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {

            switch (pageViewMain.SelectedPage.Text)
            {
                case "Booking":

                    BookingResetAll();
                    PopulateGrid();

                    break;
                case "Acceptance":
                    if (isNewShipment)
                    {
                        AcceptanceResetAll();
                        ClearSummaryData();
                        AcceptanceLoadData();
                    }
                    else
                    {
                        DisableForm();
                    }

                    break;
                case "Payment":

                    soaPayment = null;
                    payment = null;
                    soa = null;

                    datePaymentDate.Value = DateTime.Now;
                    dateCheckDate.Value = DateTime.Now;

                    bsPaymentType.DataSource = paymentTypeService.FilterActive().OrderBy(x => x.PaymentTypeName).ToList();
                    lstPaymentType.DataSource = bsPaymentType;
                    lstPaymentType.DisplayMember = "PaymentTypeName";
                    lstPaymentType.ValueMember = "PaymentTypeId";

                    if (NewPayment != null)
                    {
                        txtAwb.Text = NewPayment.AwbSoa;
                        txtAmountDue.Text = NewPayment.AmountPaidString;
                        txtAmountPaid.Focus();
                    }

                    break;
                case "Manifest":

                    Manifest_Get_Data();


                    break;
                case "Payment Summary":
                    dateCollectionDate.Value = DateTime.Now;

                    //PaymentSummaryLoadData();
                    //PopulateGrid_Prepaid();
                    //PopulateGrid_FreightCollect();
                    //PopulateGrid_CorpAcctConsignee();
                    //gridPrepaid.Columns["Validate"].PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Right;
                    //gridPrepaid.Columns["Client"].PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;

                    //gridFreightCollect.Columns["Validate"].PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Right;
                    //gridFreightCollect.Columns["Client"].PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
                    break;
                case "Tracking":
                    dateTimePicker_PickupCargo.Value = DateTime.Now;

                    break;
                default:
                    break;
            }
        }
        private void Main_Resize(object sender, EventArgs e)
        {
            //rs.ResizeAllControls(this);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // string dataSource = ConfigurationManager.ConnectionStrings["Cms"].ConnectionString;
            // Retrieve the ConnectionString from App.config 
            //string connectString = ConfigurationManager.ConnectionStrings["Cms"].ToString();
            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectString);


            panel1.BackgroundImage = Properties.Resources.online;
            lbl_Status.Text = "Online";
            lbl_Status.ForeColor = Color.Green;

            // Retrieve the DataSource property.    
            //string IPAddress = builder.DataSource;
            //if (IPAddress == "localhost")
            //{
            //    panel1.BackgroundImage = Properties.Resources.offline;
            //    lbl_Status.Text = "Offline";
            //    lbl_Status.ForeColor = Color.Red;
            //    return;
            //}
            //string ip = IPAddress.Substring(0, IPAddress.Length - (IPAddress.Length - (IPAddress.IndexOf(","))));
            //int port = Convert.ToInt32(IPAddress.Substring(IPAddress.IndexOf(",") + 1, IPAddress.Length - (IPAddress.IndexOf(",") + 1)));
            //if (port.ToString() == "")
            //{
            //    port = 1433;
            //}
            //using (TcpClient tcpClient = new TcpClient())
            //{
            //    try
            //    {
            //        tcpClient.Connect(ip, port);
            //        //panel1
            //        panel1.BackgroundImage = Properties.Resources.online;
            //        lbl_Status.Text = "Online";
            //        lbl_Status.ForeColor = Color.Green;
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine("Port closed");
            //        panel1.BackgroundImage = Properties.Resources.offline;
            //        lbl_Status.Text = "Offline";
            //        lbl_Status.ForeColor = Color.Red;

            //    }
            //}

            //System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("Sychronization Service");

            //switch (sc.Status)
            //{
            //    case System.ServiceProcess.ServiceControllerStatus.Running:
            //        lblService.Text = "Sync service is running";
            //        break;
            //    case System.ServiceProcess.ServiceControllerStatus.Stopped:
            //        lblService.Text = "Sync service has stopped";
            //        break;
            //    default:
            //        lblService.Text = "Sync service has stopped";
            //        break;
            //}


        }
        #endregion

        #region Booking

        private void lstOriginCity_Enter(object sender, EventArgs e)
        {
            if (lstOriginBco.SelectedIndex >= 0)
            {
                var bcoId = Guid.Parse(lstOriginBco.SelectedValue.ToString());
                List<string> _cities = cities.Where(x => x.BranchCorpOfficeId == bcoId).Select(x => x.CityName).ToList();
                shipperCity = new AutoCompleteStringCollection();
                foreach (var item in _cities)
                {
                    shipperCity.Add(item);
                }
                lstOriginCity.AutoCompleteDataSource = shipperCity;
            }
        }
        private void lstOriginCity_Validated(object sender, EventArgs e)
        {
            txtShipperContactNo.Focus();
        }
        private void lstDestinationBco_Validated(object sender, EventArgs e)
        {
            if (lstDestinationBco.SelectedIndex >= 0)
            {
                var bcoId = Guid.Parse(lstDestinationBco.SelectedValue.ToString());
                SelectedDestinationCity(bcoId);

                lstDestinationCity.Refresh();
                lstDestinationCity.SelectedIndex = -1;
                lstDestinationCity.Focus();
            }
        }
        private void lstDestinationCity_Enter(object sender, EventArgs e)
        {
            if (lstDestinationBco.SelectedIndex > 0)
            {
                var bcoId = Guid.Parse(lstDestinationBco.SelectedValue.ToString());
                List<string> _cities = cities.Where(x => x.BranchCorpOfficeId == bcoId).Select(x => x.CityName).ToList(); consgineeCity = new AutoCompleteStringCollection();
                foreach (var item in _cities)
                {
                    consgineeCity.Add(item);
                }
                lstDestinationCity.AutoCompleteDataSource = consgineeCity;
            }
        }
        private void lstAssignedTo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstBookingStatus.Focus();
            }
        }
        private void lstAssignedTo_Enter(object sender, EventArgs e)
        {
            assignedTo = new AutoCompleteStringCollection();

            // change to revenue unit under bco only, where bco is in the app settings
            foreach (var item in areas.OrderBy(x => x.RevenueUnitName).Select(x => x.RevenueUnitName).ToList())
            {
                assignedTo.Add(item);
            }
            lstAssignedTo.AutoCompleteDataSource = assignedTo;
        }
        private void txtShipperAddress2_Enter(object sender, EventArgs e)
        {
            txtShipperAddress2.SelectAll();
        }
        private void txtShipperStreet_Enter(object sender, EventArgs e)
        {
            txtShipperStreet.SelectAll();
        }
        private void txtShipperBarangay_Enter(object sender, EventArgs e)
        {
            txtShipperBarangay.SelectAll();
        }
        private void txtShipperContactNo_Enter(object sender, EventArgs e)
        {
            txtShipperContactNo.SelectAll();
            //txtShipperContactNo.SelectionStart = 0;
            //txtShipperContactNo.SelectionLength = txtShipperContactNo.Mask.Length;
        }
        private void txtShipperMobile_Enter(object sender, EventArgs e)
        {
            txtShipperMobile.SelectAll();
        }
        private void txtShipperEmail_Enter(object sender, EventArgs e)
        {
            txtShipperEmail.SelectAll();
        }
        private void txtConsigneeAddress2_Enter(object sender, EventArgs e)
        {
            txtConsigneeAddress2.SelectAll();
        }
        private void txtConsgineeStreet_Enter(object sender, EventArgs e)
        {
            txtConsgineeStreet.SelectAll();
        }
        private void txtConsigneeBarangay_Enter(object sender, EventArgs e)
        {
            txtConsigneeBarangay.SelectAll();
        }
        private void txtConsigneeContactNo_Enter(object sender, EventArgs e)
        {
            txtConsigneeContactNo.SelectAll();
        }
        private void txtConsigneeMobile_Enter(object sender, EventArgs e)
        {
            txtConsigneeMobile.SelectAll();
        }
        private void txtConsigneeEmail_Enter(object sender, EventArgs e)
        {
            txtConsigneeEmail.SelectAll();
        }
        private void BookingGridView_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                Guid rowId = Guid.Parse(BookingGridView.Rows[e.RowIndex].Cells["BookingId"].Value.ToString());
                BookingGridView.Rows[e.RowIndex].IsSelected = true;
                BookingSelected(rowId);
                NewShipment();
            }
            catch (Exception ex)
            {
                return;
            }

        }
        private void BookingGridView_CellClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                Guid rowId = Guid.Parse(BookingGridView.Rows[e.RowIndex].Cells["BookingId"].Value.ToString());
                BookingGridView.Rows[e.RowIndex].IsSelected = true;
                BookingSelected(rowId);
            }
            catch (Exception)
            {
                return;
            }

        }
        private void lstOriginBco_Enter(object sender, EventArgs e)
        {
            shipperBco = new AutoCompleteStringCollection();
            var bcos = branchCorpOffices.OrderBy(x => x.BranchCorpOfficeName).Select(x => x.BranchCorpOfficeName).ToList();
            foreach (var item in bcos)
            {
                shipperBco.Add(item);
            }
            lstOriginBco.AutoCompleteDataSource = shipperBco;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveBooking();
        }
        private void btnAcceptance_Click(object sender, EventArgs e)
        {
            btnAcceptance.Enabled = false;
            btnSave.Enabled = false;
            NewShipment();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            BookingResetAll();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewBooking();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteBooking();
        }
        private void txtShippperLastName_Enter(object sender, EventArgs e)
        {
            shipperLastNames = new AutoCompleteStringCollection();
            var lastnames = clients.OrderBy(x => x.LastName).Select(x => x.LastName).ToList();
            foreach (var item in lastnames)
            {
                shipperLastNames.Add(item);
            }
            txtShipperLastName.AutoCompleteCustomSource = shipperLastNames;
        }
        private void txtShipperFirstName_Enter(object sender, EventArgs e)
        {
            shipperFirstNames = new AutoCompleteStringCollection();
            var firstnames = clients.Where(x => x.LastName.Equals(txtShipperLastName.Text.Trim())).OrderBy(x => x.FirstName).Select(x => x.FirstName).ToList();
            foreach (var item in firstnames)
            {
                shipperFirstNames.Add(item);
            }
            txtShipperFirstName.AutoCompleteCustomSource = shipperFirstNames;
        }
        private void txtShipperFirstName_Leave(object sender, EventArgs e)
        {
            CreateShipper();
        }
        private void txtShipperCompany_Enter(object sender, EventArgs e)
        {
            shipperCompany = new AutoCompleteStringCollection();
            if (shipper != null)
            {
                List<Entities.Client> _clients = clients.Where(x => x.LastName.Equals(shipper.LastName) && x.FirstName.Equals(shipper.FirstName)).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                if (_clients.Count > 0)
                {
                    foreach (Entities.Client item in _clients)
                    {
                        if (item.CompanyId != null)
                            shipperCompany.Add(item.Company.CompanyName + " - " + item.AccountNo);
                    }
                }
                else
                {
                    var _companies = companyService.FilterActive().OrderBy(x => x.CompanyName).ToList();
                    foreach (var item in _companies)
                    {
                        if (!string.IsNullOrEmpty(item.CompanyName))
                            shipperCompany.Add(item.CompanyName + " - " + item.AccountNo);
                    }
                }

                txtShipperCompany.AutoCompleteCustomSource = shipperCompany;
            }

        }
        private void txtConsigneeLastName_Enter(object sender, EventArgs e)
        {
            clientConsigneeLastNames = new AutoCompleteStringCollection();
            var lastnames = clients.OrderBy(x => x.LastName).Select(x => x.LastName).ToList();
            foreach (var item in lastnames)
            {
                clientConsigneeLastNames.Add(item);
            }
            txtConsigneeLastName.AutoCompleteCustomSource = clientConsigneeLastNames;
        }
        private void txtConsigneeFirstName_Enter(object sender, EventArgs e)
        {
            clientConsigneeFirstNames = new AutoCompleteStringCollection();
            var firstnames = clients.Where(x => x.LastName.Equals(txtConsigneeLastName.Text.Trim())).OrderBy(x => x.FirstName).Select(x => x.FirstName).ToList();
            foreach (var item in firstnames)
            {
                clientConsigneeFirstNames.Add(item);
            }
            txtConsigneeFirstName.AutoCompleteCustomSource = clientConsigneeFirstNames;
        }
        private void txtConsigneeFirstName_Leave(object sender, EventArgs e)
        {
            CreateConsignee();
        }
        private void txtConsigneeCompany_Enter(object sender, EventArgs e)
        {
            consigneeCompany = new AutoCompleteStringCollection();
            if (consignee != null)
            {
                List<Entities.Client> _clients = clients.Where(x => x.LastName.Equals(consignee.LastName) && x.FirstName.Equals(consignee.FirstName)).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
                if (_clients.Count > 0)
                {
                    foreach (Entities.Client item in _clients)
                    {
                        if (item.CompanyId != null)
                            consigneeCompany.Add(item.Company.CompanyName + " - " + item.AccountNo);
                    }
                }
                else
                {
                    var _companies = companyService.FilterActive().OrderBy(x => x.CompanyName).ToList();
                    foreach (var item in _companies)
                    {
                        if (!string.IsNullOrEmpty(item.CompanyName))
                            consigneeCompany.Add(item.CompanyName + " - " + item.AccountNo);
                    }
                }

                txtConsigneeCompany.AutoCompleteCustomSource = consigneeCompany;
            }

        }
        private void lstOriginBco_Validated(object sender, EventArgs e)
        {
            if (lstOriginBco.SelectedIndex >= 0)
            {
                var bcoId = Guid.Parse(lstOriginBco.SelectedValue.ToString());
                SelectedOriginCity(bcoId);

                lstAssignedTo.DataSource = null;
                lstAssignedTo.Refresh();
                areas = areaService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
                var _areas = areas.Where(x => x.City.BranchCorpOfficeId == bcoId).ToList();
                lstAssignedTo.DataSource = _areas;
                lstAssignedTo.DisplayMember = "RevenueUnitName";
                lstAssignedTo.ValueMember = "RevenueUnitId";
                assignedTo = new AutoCompleteStringCollection();
                foreach (var item in _areas.OrderBy(x => x.RevenueUnitName).Select(x => x.RevenueUnitName).ToList())
                {
                    assignedTo.Add(item);
                }
                lstAssignedTo.AutoCompleteDataSource = assignedTo;
                lstAssignedTo.SelectedIndex = -1;
                lstOriginCity.SelectedIndex = -1;
                lstOriginCity.Refresh();
                lstOriginCity.Focus();
            }
        }
        private void lstOriginBco_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (lstOriginBco.SelectedIndex >= 0)
            {
                string n = lstOriginBco.SelectedValue.ToString();
                Guid bcoId = new Guid();
                try
                {
                    bcoId = Guid.Parse(lstOriginBco.SelectedValue.ToString());
                }
                catch (Exception)
                {
                    return;
                }

                SelectedOriginCity(bcoId);

                lstAssignedTo.DataSource = null;
                lstAssignedTo.Refresh();
                areas = areaService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
                var _areas = areas.Where(x => x.City.BranchCorpOfficeId == bcoId).ToList();
                lstAssignedTo.DataSource = _areas;
                lstAssignedTo.DisplayMember = "RevenueUnitName";
                lstAssignedTo.ValueMember = "RevenueUnitId";
                assignedTo = new AutoCompleteStringCollection();
                foreach (var item in _areas.OrderBy(x => x.RevenueUnitName).Select(x => x.RevenueUnitName).ToList())
                {
                    assignedTo.Add(item);
                }
                lstAssignedTo.AutoCompleteDataSource = assignedTo;
                lstAssignedTo.SelectedIndex = -1;

                lstOriginCity.Refresh();
                lstOriginCity.Focus();
            }
        }
        private void lstDestinationBco_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (lstDestinationBco.SelectedIndex >= 0)
            {
                Guid bcoId = new Guid();
                try
                {
                    bcoId = Guid.Parse(lstDestinationBco.SelectedValue.ToString());
                }
                catch (Exception)
                {
                    return;
                }

                SelectedDestinationCity(bcoId);

                //lstAssignedTo.DataSource = null;
                //lstAssignedTo.Refresh();
                //areas = areaService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
                //var _areas = areas.Where(x => x.City.BranchCorpOfficeId == bcoId).ToList();
                //lstAssignedTo.DataSource = _areas;
                //lstAssignedTo.DisplayMember = "RevenueUnitName";
                //lstAssignedTo.ValueMember = "RevenueUnitId";
                //assignedTo = new AutoCompleteStringCollection();
                //foreach (var item in _areas.OrderBy(x => x.RevenueUnitName).Select(x => x.RevenueUnitName).ToList())
                //{
                //    assignedTo.Add(item);
                //}
                //lstAssignedTo.AutoCompleteDataSource = assignedTo;

                lstDestinationCity.SelectedIndex = -1;
                lstDestinationCity.Refresh();
                lstDestinationCity.Focus();
            }
        }
        private void txtShipperEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!(txtShipperEmail.Text == "N/A" || txtShipperEmail.Text == ""))
            {
                bool result = (txtShipperEmail.MaskedEditBoxElement.Provider as EMailMaskTextBoxProvider).Validate(txtShipperEmail.Text);
                if (!result)
                {
                    e.Cancel = true;
                }
            }
        }
        private void txtConsigneeEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!(txtConsigneeEmail.Text == "N/A" || txtConsigneeEmail.Text == ""))
            {
                bool result = (txtConsigneeEmail.MaskedEditBoxElement.Provider as EMailMaskTextBoxProvider).Validate(txtConsigneeEmail.Text);
                if (!result)
                {
                    e.Cancel = true;
                }
            }
        }
        private void txtShipperMobile_Validating(object sender, CancelEventArgs e)
        {

            if (!(txtShipperMobile.Text == "(0000) 000-0000"))
            {
                bool result = (txtShipperMobile.Text.Replace("_", "").ToString().Length == txtShipperMobile.Mask.Length);
                if (!result)
                {
                    e.Cancel = true;
                }
            }
        }
        private void txtShipperContactNo_Validating(object sender, CancelEventArgs e)
        {
            if (!(txtShipperContactNo.Text == "000-0000"))
            {
                bool result = (txtShipperContactNo.Text.Replace("_", "").ToString().Length == txtShipperContactNo.Mask.Length);
                if (!result)
                {
                    e.Cancel = true;
                }
            }
        }
        private void txtConsigneeMobile_Validating(object sender, CancelEventArgs e)
        {
            if (!(txtConsigneeMobile.Text == "(0000) 000-0000"))
            {
                bool result = (txtConsigneeMobile.Text.Replace("_", "").ToString().Length == txtConsigneeMobile.Mask.Length);
                if (!result)
                {
                    e.Cancel = true;
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            GroupShipper.Enabled = true;
            GroupConsignee.Enabled = true;
            GroupRemarks.Enabled = true;

            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            btnAcceptance.Enabled = false;
            btnNew.Enabled = false;
            btnReset.Enabled = true;
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (IsAdmin())
            {
                using (CmsDbCon settings = new CmsDbCon())
                {
                    if (settings.ShowDialog() == DialogResult.OK)
                    {
                        settings.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("You have insuficient privilege. Please Run as Administrator.", "Administrator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login();
            //Application.Exit();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void BookingGridView_Click(object sender, EventArgs e)
        {




        }
        #endregion

        #region Acceptance

        private void btnAddPackage_Click(object sender, EventArgs e)
        {
            AddPackage();
        }
        private void btnAddPackage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddPackage();
            }
        }
        private void btnCompute_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                ComputeCharges();
                btnAcceptanceSave.Enabled = true;
            }
            else
            {
                MessageBox.Show("Unable to compute.", "Data Validation");
            }

        }
        private void btnCompute_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void AcceptancebtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AcceptancetxtAirwayBill.Text))
            {
                AcceptancetxtAirwayBill.Focus();
                return;
            }
            else
            {
                shipment.AirwayBillNo = AcceptancetxtAirwayBill.Text.Trim();
            }

            btnReset.Enabled = false;
            btnCompute.Enabled = false;
            btnAcceptanceSave.Enabled = false;

            #region CaptureShipmentInput

            if (shipment.ShipmentId == null || shipment.ShipmentId == Guid.Empty)
            {
                shipment.ShipmentId = Guid.NewGuid();
            }

            shipment.CreatedDate = DateTime.Now;
            shipment.CreatedBy = AppUser.User.UserId;
            shipment.LastPaymentDate = null;
            shipment.DateOfFullPayment = null;
            shipment.AcceptedAreaId = AppUser.User.Employee.AssignedToAreaId;
            shipment.AcceptedArea = AppUser.Employee.AssignedToArea;
            shipment.AcceptedById = AppUser.Employee.EmployeeId;
            shipment.AcceptedBy = AppUser.Employee;
            if (shipment.CommodityId == null || shipment.CommodityId == Guid.Empty)
                shipment.CommodityId = Guid.Parse(lstCommodity.SelectedValue.ToString());
            shipment.Notes = txtNotes.Text;
            shipment.GoodsDescriptionId = Guid.Parse(lstGoodsDescription.SelectedValue.ToString());

            //shipment.DestinationCityId = Guid.Parse(lstDestinationCity.SelectedValue.ToString());
            shipment.ModifiedBy = AppUser.User.UserId;
            shipment.ModifiedDate = DateTime.Now;
            shipment.RecordStatus = (int)RecordStatus.Active;

            // TOO this is default payment term
            shipment.PaymentTermId = paymentTerms.Find(x => x.PaymentTermName.Equals("Cash")).PaymentTermId;
            if (shipment.PaymentMode.PaymentModeCode.Equals("PP"))
            {
                shipment.PaymentTermId = paymentTerms.Find(x => x.PaymentTermName.Equals("Cash")).PaymentTermId;
            }
            else if (shipment.PaymentMode.PaymentModeCode.Equals("FC"))
            {
                shipment.PaymentTermId = paymentTerms.Find(x => x.PaymentTermName.Equals("COD")).PaymentTermId;
            }
            else
            {
                if (shipment.Consignee.Company != null && shipment.Consignee.CompanyId != null)
                {
                    if (shipment.PaymentMode.PaymentModeCode.Equals("CAC"))
                    {
                        shipment.PaymentTermId = shipment.Consignee.Company.PaymentTerm.PaymentTermId;
                    }
                }

                if (shipment.Shipper.Company != null && shipment.Shipper.CompanyId != null)
                {
                    if (shipment.PaymentMode.PaymentModeCode.Equals("CAS"))
                    {
                        shipment.PaymentTermId = shipment.Shipper.Company.PaymentTerm.PaymentTermId;
                    }
                }
            }

            #region ShipmentPackages

            foreach (var item in shipment.PackageDimensions)
            {
                item.ShipmentId = shipment.ShipmentId;
                item.CreatedBy = AppUser.User.UserId;
                item.CreatedDate = DateTime.Now;
                item.ModifiedBy = AppUser.User.UserId;
                item.ModifiedDate = DateTime.Now;
                item.RecordStatus = (int)RecordStatus.Active;
            }

            #endregion

            #endregion

            ProgressIndicator saving = new ProgressIndicator("Acceptance", "Saving ...", SaveShipment);
            saving.ShowDialog();

            shipmentModel = shipment;
            btnAcceptanceReset.Enabled = true;
            btnPrint.Enabled = true;
            btnPayment.Enabled = true;
            btnSearchShipment.Enabled = true;

            DisableForm();

        }
        private void AcceptancebtnPrint_Click(object sender, EventArgs e)
        {
            //PrintAwb();
            PrintAWB_Acceptance();
        }
        private void AcceptancebtnReset_Click(object sender, EventArgs e)
        {
            ClearSummaryData();
            AcceptanceResetAll();
            isNewShipment = false;
        }
        private void lstCommodityType_Validated(object sender, EventArgs e)
        {
            CommodityTypeSelected();
        }
        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private void txtLength_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWidth.Focus();
            }
        }
        private void txtWidth_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHeight.Focus();
            }
        }
        private void txtHeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddPackage.Focus();
            }
        }
        private void dateAcceptedDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AcceptancetxtAirwayBill.Focus();
            }
        }
        private void txtAirwayBill_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (isNewShipment)
                {
                    if (IsNumericOnly(8, 8, AcceptancetxtAirwayBill.Text.Trim()))
                    {
                        if (shipmentService.FilterActiveBy(x => x.AirwayBillNo == AcceptancetxtAirwayBill.Text).Count() > 0)
                        {
                            MessageBox.Show("Airwaybill number already exist.", "Search Airwaybill Number", MessageBoxButtons.OK);
                            return;
                        }
                        lstCommodityType.Focus();
                        lstCommodityType.SelectedIndex = -1;
                        EnableForm();
                    }
                    else
                    {
                        MessageBox.Show("Invalid AirwayBill No.", "Data Error", MessageBoxButtons.OK);
                        AcceptancetxtAirwayBill.Focus();
                    }
                }
                else
                {
                    btnSearchShipment.PerformClick();
                }

            }
        }
        private void txtDeclaredValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }
        private void chkNonVatable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }
        private void btnSearchShipment_Click(object sender, EventArgs e)
        {
            List<Shipment> _shipment = shipmentService.FilterActiveBy(x => x.AirwayBillNo.Equals(AcceptancetxtAirwayBill.Text.ToString()));

            if (_shipment != null && _shipment.Count > 0)
            {
                AcceptanceLoadData();

                shipment = shipmentService.EntityToModel(_shipment.FirstOrDefault());
                commodityType = commodityTypes.Find(x => x.CommodityTypeId == shipment.CommodityTypeId);

                AcceptancePopulateForm();
                RefreshGridPackages();
                PopulateSummary();

                btnReset.Enabled = true;
                btnCompute.Enabled = false;
                btnAcceptanceSave.Enabled = false;
                btnPrint.Enabled = false;
                btnAcceptanceEdit.Enabled = true;


            }
            else
            {
                MessageBox.Show("No record found.", "Airwaybill Search");
            }

            shipmentModel = shipment;
        }
        private void AcceptancebtnPayment_Click(object sender, EventArgs e)
        {
            if (shipment.PaymentMode.PaymentModeCode.Equals("PP"))
            {
                //btnPayment.Enabled = false;
                ProceedToPayment();
            }
        }
        private void lstServiceMode_Validated(object sender, EventArgs e)
        {
            if (lstServiceMode.SelectedIndex > -1)
            {
                shipment.ServiceModeId = Guid.Parse(lstServiceMode.SelectedValue.ToString());
                shipment.ServiceMode = serviceModes.FirstOrDefault(x => x.ServiceModeId == shipment.ServiceTypeId);
                RefreshGridPackages();
                RefreshOptions();
            }

        }
        private void txtWeight_Validated(object sender, EventArgs e)
        {
            RefreshGridPackages();
        }
        private void lstCommodity_Enter(object sender, EventArgs e)
        {
            if (lstCommodityType.SelectedIndex > -1)
            {
                var commodityTypeId = Guid.Parse(lstCommodityType.SelectedValue.ToString());
                commodityCollection = new AutoCompleteStringCollection();
                foreach (
                    var item in
                        commodities.Where(x => x.CommodityTypeId == commodityTypeId)
                            .OrderBy(x => x.CommodityName)
                            .Select(x => x.CommodityName)
                            .ToList())
                {
                    commodityCollection.Add(item);
                }
                lstCommodity.AutoCompleteDataSource = commodityCollection;
            }

        }
        private void lstServiceMode_Enter(object sender, EventArgs e)
        {

            serviceModeCollection = new AutoCompleteStringCollection();
            foreach (var item in serviceModes.OrderBy(x => x.ServiceModeName).Select(x => x.ServiceModeName).ToList())
            {
                serviceModeCollection.Add(item);
            }
            lstServiceMode.AutoCompleteDataSource = serviceModeCollection;
        }
        private void lstShipMode_Enter(object sender, EventArgs e)
        {
            shipModeCollection = new AutoCompleteStringCollection();
            foreach (var item in shipModes.OrderBy(x => x.ShipModeName).Select(x => x.ShipModeName).ToList())
            {
                shipModeCollection.Add(item);
            }
            lstShipMode.AutoCompleteDataSource = shipModeCollection;
        }
        private void lstGoodsDescription_Enter(object sender, EventArgs e)
        {
            goodsDescCollection = new AutoCompleteStringCollection();
            foreach (
                var item in
                    goodsDescriptions.OrderBy(x => x.GoodsDescriptionName).Select(x => x.GoodsDescriptionName).ToList())
            {
                goodsDescCollection.Add(item);
            }
            lstGoodsDescription.AutoCompleteDataSource = goodsDescCollection;
        }
        private void lstPaymentMode_Enter(object sender, EventArgs e)
        {
            paymentModeCollection = new AutoCompleteStringCollection();
            foreach (var item in paymentModes.OrderBy(x => x.PaymentModeName).Select(x => x.PaymentModeName).ToList())
            {
                paymentModeCollection.Add(item);
            }
            lstPaymentMode.AutoCompleteDataSource = paymentModeCollection;
        }
        private void gridPackage_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {

            int index = Convert.ToInt32(e.Rows[0].Cells["No"].Value) - 1; ;
            PackageDimensionModel item = shipment.PackageDimensions.FirstOrDefault(x => x.Index == index);
            shipment.PackageDimensions.Remove(item);

            RefreshGridPackages();
        }
        private void AcceptancetxtAirwayBill_Enter(object sender, EventArgs e)
        {
            AirwayBill = new AutoCompleteStringCollection();
            string[] awbs = shipmentService.FilterActive().Select(x => x.AirwayBillNo).ToArray();
            AirwayBill.AddRange(awbs);
            AcceptancetxtAirwayBill.AutoCompleteCustomSource = AirwayBill;
        }
        private void EditAcceptance_Click(object sender, EventArgs e)
        {

            EnableForm();

            btnAcceptanceEdit.Enabled = false;
            btnCompute.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = true;
            btnPayment.Enabled = true;
            btnReset.Enabled = true;
        }
        private void lstCommodityType_Enter(object sender, EventArgs e)
        {
            commodityTypeCollection = new AutoCompleteStringCollection();
            var ctypes = commodityTypes.OrderBy(x => x.CommodityTypeName).Where(x => x.RecordStatus == 1).Select(x => x.CommodityTypeName).ToList();
            foreach (var item in ctypes)
            {
                commodityTypeCollection.Add(item);
            }
            lstCommodityType.AutoCompleteDataSource = commodityTypeCollection;
        }
        private void lstServiceType_Enter(object sender, EventArgs e)
        {
            serviceTypeCollection = new AutoCompleteStringCollection();
            var servicetype = serviceTypes.OrderBy(x => x.ServiceTypeName).Where(x => x.RecordStatus == 1).Select(x => x.ServiceTypeName).ToList();
            foreach (var item in servicetype)
            {
                serviceTypeCollection.Add(item);
            }
            lstServiceType.AutoCompleteDataSource = serviceTypeCollection;
        }
        private void AcceptancetxtAirwayBill_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void lstPaymentMode_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (shipment == null) return;

            if (lstPaymentMode.SelectedIndex > -1)
            {
                shipment.PaymentMode = paymentModes.Find(x => x.PaymentModeName == lstPaymentMode.SelectedItem.ToString());
                if (shipment.PaymentMode != null)
                {
                    shipment.PaymentModeId = shipment.PaymentMode.PaymentModeId;
                }
            }
        }
        private void lstShipMode_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (lstShipMode.SelectedIndex >= 0)
            {
                if (lstShipMode.SelectedItem.Text == "Transhipment")
                {
                    lstHub.Enabled = true;
                }
                else
                {
                    lstHub.Enabled = false;
                }
            }
        }
        #endregion

        #region Payment

        private void txtAwb_TextChanged(object sender, EventArgs e)
        {
            txtSoaNo.Enabled = false;
            txtSoaNo.Text = "";
        }
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSoaNo.Text))
            {
                // SOA Payment
                var soa = soaService.FilterActiveBy(x => x.StatementOfAccountNo.Equals(txtSoaNo.Text))
                        .FirstOrDefault();
                if (soa != null)
                {
                    soaPayment = new StatementOfAccountPayment();
                    soaPayment.StatementOfAccountPaymentId = Guid.NewGuid();
                    soaPayment.StatementOfAccountId = soa.StatementOfAccountId;
                    soaPayment.OrNo = txtOrNo.Text.Trim();
                    soaPayment.PrNo = txtPrNo.Text.Trim();
                    soaPayment.PaymentDate = datePaymentDate.Value;
                    soaPayment.Amount = decimal.Parse(txtAmountPaid.Text.Trim());
                    soaPayment.PaymentTypeId = Guid.Parse(lstPaymentType.SelectedValue.ToString());
                    if (lstPaymentType.SelectedText == "Check")
                    {
                        soaPayment.CheckBankName = txtCheckBank.Text.Trim();
                        soaPayment.CheckDate = dateCheckDate.Value;
                        soaPayment.CheckNo = txtCheckNo.Text.Trim();
                    }
                    soaPayment.ReceivedById = AppUser.Employee.EmployeeId;
                    soaPayment.Remarks = cmb_PaymentRemarks.Text;
                    soaPayment.CreatedBy = AppUser.User.UserId;
                    soaPayment.CreatedDate = DateTime.Now;
                    soaPayment.ModifiedBy = AppUser.User.UserId;
                    soaPayment.ModifiedDate = DateTime.Now;
                    soaPayment.RecordStatus = (int)RecordStatus.Active;

                }
                else
                {
                    MessageBox.Show("Invalid SOA No", "Data Error", MessageBoxButtons.OK);
                    return;
                }
            }
            else if (!string.IsNullOrEmpty(txtAwb.Text.Trim()))
            {
                // AWb Payment
                var shipment = shipmentService.FilterActiveBy(x => x.AirwayBillNo.Equals(txtAwb.Text.Trim()))
                        .FirstOrDefault();
                if (shipment != null)
                {
                    payment = new Payment();
                    payment.PaymentId = Guid.NewGuid();
                    payment.ShipmentId = shipment.ShipmentId;
                    payment.OrNo = txtOrNo.Text.Trim();
                    payment.PrNo = txtPrNo.Text.Trim();
                    payment.PaymentDate = datePaymentDate.Value;
                    try
                    {
                        if (txtAmountPaid.Value.ToString().Contains("Php"))
                        {
                            payment.Amount = decimal.Parse(txtAmountPaid.Value.ToString().Replace("Php", ""));
                        }
                        else
                        {
                            payment.Amount = decimal.Parse(txtAmountPaid.Value.ToString().Replace("₱", ""));
                        }

                        payment.TaxWithheld = decimal.Parse(txtTaxWithheld.Value.ToString());
                    }
                    catch (Exception)
                    {

                    }

                    payment.PaymentTypeId = Guid.Parse(lstPaymentType.SelectedValue.ToString());
                    if (lstPaymentType.SelectedText == "Check")
                    {
                        payment.CheckBankName = txtCheckBank.Text.Trim();
                        payment.CheckDate = dateCheckDate.Value;
                        payment.CheckNo = txtCheckNo.Text.Trim();
                    }
                    payment.ReceivedById = AppUser.Employee.EmployeeId;
                    payment.Remarks = cmb_PaymentRemarks.Text;
                    payment.CreatedBy = AppUser.User.UserId;
                    payment.CreatedDate = DateTime.Now;
                    payment.ModifiedBy = AppUser.User.UserId;
                    payment.ModifiedDate = DateTime.Now;
                    payment.RecordStatus = (int)RecordStatus.Active;
                }
                else
                {
                    MessageBox.Show("Invalid AWB No", "Data Error", MessageBoxButtons.OK);

                    return;
                }
            }

            btnAccept.Enabled = false;
            btnCancel.Enabled = false;

            ProgressIndicator saving = new ProgressIndicator("Payment", "Saving ...", SavePayment);
            saving.ShowDialog();

            //ProgressIndicator uploading = new ProgressIndicator("Payment", "Uploading ...", UploadToCentral);
            //uploading.ShowDialog();

            btnAccept.Enabled = true;
            btnCancel.Enabled = true;
            PaymentReset();

            shipmentModel = null;
            isNewShipment = false;

            NewPayment = null;

            AcceptanceResetAll();
            ClearSummaryData();
            DisableForm();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            PaymentReset();
        }
        private void txtTaxWithheld_Leave(object sender, EventArgs e)
        {
            // ComputeNetCollection();
        }
        private void lstPaymentType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (lstPaymentType.SelectedText == "Check")
            {
                txtCheckBank.Enabled = true;
                txtCheckNo.Enabled = true;
                dateCheckDate.Enabled = true;
                txtCheckBank.Focus();
            }
            else
            {
                txtCheckBank.Enabled = false;
                txtCheckNo.Enabled = false;
                dateCheckDate.Enabled = false;
                txtRemarks.Focus();
            }
        }
        private void txtAwb_Enter(object sender, EventArgs e)
        {
            AirwayBill = new AutoCompleteStringCollection();
            List<string> awbs = shipmentService.GetAll().Select(x => x.AirwayBillNo).ToList();
            foreach (string item in awbs)
            {
                AirwayBill.Add(item);
            }
            txtAwb.AutoCompleteCustomSource = AirwayBill;
        }
        private void txtAwb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                List<Shipment> _shipment = shipmentService.FilterActiveBy(x => x.AirwayBillNo.Equals(txtAwb.Text.ToString()));
                if (_shipment != null && _shipment.Count > 0)
                {
                    shipment = shipmentService.EntityToModel(_shipment.FirstOrDefault());
                    LoadPayment();
                }
                else
                {
                    MessageBox.Show("No record found.", "Airwaybill Search");
                }

            }

            shipmentModel = shipment;
        }
        private void txtTaxWithheld_TextChanged(object sender, EventArgs e)
        {
            ComputeNetCollection();
        }
        #endregion



        #endregion

        #region Methods

        #region Main
        private void Login()
        {
            Login loginForm = new Login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                string username = loginForm.username;
                string password = loginForm.password;

                _userManager = new UserManager<IdentityUser, Guid>(new UserStore(GlobalVars.UnitOfWork));

                User user = userService.GetUserByUsername(username);


                if (user != null)
                {


                    if (password == Tools.Encryption.DecryptPassword(user.PasswordHash))
                    {
                        var roles = _userManager.GetRolesAsync(user.UserId).Result.ToList();
                        AppUser.Employee = user.Employee;
                        AppUser.Principal = new GenericPrincipal(new GenericIdentity(user.UserName), roles.ToArray());
                        AppUser.User = user;


                        if (user.Employee.AssignedToArea.City.BranchCorpOfficeId != GlobalVars.DeviceBcoId)
                        {
                            if (AppUser.User.UserName != "admin")
                            {
                                InvalidLogin();
                                return;
                            }
                        }

                        UserTxt.Text = "Welcome! " + AppUser.Employee.FullName;
                        btnLogOut.Enabled = true;

                        MenuAccessBL accessService = new MenuAccessBL(GlobalVars.UnitOfWork);
                        MenuBL menuService = new MenuBL(GlobalVars.UnitOfWork);

                        List<Entities.Menu> menus = new List<Entities.Menu>();
                        menus = menuService.GetAll();

                        GlobalVars.MenuAccess = accessService.GetAll().Where(x => x.UserId == user.UserId).ToList();

                        if (GlobalVars.MenuAccess.Find(x => x.MenuId == menus.Find(z => z.MenuName == "Booking").MenuId) != null)
                        {
                            this.BookingPage.Item.Visibility = ElementVisibility.Visible;
                        }
                        else
                        {
                            this.BookingPage.Item.Visibility = ElementVisibility.Collapsed;
                        }

                        if (GlobalVars.MenuAccess.Find(x => x.MenuId == menus.Find(z => z.MenuName == "Acceptance").MenuId) != null)
                        {
                            this.AcceptancePage.Item.Visibility = ElementVisibility.Visible;
                        }
                        else
                        {
                            this.AcceptancePage.Item.Visibility = ElementVisibility.Collapsed;
                        }

                        if (GlobalVars.MenuAccess.Find(x => x.MenuId == menus.Find(z => z.MenuName == "Payment").MenuId) != null)
                        {
                            this.PaymentPage.Item.Visibility = ElementVisibility.Visible;
                        }
                        else
                        {
                            this.PaymentPage.Item.Visibility = ElementVisibility.Collapsed;
                        }

                        if (GlobalVars.MenuAccess.Find(x => x.MenuId == menus.Find(z => z.MenuName == "Manifest").MenuId) != null)
                        {
                            this.Manifest.Item.Visibility = ElementVisibility.Visible;
                        }
                        else
                        {
                            this.Manifest.Item.Visibility = ElementVisibility.Collapsed;
                        }

                        if (GlobalVars.MenuAccess.Find(x => x.MenuId == menus.Find(z => z.MenuName == "PaymentSummary").MenuId) != null)
                        {
                            this.PaymentSummaryPage.Item.Visibility = ElementVisibility.Visible;
                        }
                        else
                        {
                            this.PaymentSummaryPage.Item.Visibility = ElementVisibility.Collapsed;
                        }

                        if (GlobalVars.MenuAccess.Find(x => x.MenuId == menus.Find(z => z.MenuName == "Report").MenuId) != null)
                        {
                            this.TrackingPage.Item.Visibility = ElementVisibility.Visible;
                        }
                        else
                        {
                            this.TrackingPage.Item.Visibility = ElementVisibility.Collapsed;
                        }
                    }
                    else
                    {
                        InvalidLogin();
                    }

                }
                else
                {
                    InvalidLogin();
                }
            }

            if (AppUser.Principal != null)
            {
                if (AppUser.Principal.IsInRole("Admin"))
                {
                    //btnAppSetting.Enabled = true;
                }
            }

        }
        private void InvalidLogin()
        {
            if (MessageBox.Show("Invalid username and/or password. Try again?", "APCargo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Login();
            }
            else
            {
                this.Close();
            }
        }
        private bool IsAdmin()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
               .IsInRole(WindowsBuiltInRole.Administrator);
        }
        private NewPasswordViewModel GetNewPassword()
        {
            ChangePassword changePasswordForm = new ChangePassword();
            if (changePasswordForm.ShowDialog() == DialogResult.OK)
            {
                NewPasswordViewModel vm = new NewPasswordViewModel();
                vm.OldPassword = changePasswordForm.oldPassword;
                vm.NewPassword1 = changePasswordForm.newPassword1;
                vm.NewPassword2 = changePasswordForm.newPassword2;
                return vm;
            }
            else
            { return null; }
        }
        internal static class NativeWinAPI
        {
            internal static readonly int GWL_EXSTYLE = -20;
            internal static readonly int WS_EX_COMPOSITED = 0x02000000;

            [DllImport("user32")]
            internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32")]
            internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        }

        #endregion

        #region Booking
        private void LoadInit()
        {
            GlobalVars.UnitOfWork = new CmsUoW();
            GlobalVars.DeviceRevenueUnitId = Guid.Parse(ConfigurationManager.AppSettings["RUId"]);
            GlobalVars.DeviceBcoId = Guid.Parse(ConfigurationManager.AppSettings["BcoId"]);

            bsBookingStatus = new BindingSource();
            bsBookingRemark = new BindingSource();
            bsAreas = new BindingSource();
            bsOriginBco = new BindingSource();
            bsDestinationBco = new BindingSource();

            bookingStatus = new List<BookingStatus>();
            bookingRemarks = new List<BookingRemark>();
            branchCorpOffices = new List<BranchCorpOffice>();
            areas = new List<RevenueUnit>();
            clients = new List<Entities.Client>();
            revenueUnits = new List<RevenueUnit>();
            cities = new List<City>();
            companies = new List<Company>();

            bookingStatusService = new BookingStatusBL(GlobalVars.UnitOfWork);
            bookingRemarkService = new BookingRemarkBL(GlobalVars.UnitOfWork);
            bcoService = new BranchCorpOfficeBL(GlobalVars.UnitOfWork);
            areaService = new AreaBL(GlobalVars.UnitOfWork);
            bookingService = new BookingBL(GlobalVars.UnitOfWork);
            clientService = new ClientBL(GlobalVars.UnitOfWork);
            revenueUnitService = new RevenueUnitBL(GlobalVars.UnitOfWork);
            cityService = new CityBL(GlobalVars.UnitOfWork);
            companyService = new CompanyBL(GlobalVars.UnitOfWork);
            userService = new UserStore(GlobalVars.UnitOfWork);

            bsoService = new BranchSatOfficeBL(GlobalVars.UnitOfWork);
            gatewaySatService = new GatewaySatOfficeBL(GlobalVars.UnitOfWork);
            revenueUnitService = new RevenueUnitBL(GlobalVars.UnitOfWork);


            bookingStatus = bookingStatusService.FilterActive().OrderBy(x => x.BookingStatusName).ToList();
            bookingRemarks = bookingRemarkService.FilterActive().OrderBy(x => x.BookingRemarkName).ToList();
            branchCorpOffices = bcoService.FilterActive().OrderBy(x => x.BranchCorpOfficeName).ToList();
            areas = areaService.FilterActive().OrderBy(x => x.RevenueUnitName).ToList();
            clients = clientService.FilterActive();
            cities = cityService.FilterActive().OrderBy(x => x.CityName).ToList();
            companies = companyService.FilterActive().OrderBy(x => x.CompanyName).ToList();

        }
        private void LoadBookingComponents()
        {
            bsBookingStatus.DataSource = bookingStatus;
            bsBookingRemark.DataSource = bookingRemarks;
            bsAreas.DataSource = areas;
            bsOriginBco.DataSource = branchCorpOffices;
            bsDestinationBco.DataSource = branchCorpOffices;

            lstBookingStatus.DataSource = bsBookingStatus;
            lstBookingStatus.ValueMember = "BookingStatusId";
            lstBookingStatus.DisplayMember = "BookingStatusName";
            lstBookingStatus.SelectedIndex = -1;

            lstBookingRemarks.DataSource = bsBookingRemark;
            lstBookingRemarks.DisplayMember = "BookingRemarkName";
            lstBookingRemarks.ValueMember = "BookingRemarkId";
            lstBookingRemarks.SelectedIndex = -1;

            lstOriginBco.DataSource = bsOriginBco;
            lstOriginBco.DisplayMember = "BranchCorpOfficeName";
            lstOriginBco.ValueMember = "BranchCorpOfficeId";
            lstOriginBco.SelectedIndex = -1;

            lstDestinationBco.DataSource = bsDestinationBco;
            lstDestinationBco.DisplayMember = "BranchCorpOfficeName";
            lstDestinationBco.ValueMember = "BranchCorpOfficeId";
            lstDestinationBco.SelectedIndex = -1;

            List<RevenueUnit> _areas = areas.Where(x => x.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();
            lstAssignedTo.DataSource = _areas;
            lstAssignedTo.DisplayMember = "RevenueUnitName";
            lstAssignedTo.ValueMember = "RevenueUnitId";

        }
        private void TrackingLoadInit()
        {
            bsBCO1 = new BindingSource();
            bsDriver = new BindingSource();
            bsBatch = new BindingSource();
            bsEmployee = new BindingSource();
            bsStatus = new BindingSource();
            bsGTCommodityType = new BindingSource();
            bsRevenueUnitType = new BindingSource();
            bstrackBARevenueUnitName = new BindingSource();
            bsBundleBSO = new BindingSource();
            bsUnbundleBCO = new BindingSource();
            bsGTDestinationBCO = new BindingSource();
            bsGTBatch = new BindingSource();
            bsGODestinationBCO = new BindingSource();
            bsGOBatch = new BindingSource();
            bsGOCommodityType = new BindingSource();
            bsGICommodityType = new BindingSource();
            bsCTBatch = new BindingSource();
            bsSGBCO = new BindingSource();
            bsSGBatch = new BindingSource();
            bsAirlines = new BindingSource();
            bsFlightNumber = new BindingSource();

            branchAcceptanceService = new BranchAcceptanceBL(GlobalVars.UnitOfWork);
            batchService = new BatchBL(GlobalVars.UnitOfWork);
            deliveryStatusService = new DeliveryStatusBL(GlobalVars.UnitOfWork);
            revenueUnitTypeService = new RevenueUnitTypeBL(GlobalVars.UnitOfWork);
            revenueUnitservice = new RevenueUnitBL(GlobalVars.UnitOfWork);
            reasonService = new ReasonBL(GlobalVars.UnitOfWork);
            gatewayInboundService = new GatewayInboundBL(GlobalVars.UnitOfWork);
            gatewayTransmittalService = new GatewayTransmittalBL(GlobalVars.UnitOfWork);
            gatewayOutboundService = new GatewayOutboundBL(GlobalVars.UnitOfWork);
            cargotransferService = new CargoTransferBL(GlobalVars.UnitOfWork);
            segregationService = new SegregationBL(GlobalVars.UnitOfWork);
            statusService = new StatusBL(GlobalVars.UnitOfWork);
            airlineService = new AirlinesBL(GlobalVars.UnitOfWork);
            packageNumberService = new PackageNumberBL(GlobalVars.UnitOfWork);
            flightnumberService = new FlightInfoBL(GlobalVars.UnitOfWork);
            bundleService = new BundleBL(GlobalVars.UnitOfWork);
            unbundleService = new UnbundleBL(GlobalVars.UnitOfWork);
            distributionService = new DistributionBL(GlobalVars.UnitOfWork);
            holdCargoService = new HoldCargoBL(GlobalVars.UnitOfWork);
            deliveryService = new DeliveryBL(GlobalVars.UnitOfWork);
            deliveryReceiptService = new DeliveryReceiptBL(GlobalVars.UnitOfWork);
            deliveredPackageService = new DeliveredPackageBL(GlobalVars.UnitOfWork);

            revenueUnitTypes = new List<RevenueUnitType>();
            airlines = new List<Airlines>();
            batches = new List<Batch>();
            statuses = new List<Status>();
            reasons = new List<Reason>();
            flightnumbers = new List<FlightInformation>();
            deliveryStatuses = new List<DeliveryStatus>();
        }
        private void NewShipment()
        {
            ShipmentModel newShipment = new ShipmentModel();
            newShipment.ShipperId = booking.ShipperId;
            newShipment.Shipper = booking.Shipper;
            newShipment.OriginCityId = booking.OriginCityId;
            newShipment.OriginCity = booking.OriginCity;
            newShipment.OriginAddress = booking.OriginAddress1;
            if (!String.IsNullOrEmpty(booking.OriginAddress2))
                newShipment.OriginAddress = newShipment.OriginAddress + ", " + booking.OriginAddress2;
            if (!String.IsNullOrEmpty(booking.OriginStreet))
                newShipment.OriginAddress = newShipment.OriginAddress + ", " + booking.OriginStreet;
            newShipment.OriginBarangay = booking.OriginBarangay;
            if (!String.IsNullOrEmpty(booking.OriginBarangay))
                newShipment.OriginAddress = newShipment.OriginAddress + ", " + booking.OriginBarangay;
            newShipment.ConsigneeId = booking.ConsigneeId;
            newShipment.Consignee = booking.Consignee;
            newShipment.DestinationCityId = booking.DestinationCityId;
            newShipment.DestinationCity = booking.DestinationCity;
            newShipment.DestinationAddress = booking.DestinationAddress1;
            if (!String.IsNullOrEmpty(booking.DestinationAddress2))
                newShipment.DestinationAddress = newShipment.DestinationAddress + ", " + booking.DestinationAddress2;
            if (!String.IsNullOrEmpty(booking.DestinationStreet))
                newShipment.DestinationAddress = newShipment.DestinationAddress + ", " + booking.DestinationStreet;
            newShipment.DestinationBarangay = booking.DestinationBarangay;
            if (!String.IsNullOrEmpty(booking.DestinationBarangay))
                newShipment.DestinationAddress = newShipment.DestinationAddress + ", " + booking.DestinationBarangay;

            newShipment.BookingId = booking.BookingId;
            newShipment.Booking = booking;


            isNewShipment = true;
            shipmentModel = newShipment;
            ((RadPageView)BookingPage.Parent).SelectedPage = this.AcceptancePage;


            BookingResetAll();
        }
        private void AddDailyBooking()
        {
            List<Booking> todayBooking = new List<Booking>();
            List<Booking> yesterdayBooking = new List<Booking>();

            DateTime today = DateTime.Now;
            string dayOfWeek = today.ToString("ddd");
            if (!dayOfWeek.Equals("Sun"))
            {
                if (dayOfWeek.Equals("Mon"))
                {
                    DateTime saturdayBooking = DateTime.Now.AddDays(-2);
                    todayBooking = bookingService.FilterActiveBy(x => x.DateBooked.Year == today.Year && x.DateBooked.Month == today.Month && x.DateBooked.Day == today.Day).ToList();
                    yesterdayBooking = bookingService.FilterActiveBy(x => x.HasDailyBooking == true && x.DateBooked.Year == saturdayBooking.Year && x.DateBooked.Month == saturdayBooking.Month && x.DateBooked.Day == saturdayBooking.Day).ToList();
                }
                else
                {
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    todayBooking = bookingService.FilterActiveBy(x => x.DateBooked.Year == today.Year && x.DateBooked.Month == today.Month && x.DateBooked.Day == today.Day).ToList();
                    yesterdayBooking = bookingService.FilterActiveBy(x => x.HasDailyBooking == true && x.DateBooked.Year == yesterday.Year && x.DateBooked.Month == yesterday.Month && x.DateBooked.Day == yesterday.Day).ToList();
                }

                foreach (var item in yesterdayBooking)
                {
                    if (!todayBooking.Exists(x => x.PreviousBookingId == item.BookingId))
                    {
                        Booking newBooking = new Booking();
                        newBooking.BookingId = Guid.NewGuid();
                        newBooking.BookingNo = GetBookingNumber();
                        newBooking.DateBooked = DateTime.Now;
                        newBooking.ShipperId = item.ShipperId;
                        newBooking.OriginAddress1 = item.OriginAddress1;
                        newBooking.OriginAddress2 = item.OriginAddress2;
                        newBooking.OriginBarangay = item.OriginBarangay;
                        newBooking.OriginCityId = item.OriginCityId;
                        newBooking.ConsigneeId = item.ConsigneeId;
                        newBooking.DestinationAddress1 = item.DestinationAddress1;
                        newBooking.DestinationAddress2 = item.DestinationAddress2;
                        newBooking.DestinationBarangay = item.DestinationBarangay;
                        newBooking.DestinationCityId = item.DestinationCityId;
                        newBooking.Remarks = item.Remarks;
                        newBooking.HasDailyBooking = item.HasDailyBooking;
                        newBooking.BookedById = item.BookedById;
                        newBooking.AssignedToAreaId = item.AssignedToAreaId;
                        newBooking.BookingStatusId =
                            bookingStatus.Where(x => x.BookingStatusName.Equals("Pending"))
                                .First()
                                .BookingStatusId;
                        newBooking.BookingRemarkId =
                            bookingRemarks.Where(x => x.BookingRemarkName.Equals("Lack of Times"))
                                .First()
                                .BookingRemarkId;
                        newBooking.CreatedBy = item.CreatedBy;
                        newBooking.CreatedDate = DateTime.Now;
                        newBooking.ModifiedBy = item.CreatedBy;
                        newBooking.ModifiedDate = DateTime.Now;
                        newBooking.RecordStatus = (int)RecordStatus.Active;
                        newBooking.PreviousBookingId = item.BookingId;
                        bookingService.AddEdit(newBooking);
                    }
                }
            }
        }
        private void PopulateGrid()
        {

            List<Booking> books = bookingService.FilterActiveBy(x => x.BookingStatus.BookingStatusName != "Picked-up" && x.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId).OrderByDescending(x => x.DateBooked).ToList(); //bookingService.FilterActiveBy(x => x.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList().OrderByDescending(x => x.CreatedDate);
            if (books.Count > 0 && books != null)
            {
                BookingGridView.DataSource = null;
                BookingGridView.DataSource = books;
                BookingGridView.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="Evm"></param>
        /// <returns></returns>
        private DataTable ConvertToDataTable(ShipmentModel list, out decimal Evm)
        {
            decimal totalEvm = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("No", typeof(string)));
            dt.Columns.Add(new DataColumn("Length", typeof(string)));
            dt.Columns.Add(new DataColumn("Width", typeof(string)));
            dt.Columns.Add(new DataColumn("Height", typeof(string)));
            dt.Columns.Add(new DataColumn("Crating", typeof(string)));
            dt.Columns.Add(new DataColumn("Packaging", typeof(string)));
            dt.Columns.Add(new DataColumn("Draining", typeof(string)));

            dt.BeginLoadData();

            foreach (PackageDimensionModel item in list.PackageDimensions)
            {
                totalEvm = totalEvm + item.Evm;
                DataRow row = dt.NewRow();
                row["No"] = item.Index + 1;
                row["Length"] = item.LengthString;
                row["Width"] = item.WidthString;
                row["Height"] = item.HeightString;
                row["Crating"] = item.CratingFeeString;
                row["Packaging"] = item.PackagingFeeString;
                row["Draining"] = item.DrainingFeeString;

                dt.Rows.Add(row);
            }

            dt.EndLoadData();
            Evm = totalEvm;
            return dt;
        }
        private void BookingSelected(Guid id)
        {
            booking = bookingService.FilterActiveBy(x => x.BookingId == id).FirstOrDefault();

            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnReset.Enabled = true;
            btnAcceptance.Enabled = true;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;

            GroupShipper.Enabled = false;
            GroupConsignee.Enabled = false;
            GroupRemarks.Enabled = false;

            BookingPopulateForm();
        }
        private void BookingPopulateForm()
        {
            if (booking != null)
            {
                txtShipperAccountNo.Text = booking.Shipper.AccountNo;
                txtShipperLastName.Text = booking.Shipper.LastName;
                txtShipperFirstName.Text = booking.Shipper.FirstName;
                if (booking.Shipper.CompanyId != null)
                {
                    txtShipperCompany.Text = booking.Shipper.Company.CompanyName + " - " + booking.Shipper.Company.AccountNo;
                }
                else
                {
                    txtShipperCompany.Text = booking.Shipper.CompanyName;
                }
                txtShipperAddress1.Text = booking.OriginAddress1;
                txtShipperAddress2.Text = booking.OriginAddress2;
                txtShipperStreet.Text = booking.OriginStreet;
                txtShipperBarangay.Text = booking.OriginBarangay;
                var bcoId = cities.FirstOrDefault(x => x.CityId == booking.OriginCityId).BranchCorpOffice.BranchCorpOfficeId;
                SelectedOriginCity(bcoId);
                lstOriginBco.SelectedValue = bcoId;
                lstOriginCity.SelectedValue = booking.OriginCityId;
                txtShipperContactNo.Text = booking.Shipper.ContactNo;
                txtShipperMobile.Text = booking.Shipper.Mobile;
                txtShipperEmail.Text = booking.Shipper.Email;

                txtConsigneeAccountNo.Text = booking.Consignee.AccountNo;
                txtConsigneeLastName.Text = booking.Consignee.LastName;
                txtConsigneeFirstName.Text = booking.Consignee.FirstName;
                if (booking.Consignee.CompanyId != null)
                {
                    txtConsigneeCompany.Text = booking.Consignee.Company.CompanyName + " - " + booking.Consignee.Company.AccountNo;
                }
                else
                {
                    txtConsigneeCompany.Text = booking.Consignee.CompanyName;
                }
                txtConsigneeAddress1.Text = booking.DestinationAddress1;
                txtConsigneeAddress2.Text = booking.DestinationAddress2;
                txtConsgineeStreet.Text = booking.DestinationStreet;
                txtConsigneeBarangay.Text = booking.DestinationBarangay;
                bcoId = cities.FirstOrDefault(x => x.CityId == booking.DestinationCityId).BranchCorpOffice.BranchCorpOfficeId;
                SelectedDestinationCity(bcoId);
                lstDestinationBco.SelectedValue = bcoId;
                lstDestinationCity.SelectedValue = booking.DestinationCityId;
                txtConsigneeContactNo.Text = booking.Consignee.ContactNo;
                txtConsigneeMobile.Text = booking.Consignee.Mobile;
                txtConsigneeEmail.Text = booking.Consignee.Email;

                dateDateBooked.Value = booking.DateBooked;
                txtBookedBy.Text = booking.BookedBy.FullName;
                txtRemarks.Text = booking.Remarks;
                txtBookingNo.Text = booking.BookingNo;
                chkHasDailyBooking.Checked = booking.HasDailyBooking;
                lstAssignedTo.SelectedValue = booking.AssignedToAreaId;
                lstBookingStatus.SelectedValue = booking.BookingStatusId;
                lstBookingRemarks.SelectedIndex = -1;
                if (booking.BookingRemarkId != null)
                {
                    lstBookingRemarks.SelectedValue = booking.BookingRemarkId;
                }

                shipper = clientService.GetById(booking.ShipperId);
                consignee = clientService.GetById(booking.ConsigneeId);

            }
        }
        private void NewBooking()
        {

            if (AppUser.Principal.Identity.IsAuthenticated)
            {

                string bookingNo = GetBookingNumber();
                txtBookingNo.Text = bookingNo;
                booking = new Booking();
                booking.BookingNo = bookingNo;
                booking.BookedById = AppUser.User.EmployeeId;

                txtShipperAccountNo.Text = "";
                txtShipperLastName.Text = "";
                txtShipperFirstName.Text = "";
                txtShipperCompany.Text = "";
                txtShipperAddress1.Text = "";
                txtShipperAddress2.Text = "N/A";
                txtShipperStreet.Text = "N/A";
                txtShipperBarangay.Text = "N/A";
                if (lstOriginBco.Items.Count > 0)
                    lstOriginBco.SelectedIndex = -1;
                if (lstOriginCity.Items.Count > 0)
                    lstOriginCity.SelectedIndex = -1;
                txtShipperContactNo.Text = "0000000";
                txtShipperMobile.Text = "00000000000";
                txtShipperEmail.Text = "N/A";

                txtConsigneeAccountNo.Text = "";
                txtConsigneeLastName.Text = "";
                txtConsigneeFirstName.Text = "";
                txtConsigneeCompany.Text = "";
                txtConsigneeAddress1.Text = "";
                txtConsigneeAddress2.Text = "N/A";
                txtConsgineeStreet.Text = "N/A";
                txtConsigneeBarangay.Text = "N/A";
                if (lstDestinationBco.Items.Count > 0)
                    lstDestinationBco.SelectedIndex = -1;
                if (lstDestinationCity.Items.Count > 0)
                    lstDestinationCity.SelectedIndex = -1;
                txtConsigneeContactNo.Text = "0000000";
                txtConsigneeMobile.Text = "00000000000";
                txtConsigneeEmail.Text = "N/A";

                txtRemarks.Text = "";
                dateDateBooked.Value = DateTime.Now;
                txtBookedBy.Text = "";

                if (lstAssignedTo.Items.Count > 0)
                    lstAssignedTo.SelectedIndex = 0;

                if (lstBookingStatus.Items.Count > 0)
                {
                    lstBookingStatus.SelectedIndex = 1;
                }
                if (lstBookingRemarks.Items.Count > 0)
                    lstBookingRemarks.SelectedIndex = -1;

                GroupShipper.Enabled = true;
                GroupConsignee.Enabled = true;
                GroupRemarks.Enabled = true;
                chkHasDailyBooking.Enabled = true;

                btnNew.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                btnReset.Enabled = true;
                btnAcceptance.Enabled = false;
                btnDelete.Enabled = false;

                lstOriginBco.SelectedValue = GlobalVars.DeviceBcoId;
                txtShipperLastName.Focus();
                this.ActiveControl = txtShipperLastName;
            }
        }
        private void DeleteBooking()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                booking.ModifiedBy = AppUser.User.UserId;
                booking.ModifiedDate = DateTime.Now;
                booking.RecordStatus = (int)RecordStatus.Deleted;
                bookingService.AddEdit(booking);
                AcceptanceResetAll();
                PopulateGrid();
            }
        }
        private void BookingResetAll()
        {
            txtShipperAccountNo.Text = "";
            txtShipperLastName.Text = "";
            txtShipperFirstName.Text = "";
            txtShipperCompany.Text = "";
            txtShipperAddress1.Text = "";
            txtShipperAddress2.Text = "";
            txtShipperStreet.Text = "";
            txtShipperBarangay.Text = "";
            if (lstOriginBco.Items.Count > 0)
                lstOriginBco.SelectedIndex = -1;
            if (lstOriginCity.Items.Count > 0)
                lstOriginCity.SelectedIndex = -1;
            txtShipperContactNo.Text = txtShipperContactNo.Mask;
            txtShipperMobile.Text = txtShipperMobile.Mask;
            txtShipperEmail.Text = "";

            txtConsigneeAccountNo.Text = "";
            txtConsigneeLastName.Text = "";
            txtConsigneeFirstName.Text = "";
            txtConsigneeCompany.Text = "";
            txtConsigneeAddress1.Text = "";
            txtConsigneeAddress2.Text = "";
            txtConsgineeStreet.Text = "";
            txtConsigneeBarangay.Text = "";
            if (lstDestinationBco.Items.Count > 0)
                lstDestinationBco.SelectedIndex = -1;
            if (lstDestinationCity.Items.Count > 0)
                lstDestinationCity.SelectedIndex = -1;
            txtConsigneeContactNo.Text = txtConsigneeContactNo.Mask;
            txtConsigneeMobile.Text = txtConsigneeMobile.Mask;
            txtConsigneeEmail.Text = "";

            txtRemarks.Text = "";

            chkHasDailyBooking.Checked = false;
            txtBookedBy.Text = "";
            txtBookingNo.Text = "";
            if (lstAssignedTo.Items.Count > 0)
                lstAssignedTo.SelectedIndex = 0;
            if (lstBookingStatus.Items.Count > 0)
                lstBookingStatus.SelectedIndex = 0;
            if (lstBookingRemarks.Items.Count > 0)
                lstBookingRemarks.SelectedIndex = -1;


            GroupShipper.Enabled = false;
            GroupConsignee.Enabled = false;
            GroupRemarks.Enabled = false;


            //txtRemarks.Enabled = false;
            //dateDateBooked.Enabled = false;
            chkHasDailyBooking.Enabled = false;
            //txtBookedBy.Enabled = false;
            //txtBookingNo.Enabled = false;
            //lstAssignedTo.Enabled = false;
            //lstBookingStatus.Enabled = false;
            //lstBookingRemarks.Enabled = false;

            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnReset.Enabled = false;
            btnAcceptance.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
        }
        private string GetBookingNumber()
        {
            string date = DateTime.Now.ToString("yy");
            int lastBooking = 1;
            var deviceCode = ConfigurationSettings.AppSettings["DeviceCode"];
            var bookings = bookingService.FilterActive();
            if (bookings != null && bookings.Count > 0)
            {
                lastBooking = Convert.ToInt32(bookings.Max(x => Convert.ToInt32(x.BookingNo.Substring(x.BookingNo.Length - 5, 5)))) + 1;
            }
            if (string.IsNullOrEmpty(deviceCode))
            {
                deviceCode = "C000";
            }

            return deviceCode + date + lastBooking.ToString("00000");
        }
        private void CreateShipper()
        {
            Entities.Client _client = clients.FirstOrDefault(x => x.LastName.Equals(txtShipperLastName.Text.Trim()) && x.FirstName.Equals(txtShipperFirstName.Text.Trim()));
            if (_client != null)
            {
                shipper = _client;
                booking.ShipperId = shipper.ClientId;
                booking.Shipper = shipper;
                txtShipperAccountNo.Text = shipper.AccountNo;
                if (shipper.CompanyId != null)
                { txtShipperCompany.Text = shipper.Company.CompanyName + " - " + shipper.Company.AccountNo; }
                else
                {
                    txtShipperCompany.Text = shipper.CompanyName;
                }
                txtShipperAddress1.Text = shipper.Address1;
                txtShipperAddress2.Text = shipper.Address2;
                txtShipperStreet.Text = shipper.Street;
                txtShipperBarangay.Text = shipper.Barangay;
                lstOriginBco.Text = shipper.City.BranchCorpOffice.BranchCorpOfficeName;
                lstOriginCity.Text = shipper.City.CityName;
                if (shipper.City != null)
                {
                    lstOriginBco.SelectedValue = shipper.City.BranchCorpOfficeId;
                    lstOriginCity.SelectedValue = shipper.City.CityId;
                }
                txtShipperContactNo.Text = shipper.ContactNo;
                txtShipperMobile.Text = shipper.Mobile;
                txtShipperEmail.Text = shipper.Email;
            }
            else
            {
                shipper = new Entities.Client();
                shipper.LastName = txtShipperLastName.Text.Trim();
                shipper.FirstName = txtShipperFirstName.Text.Trim();
            }
        }
        private void CreateConsignee()
        {
            var _client = clients.FirstOrDefault(x => x.LastName.Equals(txtConsigneeLastName.Text.Trim()) && x.FirstName.Equals(txtConsigneeFirstName.Text.Trim()));
            if (_client != null)
            {
                consignee = _client;
                booking.ConsigneeId = consignee.ClientId;
                booking.Consignee = consignee;
                txtConsigneeAccountNo.Text = consignee.AccountNo;
                if (consignee.CompanyId != null)
                { txtConsigneeCompany.Text = consignee.Company.CompanyName + " - " + consignee.Company.AccountNo; }
                else
                { txtConsigneeCompany.Text = consignee.CompanyName; }

                txtConsigneeAddress1.Text = consignee.Address1;
                txtConsigneeAddress2.Text = consignee.Address2;
                txtConsgineeStreet.Text = consignee.Street;
                txtConsigneeBarangay.Text = consignee.Barangay;
                lstDestinationBco.Text = consignee.City.BranchCorpOffice.BranchCorpOfficeName;
                lstDestinationCity.Text = consignee.City.CityName;
                if (consignee.City != null)
                {
                    lstDestinationBco.SelectedValue = consignee.City.BranchCorpOfficeId;
                    lstDestinationCity.SelectedValue = consignee.City.CityId;
                }
                txtConsigneeContactNo.Text = consignee.ContactNo;
                txtConsigneeMobile.Text = consignee.Mobile;
                txtConsigneeEmail.Text = consignee.Email;
            }
            else
            {
                consignee = new Entities.Client();
                consignee.LastName = txtConsigneeLastName.Text.Trim();
                consignee.FirstName = txtConsigneeFirstName.Text.Trim();
            }
            btnSave.Enabled = true;
        }
        private bool IsDataValid()
        {
            bool isValid = true;

            if (lstAssignedTo.SelectedIndex < 0)
            {
                MessageBox.Show("Invalid Assgined Area", "Booking", MessageBoxButtons.OK);
                lstAssignedTo.Focus();
                isValid = false;
                return isValid;
            }

            if (string.IsNullOrEmpty(txtShipperLastName.Text))
            {
                MessageBox.Show("Invalid Shipper Lastname", "Data Error", MessageBoxButtons.OK);
                txtShipperLastName.Focus();
                isValid = false;
                return isValid;
            }
            if (string.IsNullOrEmpty(txtShipperFirstName.Text))
            {
                MessageBox.Show("Invalid Shipper Firstname", "Data Error", MessageBoxButtons.OK);
                txtShipperFirstName.Focus();
                isValid = false;
                return isValid;
            }
            if (string.IsNullOrEmpty(txtShipperAddress1.Text))
            {
                MessageBox.Show("Invalid Shipper Address", "Data Error", MessageBoxButtons.OK);
                txtShipperAddress1.Focus();
                isValid = false;
                return isValid;
            }
            if (string.IsNullOrEmpty(txtConsigneeLastName.Text))
            {
                MessageBox.Show("Invalid Consignee Lastname", "Data Error", MessageBoxButtons.OK);
                txtConsigneeLastName.Focus();
                isValid = false;
                return isValid;
            }
            if (string.IsNullOrEmpty(txtConsigneeFirstName.Text))
            {
                MessageBox.Show("Invalid Consignee Firstname", "Data Error", MessageBoxButtons.OK);
                txtConsigneeFirstName.Focus();
                isValid = false;
                return isValid;
            }
            if (string.IsNullOrEmpty(txtConsigneeAddress1.Text))
            {
                MessageBox.Show("Invalid Consignee Address", "Data Error", MessageBoxButtons.OK);
                txtConsigneeAddress1.Focus();
                isValid = false;
                return isValid;
            }
            //if (lstAssignedTo.SelectedIndex <= -1)
            //{
            //    MessageBox.Show("Booking is not assigned to an Area.", "Data Error", MessageBoxButtons.OK);
            //    lstAssignedTo.Focus();
            //    isValid = false;
            //    return isValid;
            //}
            if (lstBookingStatus.SelectedIndex <= -1)
            {
                MessageBox.Show("Invalid Booking Status", "Data Error", MessageBoxButtons.OK);
                lstBookingStatus.Focus();
                isValid = false;
                return isValid;
            }

            if (lstOriginBco.SelectedIndex < 0)
            {
                if (lstOriginBco.Text.Trim() == "")
                {
                    MessageBox.Show("Invalid Shipper BCO.", "Data Error", MessageBoxButtons.OK);
                    isValid = false;
                    lstOriginBco.Focus();
                    return isValid;
                }

            }

            if (lstOriginCity.SelectedIndex < 0)
            {
                if (lstOriginCity.Text.Trim() == "")
                {
                    MessageBox.Show("Invalid Shipper City.", "Data Error", MessageBoxButtons.OK);
                    isValid = false;
                    lstOriginCity.Focus();
                    return isValid;
                }

            }

            if (lstDestinationBco.SelectedIndex < 0)
            {
                if (lstDestinationBco.Text == "")
                {
                    MessageBox.Show("Invalid Consignee BCO.", "Data Error", MessageBoxButtons.OK);
                    isValid = false;
                    lstDestinationBco.Focus();
                    return isValid;
                }
            }

            if (lstDestinationCity.SelectedIndex < 0)
            {
                if (lstDestinationCity.Text == "")
                {
                    MessageBox.Show("Invalid Consignee City.", "Data Error", MessageBoxButtons.OK);
                    lstDestinationCity.Focus();
                    isValid = false;
                    return isValid;
                }

            }

            if ((txtShipperContactNo.Text == txtShipperContactNo.Mask) && (txtShipperMobile.Text == txtShipperMobile.Mask))
            {
                MessageBox.Show("Atleast one contact number is required.", "Data Error", MessageBoxButtons.OK);
                txtShipperContactNo.Focus();
                isValid = false;
                return isValid;
            }

            //if ((!txtShipperContactNo.) || (!txtShipperContactNo.MaskedEditBoxElement))
            //{
            //    toolTip1.ToolTipTitle = "Invalid contact number.";
            //    toolTip1.Show("Contact No must be in 000-0000 format.", txtShipperContactNo, -16, -73, 5000);
            //    txtShipperContactNo.Focus();
            //    isValid = false;
            //    return isValid;
            //}
            //if ((!txtShipperMobile.MaskFull) || (!txtShipperMobile.MaskCompleted))
            //{
            //    toolTip1.ToolTipTitle = "Invalid Shipper number.";
            //    toolTip1.Show("Mobile No must be in (000)000-0000 format.", txtShipperMobile, -16, -73, 5000);
            //    txtShipperMobile.Focus();
            //    isValid = false;
            //    return isValid;
            //}

            if ((txtConsigneeContactNo.Text == txtConsigneeContactNo.Mask) && (txtConsigneeMobile.Text == txtConsigneeMobile.Mask))
            {
                MessageBox.Show("Atleast one contact number is required.", "Data Error", MessageBoxButtons.OK);
                txtConsigneeContactNo.Focus();
                isValid = false;
                return isValid;
            }

            //if ((!txtConsigneeContactNo.MaskFull) || (!txtConsigneeContactNo.MaskCompleted))
            //{
            //    toolTip1.ToolTipTitle = "Invalid Consignee number.";
            //    toolTip1.Show("Contact No must be in 000-0000 format.", txtConsigneeContactNo, -16, -73, 5000);
            //    txtConsigneeContactNo.Focus();
            //    isValid = false;
            //    return isValid;
            //}

            //if ((!txtConsigneeMobile.MaskFull) || (!txtConsigneeMobile.MaskCompleted))
            //{
            //    toolTip1.ToolTipTitle = "Invalid Consignee number.";
            //    toolTip1.Show("Mobile No must be in (000)000-0000 format.", txtConsigneeMobile, -16, -73, 5000);
            //    txtConsigneeMobile.Focus();
            //    isValid = false;
            //    return isValid;
            //}

            //if (!isMailValid(txtShipperEmail.Text))
            //{
            //    toolTip1.ToolTipTitle = "Invalid email address.";
            //    toolTip1.Show("Email must be in correct format.", txtShipperEmail, -16, -73, 5000);
            //    txtShipperEmail.Focus();
            //    isValid = false;
            //    return isValid;
            //}

            //if (!isMailValid(txtConsigneeEmail.Text))
            //{
            //    toolTip1.ToolTipTitle = "Invalid email address.";
            //    toolTip1.Show("Email must be in correct format.", txtConsigneeEmail, -16, -73, 5000);
            //    txtConsigneeEmail.Focus();
            //    isValid = false;
            //    return isValid;
            //}
            if (txtConsigneeEmail.Text.Trim() == "")
            {
                txtConsigneeEmail.Focus();
                isValid = false;
                return isValid;
            }
            if (txtShipperEmail.Text.Trim() == "")
            {
                txtShipperEmail.Focus();
                isValid = false;
                return isValid;
            }

            if (txtConsigneeCompany.Text.Trim() == "")
            {
                txtConsigneeCompany.Focus();
                isValid = false;
                return isValid;
            }
            if (txtShipperCompany.Text.Trim() == "")
            {
                txtShipperCompany.Focus();
                isValid = false;
                return isValid;
            }
            return isValid;
        }
        private Boolean IsNumericOnly(int intMin, int intMax, string strNumericOnly)
        {
            Boolean blnResult = false;
            Regex regexPattern = new Regex("^[0-9\\s]{" + intMin.ToString() + "," + intMax.ToString() + "}$");
            if ((strNumericOnly.Length >= intMin & strNumericOnly.Length <= intMax))
            {
                // check here if there are other none alphanumeric characters
                strNumericOnly = strNumericOnly.Trim();
                blnResult = regexPattern.IsMatch(strNumericOnly);
            }
            else
            {
                blnResult = false;
            }
            return blnResult;
        }
        private void SaveBooking()
        {
            if (IsDataValid())
            {
                #region ShipperInfo
                shipper.CreatedBy = AppUser.User.UserId;
                shipper.CreatedDate = DateTime.Now;
                shipper.ModifiedBy = AppUser.User.UserId;
                shipper.ModifiedDate = DateTime.Now;
                shipper.RecordStatus = (int)RecordStatus.Active;
                if (shipper.CompanyId == null)
                {
                    Company company = companies.Find(x => x.CompanyName == txtShipperCompany.Text.Trim());
                    if (company != null)
                    {
                        shipper.Company = company;
                        shipper.CompanyId = company.CompanyId;
                    }
                    else
                    {
                        shipper.CompanyName = txtShipperCompany.Text.Trim();
                    }
                }

                shipper.Address1 = txtShipperAddress1.Text.Trim();
                shipper.Address2 = txtShipperAddress2.Text.Trim();
                shipper.Street = txtShipperStreet.Text.Trim();
                shipper.Barangay = txtShipperBarangay.Text.Trim();
                if (lstOriginCity.SelectedIndex >= 0)
                {
                    shipper.CityId = Guid.Parse(lstOriginCity.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Invalid Shipper City.", "Data Error", MessageBoxButtons.OK);
                    return;
                }

                shipper.ContactNo = txtShipperContactNo.Text.Trim();
                shipper.Mobile = txtShipperMobile.Text.Trim();
                shipper.Email = txtShipperEmail.Text.Trim();

                #endregion

                #region ConsingnessInfo
                consignee.CreatedBy = AppUser.User.UserId;
                consignee.CreatedDate = DateTime.Now;
                consignee.ModifiedBy = AppUser.User.UserId;
                consignee.ModifiedDate = DateTime.Now;
                consignee.RecordStatus = (int)RecordStatus.Active;

                if (consignee.CompanyId == null)
                {
                    Company consigneeCompany = companies.Find(x => x.CompanyName == txtConsigneeCompany.Text.Trim());
                    if (consigneeCompany != null)
                    {
                        consignee.Company = consigneeCompany;
                        consignee.CompanyId = consigneeCompany.CompanyId;
                    }
                    else
                    {
                        consignee.CompanyName = txtConsigneeCompany.Text.Trim();
                    }
                }

                consignee.Address1 = txtConsigneeAddress1.Text.Trim();
                consignee.Address2 = txtConsigneeAddress2.Text.Trim();
                consignee.Street = txtConsgineeStreet.Text.Trim();
                consignee.Barangay = txtConsigneeBarangay.Text.Trim();
                if (lstDestinationCity.SelectedIndex >= 0)
                {
                    consignee.CityId = Guid.Parse(lstDestinationCity.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Invalid Consignee City.", "Data Error", MessageBoxButtons.OK);
                    return;
                }
                consignee.ContactNo = txtConsigneeContactNo.Text.Trim();
                consignee.Mobile = txtConsigneeMobile.Text.Trim();
                consignee.Email = txtConsigneeEmail.Text.Trim();
                //if (consignee.CompanyId == null)

                #endregion

                #region CaptureBookingInput
                booking.OriginAddress1 = txtShipperAddress1.Text.Trim();
                booking.OriginAddress2 = txtShipperAddress2.Text.Trim();
                booking.OriginStreet = txtShipperStreet.Text.Trim();
                booking.OriginBarangay = txtShipperBarangay.Text.Trim();
                booking.OriginCityId = Guid.Parse(lstOriginCity.SelectedValue.ToString());
                booking.DestinationAddress1 = txtConsigneeAddress1.Text.Trim();
                booking.DestinationAddress2 = txtConsigneeAddress2.Text.Trim();
                booking.DestinationStreet = txtConsgineeStreet.Text.Trim();
                booking.DestinationBarangay = txtConsigneeBarangay.Text.Trim();
                booking.DestinationCityId = Guid.Parse(lstDestinationCity.SelectedValue.ToString());
                booking.DateBooked = dateDateBooked.Value;
                booking.Remarks = txtRemarks.Text;
                booking.HasDailyBooking = chkHasDailyBooking.Checked;
                if (lstAssignedTo.SelectedIndex > -1)
                {
                    booking.AssignedToAreaId = Guid.Parse(lstAssignedTo.SelectedValue.ToString());
                }

                booking.BookingStatusId = Guid.Parse(lstBookingStatus.SelectedValue.ToString());
                if (lstBookingRemarks.SelectedValue != null)
                    booking.BookingRemarkId = Guid.Parse(lstBookingRemarks.SelectedValue.ToString());
                booking.ModifiedBy = AppUser.User.UserId;
                booking.ModifiedDate = DateTime.Now;
                booking.RecordStatus = (int)RecordStatus.Active;
                if (booking.BookingId == null || booking.BookingId == Guid.Empty)
                {
                    booking.BookingId = Guid.NewGuid();
                    booking.CreatedBy = AppUser.User.UserId;
                    booking.CreatedDate = DateTime.Now;
                }
                #endregion

                ProgressIndicator saving = new ProgressIndicator("Booking", "Saving ...", Saving);
                saving.ShowDialog();

                //if (booking.AssignedToAreaId == null || booking.AssignedToAreaId == Guid.Empty)
                if (booking.AssignedToArea.RevenueUnitName.Contains("Walk"))
                {
                    BookingSelected(booking.BookingId);
                    NewShipment();
                }
                else
                {
                    PopulateGrid();
                    BookingResetAll();
                }

                booking = null;
                shipper = null;
                consignee = null;
            }
        }
        private void Saving(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 2; // # of processes

            #region NewClient
            if (shipper.ClientId == null || shipper.ClientId == Guid.Empty)
            {
                shipper.ClientId = Guid.NewGuid();
                if (shipper.City == null)
                    shipper.City = cities.FirstOrDefault(x => x.CityId == shipper.CityId);
                if (shipper.CompanyId == null)
                {
                    // non-rep client account #
                    shipper.AccountNo = clientService.GetNewAccountNo(shipper.City.BranchCorpOffice.BranchCorpOfficeCode, false);
                }
                else
                {
                    shipper.AccountNo = clientService.GetNewAccountNo(shipper.City.BranchCorpOffice.BranchCorpOfficeCode, false);
                }
                clientService.Add(shipper);
                booking.ShipperId = shipper.ClientId;
            }

            if (consignee.ClientId == null || consignee.ClientId == Guid.Empty)
            {
                consignee.ClientId = Guid.NewGuid();
                if (consignee.City == null)
                    consignee.City = cities.FirstOrDefault(x => x.CityId == consignee.CityId);
                if (consignee.CompanyId == null)
                {
                    // non-rep client account #
                    consignee.AccountNo = clientService.GetNewAccountNo(consignee.City.BranchCorpOffice.BranchCorpOfficeCode, false); //"1" 
                }
                else
                {
                    consignee.AccountNo = clientService.GetNewAccountNo(consignee.City.BranchCorpOffice.BranchCorpOfficeCode, false);//"1" +
                }
                //consignee.AccountNo = clientService.GetNewAccountNo(consignee.City.BranchCorpOffice.BranchCorpOfficeCode, false);

                clientService.Add(consignee);
                booking.ConsigneeId = consignee.ClientId;

            }
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;

            #endregion

            #region SaveBooking
            bookingService.AddEdit(booking);

            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;
            #endregion

        }
        private void SelectedDestinationCity(Guid bcoId)
        {
            List<City> _cities = cities.Where(x => x.BranchCorpOffice.BranchCorpOfficeId == bcoId).OrderBy(x => x.CityName).ToList();
            lstDestinationCity.DataSource = _cities;
            lstDestinationCity.DisplayMember = "CityName";
            lstDestinationCity.ValueMember = "CityId";
        }
        private void SelectedOriginCity(Guid bcoId)
        {
            List<City> _cities = cities.Where(x => x.BranchCorpOffice.BranchCorpOfficeId == bcoId).OrderBy(x => x.CityName).ToList();  // x => cityIds.Contains(x.CityId)).OrderBy(x => x.CityName).ToList();
            lstOriginCity.DataSource = _cities;
            lstOriginCity.DisplayMember = "CityName";
            lstOriginCity.ValueMember = "CityId";

        }
        private bool ValidateData()
        {
            if (lstCommodityType.SelectedIndex <= -1 || lstCommodity.SelectedIndex <= -1 || lstGoodsDescription.SelectedIndex <= -1 ||
                lstServiceType.SelectedIndex <= -1 || lstServiceMode.SelectedIndex <= -1 || lstShipMode.SelectedIndex <= -1 || lstPaymentMode.SelectedIndex <= -1)
            {
                return false;
            }

            if (lstShipMode.SelectedText == "Transhipment")
            {
                if (lstHub.SelectedIndex <= -1)
                {
                    return false;
                }

            }

            if (shipment.PackageDimensions == null)
            {
                return false;
            }
            else
            {
                if (shipment.PackageDimensions.Count <= 0)
                {
                    return false;
                }
            }
            return true;

        }
        #endregion

        #region Acceptance

        private void AcceptanceLoadInit()
        {
            shipment = new ShipmentModel();
            shipment.PackageDimensions = new List<PackageDimensionModel>();

            bsCommodityType = new BindingSource();
            bsCommodity = new BindingSource();
            bsServiceType = new BindingSource();
            bsServiceMode = new BindingSource();
            bsPaymentMode = new BindingSource();
            bsCrating = new BindingSource();
            bsPackaging = new BindingSource();
            bsGoodsDescription = new BindingSource();
            bsShipMode = new BindingSource();
            bsTranshipmentLeg = new BindingSource();
            bsRevenueUnits = new BindingSource();

            commodityTypes = new List<CommodityType>();
            commodities = new List<Commodity>();
            serviceTypes = new List<ServiceType>();
            serviceModes = new List<ServiceMode>();
            paymentModes = new List<PaymentMode>();
            shipmentBasicFees = new List<ShipmentBasicFee>();
            cratings = new List<Crating>();
            packagings = new List<Packaging>();
            goodsDescriptions = new List<GoodsDescription>();
            shipModes = new List<ShipMode>();
            transShipmentLegs = new List<TransShipmentLeg>();
            paymentTerms = new List<PaymentTerm>();

            applicableRateService = new ApplicableRateBL();
            commodityTypeService = new CommodityTypeBL(GlobalVars.UnitOfWork);
            commodityService = new CommodityBL(GlobalVars.UnitOfWork);
            serviceTypeService = new ServiceTypeBL(GlobalVars.UnitOfWork);
            serviceModeService = new ServiceModeBL(GlobalVars.UnitOfWork);
            paymentModeService = new PaymentModeBL(GlobalVars.UnitOfWork);
            shipmentService = new ShipmentBL(GlobalVars.UnitOfWork);
            bookingService = new BookingBL(GlobalVars.UnitOfWork);
            bookingStatusService = new BookingStatusBL(GlobalVars.UnitOfWork);
            shipmentBasicFeeService = new ShipmentBasicFeeBL(GlobalVars.UnitOfWork);
            cratingService = new CratingBL(GlobalVars.UnitOfWork);
            packagingService = new PackagingBL(GlobalVars.UnitOfWork);
            goodsDescriptionService = new GoodsDescriptionBL(GlobalVars.UnitOfWork);
            shipModeService = new ShipModeBL(GlobalVars.UnitOfWork);
            transShipmentLegService = new TransShipmentLegBL(GlobalVars.UnitOfWork);
            rateMatrixService = new RateMatrixBL(GlobalVars.UnitOfWork);
            paymentTermService = new PaymentTermBL(GlobalVars.UnitOfWork);

            LogPath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\";

        }
        private void AcceptanceLoadData()
        {
            if (commodityTypes.Count == 0)
            {
                commodityTypes = commodityTypeService.FilterActive().OrderBy(x => x.CommodityTypeName).ToList();
            }
            if (commodities.Count == 0)
            {
                commodities = commodityService.FilterActive().OrderBy(x => x.CommodityName).ToList();
            }
            if (serviceTypes.Count == 0)
            {
                serviceTypes = serviceTypeService.FilterActive().OrderBy(x => x.ServiceTypeName).ToList();
            }
            if (serviceModes.Count == 0)
            {
                serviceModes = serviceModeService.FilterActive().OrderBy(x => x.ServiceModeName).ToList();
            }
            if (paymentModes.Count == 0)
            {
                paymentModes = paymentModeService.FilterActive().OrderBy(x => x.PaymentModeName).ToList();
            }
            if (shipmentBasicFees.Count == 0)
            {
                shipmentBasicFees = shipmentBasicFeeService.FilterActive();
            }
            if (cratings.Count == 0)
            {
                cratings = cratingService.FilterActive().OrderBy(x => x.CratingName).ToList();
            }
            if (packagings.Count == 0)
            {
                packagings = packagingService.FilterActive().OrderBy(x => x.PackagingName).ToList();
            }
            if (goodsDescriptions.Count == 0)
            {
                goodsDescriptions = goodsDescriptionService.FilterActive().OrderBy(x => x.GoodsDescriptionName).ToList();
            }
            if (shipModes.Count == 0)
            {
                shipModes = shipModeService.FilterActive().OrderBy(x => x.ShipModeName).ToList();
            }
            if (transShipmentLegs.Count == 0)
            {
                transShipmentLegs = transShipmentLegService.FilterActive().OrderBy(x => x.LegOrder).ToList();
            }
            if (paymentTerms.Count == 0)
            {
                paymentTerms = paymentTermService.FilterActive().OrderBy(x => x.PaymentTermName).ToList();
            }

            bsCommodityType.DataSource = commodityTypes;
            bsCommodity.DataSource = commodities;
            bsServiceType.DataSource = serviceTypes;
            bsServiceMode.DataSource = serviceModes;
            bsPaymentMode.DataSource = paymentModes;
            bsCrating.DataSource = cratings;
            bsPackaging.DataSource = packagings;
            bsGoodsDescription.DataSource = goodsDescriptions;
            bsShipMode.DataSource = shipModes;
            bsTranshipmentLeg.DataSource = transShipmentLegs;

            dateAcceptedDate.Value = DateTime.Now;

            btnSave.Enabled = false;
            btnPrint.Enabled = false;
            btnPayment.Enabled = false;

            lstCommodityType.DataSource = bsCommodityType;
            lstCommodityType.DisplayMember = "CommodityTypeName";
            lstCommodityType.ValueMember = "CommodityTypeId";

            lstCommodity.DataSource = bsCommodity;
            lstCommodity.DisplayMember = "CommodityName";
            lstCommodity.ValueMember = "CommodityId";

            lstServiceType.DataSource = bsServiceType;
            lstServiceType.DisplayMember = "ServiceTypeName";
            lstServiceType.ValueMember = "ServiceTypeId";

            lstServiceMode.DataSource = bsServiceMode;
            lstServiceMode.DisplayMember = "ServiceModeName";
            lstServiceMode.ValueMember = "ServiceModeId";

            lstPaymentMode.DataSource = bsPaymentMode;
            lstPaymentMode.DisplayMember = "PaymentModeName";
            lstPaymentMode.ValueMember = "PaymentModeId";

            lstCrating.DataSource = bsCrating;
            lstCrating.DisplayMember = "CratingName";
            lstCrating.ValueMember = "CratingId";

            lstGoodsDescription.DataSource = bsGoodsDescription;
            lstGoodsDescription.DisplayMember = "GoodsDescriptionName";
            lstGoodsDescription.ValueMember = "GoodsDescriptionId";

            lstShipMode.DataSource = bsShipMode;
            lstShipMode.DisplayMember = "ShipModeName";
            lstShipMode.ValueMember = "ShipModeId";

            lstHub.DataSource = bsTranshipmentLeg;
            lstHub.DisplayMember = "LegName";
            lstHub.ValueMember = "TransShipmentLegId";

            //bsCommodityType.ResetBindings(false);
            //bsCommodity.ResetBindings(false);
            //bsServiceType.ResetBindings(false);
            //bsServiceMode.ResetBindings(false);
            //bsPaymentMode.ResetBindings(false);
            //bsCrating.ResetBindings(false);
            //bsPackaging.ResetBindings(false);
            //bsGoodsDescription.ResetBindings(false);
            //bsShipMode.ResetBindings(false);
            //bsTranshipmentLeg.ResetBindings(false);

            lstCommodityType.SelectedIndex = -1;
            lstCommodity.SelectedIndex = -1;
            lstCrating.SelectedIndex = -1;
            lstShipMode.SelectedIndex = -1;
            lstGoodsDescription.SelectedIndex = -1;
            chkNonVatable.Checked = false;
            lstHub.SelectedIndex = -1;

            DisableForm();
            ShowNewShipment();
        }
        public void ShowNewShipment()
        {
            if (shipmentModel != null)
            {
                shipment = new ShipmentModel();
                shipment.ShipmentId = Guid.NewGuid();
                shipment.OriginCityId = shipmentModel.OriginCityId;
                shipment.OriginCity = shipmentModel.OriginCity;
                shipment.DestinationCityId = shipmentModel.DestinationCityId;
                shipment.DestinationCity = shipmentModel.DestinationCity;
                shipment.ShipperId = shipmentModel.ShipperId;
                shipment.Shipper = shipmentModel.Shipper;
                shipment.OriginAddress = shipmentModel.OriginAddress;
                shipment.ConsigneeId = shipmentModel.ConsigneeId;
                shipment.Consignee = shipmentModel.Consignee;
                shipment.DestinationAddress = shipmentModel.DestinationAddress;
                shipment.BookingId = shipmentModel.BookingId;

                AcceptancePopulateForm();
                DisableForm();

                AcceptancetxtAirwayBill.Focus();
                // btnSearchShipment.Enabled = false;
                btnAcceptanceEdit.Enabled = false;
            }
            else
            {
                AcceptancetxtAirwayBill.Focus();
                //btnSearchShipment.Enabled = true;
            }
        }
        private void SaveShipment(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 2; // # of processes

            #region SaveShipment

            shipmentService.AddEdit(shipment);
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;

            #endregion

            #region UpdateBookingStatus

            var booking = bookingService.GetById(shipment.BookingId);
            if (booking != null)
            {
                var bookingStatus = bookingStatusService.FilterActiveBy(x => x.BookingStatusName.Equals("Picked-up"));
                if (bookingStatus != null)
                {
                    booking.BookingStatusId = bookingStatus.FirstOrDefault().BookingStatusId;
                    booking.ModifiedBy = AppUser.User.UserId;
                    booking.ModifiedDate = DateTime.Now;
                    bookingService.Edit(booking);
                }
            }
            percent = index * 100 / max;
            _worker.ReportProgress(percent);
            index++;

            #endregion
        }
        private void AcceptanceResetAll()
        {
            dateAcceptedDate.Value = DateTime.Now;
            AcceptancetxtShipperAccountNo.Text = "";
            AcceptancetxtShipperFullName.Text = "";
            AcceptancetxtShipperCompany.Text = "";
            AcceptancetxtShipperAddress.Text = "";
            AcceptancetxtShipperBarangay.Text = "";
            AcceptancetxtShipperCity.Text = "";
            AcceptancetxtShipperContactNo.Value = AcceptancetxtShipperContactNo.Mask;
            AcceptancetxtShipperMobile.Value = AcceptancetxtShipperMobile.Mask;
            AcceptancetxtShipperEmail.Text = "";
            AcceptancetxtConsigneeAccountNo.Text = "";
            AcceptancetxtConsigneeFullName.Text = "";
            AcceptancetxtConsigneeCompany.Text = "";
            AcceptancetxtConsigneeAddress.Text = "";
            AcceptancetxtConsigneeBarangay.Text = "";
            AcceptancetxtConsigneeCity.Text = "";
            AcceptancetxtConsigneeContactNo.Value = AcceptancetxtConsigneeContactNo.Mask;
            AcceptancetxtConsingneeMobile.Value = AcceptancetxtConsingneeMobile.Mask;
            AcceptancetxtConsigneeEmail.Text = "";
            AcceptancetxtAirwayBill.Text = "";
            txtQuantity.Text = "1";
            txtWeight.Text = "0";
            txtWidth.Text = "0";
            txtLength.Text = "0";
            txtHeight.Text = "0";
            txtTotalEvm.Text = "";
            txtTotalWeightCharge.Text = "";
            gridPackage.DataSource = null;
            txtDeclaredValue.Text = "";
            txtTotalEvm.Text = "";
            txtTotalWeightCharge.Text = "";
            txtRfa.Text = "";
            txtHandlingFee.Text = "";
            txtQuarantineFee.Text = "";


            shipment.PackageDimensions = new List<PackageDimensionModel>();
            shipment = new ShipmentModel();

            if (lstCommodityType.Items.Count > 0)
                lstCommodityType.SelectedIndex = -1;
            if (lstCommodity.Items.Count > 0)
                lstCommodity.SelectedIndex = -1;
            if (lstServiceMode.Items.Count > 0)
                lstServiceMode.SelectedIndex = -1;
            if (lstPaymentMode.Items.Count > 0)
                lstPaymentMode.SelectedIndex = -1;
            if (lstServiceType.Items.Count > 0)
                lstServiceType.SelectedIndex = -1;
            if (lstShipMode.Items.Count > 0)
                lstShipMode.SelectedIndex = -1;
            if (lstHub.Items.Count > 0)
                lstHub.SelectedIndex = -1;
            if (lstGoodsDescription.Items.Count > 0)
                lstGoodsDescription.SelectedIndex = -1;

            txtNotes.Text = "";
            btnCompute.Enabled = false;
            btnAcceptanceSave.Enabled = false;
            btnAcceptanceEdit.Enabled = false;
            btnPrint.Enabled = false;
            btnAcceptanceEdit.Enabled = false;
            btnPayment.Enabled = false;
            btnSearchShipment.Enabled = true;

            chkNonVatable.Checked = false;

        }
        private void ComputeCharges()
        {
            shipment.DateAccepted = dateAcceptedDate.Value;
            shipment.CommodityTypeId = Guid.Parse(lstCommodityType.SelectedValue.ToString());
            shipment.CommodityType = commodityTypes.Find(x => x.CommodityTypeId == shipment.CommodityTypeId);
            shipment.ServiceTypeId = Guid.Parse(lstServiceType.SelectedValue.ToString());
            shipment.ServiceType = serviceTypes.Find(x => x.ServiceTypeId == shipment.ServiceTypeId);
            shipment.ServiceModeId = Guid.Parse(lstServiceMode.SelectedValue.ToString());
            shipment.ServiceMode = serviceModes.Find(x => x.ServiceModeId == shipment.ServiceModeId);
            shipment.ShipModeId = Guid.Parse(lstShipMode.SelectedValue.ToString());
            shipment.ShipMode = shipModes.Find(x => x.ShipModeId == shipment.ShipModeId);
            if (shipment.ShipMode.ShipModeName == "Transhipment")
            {
                shipment.TransShipmentLegId = Guid.Parse(lstHub.SelectedValue.ToString());
                shipment.TransShipmentLeg = transShipmentLegs.Find(x => x.TransShipmentLegId == shipment.TransShipmentLegId);
            }
            if (shipment.GoodsDescriptionId == null || shipment.GoodsDescription == null)
            {
                if (lstGoodsDescription.SelectedValue == null)
                {
                    lstGoodsDescription.SelectedIndex = 0;
                }
                shipment.GoodsDescriptionId = Guid.Parse(lstGoodsDescription.SelectedValue.ToString());
                shipment.GoodsDescription =
                    goodsDescriptions.Find(x => x.GoodsDescriptionId == shipment.GoodsDescriptionId);
            }

            if (shipment.PaymentModeId == null || shipment.PaymentMode == null)
            {
                if (lstPaymentMode.SelectedValue == null)
                {
                    lstPaymentMode.SelectedIndex = 0;
                }
                shipment.PaymentModeId = Guid.Parse(lstPaymentMode.SelectedValue.ToString());
                shipment.PaymentMode = paymentModes.Find(x => x.PaymentModeId == shipment.PaymentModeId);
            }

            try
            {
                shipment.DeclaredValue = Decimal.Parse(txtDeclaredValue.Value.ToString().Replace("₱", ""));
                shipment.HandlingFee = Decimal.Parse(txtHandlingFee.Value.ToString().Replace("₱", ""));
                shipment.QuarantineFee = Decimal.Parse(txtQuarantineFee.Value.ToString().Replace("₱", ""));
                shipment.Discount = Decimal.Parse(txtRfa.Value.ToString());
            }
            catch (Exception ex)
            {
                shipment.DeclaredValue = Decimal.Parse(txtDeclaredValue.Value.ToString().Replace("Php", ""));
                shipment.HandlingFee = Decimal.Parse(txtHandlingFee.Value.ToString().Replace("Php", ""));
                shipment.QuarantineFee = Decimal.Parse(txtQuarantineFee.Value.ToString().Replace("Php", ""));
                shipment.Discount = Decimal.Parse(txtRfa.Value.ToString());
            }

            if (shipment.Shipper != null)
            {
                if (shipment.Shipper.Company != null)
                {
                    shipment.Discount = shipment.Shipper.Company.Discount;
                }
            }

            if (chkNonVatable.Checked)
            {
                shipment.IsVatable = false;
            }
            else
            {
                shipment.IsVatable = true;
            }

            shipment = shipmentService.ComputeCharges(shipment);
            PopulateSummary();
        }
        private void AcceptancePopulateForm()
        {
            if (shipment != null)
            {
                if (shipment.Shipper != null)
                {
                    AcceptancetxtShipperAccountNo.Text = shipment.Shipper.AccountNo;
                    AcceptancetxtShipperFullName.Text = shipment.Shipper.LastName + ", " + shipment.Shipper.FirstName;
                    if (shipment.Shipper.CompanyId != null)
                    {
                        AcceptancetxtShipperCompany.Text = shipment.Shipper.Company.CompanyName;
                    }
                    else
                    {
                        AcceptancetxtShipperCompany.Text = shipment.Shipper.CompanyName;
                    }
                    AcceptancetxtShipperAddress.Text = shipment.Shipper.Address1 + ", " + shipment.Shipper.Address2;
                    AcceptancetxtShipperBarangay.Text = shipment.Shipper.Barangay;
                    AcceptancetxtShipperContactNo.Text = shipment.Shipper.ContactNo;
                    AcceptancetxtShipperMobile.Text = shipment.Shipper.Mobile;
                    AcceptancetxtShipperEmail.Text = shipment.Shipper.Email;
                }
                if (shipment.OriginCity != null)
                {
                    AcceptancetxtShipperCity.Text = shipment.OriginCity.CityName;
                }
                else
                {
                    AcceptancetxtShipperCity.Text = "N/A";
                }


                if (shipment.Consignee != null)
                {
                    AcceptancetxtConsigneeAccountNo.Text = shipment.Consignee.AccountNo;
                    AcceptancetxtConsigneeFullName.Text = shipment.Consignee.LastName + ", " + shipment.Consignee.FirstName;
                    if (shipment.Consignee.CompanyId != null)
                    {
                        AcceptancetxtConsigneeCompany.Text = shipment.Consignee.Company.CompanyName;
                    }
                    else
                    {
                        AcceptancetxtConsigneeCompany.Text = shipment.Consignee.CompanyName;
                    }
                    AcceptancetxtConsigneeAddress.Text = shipment.Consignee.Address1 + ", " + shipment.Consignee.Address2;
                    AcceptancetxtConsigneeBarangay.Text = shipment.Consignee.Barangay;
                    AcceptancetxtConsigneeContactNo.Text = shipment.Consignee.ContactNo;
                    AcceptancetxtConsingneeMobile.Text = shipment.Consignee.Mobile;
                    AcceptancetxtConsigneeEmail.Text = shipment.Consignee.Email;
                }

                if (shipment.OriginCity != null)
                {
                    AcceptancetxtConsigneeCity.Text = shipment.DestinationCity.CityName;
                }
                else
                {
                    AcceptancetxtConsigneeCity.Text = "N/A";
                }
                lstServiceType.SelectedIndex = -1;
                lstServiceMode.SelectedIndex = -1;
                lstPaymentMode.SelectedIndex = -1;
                if (shipment.CommodityTypeId != null && shipment.CommodityTypeId != Guid.Empty)
                    lstCommodityType.SelectedValue = shipment.CommodityTypeId;
                if (shipment.CommodityId != null && shipment.CommodityId != Guid.Empty)
                    lstCommodity.SelectedValue = shipment.CommodityId;
                if (shipment.ServiceModeId != null && shipment.ServiceModeId != Guid.Empty)
                    lstServiceMode.SelectedValue = shipment.ServiceModeId;
                if (shipment.PaymentModeId != null && shipment.ServiceModeId != Guid.Empty)
                    lstPaymentMode.SelectedValue = shipment.PaymentModeId;
                if (shipment.ServiceTypeId != null && shipment.ServiceTypeId != Guid.Empty)
                    lstServiceType.SelectedValue = shipment.ServiceTypeId;
                if (shipment.ShipModeId != null && shipment.ShipModeId != Guid.Empty)
                {
                    lstShipMode.SelectedValue = shipment.ShipModeId;
                }
                if (shipment.TransShipmentLeg != null && shipment.TransShipmentLegId != Guid.Empty)
                {
                    lstHub.SelectedValue = transShipmentLegService.GetById(shipment.TransShipmentLegId).TransShipmentLegId;
                }
                if (shipment.GoodsDescriptionId != null && shipment.GoodsDescriptionId != Guid.Empty)
                    lstGoodsDescription.SelectedValue = shipment.GoodsDescriptionId;

                txtQuantity.Text = shipment.Quantity.ToString();
                txtWeight.Text = shipment.Weight.ToString("N");
                txtDeclaredValue.Text = shipment.DeclaredValueString;
                txtHandlingFee.Text = shipment.HandlingFeeString;
                txtQuarantineFee.Text = shipment.QuanrantineFeeString;
                txtRfa.Text = (shipment.Discount * (Decimal)(.100)).ToString();
                txtNotes.Text = shipment.Notes;
                chkNonVatable.Checked = false;
                if (!shipment.IsVatable)
                {
                    chkNonVatable.Checked = true;
                }
                shipment.AcceptedAreaId = AppUser.User.Employee.AssignedToAreaId;
                shipment.AcceptedArea = AppUser.Employee.AssignedToArea;

            }

        }
        private void PopulateSummary()
        {
            txtSumChargeableWeight.Text = shipment.ChargeableWeightString;
            txtSumWeightCharge.Text = shipment.WeightChargeString;
            if (shipment.AwbFee != null)
            {
                txtSumAwbFee.Text = shipment.AwbFee.Amount.ToString("N");
            }
            else
            {
                txtSumAwbFee.Text = "0.00";
            }
            txtSumValuation.Text = "0.00";
            txtSumValuation.Text = shipment.ValuationAmountString;
            if (shipment.DeliveryFee != null)
                txtSumDeliveryFee.Text = shipment.DeliveryFee.AmountString;
            else
            {
                txtSumDeliveryFee.Text = "0.00";
            }
            if (shipment.FreightCollectCharge != null)
            {
                txtSumFreightCollect.Text = shipment.FreightCollectCharge.Amount.ToString("N");
            }
            else
            {
                txtSumFreightCollect.Text = "0.00";
            }
            if (shipment.PeracFee != null)
            {
                txtSumPeracFee.Text = shipment.PeracFee.Amount.ToString("N");
            }
            else
            {
                txtSumPeracFee.Text = "0.00";
            }
            if (shipment.DangerousFee != null)
            {
                txtSumDangerousFee.Text = shipment.DangerousFee.AmountString;
            }
            else
            {
                txtSumDangerousFee.Text = "0.00";
            }
            txtSumFuelSurcharge.Text = shipment.FuelSurchargeAmountstring;
            txtSumCratingFee.Text = shipment.CratingFeeString;
            txtSumDrainingFee.Text = shipment.DrainingFeeString;
            txtSumPackagingFee.Text = shipment.PackagingFeeString;
            txtSumHandlingFee.Text = shipment.HandlingFeeString;
            txtSumQuarantineFee.Text = shipment.QuanrantineFeeString;
            txtSumDiscount.Text = shipment.DiscountAmountString;
            if (shipment.Insurance != null)
            {
                txtSumInsurance.Text = shipment.InsuranceAmountString;
            }
            else
            {
                txtSumInsurance.Text = "0.00";
            }
            if (chkNonVatable.Checked)
            {
                txtSumVatAmount.Text = "0.00";
            }
            else
            {
                txtSumVatAmount.Text = shipment.ShipmentVatAmountString;
            }
            txtSumSubTotal.Text = shipment.ShipmentSubTotalString;
            txtSumTotal.Text = shipment.ShipmentTotalString;
        }
        private void AddPackage()
        {
            if (shipment.PackageDimensions == null)
                shipment.PackageDimensions = new List<PackageDimensionModel>();

            if (shipment.CommodityTypeId == null || shipment.CommodityType == null)
            {
                lstCommodityType.Focus();
                return;
            }

            if (shipment.ServiceTypeId == null || shipment.ServiceType == null)
            {
                if (lstServiceType.SelectedValue == null)
                {
                    lstServiceType.SelectedIndex = 0;
                }
                shipment.ServiceTypeId = Guid.Parse(lstServiceType.SelectedValue.ToString());
                shipment.ServiceType = serviceTypes.Find(x => x.ServiceTypeId == shipment.ServiceTypeId);
            }

            if (shipment.ServiceModeId == null || shipment.ServiceMode == null)
            {
                if (lstServiceMode.SelectedValue == null)
                {
                    lstServiceMode.SelectedIndex = 0;
                }
                shipment.ServiceModeId = Guid.Parse(lstServiceMode.SelectedValue.ToString());
                shipment.ServiceMode = serviceModes.Find(x => x.ServiceModeId == shipment.ServiceModeId);
            }

            if (shipment.ShipModeId == null || shipment.ShipMode == null)
            {
                if (lstShipMode.SelectedValue == null)
                {
                    lstShipMode.SelectedIndex = 0;
                }
                shipment.ShipModeId = Guid.Parse(lstShipMode.SelectedValue.ToString());
                shipment.ShipMode = shipModes.Find(x => x.ShipModeId == shipment.ShipModeId);
            }

            if (shipment.ShipMode.ShipModeName == "Transhipment")
            {
                shipment.TransShipmentLegId = Guid.Parse(lstHub.SelectedValue.ToString());
                shipment.TransShipmentLeg = transShipmentLegs.Find(x => x.TransShipmentLegId == shipment.TransShipmentLegId);
            }

            if (shipment.PaymentModeId == null || shipment.PaymentMode == null)
            {
                if (lstPaymentMode.SelectedValue == null)
                {
                    lstPaymentMode.SelectedIndex = 0;
                }
                shipment.PaymentModeId = Guid.Parse(lstPaymentMode.SelectedValue.ToString());
                shipment.PaymentMode = paymentModes.Find(x => x.PaymentModeId == shipment.PaymentModeId);
            }

            try
            {
                shipment.Quantity = Int32.Parse(txtQuantity.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Quantity.", "Data Error", MessageBoxButtons.OK);
                txtQuantity.Text = "1";
                txtQuantity.Focus();
                return;
            }
            if (shipment.Quantity <= 0)
            {
                MessageBox.Show("Invalid Quantity.", "Data Error", MessageBoxButtons.OK);
                txtQuantity.Text = "1";
                txtQuantity.Focus();
                return;
            }
            try
            {
                shipment.Weight = Decimal.Parse(txtWeight.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Weight.", "Data Error", MessageBoxButtons.OK);
                txtWeight.Text = "1";
                txtWeight.Focus();
                return;
            }
            if (shipment.Weight <= 0)
            {
                MessageBox.Show("Invalid Weight.", "Data Error", MessageBoxButtons.OK);
                txtWeight.Text = "1";
                txtWeight.Focus();
                return;
            }
            decimal length = 0;
            decimal width = 0;
            decimal height = 0;
            try
            {
                length = Decimal.Parse(txtLength.Text);
                width = Decimal.Parse(txtWidth.Text);
                height = Decimal.Parse(txtHeight.Text);
                if (!(length > 0 && width > 0 && height > 0))
                {
                    txtLength.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Dimension.", "Data Error", MessageBoxButtons.OK);
                return;
            }

            int index = 0;
            for (index = 0; index < shipment.PackageDimensions.Count; index++)
            {
                var temp = shipment.PackageDimensions.Find(x => x.Index == index);
                if (temp == null)
                {
                    break;
                }
            }
            packageDimensionModel = new PackageDimensionModel();
            packageDimensionModel.Index = index;
            packageDimensionModel.Length = length;
            packageDimensionModel.Width = width;
            packageDimensionModel.Height = height;
            packageDimensionModel.CommodityTypeId = shipment.CommodityTypeId;
            packageDimensionModel.CratingId = null;
            if (lstCrating.SelectedValue != null)
            {
                packageDimensionModel.CratingId = Guid.Parse(lstCrating.SelectedValue.ToString());
                packageDimensionModel.CratingName = lstCrating.SelectedText;
            }
            packageDimensionModel.ForPackaging = chkPackaging.Checked;
            packageDimensionModel.ForDraining = chkDraining.Checked;
            shipment.PackageDimensions.Add(packageDimensionModel);
            gridPackage.Enabled = true;
            RefreshGridPackages();
            btnCompute.Enabled = true;

            txtLength.Text = "0";
            txtWidth.Text = "0";
            txtHeight.Text = "0";
            lstCrating.SelectedIndex = -1;
            chkPackaging.Checked = false;
            chkDraining.Checked = false;
        }
        private void ProceedToPayment()
        {
            PaymentDetailsViewModel newPayment = new PaymentDetailsViewModel();
            newPayment.AwbSoa = AcceptancetxtAirwayBill.Text;
            try
            {
                newPayment.AmountPaid = decimal.Parse(txtSumTotal.Value.ToString().Replace("₱", ""));
            }
            catch (Exception ex)
            {
                newPayment.AmountPaid = decimal.Parse(txtSumTotal.Value.ToString().Replace("Php", ""));
            }


            NewPayment = newPayment;
            ((RadPageView)BookingPage.Parent).SelectedPage = this.PaymentPage;

        }
        private void RefreshOptions()
        {
            Guid commodityTypeId = new Guid();
            Guid commodityId = new Guid();
            Guid serviceTypeId = new Guid();
            Guid serviceModeId = new Guid();

            if (lstCommodityType.SelectedValue != null)
                commodityTypeId = Guid.Parse(lstCommodityType.SelectedValue.ToString());
            //if (lstCommodity.SelectedValue != null)
            //    commodityId = Guid.Parse(lstCommoditySelectedValue.ToString());
            if (lstServiceType.SelectedValue != null)
                serviceTypeId = Guid.Parse(lstServiceType.SelectedValue.ToString());
            if (lstServiceMode.SelectedValue != null)
                serviceModeId = Guid.Parse(lstServiceMode.SelectedValue.ToString());

            //var matrix =
            //    rateMatrixService.FilterActiveBy(
            //        x =>
            //            x.CommodityTypeId == commodityTypeId && x.ServiceTypeId == serviceTypeId &&
            //            x.ServiceModeId == serviceModeId).FirstOrDefault();

            //if (matrix != null)
            //{
            //    //shipment. = matrix.DeliveryFee;
            //    //shipment.DangerousFee = matrix.DangerousFee;
            //}
        }
        private void RefreshGridPackages()
        {
            if (shipment.PackageDimensions != null)
            {
                //decimal totalWeightCharge = 0;
                decimal totalEvm = 0;
                if (shipment.PackageDimensions.Count > 0)
                {
                    shipment.WeightCharge = 0;
                    gridPackage.DataSource = null;
                    shipment = shipmentService.ComputePackageEvmCrating(shipment);

                    gridPackage.DataSource = ConvertToDataTable(shipment, out totalEvm);

                    gridPackage.Columns["No"].Width = 20;
                    gridPackage.Columns["Length"].Width = 70;
                    gridPackage.Columns["Width"].Width = 70;
                    gridPackage.Columns["Height"].Width = 70;
                    gridPackage.Columns["Crating"].Width = 70;
                    gridPackage.Columns["Packaging"].Width = 70;
                    gridPackage.Columns["Draining"].Width = 70;

                    gridPackage.Columns["No"].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                    gridPackage.Columns["Length"].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                    gridPackage.Columns["Width"].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                    gridPackage.Columns["Height"].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                    gridPackage.Columns["Crating"].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                    gridPackage.Columns["Packaging"].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
                    gridPackage.Columns["Draining"].TextAlignment = System.Drawing.ContentAlignment.MiddleRight;

                    shipment = shipmentService.ComputePackageWeightCharge(shipment);

                }
                else
                {
                    shipment.WeightCharge = 0;
                    totalEvm = 0;
                    gridPackage.DataSource = null;
                }
                txtTotalEvm.Text = totalEvm.ToString("N");
                txtTotalWeightCharge.Text = shipment.WeightChargeString;
            }
        }
        private void CommodityTypeSelected()
        {
            commodityType = new CommodityType();
            if (lstCommodityType.Items.Count > 0)
            {
                if (lstCommodityType.SelectedIndex >= 0)
                {
                    Guid commodityTypeId = Guid.Parse(lstCommodityType.SelectedValue.ToString());
                    commodityType = commodityTypes.Find(x => x.CommodityTypeId == commodityTypeId);
                    shipment.CommodityTypeId = commodityTypeId;
                    shipment.CommodityType = commodityTypes.Find(x => x.CommodityTypeId == commodityTypeId);

                    var _commodities =
                        commodities.Where(x => x.CommodityTypeId == commodityTypeId).OrderBy(x => x.CommodityName).ToList();
                    lstCommodity.DataSource = _commodities;
                    lstCommodity.DisplayMember = "CommodityName";
                    lstCommodity.ValueMember = "CommodityId";
                }
                else
                {
                    lstCommodityType.Focus();
                }

            }

            btnAddPackage.Enabled = true;

            gridPackage.ReadOnly = false;

            txtQuantity.Enabled = true;
            txtWeight.Enabled = true;
            txtLength.Enabled = true;
            txtWidth.Enabled = true;
            txtHeight.Enabled = true;
            btnAddPackage.Enabled = true;
            RefreshGridPackages();
            RefreshOptions();
            lstCommodity.Focus();
        }
        private void EnableForm()
        {
            lstCommodityType.Enabled = true;
            lstCommodity.Enabled = true;
            lstServiceType.Enabled = true;
            lstServiceMode.Enabled = true;
            lstShipMode.Enabled = true;
            lstHub.Enabled = true;
            txtQuantity.Enabled = true;
            txtWeight.Enabled = true;
            txtLength.Enabled = true;
            txtWidth.Enabled = true;
            txtHeight.Enabled = true;
            lstCrating.Enabled = true;
            chkPackaging.Enabled = true;
            chkDraining.Enabled = true;
            btnAddPackage.Enabled = true;
            gridPackage.Enabled = true;
            lstGoodsDescription.Enabled = true;
            lstPaymentMode.Enabled = true;
            txtTotalEvm.Enabled = true;
            txtTotalWeightCharge.Enabled = true;
            txtDeclaredValue.Enabled = true;
            txtHandlingFee.Enabled = true;
            txtQuarantineFee.Enabled = true;
            txtRfa.Enabled = true;
            chkNonVatable.Enabled = true;
            txtNotes.Enabled = true;

            btnCompute.Enabled = true;
            btnAcceptanceSave.Enabled = false;
            btnAcceptanceEdit.Enabled = false;
            btnAcceptanceReset.Enabled = true;
            btnPayment.Enabled = false;
        }
        public void DisableForm()
        {
            lstCommodityType.Enabled = false;
            lstCommodity.Enabled = false;
            lstServiceType.Enabled = false;
            lstServiceMode.Enabled = false;
            lstShipMode.Enabled = false;
            lstHub.Enabled = false;
            txtQuantity.Enabled = false;
            txtWeight.Enabled = false;
            txtLength.Enabled = false;
            txtWidth.Enabled = false;
            txtHeight.Enabled = false;
            lstCrating.Enabled = false;
            chkPackaging.Enabled = false;
            chkDraining.Enabled = false;
            btnAddPackage.Enabled = false;
            gridPackage.Enabled = false;
            lstGoodsDescription.Enabled = false;
            lstPaymentMode.Enabled = false;
            txtTotalEvm.Enabled = false;
            txtTotalWeightCharge.Enabled = false;
            txtDeclaredValue.Enabled = false;
            txtHandlingFee.Enabled = false;
            txtQuarantineFee.Enabled = false;
            txtRfa.Enabled = false;
            chkNonVatable.Enabled = false;
            chkNonVatable.Checked = false;
            txtNotes.Enabled = false;

            btnCompute.Enabled = false;
            btnAcceptanceSave.Enabled = false;
            btnAcceptanceEdit.Enabled = false;
            btnPayment.Enabled = false;
            btnPrint.Enabled = false;

            if (AcceptancetxtShipperAccountNo.Text != "")
            {
                btnAcceptanceEdit.Enabled = true;
            }
        }
        private void ClearSummaryData()
        {
            txtSumChargeableWeight.Text = "0.00";
            txtSumWeightCharge.Text = "0.00";
            txtSumAwbFee.Text = "0.00";
            txtSumValuation.Text = "0.00";
            txtSumDeliveryFee.Text = "0.00";
            txtSumFreightCollect.Text = "0.00";
            txtSumPeracFee.Text = "0.00";
            txtSumFuelSurcharge.Text = "0.00";
            txtSumDangerousFee.Text = "0.00";
            txtSumCratingFee.Text = "0.00";
            txtSumDrainingFee.Text = "0.00";
            txtSumPackagingFee.Text = "0.00";
            txtSumHandlingFee.Text = "0.00";
            txtSumQuarantineFee.Text = "0.00";
            txtSumDiscount.Text = "0.00";
            txtSumInsurance.Text = "0.00";
            txtSumSubTotal.Text = "0.00";
            txtSumVatAmount.Text = "0.00";
            txtSumTotal.Text = "0.00";
            txtAmountDue.Text = "0.00";
            txtAmountPaid.Text = "0.00";
        }
        private void PrintAWB_Acceptance()
        {

            btnReset.Enabled = false;
            btnCompute.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("CommodityType", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("ActualWt", typeof(string)));
            dt.Columns.Add(new DataColumn("Dimensions", typeof(string)));
            dt.Columns.Add(new DataColumn("EVM", typeof(string)));
            dt.Columns.Add(new DataColumn("ChargeableWt", typeof(string)));

            for (int x = 0; x < shipmentModel.PackageDimensions.Count; x++)
            {
                DataRow row = dt.NewRow();
                row[0] = txtQuantity.Text;
                row[1] = txtWeight.Text;
                row[2] = Math.Round(shipmentModel.PackageDimensions[x].Length).ToString() + " x " + Math.Round(shipmentModel.PackageDimensions[x].Width).ToString() + " x " + Math.Round(shipmentModel.PackageDimensions[x].Height).ToString();
                row[3] = txtTotalEvm.Text;
                row[4] = txtSumChargeableWeight.Text;
                dt.Rows.Add(row);
            }

            AcceptanceModelReport.table = dt;

            AcceptanceModelReport.ServiceMode = shipmentModel.ServiceMode.ServiceModeName;
            AcceptanceModelReport.PaymentMode = lstPaymentMode.SelectedItem.Text;

            AcceptanceModelReport.Origin = AcceptancetxtShipperCity.Text;
            AcceptanceModelReport.Destination = AcceptancetxtConsigneeCity.Text;
            AcceptanceModelReport.DateandPlace = shipmentModel.DateAccepted.ToString("MMM dd, yyyy h:mmtt") + " / " + shipmentModel.AcceptedArea.RevenueUnitName;

            AcceptanceModelReport.AWB1 = AcceptancetxtAirwayBill.Text;
            AcceptanceModelReport.AWB2 = AcceptancetxtAirwayBill.Text;

            AcceptanceModelReport.ShipperName = AcceptancetxtShipperFullName.Text;
            AcceptanceModelReport.ShipperAddress = AcceptancetxtShipperAddress.Text;

            AcceptanceModelReport.ConsigneeName = AcceptancetxtConsigneeFullName.Text;
            AcceptanceModelReport.ConsigneeAddress = AcceptancetxtConsigneeCity.Text;

            AcceptanceModelReport.txt1 = txtSumChargeableWeight.Text;
            AcceptanceModelReport.txt2 = txtSumWeightCharge.Text;
            AcceptanceModelReport.txt3 = txtSumAwbFee.Text;
            AcceptanceModelReport.txt4 = txtSumValuation.Text;
            AcceptanceModelReport.txt5 = txtSumDeliveryFee.Text;
            AcceptanceModelReport.txt6 = txtSumFreightCollect.Text;
            AcceptanceModelReport.txt7 = txtSumPeracFee.Text;
            AcceptanceModelReport.txt8 = txtSumFuelSurcharge.Text;
            AcceptanceModelReport.txt9 = txtSumDangerousFee.Text;
            AcceptanceModelReport.txt10 = txtSumCratingFee.Text;
            AcceptanceModelReport.txt11 = txtSumDrainingFee.Text;
            AcceptanceModelReport.txt12 = txtSumPackagingFee.Text;
            AcceptanceModelReport.txt13 = txtSumHandlingFee.Text;
            AcceptanceModelReport.txt14 = txtSumQuarantineFee.Text;
            AcceptanceModelReport.txt15 = txtSumInsurance.Text;
            AcceptanceModelReport.txt16 = txtSumDiscount.Text;
            AcceptanceModelReport.txt17 = txtSumSubTotal.Text;
            AcceptanceModelReport.txt18 = txtSumVatAmount.Text;

            AcceptanceModelReport.GrandTotal = txtSumTotal.Text;

            ReportGlobalModel.Report = "AcceptanceReport";
            ReportViewer viewer = new ReportViewer();
            viewer.Show();

        }
        #endregion

        #region Payment

        private void PaymentInit()
        {
            bsPaymentType = new BindingSource();
            shipmentService = new ShipmentBL(GlobalVars.UnitOfWork);
            soaService = new StatementOfAccountBL(GlobalVars.UnitOfWork);
            paymentService = new PaymentBL(GlobalVars.UnitOfWork);
            paymentTypeService = new PaymentTypeBL(GlobalVars.UnitOfWork);
        }
        private void PaymentReset()
        {
            txtSoaNo.Text = "";
            txtSoaNo.Enabled = true;
            txtAwb.Text = "";
            txtAwb.Enabled = true;
            txtOrNo.Text = "";
            txtAmountPaid.Text = "";
            txtAmountDue.Text = "";
            txtPrNo.Text = "";
            txtAmountDue.Text = txtAmountDue.Mask;
            txtAmountPaid.Text = txtAmountPaid.Mask;
            txtNetCollection.Text = txtNetCollection.Mask;
            txtTaxWithheld.Text = txtTaxWithheld.Mask;
            datePaymentDate.Value = DateTime.Now;
            txtAmountPaid.Text = txtAmountPaid.Mask;
            txtTaxWithheld.ResetText();
            txtNetCollection.ResetText();
            lstPaymentType.Text = "Cash";
            txtCheckBank.Text = "";
            txtCheckBank.Enabled = false;
            txtCheckNo.Text = "";
            txtCheckNo.Enabled = false;
            dateCheckDate.Value = DateTime.Now;
            dateCheckDate.Enabled = false;
            cmb_PaymentRemarks.Text = "";
        }
        private void SavePayment(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 2; // # of processes

            if (soaPayment != null)
            {
                soaService.MakePayment(soaPayment, soaService.EntityToModel(soa));
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }
            if (payment != null)
            {
                paymentService.Add(payment);
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }
        }
        private void ComputeNetCollection()
        {
            try
            {
                decimal amountdue = 0;
                decimal tax = 0;
                if (txtAmountDue.Value.ToString().Contains("₱"))
                {
                    amountdue = decimal.Parse(txtAmountDue.Value.ToString().Replace("₱", ""));
                }
                else
                {
                    amountdue = decimal.Parse(txtAmountDue.Value.ToString().Replace("Php", ""));
                }

                tax = decimal.Parse(txtTaxWithheld.Value.ToString());
                txtNetCollection.Text = (amountdue - (tax * amountdue)).ToString();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "ComputeNetCollection", ex);
            }
        }
        private void LoadPayment()
        {
            if (shipment != null)
            {
                txtAmountDue.Text = Convert.ToString(shipment.ShipmentTotal);
            }
        }
        private void isPaidOrPartial()
        {
            decimal amountdue = 0;
            decimal amountpaid = 0;
            if (txtAmountDue.Value.ToString().Contains("₱"))
            {
                amountdue = decimal.Parse(txtAmountDue.Value.ToString().Replace("₱", ""));
            }
            else
            {
                amountdue = decimal.Parse(txtAmountDue.Value.ToString().Replace("Php", ""));
            }

            if (txtAmountPaid.Value.ToString().Contains("₱"))
            {
                amountpaid = decimal.Parse(txtAmountPaid.Value.ToString().Replace("₱", ""));
            }
            else
            {
                amountpaid = decimal.Parse(txtAmountPaid.Value.ToString().Replace("Php", ""));
            }

            if (amountdue == amountpaid || amountpaid > amountdue)
            {
                cmb_PaymentRemarks.SelectedValue = "Full";
            }
            else
            {
                cmb_PaymentRemarks.SelectedValue = "Partial";
            }
        }
        #endregion

        #region Manifest

        #endregion



        #endregion

        #region TODO in creating Tracking Reports
        //TODO 1: Create container

        //TODO 2: Create main method to load container with corresponding date range

        //TODO 3: Create method for conveting entities to view and get discrepancies to be loaded on grid

        //TODO 4: Create method for loading grid and filters with views

        //TODO 5: Create function for loading auto complete search data on txtSackNo(Optional)

        //TODO 6: Create method for search and filter

        //TODO 7: Create method for printing unfiltered/filtered data
        #endregion

        #region Tracking PickUp Cargo

        private List<PickupCargoManifestViewModel> pickUpCargoList = new List<PickupCargoManifestViewModel>();
        private void getPickUpCargoData(DateTime date)
        {
            DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
            DateTime dateTo = dateFrom.AddDays(1);
            List<Shipment> _shipments = shipmentService.FilterActiveBy(x => x.Booking.BookingStatus.BookingStatusName == "Picked-up"
                && x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId
                && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo);

            this.pickUpCargoList = PickUpCargoToView(_shipments);
            this.PickUpCargoLoadData();
        }
        private List<PickupCargoManifestViewModel> PickUpCargoToView(List<Shipment> _shipment)
        {
            List<PickupCargoManifestViewModel> _results = new List<PickupCargoManifestViewModel>();
            try
            {
                foreach (Shipment shipment in _shipment)
                {
                    PickupCargoManifestViewModel model = new PickupCargoManifestViewModel();

                    PickupCargoManifestViewModel isExist = _results.Find(x => x.AirwayBillNo == shipment.AirwayBillNo);

                    if (isExist != null)
                    {
                        isExist.QTY++;
                    }
                    else
                    {
                        model.AirwayBillNo = shipment.AirwayBillNo;
                        model.Shipper = shipment.Shipper.FullName;
                        model.Consignee = shipment.Consignee.FullName;

                        model.ConsigneeAddress = shipment.Consignee.Address1 + " " + shipment.Consignee.Address2;
                        model.ShipperAddress = shipment.Shipper.Address1 + " " + shipment.Shipper.Address2;
                        model.Commodity = shipment.Commodity.CommodityName;
                        model.QTY = shipment.PackageNumbers.Count(); ;
                        model.AGW += shipment.Weight;
                        model.ServiceMode = shipment.ServiceMode.ServiceModeName;
                        model.PaymentMode = shipment.PaymentMode.PaymentModeName;
                        model.Amount = shipment.TotalAmount.ToString("N2");
                        model.ScannedBy = shipment.AcceptedBy.FullName;
                        model.Area = shipment.AcceptedBy.AssignedToArea.RevenueUnitName;
                        model.RevenueUnitType = shipment.AcceptedBy.AssignedToArea.RevenueUnitType;

                        _results.Add(model);
                    }

                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "PickUpCargoToView", ex);
            }



            return _results;

        }
        public void PickUpCargoLoadData()
        {
            try
            {
                this.gridPickupCargo.FilterDescriptors.Clear();
                this.gridPickupCargo.DataSource = pickUpCargoList;

                List<RevenueUnitType> _revenueUnitTypes = pickUpCargoList.Select(x => x.RevenueUnitType).Distinct().ToList();
                this.bsRevenueUnitType.DataSource = _revenueUnitTypes;
                this.cmb_RevenueUnitType.DataSource = bsRevenueUnitType;
                this.cmb_RevenueUnitType.Items.Add("All");
                this.cmb_RevenueUnitType.DisplayMember = "RevenueUnitTypeName";
                this.cmb_RevenueUnitType.ValueMember = "RevenueUnitTypeId";
                this.cmb_RevenueUnitType.SelectedValue = "All";

                this.cmb_RevenueUnit.Items.Clear();
                this.cmb_RevenueUnit.Items.Add("All");
                this.cmb_RevenueUnit.Items.AddRange(pickUpCargoList.Select(x => x.Area).Distinct().ToList());
                this.cmb_RevenueUnit.SelectedValue = "All";

            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "PickUpCargoLoadData", ex);
            }


        }
        private void PickUpCargoSearch()
        {
            try
            {
                this.gridPickupCargo.FilterDescriptors.Clear();
                if (this.cmb_RevenueUnit.SelectedIndex > -1 && this.cmb_RevenueUnit.SelectedItem.ToString() != "All")
                {
                    this.gridPickupCargo.FilterDescriptors.Add("Area", FilterOperator.Contains, this.cmb_RevenueUnit.SelectedItem.ToString());

                }
                if (this.cmb_RevenueUnitType.SelectedIndex > -1 && this.cmb_RevenueUnitType.SelectedItem.ToString() != "All")
                {
                    this.gridPickupCargo.FilterDescriptors.Add("RevenueUnitTypeName", FilterOperator.Contains, this.cmb_RevenueUnitType.SelectedItem.ToString());
                }

            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Pickup Cargo btnSearch_PicupCargo_Click", ex);
            }
        }
        private void LoadRevenueUnitByRevenueUnitType()
        {
            PickUpCargoSearch();
            if (cmb_RevenueUnitType.SelectedItem.ToString() != "All")
            {
                cmb_RevenueUnit.Enabled = true;
                List<string> _revenueUnits = new List<string>();
                _revenueUnits.Add("All");
                _revenueUnits.AddRange(pickUpCargoList.Where(x => x.RevenueUnitType.RevenueUnitTypeName == cmb_RevenueUnitType.SelectedItem.Text).Select(x => x.Area).Distinct().ToList());
                bsRevenueUnits.DataSource = _revenueUnits;
                cmb_RevenueUnit.DataSource = bsRevenueUnits;
                cmb_RevenueUnit.SelectedValue = "All";
            }
            else
            {
                cmb_RevenueUnit.Enabled = false;
                cmb_RevenueUnit.Items.Add("All");
                cmb_RevenueUnit.SelectedValue = "All";
            }


        }
        private void PickUpCargoPrint()
        {
            try
            {
                ReportGlobalModel.PickUpCargoReportData = pickUpCargoList;
                if (cmb_RevenueUnitType.SelectedItem.Text != "All")
                {
                    ReportGlobalModel.PickUpCargoReportData = ReportGlobalModel.PickUpCargoReportData.Where(x => x.RevenueUnitType.RevenueUnitTypeName == cmb_RevenueUnitType.SelectedItem.Text).ToList();
                }
                if (cmb_RevenueUnit.SelectedItem.Text != "All")
                {
                    ReportGlobalModel.PickUpCargoReportData = ReportGlobalModel.PickUpCargoReportData.Where(x => x.Area == cmb_RevenueUnit.SelectedItem.Text).ToList();
                }
                int ctr = 1;
                ReportGlobalModel.PickUpCargoReportData.ForEach(x => x.No = ctr++);
                ReportGlobalModel.Date = dateTimePicker_PickupCargo.Value.ToLongDateString();
                ReportGlobalModel.Area = cmb_RevenueUnit.SelectedItem.ToString();
                ReportGlobalModel.ScannedBy = string.Join(", ", ReportGlobalModel.PickUpCargoReportData.Select(x => x.ScannedBy).ToArray());

                ReportGlobalModel.Report = "PickUpCargo";

                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "PickUpCargoPrint", ex);
            }
        }

        #endregion

        #region Tracking Branch Acceptance

        private List<BranchAcceptanceViewModel> branchAcceptanceList = new List<BranchAcceptanceViewModel>();
        private void getBranchAcceptanceData(DateTime date)
        {
            DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
            DateTime dateTo = dateFrom.AddDays(1);

            List<Shipment> shipments = shipmentService.FilterActiveBy(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId
                && x.RecordStatus == 1 && x.Booking.BookingStatus.BookingStatusName == "Picked-up"
                && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();
            List<BranchAcceptance> branchAcceptance = branchAcceptanceService.FilterActiveBy(x => x.User.Employee.AssignedToArea.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId
                && !x.User.Employee.AssignedToArea.RevenueUnitName.Contains("Walk-in")
                && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();

            this.branchAcceptanceList = BranchAcceptanceEntitesToView(branchAcceptance, shipments);
            this.BranchAcceptanceLoadData();

        }
        private List<BranchAcceptanceViewModel> BranchAcceptanceEntitesToView(List<BranchAcceptance> _branchAcceptances, List<Shipment> _shipments)
        {
            List<BranchAcceptanceViewModel> _results = new List<BranchAcceptanceViewModel>();
            try
            {
                foreach (Shipment shipment in _shipments)
                {
                    BranchAcceptanceViewModel model = new BranchAcceptanceViewModel();
                    List<PackageNumber> packageNumbers = shipment.PackageNumbers;

                    foreach (PackageNumber packagenumber in packageNumbers)
                    {
                        BranchAcceptanceViewModel isAirawayBillExist = _results.Find(x => x.AirwayBillNo == shipment.AirwayBillNo);

                        BranchAcceptance _brachAcceptance = _branchAcceptances.Find(x => x.Cargo == packagenumber.PackageNo);
                        
                        if (_brachAcceptance != null)
                        {
                            if (isAirawayBillExist != null)
                            {
                                isAirawayBillExist.TotalRecieved++;
                                isAirawayBillExist.Total = isAirawayBillExist.TotalRecieved;
                            }
                            else
                            {
                                model.AirwayBillNo = shipment.AirwayBillNo;
                                model.Area = _brachAcceptance.User.Employee.AssignedToArea.RevenueUnitName;
                                model.Driver = _brachAcceptance.Driver;
                                model.Checker = _brachAcceptance.Checker;
                                model.PlateNo = "N/A";
                                model.Batch = _brachAcceptance.Batch.BatchName;
                                model.TotalRecieved++;
                                model.Total = model.TotalRecieved;
                                model.CreatedBy = _brachAcceptance.CreatedDate;
                                model.BCO = _brachAcceptance.BranchCorpOffice.BranchCorpOfficeName;
                                model.BSO = "N/A";
                                model.BSO = _brachAcceptance.User.Employee.AssignedToArea.RevenueUnitName;
                                model.ScannedBy = "N/A";
                                model.ScannedBy = _brachAcceptance.User.Employee.FullName;
                                model.Remarks = shipment.Remarks;
                                model.Notes = _brachAcceptance.Notes;
                                _results.Add(model);

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "BranchAcceptanceEntitesToView", ex);
            }

            return _results;

        }
        public void BranchAcceptanceLoadData()
        {
            try
            {
                this.gridBranchAcceptance.FilterDescriptors.Clear();
                this.gridBranchAcceptance.DataSource = branchAcceptanceList;

                dropDownBranchAcceptance_BCO_BSO.Items.Clear();
                dropDownBranchAcceptance_BCO_BSO.Items.Add("All");
                dropDownBranchAcceptance_BCO_BSO.Items.AddRange(branchAcceptanceList.Select(x => x.BSO).Distinct().ToList());
                dropDownBranchAcceptance_BCO_BSO.SelectedValue = "All";

                dropDownBranchAcceptance_Driver.Items.Clear();
                dropDownBranchAcceptance_Driver.Items.Add("All");
                dropDownBranchAcceptance_Driver.Items.AddRange(branchAcceptanceList.Select(x => x.Driver).Distinct().ToList());
                dropDownBranchAcceptance_Driver.SelectedValue = "All";

                dropDownBranchAcceptance_Batch.Items.Clear();
                dropDownBranchAcceptance_Batch.Items.Add("All");
                dropDownBranchAcceptance_Batch.Items.AddRange(branchAcceptanceList.Select(x => x.Batch).Distinct().ToList());
                dropDownBranchAcceptance_Batch.SelectedValue = "All";

            }
            catch (Exception ex)
            {
                //MessageBox.Show("BranchAcceptanceLoadData Error.");
                Logs.ErrorLogs(LogPath, "BranchAcceptanceLoadData", ex);
            }

        }
        private void BranchAcceptanceSearch()
        {
            try
            {
                this.gridBranchAcceptance.FilterDescriptors.Clear();
                if (dropDownBranchAcceptance_BCO_BSO.SelectedIndex > -1 && dropDownBranchAcceptance_BCO_BSO.SelectedItem.ToString() != "All")
                {
                    this.gridBranchAcceptance.FilterDescriptors.Add("Area", FilterOperator.Contains, dropDownBranchAcceptance_BCO_BSO.SelectedItem.ToString());
                }
                if (dropDownBranchAcceptance_Driver.SelectedIndex > -1 && dropDownBranchAcceptance_Driver.SelectedItem.ToString() != "All")
                {
                    this.gridBranchAcceptance.FilterDescriptors.Add("Driver", FilterOperator.Contains, dropDownBranchAcceptance_Driver.SelectedItem.ToString());
                }
                if (dropDownBranchAcceptance_Batch.SelectedIndex > -1 && dropDownBranchAcceptance_Batch.SelectedItem.ToString() != "All")
                {
                    this.gridBranchAcceptance.FilterDescriptors.Add("Batch", FilterOperator.Contains, dropDownBranchAcceptance_Batch.SelectedItem.ToString());

                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "BranchAcceptanceSearch", ex);
            }

        }
        private void BranchAcceptancePrint()
        {
            try
            {
                ReportGlobalModel.BranchAcceptanceReportData = branchAcceptanceList;
                List<BranchAcceptanceViewModel> tempBranchAcceptanceList = branchAcceptanceList;

                if (dropDownBranchAcceptance_BCO_BSO.SelectedItem.Text != "All")
                {
                    tempBranchAcceptanceList = tempBranchAcceptanceList.Where(x => x.Area == dropDownBranchAcceptance_BCO_BSO.SelectedItem.Text).ToList();
                }
                if (dropDownBranchAcceptance_Driver.SelectedItem.Text != "All")
                {
                    tempBranchAcceptanceList = tempBranchAcceptanceList.Where(x => x.Driver == dropDownBranchAcceptance_Driver.SelectedItem.Text).ToList();
                }
                if (dropDownBranchAcceptance_Batch.SelectedItem.Text != "All")
                {
                    tempBranchAcceptanceList = tempBranchAcceptanceList.Where(x => x.Batch == dropDownBranchAcceptance_Batch.SelectedItem.Text).ToList();
                }

                int ctr = 1;
                tempBranchAcceptanceList.ForEach(x => x.No = ctr++);
                ReportGlobalModel.BranchAcceptanceReportData = tempBranchAcceptanceList;
                ReportGlobalModel.Date = dateTimePickerBranchAcceptance_Date.Value.ToLongDateString();
                ReportGlobalModel.Branch = branchCorpOffices.Find(x => x.BranchCorpOfficeId == GlobalVars.DeviceBcoId).BranchCorpOfficeName;
                ReportGlobalModel.Driver = dropDownBranchAcceptance_Driver.SelectedItem.ToString();
                ReportGlobalModel.Checker = string.Join(", ", ReportGlobalModel.BranchAcceptanceReportData.Select(x => x.Checker).Distinct());
                ReportGlobalModel.PlateNo = string.Join(", ", ReportGlobalModel.BranchAcceptanceReportData.Select(x => x.PlateNo).Distinct());
                ReportGlobalModel.Batch = dropDownBranchAcceptance_Batch.SelectedItem.ToString();
                ReportGlobalModel.Remarks = string.Join(", ", ReportGlobalModel.BranchAcceptanceReportData.Select(x => x.Remarks).Distinct());
                ReportGlobalModel.Notes = string.Join(", ", ReportGlobalModel.BranchAcceptanceReportData.Select(x => x.Notes).Distinct());

                ReportGlobalModel.Report = "BranchAcceptance";
                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Branch Acceptance Print", ex);
            }
        }

        #endregion

        #region Tracking Bundle

        List<BundleViewModel> bundleList = new List<BundleViewModel>();
        private void getBundleData(DateTime date)
        {
            try
            {
                DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
                DateTime dateTo = dateFrom.AddDays(1);
                List<Bundle> bundles = bundleService.FilterActiveBy(x => x.BundleBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();
                this.bundleList = BundleEntitiesToView(bundles);
                this.BundleLoadData();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("getBundleData");
                Logs.ErrorLogs("", "getBundleData", ex);
            }


        }
        private List<BundleViewModel> BundleEntitiesToView(List<Bundle> bundle)
        {
            List<BundleViewModel> _results = new List<BundleViewModel>();
            try
            {
                foreach (Bundle _bundle in bundle)
                {
                    BundleViewModel model = new BundleViewModel();

                    Shipment shipment = packageNumberService.FilterActiveBy(x => x.PackageNo == _bundle.Cargo).FirstOrDefault().Shipment;
                    if (shipment == null)
                        continue;
                    BundleViewModel isExist = _results.Find(x => x.AirwayBillNo == shipment.AirwayBillNo);

                    if (isExist != null)
                    {
                        isExist.Qty++;
                    }
                    else
                    {
                        model.AirwayBillNo = shipment.AirwayBillNo;
                        model.Shipper = shipment.Shipper.FullName;
                        model.Consignee = shipment.Consignee.FullName;
                        model.Address = shipment.Consignee.Address1;
                        model.CommodityType = shipment.CommodityType.CommodityTypeName;
                        model.Commodity = shipment.Commodity.CommodityName;
                        model.Qty++;
                        model.AGW += _bundle.Weight;
                        model.ServiceMode = shipment.ServiceMode.ServiceModeName;
                        model.PaymentMode = shipment.PaymentMode.PaymentModeName;
                        model.SackNo = _bundle.SackNo;
                        model.DestinationBCO = _bundle.DestinationBco;
                        model.Scannedby = _bundle.BundleBy.Employee.FullName;

                        _results.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("BundleMatch");
                Logs.ErrorLogs(LogPath, "BundleEntitiesToView", ex);
            }

            return _results;
        }
        private void BundleLoadData()
        {
            try
            {
                this.gridBundle.FilterDescriptors.Clear();
                this.gridBundle.DataSource = bundleList;

                dropDownBundle_BCO_BSO.Items.Clear();
                dropDownBundle_BCO_BSO.Items.Add("All");
                dropDownBundle_BCO_BSO.Items.AddRange(bundleList.Select(x => x.DestinationBCO.BranchCorpOfficeName).Distinct().ToList());
                dropDownBundle_BCO_BSO.SelectedValue = "All";

                txtBU_SackNo.Text = "";

            }
            catch (Exception ex)
            {
                //MessageBox.Show("BundleLoadData" + ex.StackTrace.ToString());
                Logs.ErrorLogs(LogPath, "BundleLoadData", ex);
            }


        }
        private void LoadSackAutoCompleteSource()
        {
            try
            {
                if (dropDownBundle_BCO_BSO.SelectedIndex > -1 && dropDownBundle_BCO_BSO.SelectedItem.ToString() != "All")
                {
                    AutoCompleteStringCollection sacks = new AutoCompleteStringCollection();
                    sacks.AddRange(bundleList.Where(x => x.DestinationBCO.BranchCorpOfficeName == dropDownBundle_BCO_BSO.SelectedItem.ToString()).Select(x => x.SackNo).ToArray());
                    txtBU_SackNo.AutoCompleteCustomSource = sacks;
                }
                else
                {
                    AutoCompleteStringCollection sacks = new AutoCompleteStringCollection();
                    sacks.AddRange(bundleList.Select(x => x.SackNo).ToArray());
                    txtBU_SackNo.AutoCompleteCustomSource = sacks;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("LoadSackAutoCompleteSource");
                Logs.ErrorLogs(LogPath, "LoadSackAutoCompleteSource", ex);
            }
        }
        private void BundleSearch()
        {
            try
            {
                gridBundle.FilterDescriptors.Clear();

                if (dropDownBundle_BCO_BSO.SelectedIndex > -1 && dropDownBundle_BCO_BSO.SelectedItem.Text != "All")
                {
                    gridBundle.FilterDescriptors.Add("DestinationBco", FilterOperator.Contains, dropDownBundle_BCO_BSO.SelectedItem.Text);
                }
                if (txtBU_SackNo.Text.Trim() != "")
                {
                    gridBundle.FilterDescriptors.Add("SackNo", FilterOperator.Contains, txtBU_SackNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("BundleSearch");
                Logs.ErrorLogs(LogPath, "BundleSearch", ex);
            }


        }
        private void BundlePrint()
        {
            try
            {
                ReportGlobalModel.BundleReportData = bundleList;
                if (dropDownBundle_BCO_BSO.SelectedIndex > -1 && dropDownBundle_BCO_BSO.SelectedItem.ToString() != "All")
                {
                    ReportGlobalModel.BundleReportData = ReportGlobalModel.BundleReportData.Where(x => x.DestinationBCO.BranchCorpOfficeName == dropDownBundle_BCO_BSO.SelectedItem.ToString()).ToList();
                }
                if (txtBU_SackNo.Text != "")
                {
                    ReportGlobalModel.BundleReportData = ReportGlobalModel.BundleReportData.Where(x => x.SackNo == txtBU_SackNo.Text.Trim()).ToList();
                }
                int ctr = 1;
                ReportGlobalModel.BundleReportData.ForEach(x => x.No = ctr++);
                ReportGlobalModel.Date = dateTimeBundle_Date.Value.ToLongDateString();
                ReportGlobalModel.SackNo = string.Join(", ", ReportGlobalModel.BundleReportData.Select(x => x.SackNo).Distinct());
                ReportGlobalModel.Weight = ReportGlobalModel.BundleReportData.Sum(x => x.AGW).ToString();
                ReportGlobalModel.ScannedBy = UserTxt.Text.Replace("Welcome!", "");

                ReportGlobalModel.Report = "Bundle";

                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "BundlePrint", ex);
            }
        }
        #endregion

        #region Tracking Unbundle
        List<UnbundleViewModel> UnbundleList = new List<UnbundleViewModel>();
        private void getUnBundleData(DateTime date)
        {
            try
            {
                DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
                DateTime dateTo = dateFrom.AddDays(1);
                List<Unbundle> _unbundleList = unbundleService.FilterActiveBy(x => x.UnbundleBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();
                List<Bundle> _bundleList = bundleService.FilterActiveBy(x => x.BranchCorpOfficeID == GlobalVars.DeviceBcoId && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();

                this.UnbundleList = UnbundleEntitiesToView(_unbundleList, _bundleList);
                this.UnbundleLoadData();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("getUnBundleData");
                Logs.ErrorLogs("", "getUnBundleData", ex);
            }

        }
        private List<UnbundleViewModel> UnbundleEntitiesToView(List<Unbundle> _unbundle, List<Bundle> _bundle)
        {
            List<UnbundleViewModel> _results = new List<UnbundleViewModel>();
            try
            {
                foreach (Unbundle unBundle in _unbundle)
                {
                    UnbundleViewModel model = new UnbundleViewModel();

                    PackageNumber _packageNumber = packageNumberService.FilterActiveBy(x => x.PackageNo == unBundle.Cargo).FirstOrDefault();
                    if (_packageNumber == null) continue;

                    UnbundleViewModel isExist = _results.Find(x => x.AirwayBillNo == _packageNumber.Shipment.AirwayBillNo);

                    Bundle bundle = _bundle.Find(x => x.Cargo == unBundle.Cargo);

                    if (bundle != null)
                    {
                        if (isExist != null)
                        {
                            isExist.ScannedPcs++;
                            isExist.TotalPcs = isExist.ScannedPcs;
                        }
                        else
                        {
                            model.AirwayBillNo = _packageNumber.Shipment.AirwayBillNo;
                            model.SackNo = bundle.SackNo;
                            model.ScannedPcs++;
                            model.Weight += bundle.Weight;
                            model.TotalPcs = model.ScannedPcs;
                            model.Origin = unBundle.OriginBco.BranchCorpOfficeName;
                            model.ScannedBy = unBundle.UnbundleBy.Employee.FullName;

                            _results.Add(model);
                        }
                    }
                    else
                    {
                        if (isExist != null)
                        {
                            isExist.TotalDiscrepency++;
                            isExist.TotalPcs = isExist.TotalDiscrepency;
                        }
                        else
                        {
                            model.AirwayBillNo = _packageNumber.Shipment.AirwayBillNo;
                            model.SackNo = unBundle.SackNo;
                            model.TotalDiscrepency++;
                            model.TotalPcs = model.TotalDiscrepency;
                            model.Weight += 0;
                            model.ScannedBy = unBundle.UnbundleBy.Employee.FullName;
                            model.Origin = unBundle.OriginBco.BranchCorpOfficeName;

                            _results.Add(model);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("UnbundleMatch");
                Logs.ErrorLogs("", "UnbundleEntitiesToView", ex);
            }
            return _results;
        }
        public void UnbundleLoadData()
        {
            try
            {
                this.gridUnbundle.FilterDescriptors.Clear();
                this.gridUnbundle.DataSource = UnbundleList;

                this.dropDownUnbundle_BCO.Items.Clear();
                this.dropDownUnbundle_BCO.Items.Add("All");
                this.dropDownUnbundle_BCO.Items.AddRange(UnbundleList.Select(x => x.Origin).Distinct());
                this.dropDownUnbundle_BCO.SelectedValue = "All";

                this.txtUnbundle_SackNo.Text = "";
            }
            catch (Exception ex)
            {
                //MessageBox.Show("UnbundleLoadData");
                Logs.ErrorLogs(LogPath, "UnbundleLoadData", ex);
            }
        }
        private void UnbundleLoadSackAutoCompleteSource()
        {
            try
            {
                if (dropDownUnbundle_BCO.SelectedIndex > -1 && dropDownUnbundle_BCO.SelectedItem.ToString() != "All")
                {
                    AutoCompleteStringCollection sacks = new AutoCompleteStringCollection();
                    sacks.AddRange(UnbundleList.Where(x => x.Origin == dropDownUnbundle_BCO.SelectedItem.ToString()).Select(x => x.SackNo).ToArray());
                    txtUnbundle_SackNo.AutoCompleteCustomSource = sacks;
                }
                else
                {
                    AutoCompleteStringCollection sacks = new AutoCompleteStringCollection();
                    sacks.AddRange(UnbundleList.Select(x => x.SackNo).ToArray());
                    txtUnbundle_SackNo.AutoCompleteCustomSource = sacks;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("UnbundleLoadSackAutoCompleteSource");
                Logs.ErrorLogs(LogPath, "UnbundleLoadSackAutoCompleteSource", ex);
            }
        }
        private void UnbundleSearch()
        {
            try
            {
                gridUnbundle.FilterDescriptors.Clear();

                if (dropDownUnbundle_BCO.SelectedIndex > -1 && dropDownUnbundle_BCO.SelectedItem.Text != "All")
                {
                    gridUnbundle.FilterDescriptors.Add("Origin", FilterOperator.Contains, dropDownUnbundle_BCO.SelectedItem.Text);
                }
                if (txtUnbundle_SackNo.Text.Trim() != "")
                {
                    gridUnbundle.FilterDescriptors.Add("SackNo", FilterOperator.Contains, txtUnbundle_SackNo.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("BundleSearch");
                Logs.ErrorLogs(LogPath, "BundleSearch", ex);
            }
        }
        private void UnbundlePrint()
        {
            try
            {
                ReportGlobalModel.UnbundleReportData = UnbundleList;
                if (dropDownUnbundle_BCO.SelectedIndex > -1 && dropDownUnbundle_BCO.SelectedItem.Text != "All")
                {
                    ReportGlobalModel.UnbundleReportData = ReportGlobalModel.UnbundleReportData.Where(x => x.Origin == dropDownUnbundle_BCO.SelectedItem.Text).ToList();
                }
                if (txtUnbundle_SackNo.Text.Trim() != "")
                {
                    ReportGlobalModel.UnbundleReportData = ReportGlobalModel.UnbundleReportData.Where(x => x.SackNo == txtUnbundle_SackNo.Text.Trim()).ToList();
                }
                int ctr = 1;
                ReportGlobalModel.UnbundleReportData.ForEach(x => x.No = ctr++);
                ReportGlobalModel.Date = dateTimeUnbunde_Date.Value.ToLongDateString();
                ReportGlobalModel.SackNo = string.Join(", ", ReportGlobalModel.UnbundleReportData.Select(x => x.SackNo));
                ReportGlobalModel.Origin = string.Join(", ", ReportGlobalModel.UnbundleReportData.Select(x => x.Origin));
                ReportGlobalModel.ScannedBy = string.Join(", ", ReportGlobalModel.UnbundleReportData.Select(x => x.ScannedBy));
                ReportGlobalModel.Report = "Unbundle";

                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("UnbundePrint");
                Logs.ErrorLogs(LogPath, "BundlePrint", ex);

            }
        }
        #endregion

        #region Tracking Gateway Transmittal

        List<GatewayTransmitalViewModel> GatewayTransmittalList = new List<GatewayTransmitalViewModel>();
        private void getGatewayTransmitalData(DateTime date)
        {
            DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
            DateTime dateTo = dateFrom.AddDays(1);
            List<GatewayTransmittal> _gatewayTransmittals = gatewayTransmittalService.FilterActiveBy(x => x.CreatedDate > dateFrom && x.CreatedDate <= dateTo && x.TransmittalBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();
            GatewayTransmittalList = GatewayTransmittalEntitiesToView(_gatewayTransmittals);
            ReportGlobalModel.GatewayTransmittalReportData = GatewayTransmittalList;
            GatewayTransmittalLoadData();

        }
        private List<GatewayTransmitalViewModel> GatewayTransmittalEntitiesToView(List<GatewayTransmittal> _transmital)
        {
            List<GatewayTransmitalViewModel> _results = new List<GatewayTransmitalViewModel>();
            try
            {
                foreach (GatewayTransmittal transmital in _transmital)
                {
                    GatewayTransmitalViewModel model = new GatewayTransmitalViewModel();

                    Shipment _shipment = shipmentService.FilterActiveBy(x => x.AirwayBillNo == transmital.AirwayBillNo).FirstOrDefault();
                    if (_shipment == null)
                    {
                        List<Bundle> _bundles = bundleService.FilterActiveBy(x => x.SackNo == transmital.AirwayBillNo);
                        if (_bundles == null) continue;

                        GatewayTransmitalViewModel model1 = new GatewayTransmitalViewModel();
                        GatewayTransmitalViewModel isExistSackNo = _results.Find(x => x.AirwayBillNo == transmital.AirwayBillNo);
                        if (isExistSackNo != null)
                        {
                            isExistSackNo.QTY++;
                            isExistSackNo.AGW += _bundles.First().Weight;
                        }
                        else
                        {
                            model1.AirwayBillNo = transmital.AirwayBillNo;
                            model1.Shipper = "N/A";
                            model1.Consignee = "N/A";
                            model1.Address = "N/A";
                            model1.CommodityType = transmital.CommodityType.CommodityTypeName;
                            model1.Commodity = "N/A";
                            model1.Driver = transmital.Driver;
                            model1.QTY = _bundles.Count();
                            model1.AGW = _bundles.First().Weight;
                            model1.ServiceMode = "N/A";
                            model1.PaymentMode = "N/A";
                            model1.Gateway = transmital.Gateway;
                            model1.Destination = transmital.BranchCorpOffice.BranchCorpOfficeName;
                            model1.Batch = transmital.Batch.BatchName;
                            model1.CreatedDate = transmital.CreatedDate;
                            model1.PlateNo = transmital.PlateNo;
                            model1.MAWB = transmital.MasterAirwayBillNo;
                            model1.ScannedBy = transmital.TransmittalBy.Employee.FullName;

                            _results.Add(model1);
                        }
                    }
                    else
                    {
                        GatewayTransmitalViewModel isExist = _results.Find(x => x.AirwayBillNo == transmital.AirwayBillNo);

                        if (isExist != null)
                        {
                            isExist.QTY++;
                            isExist.AGW += shipment.Weight;
                        }
                        else
                        {
                            model.AirwayBillNo = transmital.AirwayBillNo;
                            model.Shipper = _shipment.Shipper.FullName;
                            model.Consignee = _shipment.Consignee.FullName;
                            model.Address = _shipment.Consignee.Address1;
                            model.CommodityType = transmital.CommodityType.CommodityTypeName;
                            model.Commodity = _shipment.Commodity.CommodityName;
                            model.Driver = transmital.Driver;
                            model.QTY++;
                            model.AGW = _shipment.Weight;
                            model.ServiceMode = _shipment.ServiceMode.ServiceModeName;
                            model.PaymentMode = _shipment.PaymentMode.PaymentModeName;
                            model.Gateway = transmital.Gateway;
                            model.Destination = transmital.BranchCorpOffice.BranchCorpOfficeName;
                            model.Batch = transmital.Batch.BatchName;
                            model.CreatedDate = transmital.CreatedDate;
                            model.PlateNo = transmital.PlateNo;
                            model.MAWB = transmital.MasterAirwayBillNo;
                            model.ScannedBy = transmital.TransmittalBy.Employee.FullName;

                            _results.Add(model);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayTransmittalEntitiesToView", ex);
            }
            return _results;
        }
        private void GatewayTransmittalLoadData()
        {
            try
            {
                this.gridGatewayTransmital.FilterDescriptors.Clear();
                this.gridGatewayTransmital.DataSource = GatewayTransmittalList;

                this.dropDownGatewayTransmital_Destination.Items.Clear();
                this.dropDownGatewayTransmital_Destination.Items.Add("All");
                this.dropDownGatewayTransmital_Destination.Items.AddRange(GatewayTransmittalList.Select(x => x.Destination).Distinct());
                this.dropDownGatewayTransmital_Destination.SelectedValue = "All";

                this.cmbGT_Driver.Items.Clear();
                this.cmbGT_Driver.Items.Add("All");
                this.cmbGT_Driver.Items.AddRange(GatewayTransmittalList.Select(x => x.Driver).Distinct());
                this.cmbGT_Driver.SelectedValue = "All";

                this.dropDownGatewayTransmital_Gateway.Items.Clear();
                this.dropDownGatewayTransmital_Gateway.Items.Add("All");
                this.dropDownGatewayTransmital_Gateway.Items.AddRange(GatewayTransmittalList.Select(x => x.Gateway).Distinct());
                this.dropDownGatewayTransmital_Gateway.SelectedValue = "All";

                this.dropDownGatewayTransmital_Batch.Items.Clear();
                this.dropDownGatewayTransmital_Batch.Items.Add("All");
                this.dropDownGatewayTransmital_Batch.Items.AddRange(GatewayTransmittalList.Select(x => x.Batch).Distinct());
                this.dropDownGatewayTransmital_Batch.SelectedValue = "All";

                this.cmbGt_CommodityType.Items.Clear();
                this.cmbGt_CommodityType.Items.Add("All");
                this.cmbGt_CommodityType.Items.AddRange(GatewayTransmittalList.Select(x => x.CommodityType).Distinct());
                this.cmbGt_CommodityType.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayTransmittalLoadData", ex);
            }

        }
        private void GatewayTransmittalLoadMAWBAutoCompleteSource()
        {
            try
            {
                AutoCompleteStringCollection mawb = new AutoCompleteStringCollection();
                List<GatewayTransmitalViewModel> tempGatewayTransmittals = GatewayTransmittalList;

                //Destination
                if (dropDownGatewayTransmital_Destination.SelectedIndex > -1 && dropDownGatewayTransmital_Destination.SelectedItem.ToString() != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.Destination == dropDownGatewayTransmital_Destination.SelectedItem.Text).ToList();
                }
                //Driver
                if (cmbGT_Driver.SelectedIndex > -1 && cmbGT_Driver.SelectedItem.Text != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.Driver == cmbGT_Driver.SelectedItem.Text).ToList();
                }
                //Gateway
                if (dropDownGatewayTransmital_Gateway.SelectedIndex > -1 && dropDownGatewayTransmital_Gateway.SelectedItem.Text != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.Gateway == dropDownGatewayTransmital_Gateway.SelectedItem.Text).ToList();
                }
                //Batch
                if (dropDownGatewayTransmital_Batch.SelectedIndex > -1 && dropDownGatewayTransmital_Batch.SelectedItem.Text != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.Batch == dropDownGatewayTransmital_Batch.SelectedItem.Text).ToList();
                }
                //Commodity Type
                if (cmbGt_CommodityType.SelectedIndex > -1 && cmbGt_CommodityType.SelectedItem.Text != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.CommodityType == cmbGt_CommodityType.SelectedItem.Text).ToList();
                }

                mawb.AddRange(tempGatewayTransmittals.Select(x => x.MAWB).Distinct().ToArray());
                txtGatewayTransmital_MAWB.AutoCompleteCustomSource = mawb;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("GatewayTransmittalLoadMAWBAutoCompleteSource");
                Logs.ErrorLogs(LogPath, "GatewayTransmittalLoadMAWBAutoCompleteSource", ex);
            }
        }
        private void GatewayTransmittalSearch()
        {
            try
            {
                gridGatewayTransmital.FilterDescriptors.Clear();

                //Destination
                if (dropDownGatewayTransmital_Destination.SelectedIndex > -1 && dropDownGatewayTransmital_Destination.SelectedItem.Text != "All")
                {
                    gridGatewayTransmital.FilterDescriptors.Add("Destination", FilterOperator.Contains, dropDownGatewayTransmital_Destination.SelectedItem.Text);
                }
                //Driver
                if (cmbGT_Driver.SelectedIndex > -1 && cmbGT_Driver.SelectedItem.Text != "All")
                {
                    gridGatewayTransmital.FilterDescriptors.Add("Driver", FilterOperator.Contains, cmbGT_Driver.SelectedItem.Text);
                }
                //Gateway
                if (dropDownGatewayTransmital_Gateway.SelectedIndex > -1 && dropDownGatewayTransmital_Gateway.SelectedItem.Text != "All")
                {
                    gridGatewayTransmital.FilterDescriptors.Add("Gateway", FilterOperator.Contains, dropDownGatewayTransmital_Gateway.SelectedItem.Text);
                }
                //Batch
                if (dropDownGatewayTransmital_Batch.SelectedIndex > -1 && dropDownGatewayTransmital_Batch.SelectedItem.Text != "All")
                {
                    gridGatewayTransmital.FilterDescriptors.Add("Batch", FilterOperator.Contains, dropDownGatewayTransmital_Batch.SelectedItem.Text);
                }
                //Commodity Type
                if (cmbGt_CommodityType.SelectedIndex > -1 && cmbGt_CommodityType.SelectedItem.Text != "All")
                {
                    gridGatewayTransmital.FilterDescriptors.Add("CommodityType", FilterOperator.Contains, cmbGt_CommodityType.SelectedItem.Text);
                }
                //MAWB
                if (txtGatewayTransmital_MAWB.Text.Trim() != "")
                {
                    gridGatewayTransmital.FilterDescriptors.Add("MAWB", FilterOperator.Contains, txtGatewayTransmital_MAWB.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayTransmittalSearch", ex);
            }

        }
        private void GatewayTransmittalPrint()
        {
            try
            {
                List<GatewayTransmitalViewModel> tempGatewayTransmittals = GatewayTransmittalList;

                //Destination
                if (dropDownGatewayTransmital_Destination.SelectedIndex > -1 && dropDownGatewayTransmital_Destination.SelectedItem.ToString() != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.Destination == dropDownGatewayTransmital_Destination.SelectedItem.Text).ToList();
                }
                //Driver
                if (cmbGT_Driver.SelectedIndex > -1 && cmbGT_Driver.SelectedItem.Text != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.Driver == cmbGT_Driver.SelectedItem.Text).ToList();
                }
                //Gateway
                if (dropDownGatewayTransmital_Gateway.SelectedIndex > -1 && dropDownGatewayTransmital_Gateway.SelectedItem.Text != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.Gateway == dropDownGatewayTransmital_Gateway.SelectedItem.Text).ToList();
                }
                //Batch
                if (dropDownGatewayTransmital_Batch.SelectedIndex > -1 && dropDownGatewayTransmital_Batch.SelectedItem.Text != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.Batch == dropDownGatewayTransmital_Batch.SelectedItem.Text).ToList();
                }
                //Commodity Type
                if (cmbGt_CommodityType.SelectedIndex > -1 && cmbGt_CommodityType.SelectedItem.Text != "All")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.CommodityType == cmbGt_CommodityType.SelectedItem.Text).ToList();
                }
                //MAWB
                if (txtGatewayTransmital_MAWB.Text.Trim() != "")
                {
                    tempGatewayTransmittals = tempGatewayTransmittals.Where(x => x.MAWB == txtGatewayTransmital_MAWB.Text.Trim()).ToList();
                }
                int ctr = 1;
                tempGatewayTransmittals.ForEach(x => x.No = ctr++);
                ReportGlobalModel.GatewayTransmittalReportData = tempGatewayTransmittals;
                ReportGlobalModel.Date = dateTimeGatewayTransmital_Date.Value.ToLongDateString();
                ReportGlobalModel.Driver = string.Join(", ", tempGatewayTransmittals.Select(x => x.Driver).Distinct());
                ReportGlobalModel.PlateNo = string.Join(", ", tempGatewayTransmittals.Select(x => x.PlateNo).Distinct());
                ReportGlobalModel.AirwayBillNo = string.Join(", ", tempGatewayTransmittals.Select(x => x.MAWB).Distinct());
                ReportGlobalModel.Area = dropDownGatewayTransmital_Destination.SelectedItem.ToString();
                ReportGlobalModel.Gateway = dropDownGatewayTransmital_Gateway.SelectedItem.ToString();
                ReportGlobalModel.ScannedBy = string.Join(", ", tempGatewayTransmittals.Select(x => x.ScannedBy).Distinct());

                ReportGlobalModel.Report = "GatewayTransmital";

                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Gateway Transmital Print", ex);
            }
        }
        #endregion

        #region Tracking Gateway Outbound
        List<GatewayOutboundViewModel> GatewayOutboundList = new List<GatewayOutboundViewModel>();
        private void getGatewayOutboundData(DateTime date)
        {
            DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
            DateTime dateTo = dateFrom.AddDays(1);
            List<GatewayOutbound> Outboundlist = gatewayOutboundService.FilterActiveBy(x => x.OutboundBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();
            List<GatewayTransmittal> Transmittals = gatewayTransmittalService.FilterActiveBy(x => x.TransmittalBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();
            this.GatewayOutboundList = GatewayOutboundEntitiesToView(Transmittals, Outboundlist);
            ReportGlobalModel.GatewayOutboundReportData = GatewayOutboundList;
            this.GatewayOutboundLoadData();
        }
        private List<GatewayOutboundViewModel> GatewayOutboundEntitiesToView(List<GatewayTransmittal> transmittals, List<GatewayOutbound> outbounds)
        {

            List<GatewayOutboundViewModel> _results = new List<GatewayOutboundViewModel>();
            try
            {
                foreach (GatewayOutbound outbound in outbounds)
                {
                    GatewayOutboundViewModel model = new GatewayOutboundViewModel();
                    GatewayTransmittal transmittal = transmittals.Where(x => x.Cargo == outbound.Cargo).FirstOrDefault();
                    if (transmittal == null)
                    {
                        transmittal = transmittals.Where(x => x.AirwayBillNo == outbound.Cargo).FirstOrDefault();
                    }
                    if (transmittal == null) continue;

                    GatewayOutboundViewModel isExist = _results.Find(x => x.AirwayBillNo == transmittal.AirwayBillNo);
                    if (transmittals.Exists(x => x.Cargo == outbound.Cargo))
                    {
                        if (isExist != null)
                        {
                            isExist.TotalRecieved++;
                            isExist.Total = isExist.TotalRecieved;
                        }
                        else
                        {
                            model.AirwayBillNo = transmittal.AirwayBillNo;
                            model.Gateway = outbound.Gateway;
                            model.Driver = outbound.Driver;
                            model.PlateNo = outbound.PlateNo;
                            model.Batch = outbound.Batch.BatchName;
                            model.TotalRecieved++;
                            model.Total = model.TotalRecieved;
                            model.Destination = outbound.BranchCorpOffice.BranchCorpOfficeName;
                            model.ScannedBy = outbound.OutboundBy.Employee.FullName;
                            model.CommodityType = transmittal.CommodityType.CommodityTypeName;
                            model.MAWB = "N/A";
                            model.MAWB = outbound.MasterAirwayBill;

                            _results.Add(model);
                        }
                    }
                    else
                    {
                        if (isExist != null)
                        {
                            isExist.TotalDiscrepency++;
                            isExist.Total = isExist.TotalDiscrepency;
                        }
                        else
                        {
                            model.AirwayBillNo = transmittal.AirwayBillNo;
                            model.Gateway = outbound.Gateway;
                            model.Driver = outbound.Driver;
                            model.PlateNo = outbound.PlateNo;
                            model.Batch = outbound.Batch.BatchName;
                            model.TotalDiscrepency++;
                            model.Total = model.TotalDiscrepency;
                            model.Destination = outbound.BranchCorpOffice.BranchCorpOfficeName;
                            model.ScannedBy = outbound.OutboundBy.Employee.FullName;
                            model.CommodityType = transmittal.CommodityType.CommodityTypeName;
                            model.MAWB = "N/A";
                            model.MAWB = outbound.MasterAirwayBill;

                            _results.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayOutboundEntitiesToView", ex);
            }

            return _results;
        }
        private void GatewayOutboundLoadData()
        {
            try
            {
                this.gridGatewayOutbound.FilterDescriptors.Clear();
                this.gridGatewayOutbound.DataSource = GatewayOutboundList;

                this.dropDownGatewayOutbound_BCO.Items.Clear();
                this.dropDownGatewayOutbound_BCO.Items.Add("All");
                this.dropDownGatewayOutbound_BCO.Items.AddRange(GatewayOutboundList.Select(x => x.Destination).Distinct());
                this.dropDownGatewayOutbound_BCO.SelectedValue = "All";

                this.cmbGO_Driver.Items.Clear();
                this.cmbGO_Driver.Items.Add("All");
                this.cmbGO_Driver.Items.AddRange(GatewayOutboundList.Select(x => x.Driver).Distinct());
                this.cmbGO_Driver.SelectedValue = "All";

                this.dropDownGatewayOutbound_Gateway.Items.Clear();
                this.dropDownGatewayOutbound_Gateway.Items.Add("All");
                this.dropDownGatewayOutbound_Gateway.Items.AddRange(GatewayOutboundList.Select(x => x.Gateway).Distinct());
                this.dropDownGatewayOutbound_Gateway.SelectedValue = "All";

                this.dropDownGatewayOutbound_Batch.Items.Clear();
                this.dropDownGatewayOutbound_Batch.Items.Add("All");
                this.dropDownGatewayOutbound_Batch.Items.AddRange(GatewayOutboundList.Select(x => x.Batch).Distinct());
                this.dropDownGatewayOutbound_Batch.SelectedValue = "All";

                this.cmbGO_commoditType.Items.Clear();
                this.cmbGO_commoditType.Items.Add("All");
                this.cmbGO_commoditType.Items.AddRange(GatewayOutboundList.Select(x => x.CommodityType).Distinct());
                this.cmbGO_commoditType.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                //MessageBox.Show("GatewayOutboundLoadData");
                Logs.ErrorLogs("", "GatewayOutboundLoadData", ex);
            }

        }
        private void GatewayOutboundLoadMAWBAutoCompleteSource()
        {
            AutoCompleteStringCollection mawb = new AutoCompleteStringCollection();
            List<GatewayOutboundViewModel> tempGatewayOutbounds = GatewayOutboundList;
            try
            {
                if (dropDownGatewayOutbound_BCO.SelectedIndex > -1 && dropDownGatewayOutbound_BCO.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.Destination == dropDownGatewayOutbound_BCO.SelectedItem.Text).ToList();
                }
                //Driver Filter
                if (cmbGO_Driver.SelectedIndex > -1 && cmbGO_Driver.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.Driver == cmbGO_Driver.SelectedItem.Text).ToList();
                }
                //Gateway Filter
                if (dropDownGatewayOutbound_Gateway.SelectedIndex > -1 && dropDownGatewayOutbound_Gateway.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.Gateway == dropDownGatewayOutbound_Gateway.SelectedItem.Text).ToList();
                }
                //Batch Filter
                if (dropDownGatewayOutbound_Batch.SelectedIndex > -1 && dropDownGatewayOutbound_Batch.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.Batch == dropDownGatewayOutbound_Batch.SelectedItem.Text).ToList();
                }
                //Commodity Type Filter
                if (cmbGO_commoditType.SelectedIndex > -1 && cmbGO_commoditType.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.CommodityType == cmbGO_commoditType.SelectedItem.Text).ToList();
                }

                mawb.AddRange(tempGatewayOutbounds.Select(x => x.MAWB).Distinct().ToArray());
                txtGO_mawb.AutoCompleteCustomSource = mawb;
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayOutboundLoadMAWBAutoCompleteSource", ex);
            }
            //Destination Filter


        }
        private void GatewayOutboundSearch()
        {
            try
            {
                gridGatewayOutbound.FilterDescriptors.Clear();
                if (dropDownGatewayOutbound_BCO.SelectedIndex > -1 && dropDownGatewayOutbound_BCO.SelectedItem.Text != "All")
                {
                    gridGatewayOutbound.FilterDescriptors.Add("Destination", FilterOperator.Contains, dropDownGatewayOutbound_BCO.SelectedItem.Text);
                }
                if (cmbGO_Driver.SelectedIndex > -1 && cmbGO_Driver.SelectedItem.Text != "All")
                {
                    gridGatewayOutbound.FilterDescriptors.Add("Driver", FilterOperator.Contains, cmbGO_Driver.SelectedItem.Text);
                }
                if (dropDownGatewayOutbound_Gateway.SelectedIndex > -1 && dropDownGatewayOutbound_Gateway.SelectedItem.Text != "All")
                {
                    gridGatewayOutbound.FilterDescriptors.Add("Gateway", FilterOperator.Contains, dropDownGatewayOutbound_Gateway.SelectedItem.Text);
                }
                if (dropDownGatewayOutbound_Batch.SelectedIndex > -1 && dropDownGatewayOutbound_Batch.SelectedItem.Text != "All")
                {
                    gridGatewayOutbound.FilterDescriptors.Add("Batch", FilterOperator.Contains, dropDownGatewayOutbound_Batch.SelectedItem.Text);
                }
                if (cmbGO_commoditType.SelectedIndex > -1 && cmbGO_commoditType.SelectedItem.Text != "All")
                {
                    gridGatewayOutbound.FilterDescriptors.Add("CommodityType", FilterOperator.Contains, cmbGO_commoditType.SelectedItem.Text);
                }
                if (txtGO_mawb.Text.Trim() != "")
                {
                    gridGatewayOutbound.FilterDescriptors.Add("MAWB", FilterOperator.Contains, txtGO_mawb.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("GatewayOutboundSearch");
                Logs.ErrorLogs("", "GatewayOutboundSearch", ex);
            }

        }
        private void GatewayOutboundPrint()
        {
            try
            {
                List<GatewayOutboundViewModel> tempGatewayOutbounds = GatewayOutboundList;

                //Destination Filter
                if (dropDownGatewayOutbound_BCO.SelectedIndex > -1 && dropDownGatewayOutbound_BCO.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.Destination == dropDownGatewayOutbound_BCO.SelectedItem.Text).ToList();
                }
                //Driver Filter
                if (cmbGO_Driver.SelectedIndex > -1 && cmbGO_Driver.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.Driver == cmbGO_Driver.SelectedItem.Text).ToList();
                }
                //Gateway Filter
                if (dropDownGatewayOutbound_Gateway.SelectedIndex > -1 && dropDownGatewayOutbound_Gateway.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.Gateway == dropDownGatewayOutbound_Gateway.SelectedItem.Text).ToList();
                }
                //Batch Filter
                if (dropDownGatewayOutbound_Batch.SelectedIndex > -1 && dropDownGatewayOutbound_Batch.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.Batch == dropDownGatewayOutbound_Batch.SelectedItem.Text).ToList();
                }
                //Commodity Type Filter
                if (cmbGO_commoditType.SelectedIndex > -1 && cmbGO_commoditType.SelectedItem.Text != "All")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.CommodityType == cmbGO_commoditType.SelectedItem.Text).ToList();
                }
                //MAWB Filter
                if (txtGO_mawb.Text.Trim() != "")
                {
                    tempGatewayOutbounds = tempGatewayOutbounds.Where(x => x.MAWB == txtGO_mawb.Text.Trim()).ToList();
                }

                int ctr = 1;
                tempGatewayOutbounds.ForEach(x => x.No = ctr++);
                ReportGlobalModel.GatewayOutboundReportData = tempGatewayOutbounds;
                ReportGlobalModel.Date = dateTimeGatewayOutbound_Date.Value.ToLongDateString();
                ReportGlobalModel.Driver = string.Join(", ", tempGatewayOutbounds.Select(x => x.Driver).Distinct());
                ReportGlobalModel.PlateNo = string.Join(", ", tempGatewayOutbounds.Select(x => x.PlateNo).Distinct());
                ReportGlobalModel.Gateway = string.Join(", ", tempGatewayOutbounds.Select(x => x.Gateway).Distinct());
                ReportGlobalModel.ScannedBy = string.Join(", ", tempGatewayOutbounds.Select(x => x.ScannedBy).Distinct());

                ReportGlobalModel.Report = "GatewayOutbound";
                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("GatewayOutboundPrint");
                Logs.ErrorLogs("", "GatewayOutbound Print", ex);
            }


        }
        #endregion

        #region Tracking Gateway Inbound
        List<GatewayInboundViewModel> GatewayInboundList = new List<GatewayInboundViewModel>();
        private void getInboundData(DateTime date)
        {
            try
            {
                DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
                DateTime dateTo = dateFrom.AddDays(1);
                List<GatewayInbound> result = gatewayInboundService.FilterActiveBy(x => x.InboundBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId
                    && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();
                this.GatewayInboundList = GatewayInboundEntitiesToView(result);
                ReportGlobalModel.GatewayInboundReportData = GatewayInboundList;
                this.GatewayInboundLoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("getInboundData");
                Logs.ErrorLogs("", "getInboundData", ex);
            }

        }
        private List<GatewayInboundViewModel> GatewayInboundEntitiesToView(List<GatewayInbound> inbounds)
        {
            List<GatewayInboundViewModel> _results = new List<GatewayInboundViewModel>();

            foreach (GatewayInbound inbound in inbounds)
            {
                GatewayInboundViewModel model = new GatewayInboundViewModel();
                try
                {
                    PackageNumber _packageNumber = packageNumberService.FilterActiveBy(x => x.PackageNo == inbound.Cargo).FirstOrDefault();

                    //Get Cargoes if not exist in Packagenumber then check from Bundle
                    if (_packageNumber == null)
                    {
                        GatewayInboundViewModel model1 = new GatewayInboundViewModel();
                        int CargoCount = bundleService.FilterActiveBy(x => x.SackNo == inbound.Cargo).Count();
                        if (CargoCount > 0)
                        {
                            GatewayInboundViewModel isExist = _results.Find(x => x.AirwayBillNo == inbound.Cargo);
                            if (isExist != null)
                            {
                                isExist.Pieces++;
                            }
                            else
                            {
                                model1.AirwayBillNo = inbound.Cargo;
                                model1.Gateway = inbound.Gateway;
                                model1.Origin = inbound.OriginBco.BranchCorpOfficeName;
                                model1.Pieces = CargoCount;
                                model1.MAWB = inbound.MasterAirwayBill;
                                model1.FlightNo = inbound.FlightNumber;
                                model1.CommodityType = inbound.CommodityType.CommodityTypeName;
                                model1.CreatedDate = inbound.CreatedDate;
                                model1.ScannedBy = inbound.InboundBy.Employee.FullName;

                                _results.Add(model1);

                            }
                        }
                    }
                    else
                    {
                        string _airwaybill = _packageNumber.Shipment.AirwayBillNo;

                        GatewayInboundViewModel isExist = _results.Find(x => x.AirwayBillNo == _airwaybill);
                        if (isExist != null)
                        {
                            isExist.Pieces++;
                        }
                        else
                        {
                            model.AirwayBillNo = _airwaybill;
                            model.Gateway = inbound.Gateway;
                            model.Origin = inbound.OriginBco.BranchCorpOfficeName;
                            model.Pieces++;
                            model.MAWB = inbound.MasterAirwayBill;
                            model.FlightNo = inbound.FlightNumber;
                            model.CommodityType = inbound.CommodityType.CommodityTypeName;
                            model.CreatedDate = inbound.CreatedDate;
                            model.ScannedBy = inbound.InboundBy.Employee.FullName;

                            _results.Add(model);

                        }
                    }

                }
                catch (Exception ex)
                {
                    Logs.ErrorLogs("", "GatewayInboundEntitiesToView", ex);
                }

            }

            return _results;
        }
        private void GatewayInboundLoadData()
        {
            try
            {
                this.gridGatewayInbound.FilterDescriptors.Clear();
                this.gridGatewayInbound.DataSource = GatewayInboundList;

                this.dropDownGatewayInbound_Gateway.Items.Clear();
                this.dropDownGatewayInbound_Gateway.Items.Add("All");
                this.dropDownGatewayInbound_Gateway.Items.AddRange(GatewayInboundList.Select(x => x.Gateway).Distinct());
                this.dropDownGatewayInbound_Gateway.SelectedValue = "All";

                this.dropDownGatewayInbound_Origin.Items.Clear();
                this.dropDownGatewayInbound_Origin.Items.Add("All");
                this.dropDownGatewayInbound_Origin.Items.AddRange(GatewayInboundList.Select(x => x.Origin).Distinct());
                this.dropDownGatewayInbound_Origin.SelectedValue = "All";

                this.dropDownGatewayInbound_Commodity.Items.Clear();
                this.dropDownGatewayInbound_Commodity.Items.Add("All");
                this.dropDownGatewayInbound_Commodity.Items.AddRange(GatewayInboundList.Select(x => x.CommodityType).Distinct());
                this.dropDownGatewayInbound_Commodity.SelectedValue = "All";

                this.cmbGI_FlightNo.Items.Clear();
                this.cmbGI_FlightNo.Items.Add("All");
                this.cmbGI_FlightNo.Items.AddRange(GatewayInboundList.Select(x => x.FlightNo).Distinct());
                this.cmbGI_FlightNo.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayInboundLoadData", ex);
            }

        }
        private void GatewayInboundLoadMAWBAutoCompleteSource()
        {
            AutoCompleteStringCollection mawb = new AutoCompleteStringCollection();
            List<GatewayInboundViewModel> tempGatewayInboundList = GatewayInboundList;
            try
            {

                //Gateway Filter
                if (dropDownGatewayInbound_Gateway.SelectedIndex > -1 && dropDownGatewayInbound_Gateway.SelectedItem.Text != "All")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.Gateway == dropDownGatewayInbound_Gateway.SelectedItem.Text).ToList();
                }
                //Origin Filter
                if (dropDownGatewayInbound_Origin.SelectedIndex > -1 && dropDownGatewayInbound_Origin.SelectedItem.Text != "All")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.Origin == dropDownGatewayInbound_Origin.SelectedItem.Text).ToList();
                }
                //Commodity Type Filter
                if (dropDownGatewayInbound_Commodity.SelectedIndex > -1 && dropDownGatewayInbound_Commodity.SelectedItem.Text != "All")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.CommodityType == dropDownGatewayInbound_Commodity.SelectedItem.Text).ToList();
                }
                //Flight Number Filter
                if (cmbGI_FlightNo.SelectedIndex > -1 && cmbGI_FlightNo.SelectedItem.Text != "All")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.FlightNo == cmbGI_FlightNo.SelectedItem.Text).ToList();
                }
                mawb.AddRange(tempGatewayInboundList.Select(x => x.MAWB).ToArray());
                txtBoxGatewayInbound_MasterAWB.AutoCompleteCustomSource = mawb;
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayInboundLoadMAWBAutoCompleteSource", ex);
            }
        }
        private void GatewayInboundSearch()
        {
            try
            {
                gridGatewayInbound.FilterDescriptors.Clear();
                if (dropDownGatewayInbound_Gateway.SelectedIndex > -1 && dropDownGatewayInbound_Gateway.SelectedItem.Text != "All")
                {
                    gridGatewayInbound.FilterDescriptors.Add("Gateway", FilterOperator.Contains, dropDownGatewayInbound_Gateway.SelectedItem.Text);
                }
                //Origin Filter
                if (dropDownGatewayInbound_Origin.SelectedIndex > -1 && dropDownGatewayInbound_Origin.SelectedItem.Text != "All")
                {
                    gridGatewayInbound.FilterDescriptors.Add("Origin", FilterOperator.Contains, dropDownGatewayInbound_Origin.SelectedItem.Text);
                }
                //Commodity Type Filter
                if (dropDownGatewayInbound_Commodity.SelectedIndex > -1 && dropDownGatewayInbound_Commodity.SelectedItem.Text != "All")
                {
                    gridGatewayInbound.FilterDescriptors.Add("CommodityType", FilterOperator.Contains, dropDownGatewayInbound_Commodity.SelectedItem.Text);
                }
                //Flight Number Filter
                if (cmbGI_FlightNo.SelectedIndex > -1 && cmbGI_FlightNo.SelectedItem.Text != "All")
                {
                    gridGatewayInbound.FilterDescriptors.Add("FlightInfo", FilterOperator.Contains, cmbGI_FlightNo.SelectedItem.Text);
                }
                if (txtBoxGatewayInbound_MasterAWB.Text.Trim() != "")
                {
                    gridGatewayInbound.FilterDescriptors.Add("MAWB", FilterOperator.Contains, txtBoxGatewayInbound_MasterAWB.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayInboundSearch", ex);
            }
        }
        private void GatewayInboundPrint()
        {
            try
            {

                List<GatewayInboundViewModel> tempGatewayInboundList = GatewayInboundList;
                //Gateway Filter
                if (dropDownGatewayInbound_Gateway.SelectedIndex > -1 && dropDownGatewayInbound_Gateway.SelectedItem.Text != "All")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.Gateway == dropDownGatewayInbound_Gateway.SelectedItem.Text).ToList();
                }
                //Origin Filter
                if (dropDownGatewayInbound_Origin.SelectedIndex > -1 && dropDownGatewayInbound_Origin.SelectedItem.Text != "All")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.Origin == dropDownGatewayInbound_Origin.SelectedItem.Text).ToList();
                }
                //Commodity Type Filter
                if (dropDownGatewayInbound_Commodity.SelectedIndex > -1 && dropDownGatewayInbound_Commodity.SelectedItem.Text != "All")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.CommodityType == dropDownGatewayInbound_Commodity.SelectedItem.Text).ToList();
                }
                //Flight Number Filter
                if (cmbGI_FlightNo.SelectedIndex > -1 && cmbGI_FlightNo.SelectedItem.Text != "All")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.FlightNo == cmbGI_FlightNo.SelectedItem.Text).ToList();
                }
                //MAWB filter
                if (txtBoxGatewayInbound_MasterAWB.Text.Trim() != "")
                {
                    tempGatewayInboundList = tempGatewayInboundList.Where(x => x.MAWB == txtBoxGatewayInbound_MasterAWB.Text.Trim()).ToList();
                }

                int ctr = 1;
                tempGatewayInboundList.ForEach(x => x.No = ctr++);
                ReportGlobalModel.GatewayInboundReportData = tempGatewayInboundList;
                ReportGlobalModel.Date = dateTimePickerGatewayInbound_Date.Value.ToLongDateString();
                ReportGlobalModel.Gateway = dropDownGatewayInbound_Gateway.SelectedItem.ToString();
                ReportGlobalModel.AirwayBillNo = string.Join(", ", tempGatewayInboundList.Select(x => x.MAWB).Distinct());
                ReportGlobalModel.FlightNo = string.Join(", ", tempGatewayInboundList.Select(x => x.FlightNo).Distinct());
                ReportGlobalModel.CommodityType = string.Join(", ", tempGatewayInboundList.Select(x => x.CommodityType).Distinct());
                ReportGlobalModel.ScannedBy = string.Join(", ", tempGatewayInboundList.Select(x => x.ScannedBy).Distinct());
                ReportGlobalModel.Report = "GatewayInbound";

                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "GatewayinboundPrint", ex);
            }
        }

        #endregion

        #region Tracking Cargo Transfer

        List<CargoTransferViewModel> CargoTransferList = new List<CargoTransferViewModel>();
        private void getCargoTransferData(DateTime date)
        {
            DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
            DateTime dateTo = dateFrom.AddDays(1);
            List<CargoTransfer> cargoTransfers = cargotransferService.FilterActiveBy(x => x.TransferBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).GroupBy(x => x.Cargo).Select(y => y.First()).ToList();
            this.CargoTransferList = CargoTransferEntitiesToView(cargoTransfers);
            this.CargoTransferLoadData();
        }
        private List<CargoTransferViewModel> CargoTransferEntitiesToView(List<CargoTransfer> _cargoTransfers)
        {
            List<CargoTransferViewModel> _results = new List<CargoTransferViewModel>();
            PackageNumber _packageNumber = new PackageNumber();
            foreach (CargoTransfer cargoTransfer in _cargoTransfers)
            {
                CargoTransferViewModel model = new CargoTransferViewModel();
                try
                {
                    _packageNumber = packageNumberService.FilterActiveBy(x => x.PackageNo == cargoTransfer.Cargo).FirstOrDefault();
                    if (_packageNumber == null)
                    {
                        CargoTransferViewModel model1 = new CargoTransferViewModel();
                        List<Bundle> bundles = bundleService.FilterActiveBy(x => x.SackNo == cargoTransfer.Cargo).ToList();
                        if (bundles != null && bundles.Count > 0)
                        {
                            CargoTransferViewModel isExist = _results.Find(x => x.AirwayBillNo == cargoTransfer.Cargo);
                            if (isExist != null)
                            {
                                isExist.QTY++;
                                model.Pieces++;
                            }
                            else
                            {
                                model1.RevenueUnitType = cargoTransfer.RevenueUnitType.RevenueUnitTypeName;
                                model1.OriginArea = cargoTransfer.TransferBy.Employee.AssignedToArea.RevenueUnitName;
                                model1.DestinationBco = cargoTransfer.DestinationBco.BranchCorpOfficeName;
                                model1.DestinationArea = cargoTransfer.DestinationBco.BranchCorpOfficeName;
                                model1.Driver = cargoTransfer.Driver;
                                model1.Checker = cargoTransfer.Checker;
                                model1.Pieces = bundles.Count;
                                model1.PlateNo = cargoTransfer.PlateNo;
                                model1.Batch = cargoTransfer.Batch.BatchName;
                                model1.AirwayBillNo = cargoTransfer.Cargo;
                                model1.QTY = bundles.Count();
                                model1.ScannedBy = cargoTransfer.TransferBy.Employee.FullName;

                                _results.Add(model1);
                            }
                        }
                    }
                    else
                    {
                        CargoTransferViewModel isExist = _results.Find(x => x.AirwayBillNo == _packageNumber.Shipment.AirwayBillNo);

                        if (isExist != null)
                        {
                            isExist.QTY++;
                            model.Pieces++;
                        }
                        else
                        {
                            model.RevenueUnitType = cargoTransfer.RevenueUnitType.RevenueUnitTypeName;
                            model.OriginArea = cargoTransfer.TransferBy.Employee.AssignedToArea.RevenueUnitName;
                            model.DestinationArea = cargoTransfer.DestinationArea.RevenueUnitName;
                            model.DestinationBco = cargoTransfer.DestinationBco.BranchCorpOfficeName;
                            model.Driver = cargoTransfer.Driver;
                            model.Checker = cargoTransfer.Checker;
                            model.Pieces++;
                            model.PlateNo = cargoTransfer.PlateNo;
                            model.Batch = cargoTransfer.Batch.BatchName;
                            model.AirwayBillNo = _packageNumber.Shipment.AirwayBillNo;
                            model.QTY++;
                            model.ScannedBy = cargoTransfer.TransferBy.Employee.FullName;

                            _results.Add(model);

                        }
                    }

                }
                catch (Exception ex)
                {

                    Logs.ErrorLogs(LogPath, "CargoTransferEntitiesToView", ex);
                    continue;
                }

            }
            return _results;
        }
        private void CargoTransferLoadData()
        {
            try
            {
                this.gridCargoTransfer.FilterDescriptors.Clear();
                this.gridCargoTransfer.DataSource = CargoTransferList;

                this.cmbCT_BCO.Items.Clear();
                this.cmbCT_BCO.Items.Add("All");
                this.cmbCT_BCO.Items.AddRange(CargoTransferList.Select(x => x.DestinationBco).Distinct());
                this.cmbCT_BCO.SelectedValue = "All";

                this.cmbCT_RevenueType.Items.Clear();
                this.cmbCT_RevenueType.Items.Add("All");
                this.cmbCT_RevenueType.Items.AddRange(CargoTransferList.Select(x => x.RevenueUnitType).Distinct());
                this.cmbCT_RevenueType.SelectedValue = "All";


                this.cmbCT_RevenueUnit.Items.Clear();
                this.cmbCT_RevenueUnit.Items.Add("All");
                this.cmbCT_RevenueUnit.Items.AddRange(CargoTransferList.Select(x => x.DestinationArea).Distinct());
                this.cmbCT_RevenueUnit.SelectedValue = "All";

                this.cmbCT_PlateNo.Items.Clear();
                this.cmbCT_PlateNo.Items.Add("All");
                this.cmbCT_PlateNo.Items.AddRange(CargoTransferList.Select(x => x.PlateNo).Distinct());
                this.cmbCT_PlateNo.SelectedValue = "All";

                this.cmbCT_Batch.Items.Clear();
                this.cmbCT_Batch.Items.Add("All");
                this.cmbCT_Batch.Items.AddRange(CargoTransferList.Select(x => x.Batch).Distinct());
                this.cmbCT_Batch.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "CargoTransferLoadData", ex);
            }

        }
        private void CargoTransferSearch()
        {
            try
            {

                gridCargoTransfer.FilterDescriptors.Clear();
                if (cmbCT_BCO.SelectedIndex > -1 && cmbCT_BCO.SelectedItem.Text != "All")
                {
                    gridCargoTransfer.FilterDescriptors.Add("DestinationBco", FilterOperator.Contains, cmbCT_BCO.SelectedItem.Text);
                }
                if (cmbCT_RevenueType.SelectedIndex > -1 && cmbCT_RevenueType.SelectedItem.Text != "All")
                {
                    gridCargoTransfer.FilterDescriptors.Add("RevenueUnitType", FilterOperator.Contains, cmbCT_RevenueType.SelectedItem.Text);
                }
                if (cmbCT_RevenueUnit.SelectedIndex > -1 && cmbCT_RevenueUnit.SelectedItem.Text != "All")
                {
                    gridCargoTransfer.FilterDescriptors.Add("RevenuUnit", FilterOperator.Contains, cmbCT_RevenueUnit.SelectedItem.Text);
                }
                if (cmbCT_PlateNo.SelectedIndex > -1 && cmbCT_PlateNo.SelectedItem.Text != "All")
                {
                    gridCargoTransfer.FilterDescriptors.Add("PlateNo", FilterOperator.Contains, cmbCT_PlateNo.SelectedItem.Text);
                }
                if (cmbCT_Batch.SelectedIndex > -1 && cmbCT_Batch.SelectedItem.Text != "All")
                {
                    gridCargoTransfer.FilterDescriptors.Add("Batch", FilterOperator.Contains, cmbCT_Batch.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "CargoTranferSearch", ex);
            }
        }
        private void CargoTransferPrint()
        {
            try
            {
                List<CargoTransferViewModel> tempCargoTransfers = CargoTransferList;
                if (cmbCT_BCO.SelectedIndex > -1 && cmbCT_BCO.SelectedItem.Text != "All")
                {
                    tempCargoTransfers = tempCargoTransfers.Where(x => x.DestinationBco == cmbCT_BCO.SelectedItem.Text).ToList();
                }
                if (cmbCT_RevenueType.SelectedIndex > -1 && cmbCT_RevenueType.SelectedItem.Text != "All")
                {
                    tempCargoTransfers = tempCargoTransfers.Where(x => x.RevenueUnitType == cmbCT_RevenueType.SelectedItem.Text).ToList();
                }
                if (cmbCT_RevenueUnit.SelectedIndex > -1 && cmbCT_RevenueUnit.SelectedItem.Text != "All")
                {
                    tempCargoTransfers = tempCargoTransfers.Where(x => x.DestinationArea == cmbCT_RevenueUnit.SelectedItem.Text).ToList();
                }
                if (cmbCT_PlateNo.SelectedIndex > -1 && cmbCT_PlateNo.SelectedItem.Text != "All")
                {
                    tempCargoTransfers = tempCargoTransfers.Where(x => x.PlateNo == cmbCT_PlateNo.SelectedItem.Text).ToList();
                }
                if (cmbCT_Batch.SelectedIndex > -1 && cmbCT_Batch.SelectedItem.Text != "All")
                {
                    tempCargoTransfers = tempCargoTransfers.Where(x => x.Batch == cmbCT_Batch.SelectedItem.Text).ToList();
                }

                int ctr = 1;
                tempCargoTransfers.ForEach(x => x.No = ctr++);
                ReportGlobalModel.CargoTransferReportData = tempCargoTransfers;
                ReportGlobalModel.Date = dateTimeCargoTransfer_Date.Value.ToLongDateString();
                ReportGlobalModel.Origin = string.Join(", ", tempCargoTransfers.Select(x => x.OriginArea).Distinct());
                ReportGlobalModel.Destination = string.Join(", ", tempCargoTransfers.Select(x => x.DestinationArea).Distinct());
                ReportGlobalModel.Driver = string.Join(", ", tempCargoTransfers.Select(x => x.Driver).Distinct());
                ReportGlobalModel.Checker = string.Join(", ", tempCargoTransfers.Select(x => x.Checker).Distinct());
                ReportGlobalModel.PlateNo = string.Join(", ", tempCargoTransfers.Select(x => x.PlateNo).Distinct());
                ReportGlobalModel.ScannedBy = string.Join(", ", tempCargoTransfers.Select(x => x.ScannedBy).Distinct());

                ReportGlobalModel.Report = "CargoTransfer";
                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Cargo Transfer Print", ex);
            }
        }
        #endregion

        #region Tracking Segregation
        List<SegregationViewModel> SegregationList = new List<SegregationViewModel>();
        public void getSegregationData(DateTime date)
        {
            DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
            DateTime dateTo = dateFrom.AddDays(1);
            List<Segregation> _segregations = segregationService.FilterActiveBy(x => x.SegregatedBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo).ToList();
            this.SegregationList = SegregationEntitiesToModel(_segregations);
            this.SegregationLoadData();

        }
        public List<SegregationViewModel> SegregationEntitiesToModel(List<Segregation> _segregation)
        {
            List<SegregationViewModel> _results = new List<SegregationViewModel>();
            try
            {

                foreach (Segregation segregation in _segregation)
                {
                    SegregationViewModel model = new SegregationViewModel();

                    PackageNumber _packageNumber = packageNumberService.FilterActiveBy(x => x.PackageNo == segregation.Cargo).FirstOrDefault();
                    if (_packageNumber == null) continue;

                    SegregationViewModel isExist = _results.Find(x => x.AirwayBillNo == _packageNumber.Shipment.AirwayBillNo);

                    if (isExist != null)
                    {
                        isExist.Qty++;
                    }
                    else
                    {
                        model.OriginBco = segregation.BranchCorpOffice.BranchCorpOfficeName;
                        model.Driver = segregation.Driver;
                        model.Checker = segregation.Checker;
                        model.PlateNo = segregation.PlateNo;
                        model.Batch = segregation.Batch.BatchName;
                        model.AirwayBillNo = _packageNumber.Shipment.AirwayBillNo;
                        model.Qty++;
                        model.Area = _packageNumber.Shipment.AcceptedBy.AssignedToArea.RevenueUnitName;
                        model.ScannedBy = segregation.SegregatedBy.Employee.FullName;

                        _results.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Segregation", ex);
            }

            return _results;
        }
        private void SegregationLoadData()
        {
            try
            {

                this.gridSegregation.FilterDescriptors.Clear();
                this.gridSegregation.DataSource = SegregationList;

                this.dropDownSegregation_BCO.Items.Clear();
                this.dropDownSegregation_BCO.Items.Add("All");
                this.dropDownSegregation_BCO.Items.AddRange(SegregationList.Select(x => x.OriginBco).Distinct());
                this.dropDownSegregation_BCO.SelectedValue = "All";

                this.dropDownSegregation_Driver.Items.Clear();
                this.dropDownSegregation_Driver.Items.Add("All");
                this.dropDownSegregation_Driver.Items.AddRange(SegregationList.Select(x => x.Driver).Distinct());
                this.dropDownSegregation_Driver.SelectedValue = "All";

                this.dropDownSegregation_PlateNo.Items.Clear();
                this.dropDownSegregation_PlateNo.Items.Add("All");
                this.dropDownSegregation_PlateNo.Items.AddRange(SegregationList.Select(x => x.PlateNo).Distinct());
                this.dropDownSegregation_PlateNo.SelectedValue = "All";

                this.dropDownSegregation_Batch.Items.Clear();
                this.dropDownSegregation_Batch.Items.Add("All");
                this.dropDownSegregation_Batch.Items.AddRange(SegregationList.Select(x => x.Batch).Distinct());
                this.dropDownSegregation_Batch.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "SegregationLoadData", ex);
            }
        }
        private void SegregationSearch()
        {
            try
            {

                gridSegregation.FilterDescriptors.Clear();
                if (this.dropDownSegregation_BCO.SelectedIndex > -1 && this.dropDownSegregation_BCO.SelectedItem.Text != "All")
                {
                    gridSegregation.FilterDescriptors.Add("OriginBco", FilterOperator.Contains, this.dropDownSegregation_BCO.SelectedItem.Text);
                }
                if (this.dropDownSegregation_Driver.SelectedIndex > -1 && this.dropDownSegregation_Driver.SelectedItem.Text != "All")
                {
                    gridSegregation.FilterDescriptors.Add("Driver", FilterOperator.Contains, this.dropDownSegregation_Driver.SelectedItem.Text);
                }
                if (this.dropDownSegregation_PlateNo.SelectedIndex > -1 && this.dropDownSegregation_PlateNo.SelectedItem.Text != "All")
                {
                    gridSegregation.FilterDescriptors.Add("PlateNo", FilterOperator.Contains, this.dropDownSegregation_PlateNo.SelectedItem.Text);
                }
                if (this.dropDownSegregation_Batch.SelectedIndex > -1 && this.dropDownSegregation_Batch.SelectedItem.Text != "All")
                {
                    gridSegregation.FilterDescriptors.Add("Batch", FilterOperator.Contains, this.dropDownSegregation_Batch.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "SegregationPrint", ex);
            }
        }
        private void SegregationPrint()
        {
            try
            {
                List<SegregationViewModel> tempSegregation = SegregationList;
                //Origin BCO Filter
                if (this.dropDownSegregation_BCO.SelectedIndex > -1 && this.dropDownSegregation_BCO.SelectedItem.Text != "All")
                {
                    tempSegregation = tempSegregation.Where(x => x.OriginBco == this.dropDownSegregation_BCO.SelectedItem.Text).ToList();
                }
                //Driver Filter
                if (this.dropDownSegregation_Driver.SelectedIndex > -1 && this.dropDownSegregation_Driver.SelectedItem.Text != "All")
                {
                    tempSegregation = tempSegregation.Where(x => x.Driver == this.dropDownSegregation_Driver.SelectedItem.Text).ToList();
                }
                //Plate # Filter
                if (this.dropDownSegregation_PlateNo.SelectedIndex > -1 && this.dropDownSegregation_PlateNo.SelectedItem.Text != "All")
                {
                    tempSegregation = tempSegregation.Where(x => x.PlateNo == this.dropDownSegregation_PlateNo.SelectedItem.Text).ToList();
                }
                //Batch Filter
                if (this.dropDownSegregation_Batch.SelectedIndex > -1 && this.dropDownSegregation_Batch.SelectedItem.Text != "All")
                {
                    tempSegregation = tempSegregation.Where(x => x.Batch == this.dropDownSegregation_Batch.SelectedItem.Text).ToList();
                }

                int ctr = 1;
                tempSegregation.ForEach(x => x.No = ctr++);
                ReportGlobalModel.SegregationReportData = tempSegregation;

                ReportGlobalModel.Date = dateTimeSegregation_Date.Value.ToLongDateString();
                ReportGlobalModel.Driver = dropDownSegregation_Driver.SelectedItem.ToString();
                ReportGlobalModel.Checker = string.Join(", ", tempSegregation.Select(x => x.Checker).Distinct());
                ReportGlobalModel.PlateNo = string.Join(", ", tempSegregation.Select(x => x.PlateNo).Distinct());
                ReportGlobalModel.ScannedBy = string.Join(", ", tempSegregation.Select(x => x.ScannedBy).Distinct());

                ReportGlobalModel.Report = "Segregation";
                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Segregation Print", ex);
            }
        }
        #endregion

        #region Tracking Daily Trip
        List<DailyTripViewModel> DailyTripList = new List<DailyTripViewModel>();
        public void getDailyTripData(DateTime date)
        {

            DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
            DateTime dateTo = dateFrom.AddDays(1);
            List<Distribution> distributions = distributionService.FilterActiveBy(x => x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo && x.Area.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();

            DailyTripList = DailyTripEntitiesToView(distributions);
            ReportGlobalModel.DailyTripReportData = DailyTripList;
            this.DailyTripLoadData();
        }
        public List<DailyTripViewModel> DailyTripEntitiesToView(List<Distribution> _distribution)
        {
            List<DailyTripViewModel> _results = new List<DailyTripViewModel>();
            try
            {

                foreach (Distribution distribution in _distribution)
                {
                    DailyTripViewModel model = new DailyTripViewModel();

                    DailyTripViewModel isExist = _results.Find(x => x.AirwayBillNo == distribution.Shipment.AirwayBillNo);

                    if (isExist != null)
                    {
                        isExist.QTY++;
                    }
                    else
                    {
                        model.Area = distribution.Area.RevenueUnitName;
                        model.AirwayBillNo = distribution.Shipment.AirwayBillNo;
                        model.QTY++;
                        model.Consignee = distribution.Consignee.FullName;
                        model.Address = distribution.Consignee.Address1 + ", " + distribution.Consignee.Address2;
                        model.AGW += distribution.Shipment.Weight;
                        model.ServiceMode = distribution.ServiceMode.ServiceModeName;
                        model.PaymentMode = distribution.PaymentMode.PaymentModeName;
                        model.PaymentCode = distribution.PaymentMode.PaymentModeCode;
                        model.PlateNo = distribution.PlateNo;
                        model.Amount = distribution.Amount;
                        model.Driver = distribution.Driver;
                        model.Checker = distribution.Checker;
                        model.Scannedby = distribution.DistibutedBy.Employee.FullName;
                        model.Batch = distribution.Batch.BatchName;
                        _results.Add(model);
                    }

                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "DailyTripEntitiesToView", ex);
            }

            return _results;

        }
        private void DailyTripLoadData()
        {
            try
            {
                this.gridDailyTrip.FilterDescriptors.Clear();
                this.gridDailyTrip.DataSource = DailyTripList;

                this.dropDownDailyTrip_Area.Items.Clear();
                this.dropDownDailyTrip_Area.Items.Add("All");
                this.dropDownDailyTrip_Area.Items.AddRange(DailyTripList.Select(x => x.Area).Distinct());
                this.dropDownDailyTrip_Area.SelectedValue = "All";

                this.cmbDTR_Batch.Items.Clear();
                this.cmbDTR_Batch.Items.Add("All");
                this.cmbDTR_Batch.Items.AddRange(DailyTripList.Select(x => x.Batch).Distinct());
                this.cmbDTR_Batch.SelectedValue = "All";

                this.dropDownDailyTrip_PaymentMode.Items.Clear();
                this.dropDownDailyTrip_PaymentMode.Items.Add("All");
                this.dropDownDailyTrip_PaymentMode.Items.AddRange(DailyTripList.Select(x => x.PaymentMode).Distinct());
                this.dropDownDailyTrip_PaymentMode.SelectedValue = "All";

            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "DailyTripLoadData", ex);
            }

        }
        private void DailyTripSearch()
        {
            try
            {

                gridDailyTrip.FilterDescriptors.Clear();
                if (this.dropDownDailyTrip_Area.SelectedIndex > -1 && this.dropDownDailyTrip_Area.SelectedItem.Text != "All")
                {
                    gridDailyTrip.FilterDescriptors.Add("Area", FilterOperator.Contains, this.dropDownDailyTrip_Area.SelectedItem.Text);
                }
                if (this.cmbDTR_Batch.SelectedIndex > -1 && this.cmbDTR_Batch.SelectedItem.Text != "All")
                {
                    gridDailyTrip.FilterDescriptors.Add("Batch", FilterOperator.Contains, this.cmbDTR_Batch.SelectedItem.Text);
                }
                if (this.dropDownDailyTrip_PaymentMode.SelectedIndex > -1 && this.dropDownDailyTrip_PaymentMode.SelectedItem.Text != "All")
                {
                    gridDailyTrip.FilterDescriptors.Add("PaymentMode", FilterOperator.Contains, this.dropDownDailyTrip_PaymentMode.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "DailyTripSearch", ex);
            }
        }
        private void DailyTripPrint()
        {
            try
            {

                List<DailyTripViewModel> tempDailyTrips = DailyTripList;
                if (this.dropDownDailyTrip_Area.SelectedIndex > -1 && this.dropDownDailyTrip_Area.SelectedItem.Text != "All")
                {
                    tempDailyTrips = tempDailyTrips.Where(x => x.Area == this.dropDownDailyTrip_Area.SelectedItem.Text).ToList();
                }
                if (this.cmbDTR_Batch.SelectedIndex > -1 && this.cmbDTR_Batch.SelectedItem.Text != "All")
                {
                    tempDailyTrips = tempDailyTrips.Where(x => x.Batch == this.cmbDTR_Batch.SelectedItem.Text).ToList();
                }
                if (this.dropDownDailyTrip_PaymentMode.SelectedIndex > -1 && this.dropDownDailyTrip_PaymentMode.SelectedItem.Text != "All")
                {
                    tempDailyTrips = tempDailyTrips.Where(x => x.PaymentMode == this.dropDownDailyTrip_PaymentMode.SelectedItem.Text).ToList();
                }

                ReportGlobalModel.DailyTripReportData = tempDailyTrips;
                ReportGlobalModel.Date = dateTimeDailyTrip_Date.Value.ToLongDateString();

                ReportGlobalModel.Checker = string.Join(", ", tempDailyTrips.Select(x => x.Checker).Distinct());
                ReportGlobalModel.PlateNo = string.Join(", ", tempDailyTrips.Select(x => x.PlateNo).Distinct());
                ReportGlobalModel.Area = string.Join(", ", tempDailyTrips.Select(x => x.Area).Distinct());
                ReportGlobalModel.Batch = string.Join(", ", tempDailyTrips.Select(x => x.Batch).Distinct());
                ReportGlobalModel.PaymentMode = string.Join(", ", tempDailyTrips.Select(x => x.PaymentMode).Distinct());

                ReportGlobalModel.ScannedBy = string.Join(", ", tempDailyTrips.Select(x => x.Scannedby).Distinct());

                ReportGlobalModel.Report = "DailyTrip";
                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "DailyTripPrint", ex);
            }
        }
        #endregion

        #region Tracking Hold Cargo
        List<HoldCargoViewModel> HoldCargoList = new List<HoldCargoViewModel>();
        public void getHoldCargoData(DateTime fromdate, DateTime todate)
        {
            DateTime dateFrom = new DateTime(fromdate.Year, fromdate.Month, fromdate.Day);
            DateTime dateTo = new DateTime(todate.Year, todate.Month, todate.Day);
            List<HoldCargo> holdCargoes = holdCargoService.FilterActiveBy(x => x.CreatedDate >= fromdate && x.CreatedDate <= todate
                && x.HoldCargoBy.Employee.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();

            this.HoldCargoList = HoldCargoEntitesToView(holdCargoes);
            this.HoldCargoLoadData();

        }
        public List<HoldCargoViewModel> HoldCargoEntitesToView(List<HoldCargo> _holdcargo)
        {
            List<HoldCargoViewModel> _results = new List<HoldCargoViewModel>();
            try
            {

                foreach (HoldCargo holdCargo in _holdcargo)
                {
                    HoldCargoViewModel model = new HoldCargoViewModel();

                    Shipment shipment = shipmentService.FilterActiveBy(x => x.AirwayBillNo == holdCargo.AirwayBillNo).FirstOrDefault();

                    if (shipment != null)
                    {
                        model.Date = holdCargo.HoldCargoDate;
                        model.AirwayBillNo = holdCargo.AirwayBillNo;
                        model.Shipper = shipment.Shipper.FullName;
                        model.Consignee = shipment.Consignee.FullName;
                        model.Address = shipment.Consignee.Address1;
                        model.PaymentMode = shipment.PaymentMode.PaymentModeName;
                        model.ServiceMode = shipment.ServiceMode.ServiceModeName;
                        model.Status = holdCargo.Status.StatusName;
                        model.Reason = holdCargo.Reason.ReasonName;
                        model.EndorsedBy = holdCargo.Endorsedby;
                        model.ScannedBy = holdCargo.HoldCargoBy.Employee.FullName;
                        model.Aging = Math.Round((DateTime.Now - holdCargo.HoldCargoDate).TotalDays, 2);
                        model.ConsigneeContact = shipment.Booking.Consignee.Mobile + "/" + shipment.Booking.Consignee.ContactNo;
                        model.ShipperContact = shipment.Booking.Shipper.Mobile + "/" + shipment.Booking.Shipper.ContactNo;
                        model.RevenueUnitType = holdCargo.HoldCargoBy.Employee.AssignedToArea.RevenueUnitType.RevenueUnitTypeName;
                        model.Area = holdCargo.HoldCargoBy.Employee.AssignedToArea.RevenueUnitName;
                        _results.Add(model);
                    }

                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "HoldCargoEntitiesToView", ex);
            }
            return _results;

        }
        private void HoldCargoLoadData()
        {
            try
            {

                this.gridHoldCargo.FilterDescriptors.Clear();
                this.gridHoldCargo.DataSource = HoldCargoList.OrderByDescending(x => x.Date);
                this.gridHoldCargo.BestFitColumns(BestFitColumnMode.AllCells);

                this.cmbHC_Revenuetype.Items.Clear();
                this.cmbHC_Revenuetype.Items.Add("All");
                this.cmbHC_Revenuetype.Items.AddRange(HoldCargoList.Select(x => x.RevenueUnitType).Distinct());
                this.cmbHC_Revenuetype.SelectedValue = "All";

                this.cmbHC_RevenueUnit.Items.Clear();
                this.cmbHC_RevenueUnit.Items.Add("All");
                this.cmbHC_RevenueUnit.Items.AddRange(HoldCargoList.Select(x => x.Area).Distinct());
                this.cmbHC_RevenueUnit.SelectedValue = "All";

                this.dropDownHoldCargo_Status.Items.Clear();
                this.dropDownHoldCargo_Status.Items.Add("All");
                this.dropDownHoldCargo_Status.Items.AddRange(HoldCargoList.Select(x => x.Status).Distinct());
                this.dropDownHoldCargo_Status.SelectedValue = "All";

                this.cmbHC_Reason.Items.Clear();
                this.cmbHC_Reason.Items.Add("All");
                this.cmbHC_Reason.Items.AddRange(HoldCargoList.Select(x => x.Reason).Distinct());
                this.cmbHC_Reason.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "HoldCargoLoadData", ex);
            }

        }
        private void HoldCargoSearch()
        {
            try
            {

                this.gridHoldCargo.FilterDescriptors.Clear();
                if (this.cmbHC_Revenuetype.SelectedIndex > -1 && this.cmbHC_Revenuetype.SelectedItem.Text != "All")
                {
                    this.gridHoldCargo.FilterDescriptors.Add("RevenueUnitType", FilterOperator.Contains, this.cmbHC_Revenuetype.SelectedItem.Text);
                }
                if (this.cmbHC_RevenueUnit.SelectedIndex > -1 && this.cmbHC_RevenueUnit.SelectedItem.Text != "All")
                {
                    this.gridHoldCargo.FilterDescriptors.Add("Area", FilterOperator.Contains, this.cmbHC_RevenueUnit.SelectedItem.Text);
                }
                if (this.dropDownHoldCargo_Status.SelectedIndex > -1 && this.dropDownHoldCargo_Status.SelectedItem.Text != "All")
                {
                    this.gridHoldCargo.FilterDescriptors.Add("Status", FilterOperator.Contains, this.dropDownHoldCargo_Status.SelectedItem.Text);
                }
                if (this.cmbHC_Reason.SelectedIndex > -1 && this.cmbHC_Reason.SelectedItem.Text != "All")
                {
                    this.gridHoldCargo.FilterDescriptors.Add("Reason", FilterOperator.Contains, this.cmbHC_Reason.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "HoldCargoSearch", ex);
            }
        }
        private void HoldCargoPrint()
        {
            try
            {

                List<HoldCargoViewModel> tempHoldCargoes = HoldCargoList;
                if (this.cmbHC_Revenuetype.SelectedIndex > -1 && this.cmbHC_Revenuetype.SelectedItem.Text != "All")
                {
                    tempHoldCargoes = tempHoldCargoes.Where(x => x.RevenueUnitType == this.cmbHC_Revenuetype.SelectedItem.Text).ToList();
                }
                if (this.cmbHC_RevenueUnit.SelectedIndex > -1 && this.cmbHC_RevenueUnit.SelectedItem.Text != "All")
                {
                    tempHoldCargoes = tempHoldCargoes.Where(x => x.Area == this.cmbHC_RevenueUnit.SelectedItem.Text).ToList();
                }
                if (this.dropDownHoldCargo_Status.SelectedIndex > -1 && this.dropDownHoldCargo_Status.SelectedItem.Text != "All")
                {
                    tempHoldCargoes = tempHoldCargoes.Where(x => x.Status == this.dropDownHoldCargo_Status.SelectedItem.Text).ToList();
                }
                if (this.cmbHC_Reason.SelectedIndex > -1 && this.cmbHC_Reason.SelectedItem.Text != "All")
                {
                    tempHoldCargoes = tempHoldCargoes.Where(x => x.Reason == this.cmbHC_Reason.SelectedItem.Text).ToList();
                }

                int ctr = 1;
                tempHoldCargoes.ForEach(x => x.No = ctr++);

                //waiting for hold cargo report
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "HoldCargoPrint", ex);
            }
        }
        private void HoldCargoExport()
        {
            try
            {
                saveFileDialog2.Filter = "Excel File (*.xlsx)|*.xlsx";
                saveFileDialog2.DefaultExt = "xlsx";
                saveFileDialog2.AddExtension = true;

                saveFileDialog2.FileName = "HoldCargo_(" + DateTime.Now.ToShortDateString().Replace("/", "_") + ").xlsx";
                saveFileDialog2.ShowDialog();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Hold Cargo Export", ex);
            }
        }
        #endregion

        #region Tracking Delivery Status
        List<DeliveryStatusViewModel> DeliveryStatusList = new List<DeliveryStatusViewModel>();
        private void getDeliveryStatusData(DateTime date)
        {
            try
            {
                DateTime dateFrom = new DateTime(date.Year, date.Month, date.Day);
                DateTime dateTo = dateFrom.AddDays(1);
                List<Delivery> list = deliveryService.FilterActiveBy(x => x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo
                    && x.DeliveredBy.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId).ToList();

                this.DeliveryStatusList = DeliveryStatusEntitiesToView(list);
                this.DeliveryStatusLoadData();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Hold Cargo", ex);
            }
        }
        private List<DeliveryStatusViewModel> DeliveryStatusEntitiesToView(List<Delivery> _deliveries)
        {
            List<DeliveryStatusViewModel> _results = new List<DeliveryStatusViewModel>();
            try
            {
                foreach (Delivery delivery in _deliveries)
                {
                    DeliveryStatusViewModel model = new DeliveryStatusViewModel();

                    DeliveryStatusViewModel isExist = _results.Find(x => x.AirwayBillNo == delivery.Shipment.AirwayBillNo);

                    model.AirwayBillNo = delivery.Shipment.AirwayBillNo;
                    model.QTY = delivery.DeliveredPackages.Count();
                    model.Status = delivery.DeliveryStatus.DeliveryStatusName;
                    model.Remarks = "NA";
                    if (delivery.DeliveryRemark != null)
                    {
                        model.Remarks = delivery.DeliveryRemark.DeliveryRemarkName;
                    }
                    Distribution distribution = distributionService.FilterActiveBy(x => x.AirwayBillNo == delivery.Shipment.AirwayBillNo).FirstOrDefault();
                    model.RevenueUnitType = distribution.Area.RevenueUnitType.RevenueUnitTypeName;
                    model.Area = distribution.Area.RevenueUnitName;
                    model.Driver = distribution.Driver;
                    model.Checker = distribution.Checker;
                    model.Batch = distribution.Batch.BatchName;
                    model.PlateNo = distribution.PlateNo;
                    model.BCO = distribution.Area.City.BranchCorpOffice.BranchCorpOfficeName;
                    model.DeliveredBy = delivery.DeliveredBy.FullName;
                    model.ScannedBy = delivery.DeliveredBy.FullName;

                    _results.Add(model);

                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "DeliveryStatusToView", ex);
            }

            return _results;

        }
        private void DeliveryStatusLoadData()
        {
            try
            {

                this.gridDeliveryStatus.FilterDescriptors.Clear();
                this.gridDeliveryStatus.DataSource = DeliveryStatusList;

                this.cmbDS_RevenueType.Items.Clear();
                this.cmbDS_RevenueType.Items.Add("All");
                this.cmbDS_RevenueType.Items.AddRange(DeliveryStatusList.Select(x => x.RevenueUnitType).Distinct());
                this.cmbDS_RevenueType.SelectedValue = "All";

                this.cmbDS_RevenueUnit.Items.Clear();
                this.cmbDS_RevenueUnit.Items.Add("All");
                this.cmbDS_RevenueUnit.Items.AddRange(DeliveryStatusList.Select(x => x.Area).Distinct());
                this.cmbDS_RevenueUnit.SelectedValue = "All";

                this.cmbDS_DeliveredBy.Items.Clear();
                this.cmbDS_DeliveredBy.Items.Add("All");
                this.cmbDS_DeliveredBy.Items.AddRange(DeliveryStatusList.Select(x => x.DeliveredBy).Distinct());
                this.cmbDS_DeliveredBy.SelectedValue = "All";

                this.cmbDS_Status.Items.Clear();
                this.cmbDS_Status.Items.Add("All");
                this.cmbDS_Status.Items.AddRange(DeliveryStatusList.Select(x => x.Status).Distinct());
                this.cmbDS_Status.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "DeliveryStatusLoadData", ex);
            }
        }
        private void DeliveryStatusSearch()
        {
            try
            {

                this.gridDeliveryStatus.FilterDescriptors.Clear();
                if (this.cmbDS_RevenueType.SelectedIndex > -1 && this.cmbDS_RevenueType.SelectedItem.Text != "All")
                {
                    this.gridDeliveryStatus.FilterDescriptors.Add("RevenueUnitType", FilterOperator.Contains, this.cmbDS_RevenueType.SelectedItem.Text);
                }
                if (this.cmbDS_RevenueUnit.SelectedIndex > -1 && this.cmbDS_RevenueUnit.SelectedItem.Text != "All")
                {
                    this.gridDeliveryStatus.FilterDescriptors.Add("Area", FilterOperator.Contains, this.cmbDS_RevenueUnit.SelectedItem.Text);
                }
                if (this.cmbDS_DeliveredBy.SelectedIndex > -1 && this.cmbDS_DeliveredBy.SelectedItem.Text != "All")
                {
                    this.gridDeliveryStatus.FilterDescriptors.Add("DeliveredBy", FilterOperator.Contains, this.cmbDS_DeliveredBy.SelectedItem.Text);
                }
                if (this.cmbDS_Status.SelectedIndex > -1 && this.cmbDS_Status.SelectedItem.Text != "All")
                {
                    this.gridDeliveryStatus.FilterDescriptors.Add("Status", FilterOperator.Contains, this.cmbDS_Status.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "DeliveryStatusSearch", ex);
            }
        }
        private void DeliveryStatusPrint()
        {
            try
            {
                List<DeliveryStatusViewModel> tempDeliveryStatusList = DeliveryStatusList;

                if (this.cmbDS_RevenueType.SelectedIndex > -1 && this.cmbDS_RevenueType.SelectedItem.Text != "All")
                {
                    tempDeliveryStatusList = tempDeliveryStatusList.Where(x => x.RevenueUnitType == this.cmbDS_RevenueType.SelectedItem.Text).ToList();
                }
                if (this.cmbDS_RevenueUnit.SelectedIndex > -1 && this.cmbDS_RevenueUnit.SelectedItem.Text != "All")
                {
                    tempDeliveryStatusList = tempDeliveryStatusList.Where(x => x.Area == this.cmbDS_RevenueUnit.SelectedItem.Text).ToList();
                }
                if (this.cmbDS_DeliveredBy.SelectedIndex > -1 && this.cmbDS_DeliveredBy.SelectedItem.Text != "All")
                {
                    tempDeliveryStatusList = tempDeliveryStatusList.Where(x => x.DeliveredBy == this.cmbDS_DeliveredBy.SelectedItem.Text).ToList();
                }
                if (this.cmbDS_Status.SelectedIndex > -1 && this.cmbDS_Status.SelectedItem.Text != "All")
                {
                    tempDeliveryStatusList = tempDeliveryStatusList.Where(x => x.Status == this.cmbDS_Status.SelectedItem.Text).ToList();
                }

                int ctr = 1;
                tempDeliveryStatusList.ForEach(x => x.No = ctr++);
                ReportGlobalModel.DeliveryStatusReportData = tempDeliveryStatusList;
                ReportGlobalModel.Date = dateTimeDeliveryStatus_Date.Value.ToLongDateString();
                ReportGlobalModel.Area = string.Join(",", tempDeliveryStatusList.Select(x => x.Area).Distinct());
                ReportGlobalModel.Driver = string.Join(",", tempDeliveryStatusList.Select(x => x.Driver).Distinct());
                ReportGlobalModel.Checker = string.Join(",", tempDeliveryStatusList.Select(x => x.Checker).Distinct());
                ReportGlobalModel.ScannedBy = string.Join(",", tempDeliveryStatusList.Select(x => x.ScannedBy).Distinct());

                ReportGlobalModel.Report = "DeliveryStatus";
                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Delivery Status Print", ex);
            }
        }
        #endregion

        #region Tracking Events
        // Tracking Page View
        private void pageViewTracking_SelectedPageChanged(object sender, EventArgs e)
        {

            switch (pageViewTracking.SelectedPage.Text)
            {
                case "Pickup Cargo":
                    dateTimePicker_PickupCargo.Value = DateTime.Now;
                    break;
                case "Branch Acceptance":
                    dateTimePickerBranchAcceptance_Date.Value = DateTime.Now;
                    break;
                case "Bundle":
                    dateTimeBundle_Date.Value = DateTime.Now;
                    break;
                case "Unbundle":
                    dateTimeUnbunde_Date.Value = DateTime.Now;
                    break;
                case "Gateway Transmital":
                    dateTimeGatewayTransmital_Date.Value = DateTime.Now;
                    break;
                case "Gateway Outbound":
                    dateTimeGatewayOutbound_Date.Value = DateTime.Now;
                    break;
                case "Gateway Inbound":
                    dateTimePickerGatewayInbound_Date.Value = DateTime.Now;
                    break;
                case "Cargo Transfer":
                    dateTimeCargoTransfer_Date.Value = DateTime.Now;
                    break;
                case "Segregation":
                    dateTimeSegregation_Date.Value = DateTime.Now;
                    break;
                case "Daily Trip":
                    //DATE
                    dateTimeDailyTrip_Date.Value = DateTime.Now;
                    break;
                case "Hold Cargo":
                    dateTimeHoldCargo_FromDate.Value = DateTime.Now;
                    dateTimeHoldCargo_ToDate.Value = DateTime.Now.AddDays(30);
                    break;
                case "Delivery Status":
                    //DATE
                    dateTimeDeliveryStatus_Date.Value = DateTime.Now;
                    break;
                default:
                    break;
            }
        }

        // **** PICK UP CARGO **** //
        private void btnExport_PickupCargo_Click(object sender, EventArgs e)
        {
            PickUpCargoPrint();
        }
        private void btnSearch_PicupCargo_Click(object sender, EventArgs e)
        {
            PickUpCargoSearch();
        }
        private void dateTimePicker_PickupCargo_ValueChanged(object sender, EventArgs e)
        {
            getPickUpCargoData(dateTimePicker_PickupCargo.Value);
        }
        private void cmb_RevenueUnit_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            PickUpCargoSearch();
        }
        private void cmb_RevenueUnitType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            PickUpCargoSearch();
        }


        // **** BRANCH ACCEPTANCE **** //
        private void btnBranchAcceptance_Search_Click(object sender, EventArgs e)
        {
            BranchAcceptanceSearch();
        }
        private void btnBranchAcceptance_Print_Click(object sender, EventArgs e)
        {
            BranchAcceptancePrint();

        }
        private void dateTimePickerBranchAcceptance_Date_ValueChanged(object sender, EventArgs e)
        {
            getBranchAcceptanceData(dateTimePickerBranchAcceptance_Date.Value);
        }
        private void dropDownBranchAcceptance_Driver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            BranchAcceptanceSearch();
        }
        private void dropDownBranchAcceptance_Batch_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            BranchAcceptanceSearch();
        }
        private void dropDownBranchAcceptance_BCO_BSO_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            BranchAcceptanceSearch();
        }


        // **** BUNDLE **** //
        private void dropDownBundle_Branch_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            LoadSackAutoCompleteSource();
            BundleSearch();
        }
        private void btnBundle_Search_Click(object sender, EventArgs e)
        {
            BundleSearch();
        }
        private void btnBundle_Print_Click(object sender, EventArgs e)
        {
            BundlePrint();
        }
        private void dateTimeBundle_Date_ValueChanged(object sender, EventArgs e)
        {
            getBundleData(dateTimeBundle_Date.Value);

        }
        private void txtBU_SackNo_TextChanged(object sender, EventArgs e)
        {
            BundleSearch();
        }
        // **** UNBUNDLE **** //
        private void btnUnbundle_Search_Click(object sender, EventArgs e)
        {
            UnbundleSearch();
        }
        private void btnUnbundle_Print_Click(object sender, EventArgs e)
        {
            UnbundlePrint();
        }
        private void dateTimeUnbunde_Date_ValueChanged(object sender, EventArgs e)
        {
            this.getUnBundleData(dateTimeUnbunde_Date.Value);

        }
        private void dropDownUnbundle_BCO_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            UnbundleLoadSackAutoCompleteSource();
            UnbundleSearch();
        }
        private void txtUnbundle_SackNo_TextChanged(object sender, EventArgs e)
        {
            UnbundleSearch();
        }
        // **** GATEWAY TRANSMITAL **** //
        private void btnGatewayTransmital_Search_Click(object sender, EventArgs e)
        {
            GatewayTransmittalSearch();
        }
        private void btnGatewayTransmital_Print_Click(object sender, EventArgs e)
        {
            GatewayTransmittalPrint();
        }
        private void dateTimeGatewayTransmital_Date_ValueChanged(object sender, EventArgs e)
        {
            getGatewayTransmitalData(dateTimeGatewayTransmital_Date.Value);

        }
        private void Load_GatewayTransmittal_MAWB_And_Search_AutoCompleteSource(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            GatewayTransmittalSearch();
            GatewayTransmittalLoadMAWBAutoCompleteSource();
        }

        // **** GATEWAT OUTBOUND **** //
        private void btnGatewayOutbound_Search_Click(object sender, EventArgs e)
        {
            GatewayOutboundSearch();
        }
        private void btnGatewayOutbound_Print_Click(object sender, EventArgs e)
        {
            GatewayOutboundPrint();
        }
        private void dateTimeGatewayOutbound_Date_ValueChanged(object sender, EventArgs e)
        {
            getGatewayOutboundData(dateTimeGatewayOutbound_Date.Value);
        }
        private void txtGO_mawb_TextChanged(object sender, EventArgs e)
        {
            GatewayOutboundSearch();
        }
        private void Load_GatewayOutbound_MAWB_And_Search_AutoCompleteSource(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            GatewayOutboundSearch();
            GatewayOutboundLoadMAWBAutoCompleteSource();
        }

        // **** GATEWAY INBOUND **** //
        private void btnGatewayInbound_Search_Click(object sender, EventArgs e)
        {
            GatewayInboundSearch();
        }
        private void btnGatewayInbound_Print_Click(object sender, EventArgs e)
        {
            GatewayInboundPrint();
        }
        private void dateTimePickerGatewayInbound_Date_ValueChanged(object sender, EventArgs e)
        {
            getInboundData(dateTimePickerGatewayInbound_Date.Value);
        }
        private void Load_GatewayInbound_MAWB_And_Search_AutoCompleteSource(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            GatewayInboundSearch();
            GatewayInboundLoadMAWBAutoCompleteSource();
        }
        private void txtBoxGatewayInbound_MasterAWB_TextChanged(object sender, EventArgs e)
        {
            GatewayInboundSearch();
        }
        // ****CARGO TRANSFER **** //

        private void btnCargoTransfer_Search_Click(object sender, EventArgs e)
        {
            CargoTransferSearch();
        }
        private void btnCargoTransfer_Print_Click(object sender, EventArgs e)
        {
            CargoTransferPrint();
        }
        private void dateTimeCargoTransfer_Date_ValueChanged(object sender, EventArgs e)
        {
            getCargoTransferData(dateTimeCargoTransfer_Date.Value);
        }
        private void CargoTransfer_Search(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            CargoTransferSearch();
        }
        // **** SEGREGATION **** //
        private void btnSegregation_Search_Click(object sender, EventArgs e)
        {
            SegregationSearch();
        }
        private void btnSegregation_Print_Click(object sender, EventArgs e)
        {
            SegregationPrint();
        }
        private void dateTimeSegregation_Date_ValueChanged(object sender, EventArgs e)
        {
            getSegregationData(dateTimeSegregation_Date.Value);
        }
        private void Segregation_Filters_SelectIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            SegregationSearch();
        }

        // **** DAILY TRIP **** //
        private void btnDailyTrip_Search_Click(object sender, EventArgs e)
        {
            DailyTripSearch();
        }
        private void btnDailyTrip_Print_Click(object sender, EventArgs e)
        {
            DailyTripPrint();
        }
        private void dateTimeDailyTrip_Date_ValueChanged(object sender, EventArgs e)
        {
            getDailyTripData(dateTimeDailyTrip_Date.Value);

        }
        private void DailyTrip_Filters_SelectIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            DailyTripSearch();
        }

        // **** HOLD CARGO **** //        
        private void btnHoldCargo_Search_Click(object sender, EventArgs e)
        {
            HoldCargoSearch();
        }
        private void btnHoldCargo_Export_Click(object sender, EventArgs e)
        {
            HoldCargoExport();
        }
        private void dateTimeHoldCargo_FromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimeHoldCargo_FromDate.Value <= dateTimeHoldCargo_ToDate.Value)
            {
                getHoldCargoData(dateTimeHoldCargo_FromDate.Value, dateTimeHoldCargo_ToDate.Value);
            }

        }
        private void dateTimeHoldCargo_ToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimeHoldCargo_FromDate.Value <= dateTimeHoldCargo_ToDate.Value)
            {
                getHoldCargoData(dateTimeHoldCargo_FromDate.Value, dateTimeHoldCargo_ToDate.Value);
            }
        }
        private void HoldCargo_Filters_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            HoldCargoSearch();
        }

        // **** DELIVERY STATUS **** //
        private void btnDeliveryStatus_Search_Click(object sender, EventArgs e)
        {
            DeliveryStatusSearch();
        }
        private void btnDeliveryStatus_Print_Click(object sender, EventArgs e)
        {
            DeliveryStatusPrint();
        }
        private void dateTimeDeliveryStatus_Date_ValueChanged(object sender, EventArgs e)
        {
            getDeliveryStatusData(dateTimeDeliveryStatus_Date.Value);
        }
        private void DeliveryStatus_Filters_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            DeliveryStatusSearch();
        }

        // **** OTHERS **** //
        private void saveFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                string exportFile = saveFileDialog2.FileName; // @"E:\Samples\" + "HoldCargo_" + DateTime.Now + ".xlsx";
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    Telerik.WinControls.Export.GridViewSpreadExport exporter = new Telerik.WinControls.Export.GridViewSpreadExport(this.gridHoldCargo);
                    Telerik.WinControls.Export.SpreadExportRenderer renderer = new Telerik.WinControls.Export.SpreadExportRenderer();
                    exporter.RunExport(ms, renderer);

                    using (System.IO.FileStream fileStream = new System.IO.FileStream(exportFile, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(fileStream);
                        MessageBox.Show("Successfully exported!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Print", ex);
            }
        }
        private void Grid_No_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo.Name == "No" && string.IsNullOrEmpty(e.CellElement.Text))
            {
                e.CellElement.Text = (e.CellElement.RowIndex + 1).ToString();
            }
        }
        #endregion

        #region Manifest
        //TODO 1: Create container
        //TODO 2: Create function for loading container with date filter
        //TODO 3: Load gathered data to grid and filters
        //TODO 4: Create search method
        //TODO 5: Create method for search
        //TODO 6: Create method for export
        List<Shipment> ManifestShipments = new List<Shipment>();
        private void Manifest_Get_Data()
        {
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dateTo = currentDate.AddDays(1);
            ManifestShipments = shipmentService.FilterActiveBy(x => x.AcceptedBy.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.CreatedDate >= currentDate && x.CreatedDate <= dateTo);

            ManifestLoadDataAndFilters();
        }
        private void ManifestLoadDataAndFilters()
        {
            gridManifest.FilterDescriptors.Clear();
            gridManifest.DataSource = ManifestShipments;
            gridManifest.BestFitColumns(BestFitColumnMode.AllCells);

            this.cmbManifest_BCO.Items.Clear();
            this.cmbManifest_BCO.Items.Add("All");
            this.cmbManifest_BCO.Items.AddRange(ManifestShipments.Select(x => x.DestinationCity.BranchCorpOffice.BranchCorpOfficeName).Distinct());
            this.cmbManifest_BCO.SelectedValue = "All";

            this.cmbManifest_ServiceMode.Items.Clear();
            this.cmbManifest_ServiceMode.Items.Add("All");
            this.cmbManifest_ServiceMode.Items.AddRange(ManifestShipments.Select(x => x.ServiceMode.ServiceModeName).Distinct());
            this.cmbManifest_ServiceMode.SelectedValue = "All";

            this.cmbManifest_PaymentMode.Items.Clear();
            this.cmbManifest_PaymentMode.Items.Add("All");
            this.cmbManifest_PaymentMode.Items.AddRange(ManifestShipments.Select(x => x.PaymentMode.PaymentModeName).Distinct());
            this.cmbManifest_PaymentMode.SelectedValue = "All";

            this.cmbManifest_ServiceType.Items.Clear();
            this.cmbManifest_ServiceType.Items.Add("All");
            this.cmbManifest_ServiceType.Items.AddRange(ManifestShipments.Select(x => x.ServiceType.ServiceTypeName).Distinct());
            this.cmbManifest_ServiceType.SelectedValue = "All";

            this.cmbManifest_ShipMode.Items.Clear();
            this.cmbManifest_ShipMode.Items.Add("All");
            this.cmbManifest_ShipMode.Items.AddRange(ManifestShipments.Select(x => x.ShipMode.ShipModeName).Distinct());
            this.cmbManifest_ShipMode.SelectedValue = "All";

        }
        private void ManifestSearch()
        {
            gridManifest.FilterDescriptors.Clear();
            if (this.cmbManifest_BCO.SelectedIndex > -1 && this.cmbManifest_BCO.SelectedItem.Text != "All")
            {
                this.gridManifest.FilterDescriptors.Add("DestinationBco", FilterOperator.Contains, this.cmbManifest_BCO.SelectedItem.Text);
            }
            if (this.cmbManifest_ServiceMode.SelectedIndex > -1 && this.cmbManifest_ServiceMode.SelectedItem.Text != "All")
            {
                this.gridManifest.FilterDescriptors.Add("ServiceMode", FilterOperator.Contains, this.cmbManifest_ServiceMode.SelectedItem.Text);
            }
            if (this.cmbManifest_PaymentMode.SelectedIndex > -1 && this.cmbManifest_PaymentMode.SelectedItem.Text != "All")
            {
                this.gridManifest.FilterDescriptors.Add("PaymentMode", FilterOperator.Contains, this.cmbManifest_PaymentMode.SelectedItem.Text);
            }
            if (this.cmbManifest_ServiceType.SelectedIndex > -1 && this.cmbManifest_ServiceType.SelectedItem.Text != "All")
            {
                this.gridManifest.FilterDescriptors.Add("ServiceType", FilterOperator.Contains, this.cmbManifest_ServiceType.SelectedItem.Text);
            }
            if (this.cmbManifest_ShipMode.SelectedIndex > -1 && this.cmbManifest_ShipMode.SelectedItem.Text != "All")
            {
                this.gridManifest.FilterDescriptors.Add("ShipMode", FilterOperator.Contains, this.cmbManifest_ShipMode.SelectedItem.Text);
            }
        }
        private void ManifestPrint()
        {
            try
            {

                List<Shipment> tempManifestData = ManifestShipments;
                if (this.cmbManifest_BCO.SelectedIndex > -1 && this.cmbManifest_BCO.SelectedItem.Text != "All")
                {
                    tempManifestData = tempManifestData.Where(x => x.DestinationCity.BranchCorpOffice.BranchCorpOfficeName == this.cmbManifest_BCO.SelectedItem.Text).ToList();
                }
                if (this.cmbManifest_ServiceMode.SelectedIndex > -1 && this.cmbManifest_ServiceMode.SelectedItem.Text != "All")
                {
                    tempManifestData = tempManifestData.Where(x => x.ServiceMode.ServiceModeName == this.cmbManifest_ServiceMode.SelectedItem.Text).ToList();
                }
                if (this.cmbManifest_PaymentMode.SelectedIndex > -1 && this.cmbManifest_PaymentMode.SelectedItem.Text != "All")
                {
                    tempManifestData = tempManifestData.Where(x => x.PaymentMode.PaymentModeName == this.cmbManifest_PaymentMode.SelectedItem.Text).ToList();
                }
                if (this.cmbManifest_ServiceType.SelectedIndex > -1 && this.cmbManifest_ServiceType.SelectedItem.Text != "All")
                {
                    tempManifestData = tempManifestData.Where(x => x.ServiceType.ServiceTypeName == this.cmbManifest_ServiceType.SelectedItem.Text).ToList();
                }
                if (this.cmbManifest_ShipMode.SelectedIndex > -1 && this.cmbManifest_ShipMode.SelectedItem.Text != "All")
                {
                    tempManifestData = tempManifestData.Where(x => x.ShipMode.ShipModeName == this.cmbManifest_ShipMode.SelectedItem.Text).ToList();
                }

                int ctr = 1;
                tempManifestData.ForEach(x => x.No = ctr++);
                ReportGlobalModel.ManifestReportData = tempManifestData;
                ReportGlobalModel.Manifest_OriginBCO = this.cmbManifest_BCO.SelectedItem.Text;
                ReportGlobalModel.Manifest_DestinationBCO = this.cmbManifest_BCO.SelectedItem.Text;
                ReportGlobalModel.Manifest_ServiceMode = this.cmbManifest_ServiceMode.SelectedItem.Text;
                ReportGlobalModel.Manifest_Paymode = this.cmbManifest_PaymentMode.SelectedItem.Text;
                ReportGlobalModel.Manifest_ServiceType = this.cmbManifest_ServiceType.SelectedItem.Text;
                ReportGlobalModel.Manifest_Shipmode = this.cmbManifest_ShipMode.SelectedItem.Text;

                ReportGlobalModel.Report = "Manifest";

                ReportViewer viewer = new ReportViewer();
                viewer.Show();
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Manifest Print", ex);
            }


        }
        private void Manifest_Filters_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            ManifestSearch();
        }
        private void btnManifestSearch_Click(object sender, EventArgs e)
        {
            ManifestSearch();
        }
        private void btnManifestPrint_Click(object sender, EventArgs e)
        {
            ManifestPrint();
        }
        private void radButton2_Click(object sender, EventArgs e)
        {

            saveFileDialog4.Filter = "Excel (*.xlsx)|*.xlsx";
            saveFileDialog4.DefaultExt = "xlsx";
            saveFileDialog4.AddExtension = true;

            saveFileDialog4.ShowDialog();


        }
        private void saveFileDialog4_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                string name = saveFileDialog4.FileName;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    Telerik.WinControls.Export.GridViewSpreadExport exporter = new Telerik.WinControls.Export.GridViewSpreadExport(this.gridManifest);
                    Telerik.WinControls.Export.SpreadExportRenderer renderer = new Telerik.WinControls.Export.SpreadExportRenderer();
                    exporter.RunExport(ms, renderer);

                    using (System.IO.FileStream fileStream = new System.IO.FileStream(name, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs("", "Manifest Print", ex);
            }
        }
        #endregion

        #region Payment Summary Events
        private void btnSearch_Click(object sender, EventArgs e)
        {
            PaymentSummarySearch();
        }
        private void lstRevenueUnitType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            PaymentSummarySearch();
            if (this.lstRevenueUnitType.SelectedIndex > -1 && this.lstRevenueUnitType.SelectedItem.Text != "All")
            {
                this.lstRevenueUnitName.Items.Clear();
                this.lstRevenueUnitName.Items.AddRange(PaymentSummaryDetailsList.Where(x => x.AcceptedArea.RevenueUnitType.RevenueUnitTypeName == this.lstRevenueUnitType.SelectedItem.Text).Select(x => x.AcceptedArea.RevenueUnitName).Distinct());
            }
        }
        private void lstRevenueUnitName_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            PaymentSummarySearch();
            if (this.lstRevenueUnitName.SelectedIndex > -1 && this.lstRevenueUnitName.SelectedItem.Text != "All")
            {
                this.lstUser.Items.Clear();
                this.lstUser.Items.Add("All");
                this.lstUser.Items.AddRange(PaymentSummaryDetailsList.Where(x => x.AcceptedArea.RevenueUnitName == this.lstRevenueUnitName.SelectedItem.Text).Select(x => x.CollectedBy.FullName).Distinct());
                this.lstUser.SelectedValue = "All";

                this.lstRemittedBy.Items.Clear();
                this.lstRemittedBy.Items.AddRange(PaymentSummaryDetailsList.Where(x => x.AcceptedArea.RevenueUnitName == this.lstRevenueUnitName.SelectedItem.Text).Select(x => x.CollectedBy.FullName).Distinct());

            }
        }
        private void lstUser_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            PaymentSummarySearch();
        }
        private void MasterTemplate_HeaderCellToggleStateChanged(object sender, GridViewHeaderCellEventArgs e)
        {
            //if (e.State == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    ValidatePaymentSummaryDetails(PaymentSummaryDetailsList.Where(x => x.PaymentModeCode == PaymentModes.Prepaid).ToList(), true);
            //}
            //else
            //{
            //    ValidatePaymentSummaryDetails(PaymentSummaryDetailsList.Where(x => x.PaymentModeCode == PaymentModes.Prepaid).ToList(), false);
            //}

            //decimal totalCashReceived = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.Cash
            //         && x.PaymentSummaryStatus.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated).Sum(x => x.AmountDue);
            //decimal totalCheckReceived = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.Check
            //    && x.PaymentSummaryStatus.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated).Sum(x => x.AmountDue);
            //decimal totalAmountReceived = totalCashReceived + totalCheckReceived;

            //txtTotalCashReceived.Text = totalCashReceived.ToString("N2");
            //txtTotalCheckReceived.Text = totalCheckReceived.ToString("N2");
            //txtTotalAmntReceived.Text = totalAmountReceived.ToString("N2");
        }
        private void gridPrepaid_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.ActiveEditor is RadCheckBoxEditor || e.Column.Name == "Validate")
            {
                try
                {

                    string AirwayBillNo = gridPrepaid.Rows[e.RowIndex].Cells["AirwayBillNo"].Value.ToString();
                    if ((bool)e.Value == true)
                    {
                        ValidatePaymentSummaryDetails(PaymentSummaryDetailsList.Where(x => x.AirwayBillNo == AirwayBillNo).FirstOrDefault(), true);
                    }
                    else
                    {
                        ValidatePaymentSummaryDetails(PaymentSummaryDetailsList.Where(x => x.AirwayBillNo == AirwayBillNo).FirstOrDefault(), false);
                    }

                    decimal totalCashReceived = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.Cash
                         && x.PaymentSummaryStatus.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated).Sum(x => x.AmountDue);
                    decimal totalCheckReceived = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.Check
                        && x.PaymentSummaryStatus.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated).Sum(x => x.AmountDue);
                    decimal totalAmountReceived = totalCashReceived + totalCheckReceived;
                    decimal totaltax = PaymentSummaryDetailsList.Where(x => x.PaymentSummaryStatus.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated).Sum(x => x.TaxWithheld);
                    decimal difference = totalAmountReceived - totaltax;

                    txtTotalCashReceived.Text = totalCashReceived.ToString("N2");
                    txtTotalCheckReceived.Text = totalCheckReceived.ToString("N2");
                    txtTotalAmntReceived.Text = totalAmountReceived.ToString("N2");
                    txtDifference.Text = difference.ToString("N2");
                }
                catch (Exception ex)
                {
                    Logs.ErrorLogs(LogPath, "GridPrepaid Cell Value Change", ex);
                }
            }
        }
        private void gridFreightCollect_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.ActiveEditor is RadCheckBoxEditor || e.Column.Name == "Validate")
            {
                try
                {
                    string AirwayBillNo = this.gridFreightCollect.Rows[e.RowIndex].Cells["AirwayBillNo"].Value.ToString();
                    if ((bool)e.Value == true)
                    {
                        ValidatePaymentSummaryDetails(PaymentSummaryDetailsList.Where(x => x.AirwayBillNo == AirwayBillNo).FirstOrDefault(), true);
                    }
                    else
                    {
                        ValidatePaymentSummaryDetails(PaymentSummaryDetailsList.Where(x => x.AirwayBillNo == AirwayBillNo).FirstOrDefault(), false);
                    }

                    decimal totalCashReceived = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.Cash
                         && x.PaymentSummaryStatus.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated).Sum(x => x.AmountDue);
                    decimal totalCheckReceived = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.Check
                        && x.PaymentSummaryStatus.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated).Sum(x => x.AmountDue);
                    decimal totalAmountReceived = totalCashReceived + totalCheckReceived;
                    decimal difference = totalAmountReceived - Convert.ToDecimal(txtTotalTax.Text);

                    txtTotalCashReceived.Text = totalCashReceived.ToString("N2");
                    txtTotalCheckReceived.Text = totalCheckReceived.ToString("N2");
                    txtTotalAmntReceived.Text = totalAmountReceived.ToString("N2");
                    txtDifference.Text = difference.ToString("N2");
                }
                catch (Exception ex)
                {
                    Logs.ErrorLogs(LogPath, "GridFreightCollect Cell Value Change", ex);
                }

            }
        }
        private void gridPrepaid_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            PaymentSummaryDetailsList.ForEach(y => y.PaymentSummaryStatus = paymentSummaryStatuses.Find(x => x.PaymentSummaryStatusName == PaymentSummaryStatuses.Posted));
            PaymentSummaryDetailsList.ForEach(x => x.Status = false);
        }
        private void btnSavePaymentSummary_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstRemittedBy.SelectedIndex > -1)
                {
                    ProgressIndicator saving = new ProgressIndicator("Payment Summary", "Saving ...", SavingofPaymentSummary);
                    saving.ShowDialog();
                    btnPrintPaymentSummary.Enabled = true;
                    //chk_ReceivedAll.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "btnSavePaymentSummary_Click", ex);
            }

        }
        private void btnPrintPaymentSummary_Click(object sender, EventArgs e)
        {
            PaymentSummaryPrint();
        }
        private void dateCollectionDate_ValueChanged(object sender, EventArgs e)
        {
            GetPaymentSummaryData(dateCollectionDate.Value);
        }
        private void btnReceivedAll_Click(object sender, EventArgs e)
        {
            //PaymentSummaryClearData();
            //string awbsoa;
            //string clientName;
            //string paymentType;
            //decimal amountDue;
            //decimal amountPaid;
            //decimal taxWithheld;
            //string OrNo;
            //string PrNo;
            //string ValidatedBy;

            //Guid clientId = new Guid();
            //Guid paymentId = new Guid();
            //Guid validatedById = new Guid();
            //string paymentModeCode;
            //for (int i = 0; i < gridPrepaid.Rows.Count; i++)
            //{
            //    gridPrepaid.Rows[i].Cells["Validate"].Value = true;
            //    awbsoa = gridPrepaid.Rows[i].Cells["AWB No"].Value.ToString();
            //    clientName = gridPrepaid.Rows[i].Cells["Client"].Value.ToString();
            //    amountDue = Convert.ToDecimal(gridPrepaid.Rows[i].Cells["Amount Due"].Value);
            //    taxWithheld = Convert.ToDecimal(gridPrepaid.Rows[i].Cells["Tax Withheld"].Value);
            //    OrNo = gridPrepaid.Rows[i].Cells["OR No"].Value.ToString();
            //    PrNo = gridPrepaid.Rows[i].Cells["PR No"].Value.ToString();
            //    ValidatedBy = gridPrepaid.Rows[i].Cells["Validated By"].Value.ToString();
            //    paymentModeCode = "PP";

            //    amountPaid = Convert.ToDecimal(gridPrepaid.Rows[i].Cells["Amount Paid"].Value);
            //    paymentType = gridPrepaid.Rows[i].Cells["Payment Type"].Value.ToString();
            //    clientId = Guid.Parse(gridPrepaid.Rows[i].Cells["ClientId"].Value.ToString());
            //    paymentId = Guid.Parse(gridPrepaid.Rows[i].Cells["PaymentId"].Value.ToString());
            //    validatedById = Guid.Parse(gridPrepaid.Rows[i].Cells["ValidatedById"].Value.ToString());

            //    if (paymentType.Equals("Cash"))
            //    {
            //        totalCashReceived += amountPaid;
            //    }
            //    else
            //    {
            //        totalCheckReceived += amountPaid;
            //    }

            //    listofPaymentSummary(clientId, paymentId, validatedById, paymentModeCode);
            //    summaryDetails(awbsoa, clientName, paymentType, amountDue, amountPaid, taxWithheld, OrNo, PrNo, ValidatedBy, paymentModeCode);
            //}

            //for (int j = 0; j < gridFreightCollect.Rows.Count; j++)
            //{
            //    gridFreightCollect.Rows[j].Cells["Validate"].Value = true;

            //    awbsoa = gridFreightCollect.Rows[j].Cells["AWB No"].Value.ToString();
            //    clientName = gridFreightCollect.Rows[j].Cells["Client"].Value.ToString();
            //    amountDue = Convert.ToDecimal(gridFreightCollect.Rows[j].Cells["Amount Due"].Value);
            //    taxWithheld = Convert.ToDecimal(gridFreightCollect.Rows[j].Cells["Tax Withheld"].Value);
            //    OrNo = gridFreightCollect.Rows[j].Cells["OR No"].Value.ToString();
            //    PrNo = gridFreightCollect.Rows[j].Cells["PR No"].Value.ToString();
            //    ValidatedBy = gridFreightCollect.Rows[j].Cells["Validated By"].Value.ToString();
            //    paymentModeCode = "FC";


            //    amountPaid = Convert.ToDecimal(gridFreightCollect.Rows[j].Cells["Amount Paid"].Value);
            //    paymentType = gridFreightCollect.Rows[j].Cells["Payment Type"].Value.ToString();
            //    clientId = Guid.Parse(gridFreightCollect.Rows[j].Cells["ClientId"].Value.ToString());
            //    paymentId = Guid.Parse(gridFreightCollect.Rows[j].Cells["PaymentId"].Value.ToString());
            //    validatedById = Guid.Parse(gridFreightCollect.Rows[j].Cells["ValidatedById"].Value.ToString());

            //    if (paymentType.Equals("Cash"))
            //    {
            //        totalCashReceived += amountPaid;
            //    }
            //    else
            //    {
            //        totalCheckReceived += amountPaid;
            //    }

            //    listofPaymentSummary(clientId, paymentId, validatedById, paymentModeCode);
            //    summaryDetails(awbsoa, clientName, paymentType, amountDue, amountPaid, taxWithheld, OrNo, PrNo, ValidatedBy, paymentModeCode);
            //}

            //totalAmountReceived = totalCashReceived + totalCheckReceived;
            //txtTotalCashReceived.Text = totalCashReceived.ToString();
            //txtTotalCheckReceived.Text = totalCheckReceived.ToString();
            //txtTotalAmntReceived.Text = totalAmountReceived.ToString();
        }
        private void img_Signature_MouseDown(object sender, MouseEventArgs e)
        {
            _Previous = e.Location;
            img_Signature_MouseMove(sender, e);
        }
        private void img_Signature_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Previous != null)
            {
                if (img_Signature.Image == null)
                {
                    Bitmap bmp = new Bitmap(img_Signature.Width, img_Signature.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(System.Drawing.Color.White);
                    }
                    img_Signature.Image = bmp;
                    signatureImage = img_Signature.Image;
                }
                using (Graphics g = Graphics.FromImage(img_Signature.Image))
                {
                    //g.DrawLine(Pens.Black, _Previous.Value, e.Location);
                    g.DrawLine(new Pen(System.Drawing.Color.Black, 2), _Previous.Value, e.Location);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                }
                img_Signature.Invalidate(); //refreshes the picturebox
                _Previous = e.Location;//keep assigning the _Previous to the current mouse position
            }
        }
        private void img_Signature_MouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine(img_Signature.Image);
            _Previous = null;
        }
        private void btnReset_CancelPaymentSummary_Click(object sender, EventArgs e)
        {
            PaymentSummaryClearData();
        }
        private void chk_ReceivedAll_CheckStateChanged(object sender, EventArgs e)
        {
            //PaymentSummaryClearData();
            //string awbsoa;
            //string clientName;
            //string paymentType;
            //decimal amountDue;
            //decimal amountPaid;
            //decimal taxWithheld;
            //string OrNo;
            //string PrNo;
            //string ValidatedBy;

            //Guid clientId = new Guid();
            //Guid paymentId = new Guid();
            //Guid validatedById = new Guid();
            //string paymentModeCode;

            //if (chk_ReceivedAll.Checked)
            //{
            //    #region Prepaid Payment

            //    for (int i = 0; i < gridPrepaid.Rows.Count; i++)
            //    {
            //        gridPrepaid.Rows[i].Cells["Validate"].Value = true;
            //        awbsoa = gridPrepaid.Rows[i].Cells["AirwayBillNo"].Value.ToString();
            //        clientName = gridPrepaid.Rows[i].Cells["Client"].Value.ToString();
            //        amountDue = Convert.ToDecimal(gridPrepaid.Rows[i].Cells["Amount Due"].Value);
            //        taxWithheld = Convert.ToDecimal(gridPrepaid.Rows[i].Cells["Tax Withheld"].Value);
            //        OrNo = gridPrepaid.Rows[i].Cells["OR No"].Value.ToString();
            //        PrNo = gridPrepaid.Rows[i].Cells["PR No"].Value.ToString();
            //        ValidatedBy = gridPrepaid.Rows[i].Cells["Validated By"].Value.ToString();
            //        paymentModeCode = "PP";

            //        amountPaid = Convert.ToDecimal(gridPrepaid.Rows[i].Cells["Amount Paid"].Value);
            //        paymentType = gridPrepaid.Rows[i].Cells["Payment Type"].Value.ToString();
            //        clientId = Guid.Parse(gridPrepaid.Rows[i].Cells["ClientId"].Value.ToString());
            //        paymentId = Guid.Parse(gridPrepaid.Rows[i].Cells["PaymentId"].Value.ToString());
            //        validatedById = Guid.Parse(gridPrepaid.Rows[i].Cells["ValidatedById"].Value.ToString());

            //        if (paymentType.Equals("Cash"))
            //        {
            //            totalCashReceived += amountPaid;
            //        }
            //        else
            //        {
            //            totalCheckReceived += amountPaid;
            //        }

            //        listofPaymentSummary(clientId, paymentId, validatedById, paymentModeCode);
            //        summaryDetails(awbsoa, clientName, paymentType, amountDue, amountPaid, taxWithheld, OrNo, PrNo, ValidatedBy, paymentModeCode);
            //    }

            //    #endregion

            //    #region Freight Collect
            //    for (int j = 0; j < gridFreightCollect.Rows.Count; j++)
            //    {
            //        gridFreightCollect.Rows[j].Cells["Validate"].Value = true;

            //        awbsoa = gridFreightCollect.Rows[j].Cells["AWB No"].Value.ToString();
            //        clientName = gridFreightCollect.Rows[j].Cells["Client"].Value.ToString();
            //        amountDue = Convert.ToDecimal(gridFreightCollect.Rows[j].Cells["Amount Due"].Value);
            //        taxWithheld = Convert.ToDecimal(gridFreightCollect.Rows[j].Cells["Tax Withheld"].Value);
            //        OrNo = gridFreightCollect.Rows[j].Cells["OR No"].Value.ToString();
            //        PrNo = gridFreightCollect.Rows[j].Cells["PR No"].Value.ToString();
            //        ValidatedBy = gridFreightCollect.Rows[j].Cells["Validated By"].Value.ToString();
            //        paymentModeCode = "FC";


            //        amountPaid = Convert.ToDecimal(gridFreightCollect.Rows[j].Cells["Amount Paid"].Value);
            //        paymentType = gridFreightCollect.Rows[j].Cells["Payment Type"].Value.ToString();
            //        clientId = Guid.Parse(gridFreightCollect.Rows[j].Cells["ClientId"].Value.ToString());
            //        paymentId = Guid.Parse(gridFreightCollect.Rows[j].Cells["PaymentId"].Value.ToString());
            //        validatedById = Guid.Parse(gridFreightCollect.Rows[j].Cells["ValidatedById"].Value.ToString());

            //        if (paymentType.Equals("Cash"))
            //        {
            //            totalCashReceived += amountPaid;
            //        }
            //        else
            //        {
            //            totalCheckReceived += amountPaid;
            //        }

            //        listofPaymentSummary(clientId, paymentId, validatedById, paymentModeCode);
            //        summaryDetails(awbsoa, clientName, paymentType, amountDue, amountPaid, taxWithheld, OrNo, PrNo, ValidatedBy, paymentModeCode);
            //    }

            //    #endregion

            //}
            //else
            //{
            //    #region Prepaid
            //    for (int i = 0; i < gridPrepaid.Rows.Count; i++)
            //    {
            //        gridPrepaid.Rows[i].Cells["Validate"].Value = false;
            //        awbsoa = gridPrepaid.Rows[i].Cells["AWB No"].Value.ToString();
            //        clientId = Guid.Parse(gridPrepaid.Rows[i].Cells["ClientId"].Value.ToString());
            //        paymentType = gridPrepaid.Rows[i].Cells["Payment Type"].Value.ToString();
            //        amountPaid = Convert.ToDecimal(gridPrepaid.Rows[i].Cells["Amount Paid"].Value);

            //        //if (paymentType.Equals("Cash"))
            //        //{
            //        //    totalCashReceived -= amountPaid;
            //        //}
            //        //else
            //        //{
            //        //    totalCheckReceived -= amountPaid;
            //        //}


            //        var itemRemovePrepaid = listPaymentSummary.Find(r => r.ClientId == clientId);
            //        if (itemRemovePrepaid != null)
            //            listPaymentSummary.Remove(itemRemovePrepaid);

            //        var removePrepaidDetails = listpaymentSummaryDetails.Find(r => r.AirwayBillNo == awbsoa);
            //        if (removePrepaidDetails != null)
            //            listpaymentSummaryDetails.Remove(removePrepaidDetails);
            //    }

            //    #endregion

            //    #region Freight Collect
            //    for (int j = 0; j < gridFreightCollect.Rows.Count; j++)
            //    {
            //        gridFreightCollect.Rows[j].Cells["Validate"].Value = false;
            //        clientId = Guid.Parse(gridFreightCollect.Rows[j].Cells["ClientId"].Value.ToString());
            //        awbsoa = gridFreightCollect.Rows[j].Cells["AWB No"].Value.ToString();
            //        paymentType = gridFreightCollect.Rows[j].Cells["Payment Type"].Value.ToString();
            //        amountPaid = Convert.ToDecimal(gridFreightCollect.Rows[j].Cells["Amount Paid"].Value);
            //        //if (paymentType.Equals("Cash"))
            //        //{
            //        //    totalCashReceived -= amountPaid;
            //        //}
            //        //else
            //        //{
            //        //    totalCheckReceived -= amountPaid;
            //        //}
            //        var itemRemoveFreight = listPaymentSummary.Find(r => r.ClientId == clientId);
            //        if (itemRemoveFreight != null)
            //            listPaymentSummary.Remove(itemRemoveFreight);

            //        var removeFCDetails = listpaymentSummaryDetails.Find(r => r.AirwayBillNo == awbsoa);
            //        if (removeFCDetails != null)
            //            listpaymentSummaryDetails.Remove(removeFCDetails);
            //    }
            //    #endregion

            //    PaymentSummaryClearData();
            //}

            //totalAmountReceived = totalCashReceived + totalCheckReceived;
            //txtTotalCashReceived.Text = totalCashReceived.ToString();
            //txtTotalCheckReceived.Text = totalCheckReceived.ToString();
            //txtTotalAmntReceived.Text = totalAmountReceived.ToString();
        }

        #endregion

        #region PaymentSummary Methods

        List<PaymentSummaryDetails> PaymentSummaryDetailsList = new List<PaymentSummaryDetails>();
        List<PaymentSummaryStatus> paymentSummaryStatuses = new List<PaymentSummaryStatus>();
        public void PaymentSummaryLoadInit()
        {
            bsRevenueUnitType = new BindingSource();

            employees = new List<Employee>();
            paymentPrepaid = new List<Payment>();
            paymentFreightCollect = new List<Payment>();
            paymentCorpAcctConsignee = new List<Payment>();
            paymentSummary_revenueUnits = new List<RevenueUnit>();
            paymentSummary_revenueUnitType = new List<RevenueUnitType>();
            paymentSummary_employee = new List<Employee>();

            employeeService = new EmployeeBL(GlobalVars.UnitOfWork);
            paymentService = new PaymentBL(GlobalVars.UnitOfWork);
            revenueUnitTypeService = new RevenueUnitTypeBL(GlobalVars.UnitOfWork);
            revenueUnitservice = new RevenueUnitBL(GlobalVars.UnitOfWork);
            paymentSummaryStatusService = new PaymentSummaryStatusBL(GlobalVars.UnitOfWork);
            paymentSummaryService = new PaymentSummaryBL(GlobalVars.UnitOfWork);

            paymentSummaryStatuses = paymentSummaryStatusService.FilterActive();

        }
        private void GetPaymentSummaryData(DateTime collectionDate)
        {
            DateTime dateFrom = new DateTime(collectionDate.Year, collectionDate.Month, collectionDate.Day);
            DateTime dateTo = dateFrom.AddDays(1);
            List<Payment> payments = paymentService.FilterActiveBy(x => x.CreatedDate >= dateFrom && x.CreatedDate <= dateTo &&
                x.ReceivedBy.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId);

            PaymentEntitiesToModel(payments);
            PaymentSummaryLoadData();
        }
        private void PaymentEntitiesToModel(List<Payment> Payments)
        {
            PaymentSummaryStatus status = paymentSummaryStatusService.FilterActiveBy(x => x.PaymentSummaryStatusName == "Posted").FirstOrDefault();
            PaymentSummaryDetailsList = new List<PaymentSummaryDetails>();
            try
            {
                foreach (Payment payment in Payments)
                {
                    bool isValidated = paymentSummaryService.IsExist(x => x.PaymentId == payment.PaymentId);
                    if (isValidated == false)
                    {
                        PaymentSummaryDetails details = new PaymentSummaryDetails();
                        details.PaymentId = payment.PaymentId;
                        details.RemittedById = payment.ReceivedById;
                        details.RemittedBy = payment.ReceivedBy;
                        details.PaymentSummaryStatus = status;
                        details.AirwayBillNo = payment.Shipment.AirwayBillNo;
                        details.Client = payment.Shipment.Shipper;
                        details.PaymentTypeName = payment.PaymentType.PaymentTypeName;
                        details.AmountDue = payment.Shipment.TotalAmount;
                        details.AmountPaid = payment.Amount;
                        details.TaxWithheld = payment.TaxWithheld;
                        details.OrNo = payment.OrNo ?? "N/A";
                        details.PrNo = payment.PrNo ?? "N/A";
                        details.CollectedBy = payment.ReceivedBy;
                        details.ValidatedBy = AppUser.Employee.FullName;
                        details.PaymentModeCode = payment.Shipment.PaymentMode.PaymentModeCode;
                        details.AcceptedArea = payment.ReceivedBy.AssignedToArea;
                        details.Status = false;
                        details.ValidatedById = AppUser.Employee.EmployeeId;
                        details.IsSaved = false;
                        PaymentSummaryDetailsList.Add(details);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "PaymentSummaryEntitiesToModel", ex);
            }
        }
        private void PaymentSummaryLoadData()
        {
            GridViewSummaryItem amountItem = new GridViewSummaryItem("AmountDue", "Sub-Total = {0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryItem taxItem = new GridViewSummaryItem("Tax", "Total Tax = {0:N2}", GridAggregateFunction.Sum);
            GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem(new GridViewSummaryItem[] { amountItem, taxItem });
            //Prepaid
            this.gridPrepaid.FilterDescriptors.Clear();
            this.gridPrepaid.DataSource = PaymentSummaryDetailsList.Where(x => x.PaymentModeCode == PaymentModes.Prepaid || x.PaymentModeCode == PaymentModes.DeferredPayment).ToList();
            this.gridPrepaid.MasterTemplate.ShowTotals = true;
            this.gridPrepaid.SummaryRowsBottom.Clear();
            this.gridPrepaid.SummaryRowsBottom.Add(summaryRowItem);
            this.gridPrepaid.MasterView.SummaryRows[0].PinPosition = PinnedRowPosition.Bottom;
            this.gridPrepaid.BestFitColumns(BestFitColumnMode.AllCells);
            //Freight Collect
            this.gridFreightCollect.FilterDescriptors.Clear();
            this.gridFreightCollect.DataSource = PaymentSummaryDetailsList.Where(x => x.PaymentModeCode == PaymentModes.FreightCollect).ToList();
            this.gridFreightCollect.MasterTemplate.ShowTotals = true;
            this.gridFreightCollect.SummaryRowsBottom.Clear();
            this.gridFreightCollect.SummaryRowsBottom.Add(summaryRowItem);
            this.gridFreightCollect.MasterView.SummaryRows[0].PinPosition = PinnedRowPosition.Bottom;
            this.gridFreightCollect.BestFitColumns(BestFitColumnMode.AllCells);
            //Corporate Account
            this.gridCorpAcctConsignee.FilterDescriptors.Clear();
            this.gridCorpAcctConsignee.DataSource = PaymentSummaryDetailsList.Where(x => x.PaymentModeCode == PaymentModes.CorporateAccountConsignee || x.PaymentModeCode == PaymentModes.CorporateAccountShipper).ToList();
            this.gridCorpAcctConsignee.MasterTemplate.ShowTotals = true;
            this.gridCorpAcctConsignee.SummaryRowsBottom.Clear();
            this.gridCorpAcctConsignee.SummaryRowsBottom.Add(summaryRowItem);
            this.gridCorpAcctConsignee.MasterView.SummaryRows[0].PinPosition = PinnedRowPosition.Bottom;
            this.gridCorpAcctConsignee.BestFitColumns(BestFitColumnMode.AllCells);

            lstRevenueUnitType.Items.Clear();
            //lstRevenueUnitType.Items.Add("All");
            lstRevenueUnitType.Items.AddRange(PaymentSummaryDetailsList.Select(x => x.AcceptedArea.RevenueUnitType.RevenueUnitTypeName).Distinct());
            //lstRevenueUnitType.SelectedValue = "All";

            lstRevenueUnitName.Items.Clear();
            //lstRevenueUnitName.Items.Add("All");
            lstRevenueUnitName.Items.AddRange(PaymentSummaryDetailsList.Select(x => x.AcceptedArea.RevenueUnitName).Distinct());
            //lstRevenueUnitName.SelectedValue = "All";

            lstUser.Items.Clear();
            lstUser.Items.Add("All");
            lstUser.Items.AddRange(PaymentSummaryDetailsList.Select(x => x.CollectedBy.FullName).Distinct());
            lstUser.SelectedValue = "All";

            lstRemittedBy.Items.Clear();
            lstRemittedBy.Items.Add("All");
            lstRemittedBy.Items.AddRange(PaymentSummaryDetailsList.Select(x => x.CollectedBy.FullName).Distinct());
            lstRemittedBy.SelectedValue = "All";

            decimal totalCash = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.Cash).Sum(x => x.AmountDue);
            decimal totalCheck = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.Check).Sum(x => x.AmountDue);
            decimal totalCollection = totalCash + totalCheck;
            decimal totaltax = PaymentSummaryDetailsList.Sum(x => x.TaxWithheld);

            this.txtTotalCash.Text = totalCash.ToString("N2");
            this.txtTotalCheck.Text = totalCheck.ToString("N2");
            this.txtTotalCollection.Text = totalCollection.ToString("N2");
            this.txtTotalTax.Text = totaltax.ToString();
            this.txtTotalPdc.Text = PaymentSummaryDetailsList.Where(x => x.PaymentTypeName == PaymentTypes.PostDatedCheck).Sum(x => x.AmountDue).ToString("N2");
            this.txtTotalCashReceived.Text = "0.00";
            this.txtTotalCheckReceived.Text = "0.00";
            this.txtTotalAmntReceived.Text = "0.00";
            this.txtDifference.Text = "0.00";

            if (img_Signature.Image != null)
            {
                img_Signature.Image = null;
                Invalidate();
            }

        }
        private void ValidatePaymentSummaryDetails(PaymentSummaryDetails details, bool status)
        {
            if (status)
            {
                this.PaymentSummaryDetailsList.FirstOrDefault(x => x.AirwayBillNo == details.AirwayBillNo).PaymentSummaryStatus = paymentSummaryStatuses.Where(x => x.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated).FirstOrDefault();
                this.PaymentSummaryDetailsList.FirstOrDefault(x => x.AirwayBillNo == details.AirwayBillNo).Status = status;
            }
            else
            {
                this.PaymentSummaryDetailsList.FirstOrDefault(x => x.AirwayBillNo == details.AirwayBillNo).PaymentSummaryStatus = paymentSummaryStatuses.Where(x => x.PaymentSummaryStatusName == PaymentSummaryStatuses.Posted).FirstOrDefault();
                this.PaymentSummaryDetailsList.FirstOrDefault(x => x.AirwayBillNo == details.AirwayBillNo).Status = status;
            }

        }
        private void PaymentSummarySearch()
        {
            try
            {
                List<PaymentSummaryDetails> tempList = PaymentSummaryDetailsList.ToList();

                this.gridPrepaid.FilterDescriptors.Clear();
                this.gridFreightCollect.FilterDescriptors.Clear();
                this.gridCorpAcctConsignee.FilterDescriptors.Clear();

                if (this.lstRevenueUnitType.SelectedIndex > -1 && this.lstRevenueUnitType.SelectedItem.Text != "All")
                {
                    this.gridPrepaid.FilterDescriptors.Add("RevenueUnitType", FilterOperator.Contains, this.lstRevenueUnitType.SelectedItem.Text);
                    this.gridFreightCollect.FilterDescriptors.Add("RevenueUnitType", FilterOperator.Contains, this.lstRevenueUnitType.SelectedItem.Text);
                    this.gridCorpAcctConsignee.FilterDescriptors.Add("RevenueUnitType", FilterOperator.Contains, this.lstRevenueUnitType.SelectedItem.Text);

                    tempList = PaymentSummaryDetailsList.Where(x => x.AcceptedArea.RevenueUnitType.RevenueUnitTypeName == this.lstRevenueUnitType.SelectedItem.Text).ToList();
                }
                if (this.lstRevenueUnitName.SelectedIndex > -1 && this.lstRevenueUnitName.SelectedItem.Text != "All")
                {
                    this.gridPrepaid.FilterDescriptors.Add("RevenueUnitName", FilterOperator.Contains, this.lstRevenueUnitName.SelectedItem.Text);
                    this.gridFreightCollect.FilterDescriptors.Add("RevenueUnitName", FilterOperator.Contains, this.lstRevenueUnitName.SelectedItem.Text);
                    this.gridCorpAcctConsignee.FilterDescriptors.Add("RevenueUnitName", FilterOperator.Contains, this.lstRevenueUnitName.SelectedItem.Text);

                    tempList = PaymentSummaryDetailsList.Where(x => x.AcceptedArea.RevenueUnitName == this.lstRevenueUnitName.SelectedItem.Text).ToList();
                }
                if (this.lstUser.SelectedIndex > -1 && this.lstUser.SelectedItem.Text != "All")
                {
                    this.gridPrepaid.FilterDescriptors.Add("ReceivedBy", FilterOperator.Contains, this.lstUser.SelectedItem.Text);
                    this.gridFreightCollect.FilterDescriptors.Add("ReceivedBy", FilterOperator.Contains, this.lstUser.SelectedItem.Text);
                    this.gridCorpAcctConsignee.FilterDescriptors.Add("ReceivedBy", FilterOperator.Contains, this.lstUser.SelectedItem.Text);

                    tempList = PaymentSummaryDetailsList.Where(x => x.CollectedBy.FullName == this.lstUser.SelectedItem.Text).ToList();
                }

                decimal totalCash = tempList.Where(x => x.PaymentTypeName == PaymentTypes.Cash).Sum(x => x.AmountDue);
                decimal totalCheck = tempList.Where(x => x.PaymentTypeName == PaymentTypes.Check).Sum(x => x.AmountDue);
                decimal totalCollection = totalCash + totalCheck;
                decimal totaltax = tempList.Sum(x => x.TaxWithheld);

                this.txtTotalCash.Text = totalCash.ToString("N2");
                this.txtTotalCheck.Text = totalCheck.ToString("N2");
                this.txtTotalCollection.Text = totalCollection.ToString("N2");
                this.txtTotalTax.Text = totaltax.ToString();
                this.txtTotalPdc.Text = tempList.Where(x => x.PaymentTypeName == PaymentTypes.PostDatedCheck).Sum(x => x.AmountDue).ToString("N2");
                this.txtTotalCashReceived.Text = "0.00";
                this.txtTotalCheckReceived.Text = "0.00";
                this.txtTotalAmntReceived.Text = "0.00";
                this.txtDifference.Text = "0.00";
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "PaymentSummarySearch", ex);
            }
        }
        private void PaymentSummaryPrint()
        {
            try
            {
                if (lstRemittedBy.SelectedIndex > -1)
                {
                    ReportGlobalModel.PaymentSummaryDetailsReportData = PaymentSummaryDetailsList.Where(x => x.PaymentSummaryStatus == paymentSummaryStatuses.Find(y => y.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated)).ToList();
                    ReportGlobalModel.PaymentSummary_MainDetailsReportData = amountPaymentSummary();

                    ReportGlobalModel.Report = "PaymentSummaryReport";
                    ReportViewer viewer = new ReportViewer();
                    viewer.Show();
                }
            }
            catch (Exception ex)
            {
                Logs.ErrorLogs(LogPath, "Payment Summary Print", ex);
            }
        }
        private List<PaymentSummary> PaymentSummarySave()
        {
            List<PaymentSummaryDetails> PaymentSummaryToBeSave = PaymentSummaryDetailsList.Where(x => x.Status == true &&
                x.PaymentSummaryStatus == paymentSummaryStatuses.Find(y => y.PaymentSummaryStatusName == PaymentSummaryStatuses.Validated)).ToList();

            List<PaymentSummary> Summaries = new List<PaymentSummary>();
            Guid remmittedBy = PaymentSummaryToBeSave.Where(x => x.RemittedBy.FullName == lstRemittedBy.SelectedItem.Text && x.IsSaved == false).First().RemittedById;
            foreach (PaymentSummaryDetails details in PaymentSummaryToBeSave)
            {
                PaymentSummary summary = new PaymentSummary();
                details.IsSaved = true;
                summary.ClientId = details.Client.ClientId;
                summary.PaymentId = details.PaymentId;
                summary.CheckedBy = details.ValidatedById;
                summary.RemittedBy = remmittedBy;
                summary.ValidatedBy = details.ValidatedById;
                summary.PaymentSummaryStatusId = details.PaymentSummaryStatus.PaymentSummaryStatusId;
                summary.DateAccepted = DateTime.Now;
                summary.Remarks = txtRemarksPaymentSummary.Text.Trim();
                summary.Signature = imgToByteArray(signatureImage);
                summary.CreatedDate = DateTime.Now;
                summary.CreatedBy = AppUser.User.UserId;
                summary.ModifiedBy = AppUser.User.UserId;
                summary.ModifiedDate = DateTime.Now;
                summary.RecordStatus = (int)RecordStatus.Active;
                Summaries.Add(summary);
            }

            return Summaries;
        }
        private void SavingofPaymentSummary(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker _worker = sender as BackgroundWorker;
            int percent = 1;
            int index = 1;
            int max = 2; // # of processes

            if (PaymentSummaryDetailsList != null)
            {
                List<PaymentSummary> summaries = PaymentSummarySave();
                paymentSummaryService.AddMultiple(summaries);
                percent = index * 100 / max;
                _worker.ReportProgress(percent);
                index++;
            }

        }
        public List<PaymentSummary_MainDetailsModel> amountPaymentSummary()
        {
            String dt = dateCollectionDate.Text.ToString();
            string collectedBy = lstUser.Text;
            string Area = lstRevenueUnitName.Text;
            decimal TotalCash = Convert.ToDecimal(txtTotalCash.Text);
            decimal TotalCheck = Convert.ToDecimal(txtTotalCheck.Text);
            decimal TotalCollection = Convert.ToDecimal(txtTotalCollection.Text);
            decimal TotalTaxWithheld = Convert.ToDecimal(txtTotalTax.Text);
            decimal TotalPDC;
            decimal TotalCashReceived = Convert.ToDecimal(txtTotalCashReceived.Text);
            decimal TotalCheckReceived = Convert.ToDecimal(txtTotalCheckReceived.Text);
            decimal TotalAmountReceived = Convert.ToDecimal(txtTotalAmntReceived.Text);
            decimal Difference = Convert.ToDecimal(txtDifference.Text);
            string remittedBy = lstRemittedBy.Text;

            if (!string.IsNullOrEmpty(txtTotalPdc.Text))
            {
                TotalPDC = Convert.ToDecimal(txtTotalPdc.Text);
            }
            else
            {
                TotalPDC = 0;
            }

            byte[] byteArray = imgToByteArray(signatureImage);


            PaymentSummary_MainDetailsModel mainDetails = new PaymentSummary_MainDetailsModel();
            mainDetails.CollectionDate = dt;
            mainDetails.CollectedBy = collectedBy;
            mainDetails.Area = Area;
            mainDetails.TotalCash = TotalCash;
            mainDetails.TotalCheck = TotalCheck;
            mainDetails.TotalCollection = TotalCollection;
            mainDetails.TotalTaxWithheld = TotalTaxWithheld;
            mainDetails.TotalPDC = TotalPDC;
            mainDetails.TotalCashReceived = TotalCashReceived;
            mainDetails.TotalCheckReceived = TotalCheckReceived;
            mainDetails.TotalAmountReceived = TotalAmountReceived;
            mainDetails.Difference = Difference;
            mainDetails.RemittedBy = remittedBy;
            mainDetails.Signature = byteArray;
            mainDetails.ValidatedBy = AppUser.Employee.FullName;
            listMainDetails.Add(mainDetails);

            return listMainDetails;

        }
        public byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, System.Drawing.Imaging.ImageFormat.Png);
                return mStream.ToArray();
            }
        }
        private void PaymentSummaryClearData()
        {
            txtTotalCash.Text = "0.00";
            txtTotalCashReceived.Text = "0.00";
            txtTotalCheckReceived.Text = "0.00";
            txtTotalCollection.Text = "0.00";
            txtTotalTax.Text = "0.00";
            txtTotalPdc.Text = "0.00";
            txtTotalCashReceived.Text = "0.00";
            txtTotalCheckReceived.Text = "0.00";
            txtTotalAmntReceived.Text = "0.00";
            txtDifference.Text = "0.00";
            txtRemarksPaymentSummary.Text = "";
            if (img_Signature.Image != null)
            {
                img_Signature.Image = null;
                Invalidate();
            }

            gridPrepaid.FilterDescriptors.Clear();
            gridFreightCollect.FilterDescriptors.Clear();
            gridCorpAcctConsignee.FilterDescriptors.Clear();

            lstRevenueUnitType.SelectedIndex = -1;
            lstRevenueUnitName.SelectedIndex = -1;
            lstUser.SelectedValue = "All";
            lstRemittedBy.SelectedIndex = -1;

            dateCollectionDate.Value = DateTime.Now;

        }



        private void clearSummaryData()
        {
            if (img_Signature.Image != null)
            {
                img_Signature.Image = null;
                Invalidate();
            }

            txtTotalCash.Text = "";
            txtTotalCheck.Text = "";
            txtTotalCollection.Text = "";
            txtTotalTax.Text = "";
            txtTotalPdc.Text = "";
            txtTotalCashReceived.Text = "";
            txtTotalCheckReceived.Text = "";
            txtTotalAmntReceived.Text = "";
            txtDifference.Text = "";
            txtRemarksPaymentSummary.Text = "";

        }
        private void clearListofPaymentSummary()
        {
            listPaymentSummary = new List<PaymentSummaryModel>();
            listpaymentSummaryDetails = new List<PaymentSummaryDetails>();
            listMainDetails = new List<PaymentSummary_MainDetailsModel>();
            passListofPaymentSummary = new List<PaymentSummaryModel>();
        }
        public Tuple<Guid, Guid, Guid> getData()
        {
            Guid revenueUnitTypeId = new Guid();
            Guid revenueUnitId = new Guid();
            Guid userId = new Guid();
            if (lstRevenueUnitType.SelectedIndex >= 0 && lstRevenueUnitName.SelectedIndex >= 0 && lstUser.SelectedIndex >= 0)
            {
                try
                {
                    revenueUnitTypeId = Guid.Parse(lstRevenueUnitType.SelectedValue.ToString());
                    revenueUnitId = Guid.Parse(lstRevenueUnitName.SelectedValue.ToString());
                    userId = Guid.Parse(lstUser.SelectedValue.ToString());
                }
                catch (Exception)
                {

                }
            }

            return new Tuple<Guid, Guid, Guid>(revenueUnitTypeId, revenueUnitId, userId);
        }
        public Tuple<Guid, Guid> getListData()
        {
            Guid revenueUnitTypeId = new Guid();
            Guid revenueUnitId = new Guid();
            if (lstRevenueUnitType.SelectedIndex >= 0 && lstRevenueUnitName.SelectedIndex >= 0 && lstUser.SelectedIndex >= 0)
            {
                try
                {
                    revenueUnitTypeId = Guid.Parse(lstRevenueUnitType.SelectedValue.ToString());
                    revenueUnitId = Guid.Parse(lstRevenueUnitName.SelectedValue.ToString());

                }
                catch (Exception)
                {

                }
            }

            return new Tuple<Guid, Guid>(revenueUnitTypeId, revenueUnitId);
        }
        public void addCheckboxPrepaid()
        {
            GridViewCheckBoxColumn checkBoxColumn = new GridViewCheckBoxColumn();
            checkBoxColumn.DataType = typeof(bool);
            checkBoxColumn.FieldName = "Validate";
            checkBoxColumn.HeaderText = "Validate";
            checkBoxColumn.ReadOnly = false;
            gridPrepaid.MasterTemplate.Columns.Add(checkBoxColumn);

            ctrPrepaid = 1;
        }
        public void addCheckboxFreight()
        {
            GridViewCheckBoxColumn checkBoxColumn = new GridViewCheckBoxColumn();
            checkBoxColumn.DataType = typeof(bool);
            checkBoxColumn.FieldName = "Validate";
            checkBoxColumn.HeaderText = "Validate";
            checkBoxColumn.ReadOnly = false;
            gridFreightCollect.MasterTemplate.Columns.Add(checkBoxColumn);

            ctrfreight = 1;
        }
        public void addCheckboxcorpAccount()
        {
            GridViewCheckBoxColumn checkBoxColumn = new GridViewCheckBoxColumn();
            checkBoxColumn.DataType = typeof(bool);
            checkBoxColumn.FieldName = "Validate";
            checkBoxColumn.HeaderText = "Validate";
            checkBoxColumn.ReadOnly = false;
            gridCorpAcctConsignee.MasterTemplate.Columns.Add(checkBoxColumn);

            ctrcorpAcct = 1;
        }
        private void PopulateGrid_Prepaid(List<Payment> pp_payment = null)
        {

            //Get the payment by selected filter
            //If filters = all
            //Get all data from payment filtered only by transaction date
            //Or else get data from payment filtered by filter

            List<Payment> _getAllPaymentPrepaid = new List<Payment>();
            List<Payment> _prepaidPayment;
            List<Entities.PaymentSummary> _paymentSummaryprepaid;

            DateTime date = dateCollectionDate.Value;

            decimal _totalAmountPaid;
            decimal _totalTaxwithheld;
            Guid revenueUnitTypeId = new Guid();
            Guid revenueUnitId = new Guid();
            Guid userId = new Guid();

            //If User is not selected 
            string user = lstUser.Text;
            if (user.Equals("All"))
            {
                var lisData = getListData();
                revenueUnitTypeId = lisData.Item1;
                revenueUnitId = lisData.Item2;
                string code = "PP";

                if (pp_payment == null)
                {
                    _getAllPaymentPrepaid = getAllPayment(code, date, revenueUnitId, revenueUnitTypeId);
                }
                else
                {
                    _prepaidPayment = pp_payment;
                }
            }
            else //If User is selected
            {

                //get filter value
                var tuple = getData();
                revenueUnitTypeId = tuple.Item1;
                revenueUnitId = tuple.Item2;
                userId = tuple.Item3;

                if (pp_payment == null)
                {
                    _getAllPaymentPrepaid = paymentService.FilterActiveBy(x => x.Shipment.PaymentMode.PaymentModeCode == "PP"
                                     && x.PaymentDate.Date == date.Date && x.ReceivedBy.AssignedToAreaId == revenueUnitId
                                     && x.ReceivedBy.AssignedToArea.RevenueUnitTypeId == revenueUnitTypeId
                                     && x.ReceivedBy.EmployeeId == userId)
                                     .OrderBy(x => x.CreatedDate).ToList();
                }
                else
                {
                    _prepaidPayment = pp_payment;
                }
            }

            _paymentSummaryprepaid = paymentSummaryService.FilterActive().OrderBy(x => x.CreatedDate).ToList();

            _prepaidPayment = _getAllPaymentPrepaid.Where(p => !_paymentSummaryprepaid.Any(p2 => p2.PaymentId == p.PaymentId)).ToList();

            _totalAmountPaid = _prepaidPayment.Select(x => x.Amount).Sum();
            _totalTaxwithheld = _prepaidPayment.Select(x => x.TaxWithheld).Sum();
            //txtTotalAmntPrepaid.Text = _totalAmountPaid.ToString();
            //txtTotalTaxPrepaid.Text = _totalTaxwithheld.ToString();

            paymentPrepaid = _prepaidPayment;
            gridPrepaid.DataSource = ConvertToDataTable(_prepaidPayment);

            if (ctrPrepaid != 1)
            {

                addCheckboxPrepaid();
            }


            gridPrepaid.Columns["PaymentId"].IsVisible = false;
            gridPrepaid.Columns["ClientId"].IsVisible = false;
            gridPrepaid.Columns["Client"].Width = 100;
            gridPrepaid.Columns["AWB No"].Width = 100;
            gridPrepaid.Columns["Payment Type"].Width = 150;
            gridPrepaid.Columns["Amount Due"].Width = 150;
            gridPrepaid.Columns["Amount Paid"].Width = 100;
            gridPrepaid.Columns["Tax Withheld"].Width = 150;
            gridPrepaid.Columns["OR No"].Width = 150;
            gridPrepaid.Columns["PR No"].Width = 150;
            gridPrepaid.Columns["Status"].Width = 150;
            gridPrepaid.Columns["Collected By"].Width = 150;
            gridPrepaid.Columns["ValidatedById"].IsVisible = false;
            gridPrepaid.Columns["Validated By"].Width = 150;
            gridPrepaid.Refresh();

        }
        public List<Payment> getAllPayment(string code, DateTime date, Guid revenueUnitId, Guid revenueUnitTypeId)
        {
            List<Payment> _getAllPayment;

            _getAllPayment = paymentService.FilterActive().Where(x => x.Shipment.PaymentMode.PaymentModeCode == code
                                && x.PaymentDate.Date == date.Date && x.ReceivedBy.AssignedToAreaId == revenueUnitId
                                && x.ReceivedBy.AssignedToArea.RevenueUnitTypeId == revenueUnitTypeId)
                                .OrderBy(x => x.CreatedDate).ToList();

            return _getAllPayment;
        }
        private void PopulateGrid_FreightCollect(List<Payment> fc_payment = null)
        {
            List<Payment> _getAllPaymentfreightCollect = new List<Payment>();
            List<Payment> _fcPayment;
            List<Entities.PaymentSummary> _paymentSummaryfreightCollect;

            decimal _totalAmountPaid;
            decimal _totalTaxwithheld;
            Guid revenueUnitTypeId = new Guid();
            Guid revenueUnitId = new Guid();
            Guid userId = new Guid();

            String dt = dateCollectionDate.Text.ToString();
            DateTime date = Convert.ToDateTime(dt);

            string user = lstUser.Text;
            if (user.Equals("All"))
            {
                var lisData = getListData();
                revenueUnitTypeId = lisData.Item1;
                revenueUnitId = lisData.Item2;
                string code = "FC";

                if (fc_payment == null)
                {
                    _getAllPaymentfreightCollect = getAllPayment(code, date, revenueUnitId, revenueUnitTypeId);
                }
                else
                {
                    _fcPayment = fc_payment;
                }
            }
            else
            {
                var tuple = getData();
                revenueUnitTypeId = tuple.Item1;
                revenueUnitId = tuple.Item2;
                userId = tuple.Item3;

                if (fc_payment == null)
                {
                    _getAllPaymentfreightCollect = paymentService.FilterActive().
                                    Where(x => x.Shipment.PaymentMode.PaymentModeCode == "FC"
                                    && x.PaymentDate.Date == date.Date && x.ReceivedBy.AssignedToAreaId == revenueUnitId
                                    && x.ReceivedBy.AssignedToArea.RevenueUnitTypeId == revenueUnitTypeId
                                    && x.ReceivedBy.EmployeeId == userId)
                                    .OrderBy(x => x.CreatedDate).ToList();

                }
                else
                {
                    _fcPayment = fc_payment;
                }
            }

            _paymentSummaryfreightCollect = paymentSummaryService.FilterActive().OrderBy(x => x.CreatedDate).ToList();

            _fcPayment = _getAllPaymentfreightCollect.Where(p => !_paymentSummaryfreightCollect.Any(p2 => p2.PaymentId == p.PaymentId)).ToList();


            _totalAmountPaid = _fcPayment.Select(x => x.Amount).Sum();
            _totalTaxwithheld = _fcPayment.Select(x => x.TaxWithheld).Sum();
            //txtTotalAmntFreightCollect.Text = _totalAmountPaid.ToString();
            //txtTotalTaxFreightCollect.Text = _totalTaxwithheld.ToString();

            paymentFreightCollect = _fcPayment;

            gridFreightCollect.DataSource = ConvertToDataTable(_fcPayment);
            if (ctrfreight != 1)
            {

                addCheckboxFreight();
            }


            gridFreightCollect.Columns["PaymentId"].IsVisible = false;
            gridFreightCollect.Columns["ClientId"].IsVisible = false;
            gridFreightCollect.Columns["Client"].Width = 100;
            gridFreightCollect.Columns["AWB No"].Width = 100;
            gridFreightCollect.Columns["Payment Type"].Width = 150;
            gridFreightCollect.Columns["Amount Due"].Width = 150;
            gridFreightCollect.Columns["Amount Paid"].Width = 100;
            gridFreightCollect.Columns["Tax Withheld"].Width = 150;
            gridFreightCollect.Columns["OR No"].Width = 150;
            gridFreightCollect.Columns["PR No"].Width = 150;
            gridFreightCollect.Columns["Status"].Width = 150;
            gridFreightCollect.Columns["Collected By"].Width = 150;
            gridFreightCollect.Columns["ValidatedById"].IsVisible = false;
            gridFreightCollect.Columns["Validated By"].Width = 150;
            gridFreightCollect.Refresh();

        }
        private void PopulateGrid_CorpAcctConsignee(List<Payment> cac_payment = null)
        {
            List<Payment> _cacPayment;
            List<Payment> _getAllPaymentcorpAcctConsignee = new List<Payment>();
            List<Entities.PaymentSummary> _paymentSummarycorpAcctConsignee;

            decimal _totalAmountPaid;
            decimal _totalTaxwithheld;

            Guid revenueUnitTypeId = new Guid();
            Guid revenueUnitId = new Guid();
            Guid userId = new Guid();

            String dt = dateCollectionDate.Text.ToString();
            DateTime date = Convert.ToDateTime(dt);

            string user = lstUser.Text;
            if (user.Equals("All"))
            {
                var lisData = getListData();
                revenueUnitTypeId = lisData.Item1;
                revenueUnitId = lisData.Item2;
                string code = "CAC";

                if (cac_payment == null)
                {
                    _getAllPaymentcorpAcctConsignee = getAllPayment(code, date, revenueUnitId, revenueUnitTypeId);
                }
                else
                {
                    _cacPayment = cac_payment;
                }
            }
            else
            {
                var tuple = getData();
                revenueUnitTypeId = tuple.Item1;
                revenueUnitId = tuple.Item2;
                userId = tuple.Item3;


                if (cac_payment == null)
                {
                    _getAllPaymentcorpAcctConsignee = paymentService.FilterActive().
                                   Where(x => x.Shipment.PaymentMode.PaymentModeCode == "CAC"
                                   && x.PaymentDate.Date == date.Date && x.ReceivedBy.AssignedToAreaId == revenueUnitId
                                   && x.ReceivedBy.AssignedToArea.RevenueUnitTypeId == revenueUnitTypeId
                                   && x.ReceivedBy.EmployeeId == userId)
                                   .OrderBy(x => x.CreatedDate).ToList();
                }
                else
                {
                    _cacPayment = cac_payment;
                }
            }


            _paymentSummarycorpAcctConsignee = paymentSummaryService.FilterActive().OrderBy(x => x.CreatedDate).ToList();

            _cacPayment = _getAllPaymentcorpAcctConsignee.Where(p => !_paymentSummarycorpAcctConsignee.Any(p2 => p2.PaymentId == p.PaymentId)).ToList();


            _totalAmountPaid = _cacPayment.Select(x => x.Amount).Sum();
            _totalTaxwithheld = _cacPayment.Select(x => x.TaxWithheld).Sum();
            //txtTotalAmntCorpAcctConsignee.Text = _totalAmountPaid.ToString();
            //txtTotalTaxCorpAcctConsignee.Text = _totalTaxwithheld.ToString();

            paymentCorpAcctConsignee = _cacPayment;

            gridCorpAcctConsignee.DataSource = ConvertToDataTable(_cacPayment);
            if (ctrcorpAcct != 1)
            {

                addCheckboxcorpAccount();
            }

            gridCorpAcctConsignee.Columns["PaymentId"].IsVisible = false;
            gridCorpAcctConsignee.Columns["ClientId"].IsVisible = false;
            gridCorpAcctConsignee.Columns["Client"].Width = 100;
            gridCorpAcctConsignee.Columns["AWB No"].Width = 100;
            gridCorpAcctConsignee.Columns["Payment Type"].Width = 100;
            gridCorpAcctConsignee.Columns["Amount Due"].Width = 150;
            gridCorpAcctConsignee.Columns["Amount Paid"].Width = 100;
            gridCorpAcctConsignee.Columns["Tax Withheld"].Width = 150;
            gridCorpAcctConsignee.Columns["OR No"].Width = 150;
            gridCorpAcctConsignee.Columns["PR No"].Width = 150;
            gridCorpAcctConsignee.Columns["Status"].Width = 150;
            gridCorpAcctConsignee.Columns["Collected By"].Width = 150;
            gridCorpAcctConsignee.Columns["ValidatedById"].IsVisible = false;
            gridCorpAcctConsignee.Columns["Validated By"].Width = 150;
            gridCorpAcctConsignee.Refresh();

        }
        private DataTable ConvertToDataTable(List<Payment> list)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("PaymentId", typeof(string)));
            dt.Columns.Add(new DataColumn("ClientId", typeof(string)));
            dt.Columns.Add(new DataColumn("Client", typeof(string)));
            dt.Columns.Add(new DataColumn("AWB No", typeof(string)));
            dt.Columns.Add(new DataColumn("Payment Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount Due", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount Paid", typeof(string)));
            dt.Columns.Add(new DataColumn("Tax Withheld", typeof(string)));
            dt.Columns.Add(new DataColumn("OR No", typeof(string)));
            dt.Columns.Add(new DataColumn("PR No", typeof(string)));
            dt.Columns.Add(new DataColumn("Status", typeof(string)));
            dt.Columns.Add(new DataColumn("Collected By", typeof(string)));
            dt.Columns.Add(new DataColumn("ValidatedById", typeof(string)));
            dt.Columns.Add(new DataColumn("Validated By", typeof(string)));

            dt.BeginLoadData();

            foreach (Payment item in list)
            {
                DataRow row = dt.NewRow();
                row["PaymentId"] = item.PaymentId.ToString();
                row["ClientId"] = item.Shipment.Shipper.ClientId.ToString();
                row["Client"] = item.Shipment.Shipper.FullName.ToString();
                row["AWB No"] = item.Shipment.AirwayBillNo.ToString();
                row["Payment Type"] = item.PaymentType.PaymentTypeName.ToString();
                row["Amount Due"] = item.Shipment.TotalAmount.ToString();
                row["Amount Paid"] = item.Amount.ToString();
                row["Tax Withheld"] = item.TaxWithheld.ToString();
                row["OR No"] = item.OrNo.ToString();
                row["PR No"] = item.PrNo.ToString();
                row["Status"] = "Posted";
                row["Collected By"] = item.ReceivedBy.FullName.ToString();
                row["ValidatedById"] = AppUser.Employee.EmployeeId.ToString();
                row["Validated By"] = AppUser.Employee.FullName;

                dt.Rows.Add(row);
            }
            dt.EndLoadData();

            GridViewCheckBoxColumn checkBoxColumn = new GridViewCheckBoxColumn();
            gridPrepaid.MasterTemplate.Columns.Remove(checkBoxColumn);
            checkBoxColumn = new GridViewCheckBoxColumn();
            gridFreightCollect.MasterTemplate.Columns.Remove(checkBoxColumn);
            checkBoxColumn = new GridViewCheckBoxColumn();
            gridCorpAcctConsignee.MasterTemplate.Columns.Remove(checkBoxColumn);
            gridPrepaid.Refresh();
            gridFreightCollect.Refresh();
            gridCorpAcctConsignee.Refresh();

            return dt;
        }
        private void SelectedRevenueUnit(Guid revenueUnitTypeId)
        {
            lstRevenueUnitName.DataSource = null;
            List<RevenueUnit> _revenueUnit = revenueUnitservice.GetAll().Where(x => x.City.BranchCorpOffice.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.RevenueUnitTypeId == revenueUnitTypeId).OrderBy(x => x.RevenueUnitName).ToList();
            lstRevenueUnitName.DataSource = _revenueUnit;
            lstRevenueUnitName.DisplayMember = "RevenueUnitName";
            lstRevenueUnitName.ValueMember = "RevenueUnitId";

        }
        private void SelectedUser(Guid revenueUnitId)
        {
            lstUser.DataSource = null;
            List<Employee> _employee = employeeService.GetAll().Where(x => x.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.AssignedToArea.RevenueUnitId == revenueUnitId).ToList();
            List<Employee> _remittedBy = employeeService.GetAll().Where(x => x.AssignedToArea.City.BranchCorpOfficeId == GlobalVars.DeviceBcoId && x.AssignedToArea.RevenueUnitId == revenueUnitId).ToList();

            lstUser.DataSource = _employee;
            lstUser.DisplayMember = "FullName";
            lstUser.ValueMember = "EmployeeId";

            if (_employee != null)
            {
                lstUser.Items.Add("All");
            }

            lstRemittedBy.DataSource = _remittedBy;
            lstRemittedBy.DisplayMember = "FullName";
            lstRemittedBy.ValueMember = "EmployeeId";
        }
        private Tuple<decimal, decimal, decimal> SubtotalPrepaid()
        {
            decimal _totalCash;
            decimal _totalCheck;
            decimal _totaltax;

            _totalCash = paymentPrepaid.Where(x => x.PaymentType.PaymentTypeName == "Cash").Select(x => x.Amount).Sum();
            _totalCheck = paymentPrepaid.Where(x => x.PaymentType.PaymentTypeName == "Check").Select(x => x.Amount).Sum();
            _totaltax = paymentPrepaid.Select(x => x.TaxWithheld).Sum();

            return new Tuple<decimal, decimal, decimal>(_totalCash, _totalCheck, _totaltax);
        }
        private Tuple<decimal, decimal, decimal> SubtotalFreightCollect()
        {
            decimal _totalCash;
            decimal _totalCheck;
            decimal _totaltax;

            _totalCash = paymentFreightCollect.Where(x => x.PaymentType.PaymentTypeName == "Cash").Select(x => x.Amount).Sum();
            _totalCheck = paymentFreightCollect.Where(x => x.PaymentType.PaymentTypeName == "Check").Select(x => x.Amount).Sum();
            _totaltax = paymentFreightCollect.Select(x => x.TaxWithheld).Sum();

            return new Tuple<decimal, decimal, decimal>(_totalCash, _totalCheck, _totaltax);
        }
        private Tuple<decimal, decimal, decimal> SubtotalCAC()
        {
            decimal _totalCash;
            decimal _totalCheck;
            decimal _totaltax;

            _totalCash = paymentCorpAcctConsignee.Where(x => x.PaymentType.PaymentTypeName == "Cash").Select(x => x.Amount).Sum();
            _totalCheck = paymentCorpAcctConsignee.Where(x => x.PaymentType.PaymentTypeName == "Check").Select(x => x.Amount).Sum();
            _totaltax = paymentCorpAcctConsignee.Select(x => x.TaxWithheld).Sum();

            return new Tuple<decimal, decimal, decimal>(_totalCash, _totalCheck, _totaltax);
        }
        private void TotalPaymentSummary()
        {
            decimal totalCash;
            decimal totalCheck;
            // decimal totalCollection;
            decimal totalTaxWithheld;


            //Prepaid
            var prepaid = SubtotalPrepaid();
            decimal cashPrepaid = prepaid.Item1;
            decimal checkPrepaid = prepaid.Item2;
            decimal taxPrepaid = prepaid.Item3;

            //Freight Collect
            var freightCollect = SubtotalFreightCollect();
            decimal cashFreightCollect = freightCollect.Item1;
            decimal checkFreightCollect = freightCollect.Item2;
            decimal taxFreightCollec = freightCollect.Item3;

            //CAC
            var corpAcctConsignee = SubtotalCAC();
            decimal cashCac = corpAcctConsignee.Item1;
            decimal checkCac = corpAcctConsignee.Item2;
            decimal taxCac = corpAcctConsignee.Item3;

            totalCash = cashPrepaid + cashCac + cashFreightCollect;
            totalCheck = checkPrepaid + checkCac + checkFreightCollect;
            totalCollection = totalCash + totalCheck;
            totalTaxWithheld = taxPrepaid + taxCac + taxFreightCollec;

            txtTotalCash.Text = totalCash.ToString();
            txtTotalCheck.Text = totalCheck.ToString();
            txtTotalCollection.Text = totalCollection.ToString();
            txtTotalTax.Text = totalTaxWithheld.ToString();



        }
        public List<PaymentSummaryModel> listofPaymentSummary(Guid clientId, Guid paymentId, Guid validatedById, string paymentModeCode)
        {
            paymentSummary = new Entities.PaymentSummary();
            Guid paymentStatusId = new Guid();
            Guid checkById = new Guid();
            paymentStatusId = paymentSummaryStatusService.GetAll().Where(x => x.PaymentSummaryStatusName == "Validated").Select(x => x.PaymentSummaryStatusId).First();
            checkById = userService.GetAllActiveUsers().Where(x => x.UserId == AppUser.User.UserId).Select(x => x.EmployeeId).First();

            Guid remittedById = Guid.Parse(lstRemittedBy.SelectedValue.ToString());

            PaymentSummaryModel paymentSummarymodel = new PaymentSummaryModel();
            paymentSummarymodel.PaymentSummaryId = paymentSummary.PaymentSummaryId;
            paymentSummarymodel.ClientId = clientId;
            paymentSummarymodel.PaymentId = paymentId;
            paymentSummarymodel.CheckedBy = checkById;
            paymentSummarymodel.ValidatedBy = validatedById;
            paymentSummarymodel.RemittedBy = remittedById;
            paymentSummarymodel.PaymentSummaryStatusId = paymentStatusId;
            paymentSummarymodel.DateAccepted = DateTime.Now;
            paymentSummarymodel.Remarks = txtRemarksPaymentSummary.Text.Trim();
            paymentSummarymodel.Signature = null;
            paymentSummarymodel.CreatedDate = DateTime.Now;
            paymentSummarymodel.CreatedBy = AppUser.User.UserId;
            paymentSummarymodel.ModifiedBy = AppUser.User.UserId;
            paymentSummarymodel.ModifiedDate = DateTime.Now;
            paymentSummarymodel.RecordStatus = (int)RecordStatus.Active;
            paymentSummarymodel.PaymentModeCode = paymentModeCode;
            listPaymentSummary.Add(paymentSummarymodel);

            return listPaymentSummary;
        }
        public List<PaymentSummaryDetails> summaryDetails(string AwbNo, string ClientName, string PaymentTypeName,
          decimal AmountDue, decimal AmountPaid, decimal taxWithheld, string OrNo, string PrNo,
          string ValidatedBy, string PaymentCode)
        {
            PaymentSummaryDetails details = new PaymentSummaryDetails();
            details.AirwayBillNo = AwbNo;
            //details.ClientName = ClientName;
            details.PaymentTypeName = PaymentTypeName;
            details.AmountDue = AmountDue;
            details.AmountPaid = AmountPaid;
            details.TaxWithheld = taxWithheld;
            details.OrNo = OrNo;
            details.PrNo = PrNo;
            details.ValidatedBy = ValidatedBy;
            details.PaymentModeCode = PaymentCode;
            //details.Status = "Validated";
            listpaymentSummaryDetails.Add(details);

            return listpaymentSummaryDetails;

        }
        private void updateListofPaymentSummary(PaymentSummaryModel pSummary)
        {
            byte[] byteArray = imgToByteArray(signatureImage);

            pSummary.Signature = byteArray;
            pSummary.Remarks = txtRemarksPaymentSummary.Text.Trim();
        }
        private void SavepaymentSummary(List<PaymentSummaryModel> listofPaymentSummary)
        {
            paymentSummary = new Entities.PaymentSummary();
            foreach (PaymentSummaryModel paySummarymodel in listofPaymentSummary)
            {
                updateListofPaymentSummary(paySummarymodel);
            }

            passListofPaymentSummary = listofPaymentSummary;
        }






        #endregion
    }
}
