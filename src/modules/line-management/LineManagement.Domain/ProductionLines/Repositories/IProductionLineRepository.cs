using LineManagement.Domain.ProductionLines.Entities;
using SharedKernel;

namespace LineManagement.Domain.ProductionLines.Repositories;

public interface IProductionLineRepository : IBaseRepository<ProductionLine>
{
    Task<IReadOnlyList<ProductionLine>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductionLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductionLine?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
