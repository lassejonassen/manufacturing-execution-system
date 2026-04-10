namespace WebAPI.Contracts.Traceability.ConsumedMaterials;

public sealed record ConsumedMaterialDTO
{
    public required Guid Id { get; init; }
    public required Guid ProductionRunId { get; init; }
    public required string MaterialId { get; init; }
    public required double Quantity { get; init; }
    public required string UnitOfMeasure { get; init; }
    public required string SourceType { get; init; }
    public required string SourceReferenceId { get; init; }
    public required DateTimeOffset ConsumedAtUtc { get; init; }
    
}
