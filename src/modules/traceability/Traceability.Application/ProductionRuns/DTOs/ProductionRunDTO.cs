namespace Traceability.Application.ProductionRuns.DTOs;

public sealed record ProductionRunDTO
{
    public required Guid Id { get; init; }
    public required Guid WorkOrderId { get; init; }
    public required Guid OperationId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required Guid ProductionLineId { get; init; }
    public required DateTimeOffset StartTimeUtc { get; init; }
    public DateTimeOffset? EndTimeUtc { get; init; }
    public required string State { get; init; }
}
