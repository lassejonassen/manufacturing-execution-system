using Microsoft.Identity.Client;
using SharedKernel;
using Traceability.Domain.MaterialGenealogies.Enums;
using Traceability.Domain.MaterialGenealogies.Errors;
using Traceability.Domain.ProductionRuns.Entities;

namespace Traceability.Domain.MaterialGenealogies.Entities;

public class MaterialGenealogy : BaseEntity
{
    private MaterialGenealogy()
    {

    }

    private MaterialGenealogy(DateTimeOffset utcNow) : base(utcNow)
    {

    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string InputMaterialId { get; init; } = string.Empty; // ConsumedMaterial or ProducedMaterial
    public string OutputMaterialId { get; init; } = string.Empty; // ProducedMaterial
    public Guid ProductionRunId { get; init; } = Guid.Empty;
    public ProductionRun ProductionRun { get; } = null!;
    public GenealogyLinkRelationType RelationType { get; init; }

    public static Result<MaterialGenealogy> Create(
        string inputMaterialId,
        string outputMaterialId,
        Guid productionRunId,
        DateTimeOffset utcNow)
    {
        if (string.IsNullOrWhiteSpace(inputMaterialId))
        {
            return Result.Failure<MaterialGenealogy>(MaterialGenealogyErrors.InvalidInputMaterialId);
        }

        if (string.IsNullOrWhiteSpace(outputMaterialId))
        {
            return Result.Failure<MaterialGenealogy>(MaterialGenealogyErrors.InvalidOutputMaterialId);
        }

        if (productionRunId == Guid.Empty)
        {
            return Result.Failure<MaterialGenealogy>(MaterialGenealogyErrors.InvalidProductionRunId);
        }

        var materialGenealogy = new MaterialGenealogy(utcNow)
        {
            InputMaterialId = inputMaterialId,
            OutputMaterialId = outputMaterialId,
            ProductionRunId = productionRunId,
        };

        return Result.Success(materialGenealogy);
    }
}
