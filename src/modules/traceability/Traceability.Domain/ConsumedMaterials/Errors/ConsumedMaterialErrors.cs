using SharedKernel;
using Traceability.Domain.ConsumedMaterials.Entities;

namespace Traceability.Domain.ConsumedMaterials.Errors;

public static class ConsumedMaterialErrors
{
    private const string Base = nameof(ConsumedMaterial);

    public static readonly Error InvalidProductionRunId
        = new($"{Base}.InvalidProductionRunId", "The provided Production Run Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidMaterialId
        = new($"{Base}.InvalidMaterialId", "The provided Material Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidQuantity
        = new($"{Base}.InvalidQuantity", "The provided Quantity is invalid", ErrorType.Validation);

    public static readonly Error InvalidUnitOfMeasure
        = new($"{Base}.InvalidUnitOfMeasure", "The provided Unit of Measure is invalid", ErrorType.Validation);

    public static readonly Error InvalidSourceType
        = new($"{Base}.InvalidSourceType", "The provided Source Type is invalid", ErrorType.Validation);

    public static readonly Error InvalidSourceReferenceId
        = new($"{Base}.InvalidSourceReferenceId", "The provided Source Reference Id is invalid", ErrorType.Validation);
}
