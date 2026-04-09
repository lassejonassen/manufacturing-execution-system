using LineManagement.Application.Equipments.DTOs;
using LineManagement.Domain.Equipments.Repositories;
using SharedKernel.Messaging;

namespace LineManagement.Application.Equipments.Queries;

public sealed record GetAllEquipmentsQuery : IRequest<IReadOnlyList<EquipmentDTO>>;

public sealed class GetAllEquipmentsQueryHandler(
    IEquipmentRepository equipmentRepository)
    : IRequestHandler<GetAllEquipmentsQuery, IReadOnlyList<EquipmentDTO>>
{
    public async Task<IReadOnlyList<EquipmentDTO>> Handle(GetAllEquipmentsQuery request, CancellationToken cancellationToken)
    {
        var equipments = await equipmentRepository.GetAllAsync(cancellationToken);

        var dtos = equipments.Select(x => new EquipmentDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ProductionLineId = x.ProductionLineId,
        }).ToList();

        return dtos;
    }
}