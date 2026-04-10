using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using Traceability.Domain.ProducedMaterials.Entities;
using Traceability.Domain.ProducedMaterials.Repositories;
using Traceability.Persistence.DbContexts;

namespace Traceability.Persistence.Repositories;

internal sealed class ProducedMaterialRepository(ApplicationDbContext context)
    : BaseRepository<ProducedMaterial>(context), IProducedMaterialRepository
{
    public async Task<IReadOnlyList<ProducedMaterial>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<ProducedMaterial>().ToListAsync(cancellationToken);
    }

    public async Task<ProducedMaterial?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<ProducedMaterial>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
