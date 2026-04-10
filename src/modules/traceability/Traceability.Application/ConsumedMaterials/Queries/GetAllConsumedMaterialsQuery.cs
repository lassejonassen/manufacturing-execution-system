using SharedKernel.Messaging;
using Traceability.Application.ConsumedMaterials.DTOs;
using Traceability.Domain.ConsumedMaterials.Repositories;

namespace Traceability.Application.ConsumedMaterials.Queries;

public sealed record GetAllConsumedMaterialsQuery : IRequest<IReadOnlyList<ConsumedMaterialDTO>>;

public sealed class GetAllConsumedMaterialsQueryHandler(
    IConsumedMaterialRepository consumedMaterialRepository)
    : IRequestHandler<GetAllConsumedMaterialsQuery, IReadOnlyList<ConsumedMaterialDTO>>
{
    public async Task<IReadOnlyList<ConsumedMaterialDTO>> Handle(GetAllConsumedMaterialsQuery request, CancellationToken cancellationToken)
    {
        var consumedMaterials = await consumedMaterialRepository.GetAllAsync(cancellationToken);

        var dtos = consumedMaterials.Select(x => new ConsumedMaterialDTO()
        {
            Id = x.Id,
            ConsumedAtUtc = x.ConsumedAtUtc,
            MaterialId = x.MaterialId,
            ProductionRunId = x.ProductionRunId,
            Quantity = x.Quantity,
            SourceReferenceId = x.SourceReferenceId,
            SourceType = x.SourceType.ToString(),
            UnitOfMeasure = x.UnitOfMeasure
        }).ToList();

        return dtos;
    }
}