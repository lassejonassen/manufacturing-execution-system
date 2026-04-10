namespace WebAPI.Contracts.Traceability.ProductionRuns;

public sealed record CreateProductionRunRequestDTO
{
    public required Guid WorkOrderId { get; init; }
    public required Guid OperationId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required Guid ProductionLineId { get; init; }
    public required DateTimeOffset StartTimeUtc { get; init; }
}
