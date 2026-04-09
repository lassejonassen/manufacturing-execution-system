using LineManagement.Domain.Equipments.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LineManagement.Persistence.Configurations;

internal sealed class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("Equipments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();

        builder.Property(e => e.Name)
            .HasMaxLength(Equipment.NameMaxLength)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(Equipment.DescriptionMaxLength)
            .IsRequired(false);

        builder.HasOne(e => e.ProductionLine)
            .WithMany(e => e.Equipments)
            .HasForeignKey(e => e.ProductionLineId);
    }
}
