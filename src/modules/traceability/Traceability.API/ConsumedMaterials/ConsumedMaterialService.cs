using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.ConsumedMaterials.Commands;
using Traceability.Application.ConsumedMaterials.DTOs;
using Traceability.Application.ConsumedMaterials.Queries;

namespace Traceability.API.ConsumedMaterials;

internal sealed class ConsumedMaterialService(IMediator mediator) : IConsumedMaterialService
{
    public async Task<Result<Guid>> CreateAsync(CreateConsumedMaterialDTO dto, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateConsumedMaterialCommand(dto), cancellationToken);
    }

    public async Task<IReadOnlyList<ConsumedMaterialDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAllConsumedMaterialsQuery(), cancellationToken);
    }

    public async Task<Result<ConsumedMaterialDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetConsumedMaterialByIdQuery(id), cancellationToken);
    }
}
