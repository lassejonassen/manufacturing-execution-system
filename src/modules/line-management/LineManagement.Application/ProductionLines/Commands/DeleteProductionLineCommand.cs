using LineManagement.Domain.ProductionLines.Errors;
using LineManagement.Domain.ProductionLines.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.ProductionLines.Commands;

public sealed record DeleteProductionLineCommand(Guid Id) : IRequest<Result>;

public sealed class DeleteProductionLineCommandHandler(
    IProductionLineRepository productionLineRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProductionLineCommand, Result>
{
    public async Task<Result> Handle(DeleteProductionLineCommand request, CancellationToken cancellationToken)
    {
        var productionLine = await productionLineRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (productionLine is null)
        {
            return Result.Failure(ProductionLineErrors.NotFound);
        }

        productionLineRepository.Delete(productionLine);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}