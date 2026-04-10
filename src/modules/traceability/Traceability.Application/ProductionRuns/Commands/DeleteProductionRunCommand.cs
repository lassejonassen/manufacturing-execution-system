using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Domain.ProductionRuns.Errors;
using Traceability.Domain.ProductionRuns.Repositories;

namespace Traceability.Application.ProductionRuns.Commands;

public sealed record DeleteProductionRunCommand(Guid Id) : IRequest<Result>;

public sealed class DeleteProductionRunCommandHandler(
    IProductionRunRepository productionRunRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProductionRunCommand, Result>
{
    public async Task<Result> Handle(DeleteProductionRunCommand request, CancellationToken cancellationToken)
    {
        var productionRun = await productionRunRepository.GetByIdAsync(request.Id, cancellationToken);
        if (productionRun is null)
        {
            return Result.Failure(ProductionRunErrors.NotFound);
        }

        productionRunRepository.Delete(productionRun);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}