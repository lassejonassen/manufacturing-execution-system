using SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;
using Traceability.Domain.ConsumedMaterials.Enums;

namespace Traceability.Domain.ConsumedMaterials.Entities;

public sealed class ConsumedMaterial : BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid ProductionRunId {  get; init; } = Guid.NewGuid();
    public string MaterialId { get; init; } = string.Empty;
    public double Quantity { get; init; }
    public string UnitOfMeasure { get; init; } = string.Empty;
    public ConsumedMaterialSourceType SourceType { get; init; }
    public string SourceReferenceId {  get; init; } = string.Empty;
    public DateTimeOffset ConsumedAtUtc { get; init; }
}
