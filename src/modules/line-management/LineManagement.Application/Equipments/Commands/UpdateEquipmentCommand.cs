using LineManagement.Application.Equipments.DTOs;
using LineManagement.Domain.Equipments.Errors;
using LineManagement.Domain.Equipments.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.Equipments.Commands;

public sealed record UpdateEquipmentCommand(UpdateEquipmentDTO Dto) : IRequest<Result>;

public sealed class UpdateEquipmentCommandHandler(
    IEquipmentRepository equipmentRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateEquipmentCommand, Result>
{
    public async Task<Result> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = await equipmentRepository.GetByIdAsync(request.Dto.Id, cancellationToken);

        if (equipment is null)
        {
            return Result.Failure(EquipmentErrors.NotFound);
        }

        if (equipment.Name != request.Dto.Name)
        {
            var existingEquipment = await equipmentRepository.GetByNameAsync(request.Dto.Name, cancellationToken);

            if (existingEquipment is not null)
            {
                return Result.Failure<Guid>(EquipmentErrors.NameMustBeUnique);
            }
        }

        equipment.Update(request.Dto.Name, request.Dto.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}