using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using Traceability.Domain.ProductionRuns.Entities;
using Traceability.Domain.ProductionRuns.Repositories;
using Traceability.Persistence.DbContexts;

namespace Traceability.Persistence.Repositories;

internal sealed class ProductionRunRepository(ApplicationDbContext context)
    : BaseRepository<ProductionRun>(context), IProductionRunRepository
{
    public async Task<IReadOnlyList<ProductionRun>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<ProductionRun>().ToListAsync(cancellationToken);
    }

    public async Task<ProductionRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<ProductionRun>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
