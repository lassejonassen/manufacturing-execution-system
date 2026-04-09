using LineManagement.Domain.Equipments.Entities;
using LineManagement.Domain.Equipments.Errors;
using LineManagement.Domain.Equipments.Repositories;
using LineManagement.Domain.ProductionLines.Errors;
using LineManagement.Domain.ProductionLines.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.Equipments.Commands;

public sealed record CreateEquipmentCommand(string Name, string Description, Guid ProductionLineId) : IRequest<Result<Guid>>;

public sealed class CreateEquipmentCommandHandler(
    IProductionLineRepository productionLineRepository,
    IEquipmentRepository equipmentRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateEquipmentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var productionLine = await productionLineRepository.GetByIdAsync(request.ProductionLineId, cancellationToken);

        if (productionLine is null)
        {
            return Result.Failure<Guid>(ProductionLineErrors.NotFound);
        }

        var existingEquipment = await equipmentRepository.GetByNameAsync(request.Name, cancellationToken);

        if (existingEquipment is not null)
        {
            return Result.Failure<Guid>(EquipmentErrors.NameMustBeUnique);
        }

        var equipment = Equipment.Create(request.Name, request.Description, request.ProductionLineId, dateTimeProvider.UtcNow);
        if (equipment.IsFailure)
        {
            return Result.Failure<Guid>(equipment.Error);
        }

        equipmentRepository.Add(equipment.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(equipment.Value.Id);
    }
}
