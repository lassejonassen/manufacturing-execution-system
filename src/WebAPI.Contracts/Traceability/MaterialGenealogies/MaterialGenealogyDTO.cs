namespace WebAPI.Contracts.Traceability.MaterialGenealogies;

public sealed record MaterialGenealogyDTO
{
    public required Guid Id { get; init; }
    public required string InputMaterialId { get; init; }
    public required string OutputMaterialId { get; init; }
    public required Guid ProductionRunId { get; init; }
    public required string RelationType { get; init; }
}
