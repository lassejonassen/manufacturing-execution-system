using LineManagement.Domain.Equipments.Errors;
using LineManagement.Domain.ProductionLines.Entities;
using SharedKernel;

namespace LineManagement.Domain.Equipments.Entities;

public sealed class Equipment : BaseEntity
{
    public const int NameMaxLength = 50;
    public const int DescriptionMaxLength = 255;

    private Equipment() { }
    private Equipment(DateTimeOffset utcNow) : base(utcNow) { }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public Guid ProductionLineId { get; private set; }
    public ProductionLine ProductionLine { get; } = null!;


    public static Result<Equipment> Create(string name, string description, Guid productionLineId, DateTimeOffset utcNow)
    {
        var validationResult = ValidateInvariants(name, description);
        if (validationResult.IsFailure)
        {
            return Result.Failure<Equipment>(validationResult.Error);
        }

        if (productionLineId == Guid.Empty)
        {
            return Result.Failure<Equipment>(EquipmentErrors.InvalidProductionLineId);
        }

        var equipment = new Equipment(utcNow)
        {
            Name = name,
            Description = description,
            ProductionLineId = productionLineId,
        };

        return Result.Success(equipment);
    }

    public Result Update(string name, string description)
    {
        var validationResult = ValidateInvariants(name, description);
        if (validationResult.IsFailure)
        {
            return Result.Failure<Equipment>(validationResult.Error);
        }

        Name = name;
        Description = description;

        return Result.Success();
    }

    private static Result ValidateInvariants(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(EquipmentErrors.NameIsRequired);
        }

        if (name.Length > NameMaxLength)
        {
            return Result.Failure(EquipmentErrors.NameTooLong);
        }

        if (!string.IsNullOrWhiteSpace(description) && description.Length > DescriptionMaxLength)
        {
            return Result.Failure(EquipmentErrors.DescriptionTooLong);
        }

        return Result.Success();
    }
}
