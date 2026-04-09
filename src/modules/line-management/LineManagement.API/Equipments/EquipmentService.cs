using LineManagement.Application.Equipments.DTOs;
using LineManagement.Application.Equipments.Queries;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.API.Equipments;

internal class EquipmentService(IMediator mediator) : IEquipmentService
{
    public async Task<IReadOnlyList<EquipmentDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAllEquipmentsQuery(), cancellationToken);
    }

    public async Task<Result<EquipmentDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetEquipmentByIdQuery(id), cancellationToken);
    }
}
