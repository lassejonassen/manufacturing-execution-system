using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.ProductionRuns.Commands;
using Traceability.Application.ProductionRuns.DTOs;
using Traceability.Application.ProductionRuns.Queries;

namespace Traceability.API.ProductionRuns;

internal sealed class ProductionRunService(IMediator mediator) : IProductionRunService
{
    public async Task<Result> AbortAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new AbortProductionRunCommand(id), cancellationToken);
    }

    public async Task<Result> CompleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CompleteProductionRunCommand(id), cancellationToken);
    }

    public async Task<Result<Guid>> CreateAsync(CreateProductionRunDTO dto, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateProductionRunCommand(dto), cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteProductionRunCommand(id), cancellationToken);
    }

    public async Task<IReadOnlyList<ProductionRunDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAllProductionRunsQuery(), cancellationToken);
    }

    public async Task<Result<ProductionRunDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetProductionRunByIdQuery(id), cancellationToken);
    }
}
