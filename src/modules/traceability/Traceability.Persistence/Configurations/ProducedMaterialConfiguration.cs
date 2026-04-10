using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traceability.Domain.ProducedMaterials.Entities;

namespace Traceability.Persistence.Configurations;

internal sealed class ProducedMaterialConfigruation : IEntityTypeConfiguration<ProducedMaterial>
{
    public void Configure(EntityTypeBuilder<ProducedMaterial> builder)
    {
        builder.ToTable("ProducedMaterials");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.ProductionRunId)
            .IsRequired();

        builder.Property(e => e.ProductId)
            .IsRequired();

        builder.Property(e => e.UnitOfMeasure)
            .IsRequired();

        builder.Property(e => e.SerialNumber)
            .IsRequired();

        builder.Property(e => e.LotId)
            .IsRequired();

        builder.Property(e => e.SubLotId)
            .IsRequired();

        builder.Property(e => e.ProducedAtUtc)
            .IsRequired();

        builder.HasOne(e => e.ProductionRun)
            .WithMany(e => e.ProducedMaterials)
            .HasForeignKey(e => e.ProductionRunId);
    }
}
