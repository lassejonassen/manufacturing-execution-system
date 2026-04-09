using LineManagement.Application.Equipments.DTOs;
using LineManagement.Domain.Equipments.Errors;
using LineManagement.Domain.Equipments.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.Equipments.Queries;

public sealed record GetEquipmentByIdQuery(Guid Id) : IRequest<Result<EquipmentDTO>>;

public sealed class GetEquipmentByIdQueryHandler(
    IEquipmentRepository equipmentRepository)
    : IRequestHandler<GetEquipmentByIdQuery, Result<EquipmentDTO>>
{
    public async Task<Result<EquipmentDTO>> Handle(GetEquipmentByIdQuery request, CancellationToken cancellationToken)
    {
        var equipment = await equipmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (equipment is null)
        {
            return Result.Failure<EquipmentDTO>(EquipmentErrors.NotFound);
        }

        var dto = new EquipmentDTO
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Description = equipment.Description,
            ProductionLineId = equipment.ProductionLineId,
        };

        return Result.Success(dto);
    }
}
