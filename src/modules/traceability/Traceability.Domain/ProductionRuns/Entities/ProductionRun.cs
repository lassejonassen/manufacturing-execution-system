using SharedKernel;
using Traceability.Domain.ConsumedMaterials.Entities;
using Traceability.Domain.MaterialGenealogies.Entities;
using Traceability.Domain.ProducedMaterials.Entities;
using Traceability.Domain.ProductionRuns.Enums;
using Traceability.Domain.ProductionRuns.Errors;
using Traceability.Domain.ProductionRuns.Events;

namespace Traceability.Domain.ProductionRuns.Entities;

public sealed class ProductionRun : DomainEntity
{
    private ProductionRun() { }
    private ProductionRun(DateTimeOffset utcNow) : base(utcNow) { }

    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid WorkOrderId { get; private set; }
    public Guid OperationId { get; private set; }
    public Guid EquipmentId { get; private set; }
    public Guid ProductionLineId { get; private set; }
    public DateTimeOffset StartTimeUtc { get; private set; }
    public DateTimeOffset? EndTimeUtc { get; private set; }
    public ProductionRunState State { get; private set; }
    //public List<string> OperatorIds { get; private set; } = [];
    //public Guid? ShiftId { get; private set; }

    private readonly List<ConsumedMaterial> _consumedMaterials = [];
    public IReadOnlyList<ConsumedMaterial> ConsumedMaterials => _consumedMaterials.AsReadOnly();

    private readonly List<ProducedMaterial> _producedMaterials = [];
    public IReadOnlyList<ProducedMaterial> ProducedMaterials => _producedMaterials.AsReadOnly();

    private readonly List<MaterialGenealogy> _materialGenealogies = [];
    public IReadOnlyList<MaterialGenealogy> MaterialGenealogies => _materialGenealogies.AsReadOnly();

    public static Result<ProductionRun> Create(
        Guid workOrderId,
        Guid operationId,
        Guid equipmentId,
        Guid productionLineId,
        DateTimeOffset startTimeUtc,
        DateTimeOffset utcNow)
    {
        if (workOrderId == Guid.Empty)
        {
            return Result.Failure<ProductionRun>(ProductionRunErrors.InvalidWorkOrderId);
        }

        if (operationId == Guid.Empty)
        {
            return Result.Failure<ProductionRun>(ProductionRunErrors.InvalidOperationId);
        }

        if (equipmentId == Guid.Empty)
        {
            return Result.Failure<ProductionRun>(ProductionRunErrors.InvalidEquipmentId);
        }

        if (productionLineId == Guid.Empty)
        {
            return Result.Failure<ProductionRun>(ProductionRunErrors.InvalidProductionLineId);
        }

        var productionRun = new ProductionRun(utcNow)
        {
            WorkOrderId = workOrderId,
            OperationId = operationId,
            EquipmentId = equipmentId,
            ProductionLineId = productionLineId,
            State = ProductionRunState.Active,
            StartTimeUtc = startTimeUtc
        };

        productionRun.Raise(new ProductionStartedDomainEvent(productionRun.Id));

        return Result.Success(productionRun);
    }

    public Result Complete(DateTimeOffset endTimeUtc)
    {
        if (endTimeUtc < StartTimeUtc)
        {
            return Result.Failure(ProductionRunErrors.InvalidEndTime);
        }

        EndTimeUtc = endTimeUtc;
        State = ProductionRunState.Completed;

        Raise(new ProductionStoppedDomainEvent(Id));

        return Result.Success();
    }

    public Result Abort(DateTimeOffset endTimeUtc)
    {
        if (endTimeUtc < StartTimeUtc)
        {
            return Result.Failure(ProductionRunErrors.InvalidEndTime);
        }

        EndTimeUtc = endTimeUtc;
        State = ProductionRunState.Aborted;

        Raise(new ProductionStoppedDomainEvent(Id));

        return Result.Success();
    }
}
