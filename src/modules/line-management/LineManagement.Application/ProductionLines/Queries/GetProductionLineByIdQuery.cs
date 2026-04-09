using LineManagement.Application.ProductionLines.DTOs;
using LineManagement.Domain.ProductionLines.Errors;
using LineManagement.Domain.ProductionLines.Repositories;
using SharedKernel;
using SharedKernel.Messaging;

namespace LineManagement.Application.ProductionLines.Queries;

public sealed record GetProductionLineByIdQuery(Guid Id) : IRequest<Result<ProductionLineDTO>>;

public sealed class GetProductionLineByIdQueryHandler(
    IProductionLineRepository productionLineRepository)
    : IRequestHandler<GetProductionLineByIdQuery, Result<ProductionLineDTO>>
{
    public async Task<Result<ProductionLineDTO>> Handle(GetProductionLineByIdQuery request, CancellationToken cancellationToken)
    {
        var productionLine = await productionLineRepository.GetByIdAsync(request.Id, cancellationToken);

        if (productionLine is null)
        {
            return Result.Failure<ProductionLineDTO>(ProductionLineErrors.NotFound);
        }

        var dto = new ProductionLineDTO
        {
            Id = productionLine.Id,
            Name = productionLine.Name,
            Description = productionLine.Description,
        };

        return Result.Success(dto);
    }
}
