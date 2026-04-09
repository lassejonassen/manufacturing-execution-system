using LineManagement.Application.ProductionLines.Commands;
using LineManagement.Application.ProductionLines.DTOs;
using LineManagement.Application.ProductionLines.Queries;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.API.ProductionLines;

internal class ProductionLineService(IMediator mediator) : IProductionLineService
{
    public async Task<Result<Guid>> CreateAsync(CreateProductionLineDTO dto, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateProductionLineCommand(dto), cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteProductionLineCommand(id), cancellationToken);
    }

    public async Task<IReadOnlyList<ProductionLineDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAllProductionLinesQuery(), cancellationToken);
    }

    public async Task<Result<ProductionLineDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetProductionLineByIdQuery(id), cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdateProductionLineDTO dto, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateProductionLineCommand(dto), cancellationToken);
    }
}
