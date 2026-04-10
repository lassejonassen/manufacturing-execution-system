using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.ConsumedMaterials.DTOs;
using Traceability.Domain.ConsumedMaterials.Errors;
using Traceability.Domain.ConsumedMaterials.Repositories;

namespace Traceability.Application.ConsumedMaterials.Queries;

public sealed record GetConsumedMaterialByIdQuery(Guid Id) : IRequest<Result<ConsumedMaterialDTO>>;

public sealed class GetConsumedMaterialByIdQueryHandler(
    IConsumedMaterialRepository consumedMaterialRepository)
    : IRequestHandler<GetConsumedMaterialByIdQuery, Result<ConsumedMaterialDTO>>
{
    public async Task<Result<ConsumedMaterialDTO>> Handle(GetConsumedMaterialByIdQuery request, CancellationToken cancellationToken)
    {
        var consumedMaterial = await consumedMaterialRepository.GetByIdAsync(request.Id, cancellationToken);

        if (consumedMaterial is null)
        {
            return Result.Failure<ConsumedMaterialDTO>(ConsumedMaterialErrors.NotFound);
        }

        var dto = new ConsumedMaterialDTO
        {
            Id = consumedMaterial.Id,
            MaterialId = consumedMaterial.MaterialId,
            ProductionRunId = consumedMaterial.ProductionRunId,
            Quantity = consumedMaterial.Quantity,
            SourceReferenceId = consumedMaterial.SourceReferenceId,
            SourceType = consumedMaterial.SourceType.ToString(),
            UnitOfMeasure = consumedMaterial.UnitOfMeasure,
            ConsumedAtUtc = consumedMaterial.ConsumedAtUtc,
        };

        return Result.Success(dto);
    }
}