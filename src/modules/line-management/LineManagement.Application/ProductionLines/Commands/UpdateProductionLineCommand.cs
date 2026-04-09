using LineManagement.Domain.ProductionLines.Errors;
using LineManagement.Domain.ProductionLines.Repositories;
using SharedKernel;
using SharedKernel.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LineManagement.Application.ProductionLines.Commands;

public sealed record UpdateProductionLineCommand(Guid Id, string Name, string Description) : IRequest<Result>;

public sealed class UpdateProductionLineCommandHandler(
    IProductionLineRepository productionLineRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductionLineCommand, Result>
{
    public async Task<Result> Handle(UpdateProductionLineCommand request, CancellationToken cancellationToken)
    {
        var productionLine = await productionLineRepository.GetByIdAsync(request.Id, cancellationToken);

        if (productionLine is null)
        {
            return Result.Failure(ProductionLineErrors.NotFound);
        }

        if (productionLine.Name != request.Name)
        {
            var existingProductionLine = await productionLineRepository.GetByNameAsync(request.Name, cancellationToken);

            if (existingProductionLine is not null)
            {
                return Result.Failure<Guid>(ProductionLineErrors.NameMustBeUnique);
            }
        }

        productionLine.Update(request.Name, request.Description);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}