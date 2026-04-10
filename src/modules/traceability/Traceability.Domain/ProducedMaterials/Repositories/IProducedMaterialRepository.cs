using SharedKernel;
using Traceability.Domain.ProducedMaterials.Entities;

namespace Traceability.Domain.ProducedMaterials.Repositories;

public interface IProducedMaterialRepository : IBaseRepository<ProducedMaterial>
{
    Task<IReadOnlyList<ProducedMaterial>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProducedMaterial?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}