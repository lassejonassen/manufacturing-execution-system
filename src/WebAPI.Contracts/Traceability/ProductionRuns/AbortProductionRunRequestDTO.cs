namespace WebAPI.Contracts.Traceability.ProductionRuns;

public sealed record AbortProductionRunRequestDTO
{
    public required Guid Id { get; init; }
}
