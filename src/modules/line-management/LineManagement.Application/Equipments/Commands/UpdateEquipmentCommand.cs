using LineManagement.Domain.Equipments.Errors;
using LineManagement.Domain.Equipments.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.Equipments.Commands;

public sealed record UpdateEquipmentCommand(Guid Id, string Name, string Description) : IRequest<Result>;

public sealed class UpdateEquipmentCommandHandler(
    IEquipmentRepository equipmentRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateEquipmentCommand, Result>
{
    public async Task<Result> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = await equipmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (equipment is null)
        {
            return Result.Failure(EquipmentErrors.NotFound);
        }

        if (equipment.Name != request.Name)
        {
            var existingEquipment = await equipmentRepository.GetByNameAsync(request.Name, cancellationToken);

            if (existingEquipment is not null)
            {
                return Result.Failure<Guid>(EquipmentErrors.NameMustBeUnique);
            }
        }

        equipment.Update(request.Name, request.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}