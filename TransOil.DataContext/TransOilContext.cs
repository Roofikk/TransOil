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
    public DbSet<SupplyPoint> SupplyPoints { get; set; }

    public DbSet<CounterBase> Counters { get; set; }
    public DbSet<ElectricityCounter> ElectricEnergyCounters { get; set; }
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
            e.HasMany(x => x.ChildCompanies)
                .WithOne(x => x.ParentCompany)
                .HasForeignKey(x => x.ParentCompanyId);
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

        modelBuilder.Entity<CounterBase>(e =>
        {
            e.ToTable("Counters")
                .HasDiscriminator(x => x.Discriminator)
                .HasValue<ElectricityCounter>("ElectricityCounter")
                .HasValue<CurrentTransformer>("CurrentTransformer")
                .HasValue<VoltageTransformer>("VoltageTransformer");

            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.HasIndex(x => new { x.MeasurementPointId, x.Discriminator })
                .HasDatabaseName("IX_Counters_MeasurementPointId_Discriminator")
                .IsUnique();
        });

        modelBuilder.Entity<ElectricityCounter>(e =>
        {
            e.HasIndex(x => x.MeasurementPointId)
                .IsUnique(false);
        });

        modelBuilder.Entity<CurrentTransformer>(e =>
        {
            e.HasIndex(x => x.MeasurementPointId)
                .IsUnique(false);
        });

        modelBuilder.Entity<VoltageTransformer>(e =>
        {
            e.HasIndex(x => x.MeasurementPointId)
                .IsUnique(false);
        });

        modelBuilder.Entity<MeasurementPoint>(e =>
        {
            e.HasKey(x => x.MeasurementPointId);
            e.Property(x => x.MeasurementPointId).ValueGeneratedOnAdd();

            e.HasOne(x => x.VoltageTransformer)
                .WithOne(x => x.MeasurementPoint)
                .HasForeignKey<VoltageTransformer>(x => x.MeasurementPointId)
                .HasConstraintName("FK_Counters_MeasurementPoints_MeasurementPointId");

            e.HasOne(x => x.CurrentTransformer)
                .WithOne(x => x.MeasurementPoint)
                .HasForeignKey<CurrentTransformer>(x => x.MeasurementPointId)
                .HasConstraintName("FK_Counters_MeasurementPoints_MeasurementPointId");

            e.HasOne(x => x.ElectricityCounter)
                .WithOne(x => x.MeasurementPoint)
                .HasForeignKey<ElectricityCounter>(x => x.MeasurementPointId)
                .HasConstraintName("FK_Counters_MeasurementPoints_MeasurementPointId");
        });

        modelBuilder.Entity<SupplyPoint>(e =>
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
            e.HasKey(x => new { x.MeasurementPointId, x.MeasurementDeviceId, x.Date });

            e.HasOne(x => x.MeasurementPoint)
                .WithMany(x => x.Measurements)
                .HasForeignKey(x => x.MeasurementPointId);

            e.HasOne(x => x.MeasurementDevice)
                .WithMany(x => x.Measurements)
                .HasForeignKey(x => x.MeasurementDeviceId);
        });
    }
}
