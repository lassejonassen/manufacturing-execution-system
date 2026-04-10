namespace WebAPI.Contracts.Traceability.ProductionRuns;

public sealed record CompleteProductionRunRequestDTO
{
    public required Guid Id { get; init; }
}
