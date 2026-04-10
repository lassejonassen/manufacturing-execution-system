using LineManagement.Application.Equipments.Commands;
using LineManagement.Application.Equipments.DTOs;
using LineManagement.Application.Equipments.Queries;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.API.Equipments;

internal class EquipmentService(IMediator mediator) : IEquipmentService
{
    public async Task<Result<Guid>> CreateAsync(CreateEquipmentDTO dto, CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateEquipmentCommand(dto), cancellationToken);
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteEquipmentCommand(id), cancellationToken);
    }

    public async Task<IReadOnlyList<EquipmentDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAllEquipmentsQuery(), cancellationToken);
    }

    public async Task<Result<EquipmentDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetEquipmentByIdQuery(id), cancellationToken);
    }

    public async Task<Result> UpdateAsync(UpdateEquipmentDTO dto, CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateEquipmentCommand(dto), cancellationToken);
    }
}
