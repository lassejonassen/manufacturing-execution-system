using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.ProductionRuns.DTOs;
using Traceability.Domain.ProductionRuns.Entities;
using Traceability.Domain.ProductionRuns.Repositories;

namespace Traceability.Application.ProductionRuns.Commands;

public sealed record CreateProductionRunCommand(CreateProductionRunDTO Dto) : IRequest<Result<Guid>>;

public sealed class CreateProductionRunCommandHandler(
    IProductionRunRepository productionRunRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateProductionRunCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductionRunCommand request, CancellationToken cancellationToken)
    {
        var productionRun = ProductionRun.Create(
            request.Dto.WorkOrderId,
            request.Dto.OperationId,
            request.Dto.EquipmentId,
            request.Dto.ProductionLineId,
            request.Dto.StartTimeUtc,
            dateTimeProvider.UtcNow);

        if (productionRun.IsFailure)
        {
            return Result.Failure<Guid>(productionRun.Error);
        }

        productionRunRepository.Add(productionRun.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(productionRun.Value.Id);
    }
}