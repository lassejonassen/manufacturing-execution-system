using LineManagement.Domain.Equipments.Entities;
using LineManagement.Domain.Equipments.Repositories;
using LineManagement.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Persistence;

namespace LineManagement.Persistence.Repositories;

internal class EquipmentRepository(ApplicationDbContext context)
    : BaseRepository<Equipment>(context), IEquipmentRepository
{
    public async Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DbContext.Set<Equipment>().ToListAsync(cancellationToken);
    }

    public async Task<Equipment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Equipment>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Equipment?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Equipment>().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}
