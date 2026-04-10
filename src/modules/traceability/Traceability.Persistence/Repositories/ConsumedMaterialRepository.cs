using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using Traceability.Domain.ConsumedMaterials.Entities;
using Traceability.Domain.ConsumedMaterials.Repositories;
using Traceability.Persistence.DbContexts;

namespace Traceability.Persistence.Repositories;

internal sealed class ConsumedMaterialRepository(ApplicationDbContext context)
    : BaseRepository<ConsumedMaterial>(context), IConsumedMaterialRepository
{
    public async Task<IReadOnlyList<ConsumedMaterial>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<ConsumedMaterial>().ToListAsync(cancellationToken);
    }

    public async Task<ConsumedMaterial?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<ConsumedMaterial>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
