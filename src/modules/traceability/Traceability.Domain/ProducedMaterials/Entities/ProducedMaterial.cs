using SharedKernel;
using Traceability.Domain.ProducedMaterials.Enums;
using Traceability.Domain.ProducedMaterials.Errors;
using Traceability.Domain.ProducedMaterials.Events;
using Traceability.Domain.ProductionRuns.Entities;

namespace Traceability.Domain.ProducedMaterials.Entities;

public sealed class ProducedMaterial : DomainEntity
{
    private ProducedMaterial()
    {
        
    }

    private ProducedMaterial(DateTimeOffset utcNow) : base(utcNow)
    {
        
    }

    public Guid Id { get; init; } = Guid.Empty;
    public Guid ProductionRunId { get; init; }
    public Guid ProductId { get; init; }
    public string UnitOfMeasure { get; init; } = string.Empty;
    public int SerialNumber { get; init; }
    public int LotId { get; init; }
    public int SubLotId { get; init; }
    public DateTimeOffset ProducedAtUtc { get; init; }
    public ProductionRun ProductionRun { get; } = null!;

    // Quality Integration Related attributes
    public ProducedMaterialQualityStatus QualityStatus { get; init; }
    //public Guid InspectionId { get; init; }
    //public Guid DefectCategoryId { get; init; }

    public static Result<ProducedMaterial> Create(
        Guid productionRunId,
        Guid productId,
        string unitOfMeasure,
        int serialNumber,
        int lotId,
        int subLotId,
        DateTimeOffset producedAtUtc,
        DateTimeOffset utcNow)
    {
        var producedMaterial = new ProducedMaterial(utcNow)
        {
            ProductionRunId = productionRunId,
            ProductId = productId,
            UnitOfMeasure = unitOfMeasure,
            SerialNumber = serialNumber,
            LotId = lotId,
            SubLotId = subLotId,
            ProducedAtUtc = producedAtUtc,
            QualityStatus = ProducedMaterialQualityStatus.Good
        };

        producedMaterial.Raise(new MaterialProducedDomainEvent(producedAtUtc, producedMaterial.Id, productionRunId, string.Empty, string.Empty));

        return Result.Success(producedMaterial);
    }

    public static Result<ProducedMaterial> CreateScrap(
        Guid productionRunId,
        Guid productId,
        string unitOfMeasure,
        int serialNumber,
        int lotId,
        int subLotId,
        DateTimeOffset producedAtUtc,
        DateTimeOffset utcNow)
    {
        var validationResult = ValidateInvariants(productionRunId, productId, unitOfMeasure, serialNumber, lotId, subLotId);
        if (validationResult.IsFailure)
        {
            return Result.Failure<ProducedMaterial>(validationResult.Error);
        }

        var producedMaterial = new ProducedMaterial(utcNow)
        {
            ProductionRunId = productionRunId,
            ProductId = productId,
            UnitOfMeasure = unitOfMeasure,
            SerialNumber = serialNumber,
            LotId = lotId,
            SubLotId = subLotId,
            ProducedAtUtc = producedAtUtc,
            QualityStatus = ProducedMaterialQualityStatus.Scrap
        };

        producedMaterial.Raise(new ScrapRecordedDomainEvent(producedAtUtc, producedMaterial.Id, productionRunId, string.Empty, string.Empty));

        return Result.Success(producedMaterial);
    }

    public static Result<ProducedMaterial> CreateRework(
        Guid productionRunId,
        Guid productId,
        string unitOfMeasure,
        int serialNumber,
        int lotId,
        int subLotId,
        DateTimeOffset producedAtUtc,
        DateTimeOffset utcNow)
    {
        var validationResult = ValidateInvariants(productionRunId, productId, unitOfMeasure, serialNumber, lotId, subLotId);
        if (validationResult.IsFailure)
        {
            return Result.Failure<ProducedMaterial>(validationResult.Error);
        }

        var producedMaterial = new ProducedMaterial(utcNow)
        {
            ProductionRunId = productionRunId,
            ProductId = productId,
            UnitOfMeasure = unitOfMeasure,
            SerialNumber = serialNumber,
            LotId = lotId,
            SubLotId = subLotId,
            ProducedAtUtc = producedAtUtc,
            QualityStatus = ProducedMaterialQualityStatus.Rework
        };

        producedMaterial.Raise(new ReworkStartedDomainEvent(producedAtUtc, producedMaterial.Id, productionRunId, string.Empty, string.Empty));

        return Result.Success(producedMaterial);
    }

    public static Result<ProducedMaterial> CreateHold(
        Guid productionRunId,
        Guid productId,
        string unitOfMeasure,
        int serialNumber,
        int lotId,
        int subLotId,
        DateTimeOffset producedAtUtc,
        DateTimeOffset utcNow)
    {
        var validationResult = ValidateInvariants(productionRunId, productId, unitOfMeasure, serialNumber, lotId, subLotId);
        if (validationResult.IsFailure)
        {
            return Result.Failure<ProducedMaterial>(validationResult.Error);
        }

        var producedMaterial = new ProducedMaterial(utcNow)
        {
            ProductionRunId = productionRunId,
            ProductId = productId,
            UnitOfMeasure = unitOfMeasure,
            SerialNumber = serialNumber,
            LotId = lotId,
            SubLotId = subLotId,
            ProducedAtUtc = producedAtUtc,
            QualityStatus = ProducedMaterialQualityStatus.Rework
        };

        return Result.Success(producedMaterial);
    }

    private static Result ValidateInvariants(Guid productionRunId,
        Guid productId,
        string unitOfMeasure,
        int serialNumber,
        int lotId,
        int subLotId)
    {
        if (productId == Guid.Empty)
        {
            return Result.Failure(ProducedMaterialErrors.InvalidProductId);
        }

        if (string.IsNullOrWhiteSpace(unitOfMeasure))
        {
            return Result.Failure(ProducedMaterialErrors.InvalidUnitOfMeasure);
        }

        if (serialNumber < 0)
        {
            return Result.Failure(ProducedMaterialErrors.InvalidSerialNumber);
        }

        if (lotId < 0)
        {
            return Result.Failure(ProducedMaterialErrors.InvalidLotId);
        }

        if (subLotId < 0)
        {
            return Result.Failure(ProducedMaterialErrors.InvalidSubLotId);
        }

        return Result.Success();
    }
}
