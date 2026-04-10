using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;
using Traceability.Domain.MaterialGenealogies.Entities;
using Traceability.Domain.MaterialGenealogies.Repositories;
using Traceability.Persistence.DbContexts;

namespace Traceability.Persistence.Repositories;

internal sealed class MaterialGenealogyRepository(ApplicationDbContext context)
    : BaseRepository<MaterialGenealogy>(context), IMaterialGenealogyRepository
{
    public async Task<IReadOnlyList<MaterialGenealogy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<MaterialGenealogy>().ToListAsync(cancellationToken);
    }

    public async Task<MaterialGenealogy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<MaterialGenealogy>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
