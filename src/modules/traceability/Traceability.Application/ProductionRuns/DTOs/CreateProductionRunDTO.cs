namespace Traceability.Application.ProductionRuns.DTOs;

public sealed record CreateProductionRunDTO
{
    public required Guid WorkOrderId { get; init; }
    public required Guid OperationId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required Guid ProductionLineId { get; init; }
    public required DateTimeOffset StartTimeUtc { get; init; }
}
