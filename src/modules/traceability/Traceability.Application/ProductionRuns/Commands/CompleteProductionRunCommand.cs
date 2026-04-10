using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Domain.ProductionRuns.Errors;
using Traceability.Domain.ProductionRuns.Repositories;

namespace Traceability.Application.ProductionRuns.Commands;

public sealed record CompleteProductionRunCommand(Guid Id) : IRequest<Result>;

public sealed class CompleteProductionRunCommandHandler(
    IProductionRunRepository productionRunRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CompleteProductionRunCommand, Result>
{
    public async Task<Result> Handle(CompleteProductionRunCommand request, CancellationToken cancellationToken)
    {
        var productionRun = await productionRunRepository.GetByIdAsync(request.Id, cancellationToken);
        if (productionRun is null)
        {
            return Result.Failure(ProductionRunErrors.NotFound);
        }

        productionRun.Complete(dateTimeProvider.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}