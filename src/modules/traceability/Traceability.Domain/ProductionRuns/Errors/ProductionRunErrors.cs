using SharedKernel;
using Traceability.Domain.ProductionRuns.Entities;

namespace Traceability.Domain.ProductionRuns.Errors;

public static class ProductionRunErrors
{
    private const string Base = nameof(ProductionRun);

    public static readonly Error InvalidWorkOrderId
        = new($"{Base}.InvalidWorkOrderId", "The provided Work Order Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidOperationId
        = new($"{Base}.InvalidOperationId", "The provided Operation Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidEquipmentId
       = new($"{Base}.InvalidEquipmentId", "The provided Equipment Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidProductionLineId
       = new($"{Base}.InvalidProductionLineId", "The provided Production Line Id is invalid", ErrorType.Validation);

    public static readonly Error InvalidEndTime
   = new($"{Base}.InvalidEndTime", "The provided End Time is invalid", ErrorType.Validation);
}
