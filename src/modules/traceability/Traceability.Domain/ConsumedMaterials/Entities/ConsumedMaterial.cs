using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using Traceability.Domain.ConsumedMaterials.Enums;
using Traceability.Domain.ConsumedMaterials.Errors;
using Traceability.Domain.ConsumedMaterials.Events;
using Traceability.Domain.ProductionRuns.Entities;

namespace Traceability.Domain.ConsumedMaterials.Entities;

public sealed class ConsumedMaterial : DomainEntity
{
    private ConsumedMaterial()
    {
        
    }

    private ConsumedMaterial(DateTimeOffset utcNow) : base(utcNow)
    {

    }

    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid ProductionRunId {  get; init; } = Guid.NewGuid();
    public string MaterialId { get; init; } = string.Empty;
    public double Quantity { get; init; }
    public string UnitOfMeasure { get; init; } = string.Empty;
    public ConsumedMaterialSourceType SourceType { get; init; }
    public string SourceReferenceId {  get; init; } = string.Empty;
    public DateTimeOffset ConsumedAtUtc { get; init; }

    public ProductionRun ProductionRun { get; } = null!;


    public static Result<ConsumedMaterial> Create(
        Guid productionRunId,
        string materialId,
        double quantity,
        string unitOfMeasure,
        string sourceType,
        string sourceReferenceId,
        DateTimeOffset consumedAtUtc,
        DateTimeOffset utcNow)
    {
        if (productionRunId == Guid.Empty)
        {
            return Result.Failure<ConsumedMaterial>(ConsumedMaterialErrors.InvalidProductionRunId);
        }

        if (string.IsNullOrWhiteSpace(materialId))
        {
            return Result.Failure<ConsumedMaterial>(ConsumedMaterialErrors.InvalidMaterialId);
        }

        if (quantity < 0)
        {
            return Result.Failure<ConsumedMaterial>(ConsumedMaterialErrors.InvalidQuantity);
        }

        if (string.IsNullOrWhiteSpace(unitOfMeasure))
        {
            return Result.Failure<ConsumedMaterial>(ConsumedMaterialErrors.InvalidUnitOfMeasure);
        }

        var mappedSourceType = Map(sourceType);
        if (mappedSourceType.IsFailure)
        {
            return Result.Failure<ConsumedMaterial>(mappedSourceType.Error);
        }

        if (string.IsNullOrWhiteSpace(sourceReferenceId))
        {
            return Result.Failure<ConsumedMaterial>(ConsumedMaterialErrors.InvalidSourceReferenceId);
        }

        var consumedMaterial = new ConsumedMaterial(utcNow)
        {
            ProductionRunId = productionRunId,
            MaterialId = materialId,
            Quantity = quantity,
            UnitOfMeasure = unitOfMeasure,
            SourceType = mappedSourceType.Value,
            SourceReferenceId = sourceReferenceId,
            ConsumedAtUtc = consumedAtUtc
        };

        consumedMaterial.Raise(new MaterialConsumedDomainEvent(consumedAtUtc, consumedMaterial.Id, productionRunId, string.Empty, string.Empty));

        return Result.Success(consumedMaterial);
    }

    private static Result<ConsumedMaterialSourceType> Map(string sourceType)
    {
        return sourceType switch
        {
            _ => Result.Failure<ConsumedMaterialSourceType>(ConsumedMaterialErrors.InvalidSourceType)
        };
    }
}
