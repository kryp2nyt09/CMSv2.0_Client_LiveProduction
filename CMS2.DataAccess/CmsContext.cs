using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CMS2.Entities;

namespace CMS2.DataAccess
{
    public class CmsContext : DbContext
    {
        public CmsContext()
            : base("name=Cms")
        {
        }

        public CmsContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<AccountStatus> AccountStatus { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AdjustmentReason> AdjustmentReasons { get; set; }
        public DbSet<ApplicableRate> ApplicableRates { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<ApprovingAuthority> ApprovingAuthorities { get; set; }
        public DbSet<AwbIssuance> AwbIssuances { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BillingPeriod> BillingPeriods { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingRemark> BookingRemarks { get; set; }
        public DbSet<BookingStatus> BookingStatus { get; set; }
        public DbSet<BranchAcceptance> BranchAcceptances { get; set; }
        public DbSet<BranchCorpOffice> BranchCorpOffices { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<CargoTransfer> CargoTransfer { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommodityType> CommodityTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeliveredPackage> DeliveredPackages { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryRemark> DeliveryRemarks { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatus { get; set; }
        public DbSet<DeliveryReceipt> DeliveryReceipts { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExpressRate> ExpressRates { get; set; }
        public DbSet<FlightInformation> FlightInfos { get; set; }
        public DbSet<FuelSurcharge> FuelSurcharges { get; set; }
        public DbSet<GatewayInbound> GatewayInbounds { get; set; }
        public DbSet<GatewayOutbound> GatewayOutbounds { get; set; }
        public DbSet<GatewayTransmittal> GatewayTransmittals { get; set; }
        public DbSet<HoldCargo> HoldCargo { get; set; }
        public DbSet<GoodsDescription> GoodsDescriptions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuAccess> MenuAccess { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<PackageDimension> PackageDimensions { get; set; }
        public DbSet<PackageNumber> PackageNumbers { get; set; }
        public DbSet<PackageNumberAcceptance> PackageNumberAcceptances { get; set; }
        public DbSet<PackageTransfer> PackageTransfers { get; set; }
        public DbSet<PackageNumberTransfer> PackageNumberTransfers { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<PaymentSummary> PaymentSummaries { get; set; }
        public DbSet<PaymentSummaryStatus> PaymentSummaryStatus { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentTurnover> PaymentTurnovers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<RateMatrix> RateMatrix { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<RecordChange> RecordChanges { get; set; }
        public DbSet<Remarks> Remarks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RevenueUnit> RevenueUnits { get; set; }
        public DbSet<RevenueUnitType> RevenueUnitTypes { get; set; }
        public DbSet<Segregation> Segregation { get; set; }
        public DbSet<ServiceMode> ServiceModes { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipMode> ShipModes { get; set; }
        public DbSet<ShipmentAdjustment> ShipmentAdjustments { get; set; }
        public DbSet<ShipmentBasicFee> ShipmentBasicFees { get; set; }
        public DbSet<ShipmentStatus> ShipmentStatus { get; set; }
        public DbSet<ShipmentTracking> ShipmentTrackings { get; set; }
        public DbSet<StatementOfAccount> StatementOfAccounts { get; set; }
        public DbSet<StatementOfAccountNumber> StatementOfAccountNumbers { get; set; }
        public DbSet<StatementOfAccountPayment> StatementOfAccountPayments { get; set; }
        public DbSet<StatementOfAccountPrint> StatementOfAccountPrints { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<TerminalRevenueUnitMapping> TerminalRevenueUnitMappings { get; set; }
        public DbSet<TntMaint> TntMaints { get; set; }
        public DbSet<TransmittalStatus> TransmittalStatus { get; set; }
        public DbSet<TransferAcceptance> TransferAcceptances { get; set; }
        public DbSet<TransShipmentLeg> TransShipmentLegs { get; set; }
        public DbSet<TransShipmentRoute> TransShipmentRoutes { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<TruckAreaMapping> TruckAreaMappings { get; set; }
        public DbSet<WeightBreak> WeigthBreaks { get; set; }
        public DbSet<Unbundle> Unbundles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Claim> Claims { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // prevents the table names from being pluralized
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<AwbIssuance>().HasRequired(x => x.IssuedTo).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>().HasRequired(x => x.Consignee).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(x => x.Shipper).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(x => x.BookingStatus).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Booking>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<BranchAcceptance>().HasRequired(x => x.BranchCorpOffice).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Bundle>().HasRequired(x => x.DestinationBco).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<BranchCorpOffice>().HasMany(e => e.Clusters).WithRequired(e => e.BranchCorpOffice).WillCascadeOnDelete(false);
            modelBuilder.Entity<BranchCorpOffice>().HasMany(e => e.Cities).WithRequired(e => e.BranchCorpOffice).WillCascadeOnDelete(false);
            modelBuilder.Entity<BranchCorpOffice>().Property(e => e.BranchCorpOfficeCode).IsOptional();

            modelBuilder.Entity<Bundle>().HasRequired(x => x.DestinationBco).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<CargoTransfer>().HasRequired(x => x.DestinationBco).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<CargoTransfer>().HasRequired(x => x.RevenueUnitType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<CargoTransfer>().HasRequired(x => x.DestinationArea).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<City>().HasMany(e => e.RevenueUnits).WithRequired(e => e.City).WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>().Property(x => x.CompanyId).IsOptional();
            modelBuilder.Entity<Client>().Property(x => x.AreaId).IsOptional();

            modelBuilder.Entity<Cluster>().HasRequired(x => x.BranchCorpOffice).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Company>().Property(x => x.PaymentTermId).IsOptional();
            modelBuilder.Entity<Company>().Property(x => x.Discount).HasPrecision(9, 2);
            modelBuilder.Entity<Company>().Property(x => x.CreditLimit).HasPrecision(9, 2);
            modelBuilder.Entity<Company>().HasRequired(x => x.BillingCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Company>().Property(x => x.AreaId).IsOptional();

            modelBuilder.Entity<Crating>().Property(x => x.Multiplier).HasPrecision(10, 10);

            modelBuilder.Entity<Delivery>().HasRequired(x => x.DeliveredBy).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Distribution>().HasRequired(x => x.Consignee).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Distribution>().HasRequired(x => x.Area).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Distribution>().HasRequired(x => x.Shipment).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Distribution>().HasRequired(x => x.PaymentMode).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Distribution>().HasRequired(x => x.ServiceMode).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<ExpressRate>().Property(x => x.C1to5Cost).HasPrecision(9, 2);
            modelBuilder.Entity<ExpressRate>().Property(x => x.C6to49Cost).HasPrecision(9, 2);
            modelBuilder.Entity<ExpressRate>().Property(x => x.C50to249Cost).HasPrecision(9, 2);
            modelBuilder.Entity<ExpressRate>().Property(x => x.C250to999Cost).HasPrecision(9, 2);
            modelBuilder.Entity<ExpressRate>().Property(x => x.C1000_10000Cost).HasPrecision(9, 2);
            modelBuilder.Entity<ExpressRate>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ExpressRate>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<FlightInformation>().HasRequired(x => x.OriginBco ).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<FlightInformation>().HasRequired(x => x.DestinationBco).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<FuelSurcharge>().HasRequired(x => x.OriginGroup).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<FuelSurcharge>().HasRequired(x => x.DestinationGroup).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<FuelSurcharge>().Property(x => x.Amount).HasPrecision(9, 2);

            modelBuilder.Entity<GatewayOutbound>().HasRequired(x => x.BranchCorpOffice).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<GatewayInbound>().HasRequired(x => x.OriginBco).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<GatewayTransmittal>().HasRequired(x => x.BranchCorpOffice).WithMany().WillCascadeOnDelete(false);
            
            modelBuilder.Entity<PackageTransfer>().HasRequired(x => x.ScannedBy).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PackageTransfer>().HasRequired(x => x.Driver).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>().Property(x => x.Amount).HasPrecision(9, 2);
            modelBuilder.Entity<Payment>().Property(x => x.TaxWithheld).HasPrecision(9, 2);
            modelBuilder.Entity<Payment>().HasRequired(x => x.ReceivedBy).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentSummary>().HasRequired(x => x.Validated).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PaymentSummary>().HasRequired(x => x.Client).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PaymentSummary>().HasRequired(x => x.Check).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PaymentSummary>().HasRequired(x => x.Payment).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PaymentSummary>().HasRequired(x => x.PaymentSummaryStatus).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentTurnover>().Property(x => x.ReceivedCashAmount).HasPrecision(9, 2);
            modelBuilder.Entity<PaymentTurnover>().Property(x => x.ReceivedCheckAmount).HasPrecision(9, 2);
            modelBuilder.Entity<PaymentTurnover>().HasRequired(x => x.CollectedBy).WithMany().WillCascadeOnDelete(false);

           //modelBuilder.Entity<PackageDimension>().HasRequired(x => x.Shipment).WithMany().WillCascadeOnDelete( false);

            modelBuilder.Entity<Province>().HasMany(e => e.BranchCorpOffices).WithRequired(e => e.Province).WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>().HasMany(e => e.Provinces).WithRequired(e => e.Region).WillCascadeOnDelete(false);

            modelBuilder.Entity<RevenueUnit>().HasMany(e => e.Employees).WithRequired(e => e.AssignedToArea).HasForeignKey(e => e.AssignedToAreaId);

            modelBuilder.Entity<Segregation>().HasRequired(x => x.BranchCorpOffice).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipment>().Property(x => x.Weight).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.DeclaredValue).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.HandlingFee).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.QuarantineFee).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.Discount).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.TotalAmount).HasPrecision(9, 2);
            modelBuilder.Entity<Shipment>().Property(x => x.AwbFeeId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.FreightCollectChargeId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.FuelSurchargeId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.PeracFeeId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.InsuranceId).IsOptional();
            modelBuilder.Entity<Shipment>().Property(x => x.EvatId).IsOptional();
            modelBuilder.Entity<Shipment>().HasRequired(x => x.Consignee).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.Shipper).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.AcceptedBy).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.Booking).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.Commodity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.ServiceMode).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x=>x.ServiceType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.GoodsDescription).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.ShipMode).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.PaymentTerm).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.PaymentMode).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasRequired(x => x.CommodityType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasMany(x => x.PackageDimensions).WithRequired(x => x.Shipment).WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasMany(x => x.PackageNumbers).WithRequired(x => x.Shipment).WillCascadeOnDelete(false);
            modelBuilder.Entity<Shipment>().HasMany(x => x.Deliveries).WithRequired(x => x.Shipment).WillCascadeOnDelete(false);


            modelBuilder.Entity<ShipmentAdjustment>().Property(x => x.AdjustmentAmount).HasPrecision(9, 2);

            modelBuilder.Entity<ShipmentBasicFee>().Property(x => x.Amount).HasPrecision(9, 2);

            modelBuilder.Entity<ShipmentTracking>().HasRequired(x => x.TrackedBy).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>().HasMany(e => e.Reason).WithRequired(e => e.Status).WillCascadeOnDelete(false);

            modelBuilder.Entity<StatementOfAccount>().HasRequired(x => x.Company).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<TerminalRevenueUnitMapping>().HasRequired(x => x.AssignedBy).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<TransferAcceptance>().HasRequired(x => x.ScannedBy).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<TransferAcceptance>().HasRequired(x => x.Driver).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<TransShipmentRoute>().HasRequired(x => x.OriginCity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<TransShipmentRoute>().HasRequired(x => x.DestinationCity).WithMany().WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }


    }
}
