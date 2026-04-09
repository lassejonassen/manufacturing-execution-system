using LineManagement.Domain.Equipments.Entities;
using LineManagement.Domain.ProductionLines.Errors;
using SharedKernel;

namespace LineManagement.Domain.ProductionLines.Entities;

public sealed class ProductionLine : BaseEntity
{
    public const int NameMaxLength = 50;
    public const int DescriptionMaxLength = 255;

    private ProductionLine() { }
    private ProductionLine(DateTimeOffset utcNow) : base(utcNow) { }

    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    private readonly List<Equipment> _equipments = [];
    public IReadOnlyList<Equipment> Equipments => _equipments.AsReadOnly();

    public static Result<ProductionLine> Create(string name, string description, DateTimeOffset utcNow)
    {
        var validationResult = ValidateInvariants(name, description);
        if (validationResult.IsFailure)
        {
            return Result.Failure<ProductionLine>(validationResult.Error);
        }

        var productionLine = new ProductionLine(utcNow)
        {
            Name = name,
            Description = description,
        };

        return Result.Success(productionLine);
    }

    public Result Update(string name, string description)
    {
        var validationResult = ValidateInvariants(name, description);
        if (validationResult.IsFailure)
        {
            return Result.Failure<ProductionLine>(validationResult.Error);
        }

        Name = name;
        Description = description;

        return Result.Success();
    }

    private static Result ValidateInvariants(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure(ProductionLineErrors.NameIsRequired);
        }

        if (name.Length > NameMaxLength)
        {
            return Result.Failure(ProductionLineErrors.NameTooLong);
        }

        if (!string.IsNullOrWhiteSpace(description) && description.Length > DescriptionMaxLength)
        {
            return Result.Failure(ProductionLineErrors.DescriptionTooLong);
        }

        return Result.Success();
    }
}
