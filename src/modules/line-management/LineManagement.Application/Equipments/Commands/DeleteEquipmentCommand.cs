using LineManagement.Domain.Equipments.Errors;
using LineManagement.Domain.Equipments.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.Equipments.Commands;

public sealed record DeleteEquipmentCommand(Guid Id) : IRequest<Result>;

public sealed class DeleteEquipmentCommandHandler(
    IEquipmentRepository equipmentRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteEquipmentCommand, Result>
{
    public async Task<Result> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = await equipmentRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (equipment is null)
        {
            return Result.Failure(EquipmentErrors.NotFound);
        }

        equipmentRepository.Delete(equipment);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}