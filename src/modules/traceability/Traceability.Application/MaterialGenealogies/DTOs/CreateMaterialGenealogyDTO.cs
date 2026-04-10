namespace Traceability.Application.MaterialGenealogies.DTOs;

public sealed record CreateMaterialGenealogyDTO
{
    public required string InputMaterialId { get; init; }
    public required string OutputMaterialId { get; init; }
    public required Guid ProductionRunId { get; init; }
    public required string RelationType { get; init; }
}
