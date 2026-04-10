namespace WebAPI.Contracts.Traceability.ConsumedMaterials;

public sealed record CreateConsumedMaterialRequestDTO
{
    public required Guid ProductionLineId { get; init; }
    public required string MaterialId { get; init; }
    public required double Quantity { get; init; }
    public required string UnitOfMeasure { get; init; }
    public required string SourceType { get; init; }
    public required string SourceReferenceId { get; init; }
    public required DateTimeOffset ConsumedAtUtc { get; init; }
}
