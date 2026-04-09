using LineManagement.Application.ProductionLines.DTOs;
using LineManagement.Domain.ProductionLines.Errors;
using LineManagement.Domain.ProductionLines.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.ProductionLines.Commands;

public sealed record UpdateProductionLineCommand(UpdateProductionLineDTO Dto) : IRequest<Result>;

public sealed class UpdateProductionLineCommandHandler(
    IProductionLineRepository productionLineRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductionLineCommand, Result>
{
    public async Task<Result> Handle(UpdateProductionLineCommand request, CancellationToken cancellationToken)
    {
        var productionLine = await productionLineRepository.GetByIdAsync(request.Dto.Id, cancellationToken);

        if (productionLine is null)
        {
            return Result.Failure(ProductionLineErrors.NotFound);
        }

        if (productionLine.Name != request.Dto.Name)
        {
            var existingProductionLine = await productionLineRepository.GetByNameAsync(request.Dto.Name, cancellationToken);

            if (existingProductionLine is not null)
            {
                return Result.Failure<Guid>(ProductionLineErrors.NameMustBeUnique);
            }
        }

        productionLine.Update(request.Dto.Name, request.Dto.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}