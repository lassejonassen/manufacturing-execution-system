using SharedKernel;
using Traceability.Application.ConsumedMaterials.DTOs;

namespace Traceability.API.ConsumedMaterials;

public interface IConsumedMaterialService
{
    Task<IReadOnlyList<ConsumedMaterialDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<ConsumedMaterialDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<Guid>> CreateAsync(CreateConsumedMaterialDTO dto,  CancellationToken cancellationToken);
}
