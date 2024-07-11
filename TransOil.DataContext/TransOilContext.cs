using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TransOil.DataContext.EntityModels;

namespace TransOil.DataContext;

public class TransOilContext : DbContext
{
    public DbSet<CompanyBase> Companies { get; set; }
    public DbSet<Company> ParentCompanies { get; set; }
    public DbSet<ChildCompany> ChildCompanies { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<MeasurementPoint> MeasurementPoints { get; set; }
    public DbSet<ElectricitySupplyPoint> SupplyPoints { get; set; }

    public DbSet<CounterBase> Counters { get; set; }
    public DbSet<ElectricEnergyCounter> ElectricEnergyCounters { get; set; }
    public DbSet<TransformerCounterBase> ElectricCounters { get; set; }
    public DbSet<CurrentTransformer> CurrentTransformers { get; set; }
    public DbSet<VoltageTransformer> VoltageTransformers { get; set; }

    public DbSet<MeasurementDevice> MeasurementDevices { get; set; }
    public DbSet<Measurement> Measurements { get; set; }


    public TransOilContext(DbContextOptions<TransOilContext> options)
        : base(options)
    {
    }

    public TransOilContext()
        : base()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = 3306,
                Database = "trans_oil",
                UserID = "root",
                Password = "root1234"
            };

            optionsBuilder.UseMySql(
                connectionStringBuilder.ConnectionString,
                ServerVersion.AutoDetect(connectionStringBuilder.ConnectionString),
                options =>
                {
                    options.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CompanyBase>(e =>
        {
            e.ToTable("Companies");
            e.HasKey(x => x.CompanyId);
            e.Property(x => x.CompanyId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Company>(e =>
        {
            e.ToTable("Companies");
            e.HasMany(x => x.ChildCompanies)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId);
        });

        modelBuilder.Entity<ChildCompany>(e =>
        {
            e.ToTable("Companies");
            e.HasMany(x => x.Customers)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId);
        });

        modelBuilder.Entity<Customer>(e =>
        {
            e.HasKey(x => x.CustomerId);
            e.Property(x => x.CustomerId).ValueGeneratedOnAdd();
            e.HasMany(x => x.Measurments)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
            e.HasMany(x => x.Supplies)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);
        });

        modelBuilder.Entity<ElectricEnergyCounter>(e =>
        {
            e.ToTable("Counters");
            e.HasOne(x => x.MeasurementPoint)
                .WithOne(x => x.ElectricityCounter)
                .HasForeignKey<ElectricEnergyCounter>(x => x.MeasurementPointId);
        });

        modelBuilder.Entity<CurrentTransformer>(e =>
        {
            e.ToTable("Counters");
            e.HasOne(x => x.MeasurementPoint)
                .WithOne(x => x.CurrentTransformer)
                .HasForeignKey<CurrentTransformer>(x => x.MeasurementPointId);
        });

        modelBuilder.Entity<VoltageTransformer>(e =>
        {
            e.ToTable("Counters");
            e.HasOne(x => x.MeasurementPoint)
                .WithOne(x => x.VoltageTransformer)
                .HasForeignKey<VoltageTransformer>(x => x.MeasurementPointId);
        });

        modelBuilder.Entity<MeasurementPoint>(e =>
        {
            e.HasKey(x => x.MeasurementPointId);
            e.Property(x => x.MeasurementPointId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ElectricitySupplyPoint>(e =>
        {
            e.HasKey(x => x.SupplyPointId);
            e.Property(x => x.SupplyPointId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MeasurementDevice>(e =>
        {
            e.HasKey(x => x.DeviceId);
            e.Property(x => x.DeviceId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Measurement>(e =>
        {
            e.HasKey(x => new { x.MeasurementDeviceId, x.MeasurementPointId, x.Date });

            e.HasOne(x => x.MeasurementDevice)
                .WithMany(x => x.Measurements)
                .HasForeignKey(x => x.MeasurementDeviceId);
            e.HasOne(x => x.MeasurementPoint)
                .WithMany(x => x.Measurements)
                .HasForeignKey(x => x.MeasurementPointId);
        });
    }
}
