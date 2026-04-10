using SharedKernel;
using Traceability.Domain.ProducedMaterials.Entities;

namespace Traceability.Domain.ProducedMaterials.Errors;

public static class ProducedMaterialErrors
{
    private const string Base = nameof(ProducedMaterial);

    public static readonly Error InvalidProductionRunId
        = new($"{Base}.InvalidProductionRunId", "The provided Production Run Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidProductId
        = new($"{Base}.InvalidProductId", "The provided Product Id is invalid", ErrorType.Validation);


    public static readonly Error InvalidUnitOfMeasure
        = new($"{Base}.InvalidUnitOfMeasure", "The provided Unit of Measure is invalid", ErrorType.Validation);


    public static readonly Error InvalidSerialNumber
        = new($"{Base}.InvalidSerialNumber", "The provided Serial Number is invalid", ErrorType.Validation);


    public static readonly Error InvalidLotId
        = new($"{Base}.InvalidLotId", "The provided Lot Id is invalid", ErrorType.Validation);


    public static readonly Error InvalidSubLotId
        = new($"{Base}.InvalidSubLotId", "The provided Sub Lot Id is invalid", ErrorType.Validation);
}
