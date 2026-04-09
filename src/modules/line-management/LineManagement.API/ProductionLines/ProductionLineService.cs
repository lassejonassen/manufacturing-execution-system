using LineManagement.Application.ProductionLines.DTOs;
using LineManagement.Application.ProductionLines.Queries;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.API.ProductionLines;

internal class ProductionLineService(IMediator mediator) : IProductionLineService
{
    public async Task<IReadOnlyList<ProductionLineDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAllProductionLinesQuery(), cancellationToken);
    }

    public async Task<Result<ProductionLineDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetProductionLineByIdQuery(id), cancellationToken);
    }
}
