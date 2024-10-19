using Microsoft.EntityFrameworkCore;

namespace MyRouteTracker.Web.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<RouteDataPoint> RouteDataPoints { get; set; }
    public DbSet<RouteDataSet> RouteDataSets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<RouteDataPoint>();
        builder.Entity<RouteDataSet>();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<ModelBase>()
                                    .Where(e => e.State != EntityState.Unchanged))
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.InsertDate = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateDate = DateTime.UtcNow;
                    break;
                default: break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}