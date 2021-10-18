
global using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ShareForFuture.Data;

public class S4FDbContext : DbContext
{
    public S4FDbContext(DbContextOptions<S4FDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<Offering> OfferingDevices => Set<Offering>();

    public DbSet<OfferingTag> OfferingTags => Set<OfferingTag>();

    public DbSet<OfferingImage> OfferingDeviceImages => Set<OfferingImage>();

    public DbSet<OfferingCategory> OfferingDeviceCategories => Set<OfferingCategory>();

    public DbSet<OfferingUnavailabilityPeriod> DeviceUnavailabilityPeriods => Set<OfferingUnavailabilityPeriod>();

    public DbSet<Sharing> Sharings => Set<Sharing>();

    public DbSet<Complain> Complains => Set<Complain>();

    public DbSet<ComplainNote> ComplainNotes => Set<ComplainNote>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

internal class BloggingContextFactory : IDesignTimeDbContextFactory<S4FDbContext>
{
    public S4FDbContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<S4FDbContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            // .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new S4FDbContext(optionsBuilder.Options);
    }
}
