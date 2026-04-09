using LineManagement.Application.ProductionLines.DTOs;
using LineManagement.Domain.ProductionLines.Entities;
using LineManagement.Domain.ProductionLines.Errors;
using LineManagement.Domain.ProductionLines.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.ProductionLines.Commands;

public sealed record CreateProductionLineCommand(CreateProductionLineDTO Dto) : IRequest<Result<Guid>>;

public sealed class CreateProductionLineCommandHandler(
    IProductionLineRepository productionLineRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateProductionLineCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductionLineCommand request, CancellationToken cancellationToken)
    {
        var existingProductionLine = await productionLineRepository.GetByNameAsync(request.Dto.Name, cancellationToken);

        if (existingProductionLine is not null)
        {
            return Result.Failure<Guid>(ProductionLineErrors.NameMustBeUnique);
        }

        var productionLine = ProductionLine.Create(request.Dto.Name, request.Dto.Description, dateTimeProvider.UtcNow);
        if (productionLine.IsFailure)
        {
            return Result.Failure<Guid>(productionLine.Error);
        }

        productionLineRepository.Add(productionLine.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(productionLine.Value.Id);
    }
}
