using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Spatial;
//using CMS2.Entities.TrackingEntities;

namespace CMS2.DataAccess
{
    public partial class TrackNTraceContext : DbContext
    {
        public TrackNTraceContext()
            : base("name=Tracking")
        {
        }

        //public virtual DbSet<acceptance> acceptances { get; set; }
        //public virtual DbSet<airline> airlines { get; set; }
        //public virtual DbSet<area> areas { get; set; }
        //public virtual DbSet<batch> batches { get; set; }
        //public virtual DbSet<branch> branches { get; set; }
        //public virtual DbSet<branchacceptance> branchacceptances { get; set; }
        //public virtual DbSet<bundle> bundles { get; set; }
        //public virtual DbSet<commodity> commodities { get; set; }
        //public virtual DbSet<destination> destinations { get; set; }
        //public virtual DbSet<distribution> distributions { get; set; }
        //public virtual DbSet<distribution2> distribution2s { get; set; }
        //public virtual DbSet<flight> flights { get; set; }
        //public virtual DbSet<gateway> gateways { get; set; }
        //public virtual DbSet<gatewayacceptance> gatewayacceptances { get; set; }
        //public virtual DbSet<gatewaytransmittal> gatewaytransmittals { get; set; }
        //public virtual DbSet<holdcargo> holdcargoes { get; set; }
        //public virtual DbSet<inbound> inbounds { get; set; }
        //public virtual DbSet<origin> origins { get; set; }
        //public virtual DbSet<reason> reasons { get; set; }
        //public virtual DbSet<remarks> remarks { get; set; }
        //public virtual DbSet<shipmode> shipmodes { get; set; }
        //public virtual DbSet<status> status { get; set; }
        //public virtual DbSet<transfer> transfers { get; set; }
        //public virtual DbSet<users> users { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<acceptance>()
        //     .Property(e => e.nIdentity)
        //     .HasPrecision(18, 0);

        //    modelBuilder.Entity<acceptance>()
        //        .Property(e => e.nCount)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<airline>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<area>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

        //    modelBuilder.Entity<batch>()
        //      .Property(e => e.nIdentity)
        //      .HasPrecision(18, 0);

        //    modelBuilder.Entity<branch>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

        //    modelBuilder.Entity<branchacceptance>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);
            
        //    modelBuilder.Entity<bundle>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<commodity>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<destination>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

        //    modelBuilder.Entity<distribution>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);
            
        //    modelBuilder.Entity<distribution2>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

        //    modelBuilder.Entity<flight>()
        //      .Property(e => e.nIdentity)
        //      .HasPrecision(18, 0);

        //    modelBuilder.Entity<gateway>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

        //    modelBuilder.Entity<gatewayacceptance>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<gatewaytransmittal>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<holdcargo>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<inbound>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<origin>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

        //    modelBuilder.Entity<reason>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<remarks>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<shipmode>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

        //    modelBuilder.Entity<status>()
        //        .Property(e => e.nIdentity)
        //        .HasPrecision(18, 0);

        //    modelBuilder.Entity<transfer>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

        //    modelBuilder.Entity<users>()
        //       .Property(e => e.nIdentity)
        //       .HasPrecision(18, 0);

//}
    }
}
