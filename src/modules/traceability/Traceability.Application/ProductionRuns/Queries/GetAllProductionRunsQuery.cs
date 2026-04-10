using SharedKernel.Messaging;
using Traceability.Application.ProductionRuns.DTOs;
using Traceability.Domain.ProductionRuns.Repositories;

namespace Traceability.Application.ProductionRuns.Queries;

public sealed record GetAllProductionRunsQuery : IRequest<IReadOnlyList<ProductionRunDTO>>;

public sealed class GetAllProductionRunsQueryHandler(
    IProductionRunRepository productionRunRepository)
    : IRequestHandler<GetAllProductionRunsQuery, IReadOnlyList<ProductionRunDTO>>
{
    public async Task<IReadOnlyList<ProductionRunDTO>> Handle(GetAllProductionRunsQuery request, CancellationToken cancellationToken)
    {
        var productionRuns = await productionRunRepository.GetAllAsync(cancellationToken);

        var dtos = productionRuns.Select(x => new ProductionRunDTO
        {
            Id = x.Id,
            WorkOrderId = x.WorkOrderId,
            OperationId = x.OperationId,
            EquipmentId = x.EquipmentId,
            ProductionLineId = x.ProductionLineId,
            State = x.State.ToString(),
            StartTimeUtc = x.StartTimeUtc,
            EndTimeUtc = x.EndTimeUtc
        }).ToList();

        return dtos;
    }
}