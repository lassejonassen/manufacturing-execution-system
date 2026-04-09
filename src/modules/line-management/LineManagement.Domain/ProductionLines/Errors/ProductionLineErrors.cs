using LineManagement.Domain.ProductionLines.Entities;
using SharedKernel;

namespace LineManagement.Domain.ProductionLines.Errors;

public static class ProductionLineErrors
{
    private const string Base = nameof(ProductionLine);

    public static readonly Error NotFound
        = new($"{Base}.NotFound", "Production Line not found", ErrorType.NotFound);

    public static readonly Error NameMustBeUnique
       = new($"{Base}.AlreadyExists", "Production Line name must be unique", ErrorType.Validation);

    public static readonly Error NameIsRequired
        = new($"{Base}.NameIsRequired", "Name is required", ErrorType.Validation);

    public static readonly Error NameTooLong
        = new($"{Base}.NameTooLong", $"Name must not exceed {ProductionLine.NameMaxLength} characters", ErrorType.Validation);

    public static readonly Error DescriptionTooLong
        = new($"{Base}.DescriptionTooLong", $"Description must not exceed {ProductionLine.DescriptionMaxLength} characters", ErrorType.Validation);
}
