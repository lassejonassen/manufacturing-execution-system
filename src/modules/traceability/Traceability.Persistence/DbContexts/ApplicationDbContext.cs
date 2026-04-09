using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;

namespace Traceability.Persistence.DbContexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : BaseDbContext<ApplicationDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
