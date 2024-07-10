using Microsoft.EntityFrameworkCore;
using TransOil.DataContext.EntityModels;

namespace TransOil.DataContext;

public class TransOilContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<ChildCompany> ChildCompanies { get; set; }

    public DbSet<Customer> Customers { get; set; }

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
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(e =>
        {
            e.ToTable("Companies");
            e.HasKey(x => x.CompanyId);
            e.Property(x => x.CompanyId).ValueGeneratedOnAdd();
            e.HasMany(x => x.ChildCompanies)
                .WithOne(x => x.Company)
                .HasForeignKey(x => x.CompanyId);
        });

        modelBuilder.Entity<ChildCompany>(e =>
        {
            e.ToTable("ChildCompanies");
            e.HasKey(x => x.CompanyId);
            e.Property(x => x.CompanyId).ValueGeneratedOnAdd();
        });

        base.OnModelCreating(modelBuilder);
    }
}
