using SharedKernel;
using Traceability.Application.ProductionRuns.DTOs;

namespace Traceability.API.ProductionRuns;

public interface IProductionRunService
{
    Task<IReadOnlyList<ProductionRunDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<ProductionRunDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<Guid>> CreateAsync(CreateProductionRunDTO dto, CancellationToken cancellationToken);
    Task<Result> CompleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Result> AbortAsync(Guid id, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
