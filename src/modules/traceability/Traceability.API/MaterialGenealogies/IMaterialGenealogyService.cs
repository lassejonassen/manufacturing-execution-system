using SharedKernel;
using Traceability.Application.MaterialGenealogies.DTOs;

namespace Traceability.API.MaterialGenealogies;

public interface IMaterialGenealogyService
{
    Task<IReadOnlyList<MaterialGenealogyDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<MaterialGenealogyDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<Guid>> CreateAsync(CreateMaterialGenealogyDTO dto, CancellationToken cancellationToken);
}
