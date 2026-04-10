using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.ProductionRuns.DTOs;
using Traceability.Domain.ProductionRuns.Errors;
using Traceability.Domain.ProductionRuns.Repositories;

namespace Traceability.Application.ProductionRuns.Queries;

public sealed record GetProductionRunByIdQuery(Guid Id) : IRequest<Result<ProductionRunDTO>>;

public sealed class GetProductionRunByIdQueryHandler(
    IProductionRunRepository productionRunRepository)
    : IRequestHandler<GetProductionRunByIdQuery, Result<ProductionRunDTO>>
{
    public async Task<Result<ProductionRunDTO>> Handle(GetProductionRunByIdQuery request, CancellationToken cancellationToken)
    {
        var productionRun = await productionRunRepository.GetByIdAsync(request.Id, cancellationToken);

        if (productionRun is null)
        {
            return Result.Failure<ProductionRunDTO>(ProductionRunErrors.NotFound);
        }

        var dto = new ProductionRunDTO
        {
            Id = productionRun.Id,
            WorkOrderId = productionRun.WorkOrderId,
            OperationId = productionRun.OperationId,
            EquipmentId = productionRun.EquipmentId,
            ProductionLineId = productionRun.ProductionLineId,
            State = productionRun.State.ToString(),
            StartTimeUtc = productionRun.StartTimeUtc,
            EndTimeUtc = productionRun.EndTimeUtc
        };

        return Result.Success(dto);
    }
}
