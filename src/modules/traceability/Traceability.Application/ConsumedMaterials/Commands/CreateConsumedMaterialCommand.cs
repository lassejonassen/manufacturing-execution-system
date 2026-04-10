using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.ConsumedMaterials.DTOs;
using Traceability.Domain.ConsumedMaterials.Entities;
using Traceability.Domain.ConsumedMaterials.Repositories;
using Traceability.Domain.ProductionRuns.Errors;
using Traceability.Domain.ProductionRuns.Repositories;

namespace Traceability.Application.ConsumedMaterials.Commands;

public sealed record CreateConsumedMaterialCommand(CreateConsumedMaterialDTO Dto) : IRequest<Result<Guid>>;

public sealed class CreateConsumedMaterialCommandHandler(
    IConsumedMaterialRepository consumedMaterialRepository,
    IProductionRunRepository productionRunRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateConsumedMaterialCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateConsumedMaterialCommand request, CancellationToken cancellationToken)
    {
        var productionRun = await productionRunRepository.GetByIdAsync(request.Dto.ProductionLineId, cancellationToken);

        if (productionRun is null)
        {
            return Result.Failure<Guid>(ProductionRunErrors.NotFound);
        }

        var consumedMaterial = ConsumedMaterial.Create(
            request.Dto.ProductionLineId,
            request.Dto.MaterialId,
            request.Dto.Quantity,
            request.Dto.UnitOfMeasure,
            request.Dto.SourceType,
            request.Dto.SourceReferenceId,
            request.Dto.ConsumedAtUtc,
            dateTimeProvider.UtcNow);

        if (consumedMaterial.IsFailure)
        {
            return Result.Failure<Guid>(consumedMaterial.Error);
        }

        consumedMaterialRepository.Add(consumedMaterial.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(consumedMaterial.Value.Id);
    }
}