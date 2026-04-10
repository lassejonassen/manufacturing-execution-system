using SharedKernel;
using Traceability.Domain.MaterialGenealogies.Entities;

namespace Traceability.Domain.MaterialGenealogies.Errors;

public static class MaterialGenealogyErrors
{
    private const string Base = nameof(MaterialGenealogy);

    public static readonly Error NotFound
        = new($"{Base}.NotFound", "Material Genealogy not found", ErrorType.NotFound);

    public static readonly Error InvalidInputMaterialId
        = new($"{Base}.InvalidInputMaterialId", "The provided Input Material Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidOutputMaterialId
        = new($"{Base}.InvalidOutputMaterialId", "The provided Output Material Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidProductionRunId
        = new($"{Base}.InvalidProductionRunId", "The provided Production Run Id is invalid", ErrorType.Validation);
}
