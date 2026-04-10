using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.MaterialGenealogies.Commands;
using Traceability.Application.MaterialGenealogies.DTOs;
using Traceability.Application.MaterialGenealogies.Queries;

namespace Traceability.API.MaterialGenealogies;

internal sealed class MaterialGenealogyService(IMediator mediator)
{
    public async Task<Result<Guid>> CreateAsync(CreateMaterialGenealogyDTO dto, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateMaterialGenealogyCommand(dto), cancellationToken);
    }

    public async Task<IReadOnlyList<MaterialGenealogyDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAllMaterialGenealogiesQuery(), cancellationToken);
    }

    public async Task<Result<MaterialGenealogyDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetMaterialGenealogyByIdQuery(id), cancellationToken);
    }
}
