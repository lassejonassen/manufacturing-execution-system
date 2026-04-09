using LineManagement.Domain.ProductionLines.Entities;
using LineManagement.Domain.ProductionLines.Repositories;
using LineManagement.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;

namespace LineManagement.Persistence.Repositories;

internal sealed class ProductionLineRepository(ApplicationDbContext context)
    : BaseRepository<ProductionLine>(context), IProductionLineRepository
{
    public async Task<IReadOnlyList<ProductionLine>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<ProductionLine>().ToListAsync(cancellationToken);
    }

    public async Task<ProductionLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<ProductionLine>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ProductionLine?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await DbContext.Set<ProductionLine>().FirstOrDefaultAsync(x => x.Name ==  name, cancellationToken);
    }
}
