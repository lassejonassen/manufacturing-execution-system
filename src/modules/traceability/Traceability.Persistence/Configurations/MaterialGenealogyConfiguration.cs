using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Traceability.Domain.MaterialGenealogies.Entities;

namespace Traceability.Persistence.Configurations;

internal sealed class MaterialGenealogyConfiguration : IEntityTypeConfiguration<MaterialGenealogy>
{
    public void Configure(EntityTypeBuilder<MaterialGenealogy> builder)
    {
        builder.ToTable("MaterialGenealogies");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.InputMaterialId)
            .IsRequired();

        builder.Property(e => e.OutputMaterialId)
            .IsRequired();

        builder.Property(e => e.RelationType)
            .IsRequired();

        builder.HasOne(e => e.ProductionRun)
            .WithMany(e => e.MaterialGenealogies)
            .HasForeignKey(e => e.ProductionRunId);
    }
}
