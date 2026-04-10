using SharedKernel;
using Traceability.Domain.ProductionRuns.Entities;

namespace Traceability.Domain.ProductionRuns.Repositories;

public interface IProductionRunRepository : IBaseRepository<ProductionRun>
{
    Task<IReadOnlyList<ProductionRun>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductionRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
