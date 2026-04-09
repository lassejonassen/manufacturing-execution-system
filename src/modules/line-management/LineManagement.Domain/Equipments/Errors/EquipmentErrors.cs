using LineManagement.Domain.Equipments.Entities;
using SharedKernel;

namespace LineManagement.Domain.Equipments.Errors;

public static class EquipmentErrors
{
    private const string Base = nameof(Equipment);

    public static readonly Error NotFound
        = new($"{Base}.NotFound", "Equipment not found", ErrorType.NotFound);

    public static readonly Error NameMustBeUnique
       = new($"{Base}.AlreadyExists", "equipment name must be unique", ErrorType.Validation);

    public static readonly Error NameIsRequired
        = new($"{Base}.NameIsRequired", "Name is required", ErrorType.Validation);

    public static readonly Error NameTooLong
        = new($"{Base}.NameTooLong", $"Name must not exceed {Equipment.NameMaxLength} characters", ErrorType.Validation);

    public static readonly Error DescriptionTooLong
        = new($"{Base}.DescriptionTooLong", $"Description must not exceed {Equipment.DescriptionMaxLength} characters", ErrorType.Validation);

    public static readonly Error InvalidProductionLineId
        = new($"{Base}.InvalidProductionLineId", "The provided Production Line Id is invalid", ErrorType.Validation);
}
