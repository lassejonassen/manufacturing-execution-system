using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Domain.ProductionRuns.Errors;
using Traceability.Domain.ProductionRuns.Repositories;

namespace Traceability.Application.ProductionRuns.Commands;

public sealed record AbortProductionRunCommand(Guid Id) : IRequest<Result>;

public sealed class AbortProductionRunCommandHandler(
    IProductionRunRepository productionRunRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<AbortProductionRunCommand, Result>
{
    public async Task<Result> Handle(AbortProductionRunCommand request, CancellationToken cancellationToken)
    {
        var productionRun = await productionRunRepository.GetByIdAsync(request.Id, cancellationToken);
        if (productionRun is null)
        {
            return Result.Failure(ProductionRunErrors.NotFound);
        }

        productionRun.Abort(dateTimeProvider.UtcNow);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}