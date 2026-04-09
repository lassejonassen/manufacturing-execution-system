using LineManagement.Application.ProductionLines.DTOs;
using SharedKernel;

namespace LineManagement.API.ProductionLines;

public interface IProductionLineService
{
    Task<IReadOnlyList<ProductionLineDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<ProductionLineDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
