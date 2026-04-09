using LineManagement.Application.ProductionLines.DTOs;
using LineManagement.Domain.ProductionLines.Repositories;
using SharedKernel.Messaging;

namespace LineManagement.Application.ProductionLines.Queries;

public sealed record GetAllProductionLinesQuery : IRequest<IReadOnlyList<ProductionLineDTO>>;

public sealed class GetAllProductionLinesQueryHandler(
    IProductionLineRepository productionLineRepository)
    : IRequestHandler<GetAllProductionLinesQuery, IReadOnlyList<ProductionLineDTO>>
{
    public async Task<IReadOnlyList<ProductionLineDTO>> Handle(GetAllProductionLinesQuery request, CancellationToken cancellationToken)
    {
        var productionLines = await productionLineRepository.GetAllAsync(cancellationToken);

        var dtos = productionLines.Select(x => new ProductionLineDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
        }).ToList();

        return dtos;
    }
}