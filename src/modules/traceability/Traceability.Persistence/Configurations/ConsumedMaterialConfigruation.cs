using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traceability.Domain.ConsumedMaterials.Entities;

namespace Traceability.Persistence.Configurations;

internal sealed class ConsumedMaterialConfigruation : IEntityTypeConfiguration<ConsumedMaterial>
{
    public void Configure(EntityTypeBuilder<ConsumedMaterial> builder)
    {
        builder.ToTable("ConsumedMaterials");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.ProductionRunId)
            .IsRequired();

        builder.Property(e => e.MaterialId)
            .IsRequired();

        builder.Property(e => e.Quantity)
            .IsRequired();

        builder.Property(e => e.UnitOfMeasure)
            .IsRequired();

        builder.Property(e => e.SourceType)
            .IsRequired();

        builder.Property(e => e.SourceReferenceId)
            .IsRequired();

        builder.Property(e => e.ConsumedAtUtc)
            .IsRequired();

        builder.HasOne(e => e.ProductionRun)
            .WithMany(e => e.ConsumedMaterials)
            .HasForeignKey(e => e.ProductionRunId);
    }
}
