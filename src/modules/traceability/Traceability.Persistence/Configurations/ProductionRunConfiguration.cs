using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traceability.Domain.ProductionRuns.Entities;

namespace Traceability.Persistence.Configurations;

internal sealed class ProductionRunConfiguration : IEntityTypeConfiguration<ProductionRun>
{
    public void Configure(EntityTypeBuilder<ProductionRun> builder)
    {
        builder.ToTable("ProductionRuns");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.WorkOrderId)
            .IsRequired();

        builder.Property(e => e.OperationId)
            .IsRequired();

        builder.Property(e => e.EquipmentId)
            .IsRequired();

        builder.Property(e => e.ProductionLineId)
            .IsRequired();

        builder.Property(e => e.StartTimeUtc)
            .IsRequired();

        builder.Property(e => e.EndTimeUtc)
           .IsRequired(false);

        builder.Property(e => e.State)
           .IsRequired();

        builder.HasMany(e => e.ConsumedMaterials)
           .WithOne(e => e.ProductionRun)
           .HasForeignKey(e => e.ProductionRunId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.ProducedMaterials)
           .WithOne(e => e.ProductionRun)
           .HasForeignKey(e => e.ProductionRunId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.MaterialGenealogies)
           .WithOne(e => e.ProductionRun)
           .HasForeignKey(e => e.ProductionRunId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
