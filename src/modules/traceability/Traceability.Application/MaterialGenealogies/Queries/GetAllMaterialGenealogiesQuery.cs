using SharedKernel.Messaging;
using Traceability.Application.MaterialGenealogies.DTOs;
using Traceability.Domain.MaterialGenealogies.Repositories;

namespace Traceability.Application.MaterialGenealogies.Queries;

public sealed record GetAllMaterialGenealogiesQuery : IRequest<IReadOnlyList<MaterialGenealogyDTO>>;

public sealed class GetAllMaterialGenealogiesQueryHandler(
    IMaterialGenealogyRepository materialGenealogyRepository)
    : IRequestHandler<GetAllMaterialGenealogiesQuery, IReadOnlyList<MaterialGenealogyDTO>>
{
    public async Task<IReadOnlyList<MaterialGenealogyDTO>> Handle(GetAllMaterialGenealogiesQuery request, CancellationToken cancellationToken)
    {
        var materialGenealogies = await materialGenealogyRepository.GetAllAsync(cancellationToken);

        var dtos = materialGenealogies.Select(x => new MaterialGenealogyDTO
        {
            Id = x.Id,
            InputMaterialId = x.InputMaterialId,
            OutputMaterialId = x.OutputMaterialId,
            ProductionRunId = x.ProductionRunId,
            RelationType = x.RelationType.ToString()
        }).ToList();

        return dtos;
    }
}
