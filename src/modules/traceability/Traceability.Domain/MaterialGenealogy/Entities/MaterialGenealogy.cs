using SharedKernel;
using Traceability.Domain.MaterialGenealogy.Enums;

namespace Traceability.Domain.MaterialGenealogy.Entities;

public class MaterialGenealogy : BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string InputMaterialId { get; init; } = string.Empty; // ConsumedMaterial or ProducedMaterial
    public string OutputMaterialId { get; init; } = string.Empty; // ProducedMaterial
    public GenealogyLinkRelationType RelationType { get; init; }
}
