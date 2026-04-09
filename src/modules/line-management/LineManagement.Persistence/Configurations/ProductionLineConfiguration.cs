using LineManagement.Domain.ProductionLines.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineManagement.Persistence.Configurations;

internal sealed class ProductionLineConfiguration : IEntityTypeConfiguration<ProductionLine>
{
    public void Configure(EntityTypeBuilder<ProductionLine> builder)
    {
        builder.ToTable("ProductionLines");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(ProductionLine.NameMaxLength)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(ProductionLine.DescriptionMaxLength)
            .IsRequired(false);

        builder.HasMany(e => e.Equipments)
            .WithOne(e => e.ProductionLine)
            .HasForeignKey(e => e.ProductionLineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
