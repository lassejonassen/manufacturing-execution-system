namespace WebAPI.Contracts.Traceability.MaterialGenealogies;

public sealed record CreateMaterialGenealogyRequestDTO
{
    public required string InputMaterialId { get; init; }
    public required string OutputMaterialId { get; init; }
    public required Guid ProductionRunId { get; init; }
    public required string RelationType { get; init; }
}
