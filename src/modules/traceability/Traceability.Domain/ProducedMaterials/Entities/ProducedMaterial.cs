using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using Traceability.Domain.ProducedMaterials.Enums;

namespace Traceability.Domain.ProducedMaterials.Entities;

public sealed class ProducedMaterial : BaseEntity
{
    public Guid Id { get; init; } = Guid.Empty;
    public Guid ProductionRunId { get; init; }
    public Guid ProductId { get; init; }
    public string UnitOfMeasure { get; init; } = string.Empty;
    public int SerialNumber { get; init; }
    public int LotId { get; init; }
    public int SubLotId { get; init; }
    public DateTimeOffset ProducedAtUtc { get; init; }
    
    // Quality Integration Related attributes
    public ProducedMaterialQualityStatus QualityStatus { get; init; }
    public Guid InspectionId { get; init; }
    public Guid DefectCategoryId { get; init; }

}
