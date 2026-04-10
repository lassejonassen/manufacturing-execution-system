using SharedKernel;
using Traceability.Domain.ConsumedMaterials.Entities;

namespace Traceability.Domain.ConsumedMaterials.Repositories;

public interface IConsumedMaterialRepository : IBaseRepository<ConsumedMaterial>
{
    Task<IReadOnlyList<ConsumedMaterial>> GetAllAsync(CancellationToken cancellationToken);
    Task<ConsumedMaterial?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
