using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.MaterialGenealogies.DTOs;
using Traceability.Domain.MaterialGenealogies.Errors;
using Traceability.Domain.MaterialGenealogies.Repositories;

namespace Traceability.Application.MaterialGenealogies.Queries;

public sealed record GetMaterialGenealogyByIdQuery(Guid Id) : IRequest<Result<MaterialGenealogyDTO>>;

public sealed class GetMaterialGenealogyByIdQueryHandler(
    IMaterialGenealogyRepository materialGenealogyRepository)
    : IRequestHandler<GetMaterialGenealogyByIdQuery, Result<MaterialGenealogyDTO>>
{
    public async Task<Result<MaterialGenealogyDTO>> Handle(GetMaterialGenealogyByIdQuery request, CancellationToken cancellationToken)
    {
        var materialGenealogy = await materialGenealogyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (materialGenealogy is null)
        {
            return Result.Failure<MaterialGenealogyDTO>(MaterialGenealogyErrors.NotFound);
        }

        var dto = new MaterialGenealogyDTO
        {
            Id = materialGenealogy.Id,
            InputMaterialId = materialGenealogy.InputMaterialId,
            OutputMaterialId = materialGenealogy.OutputMaterialId,
            ProductionRunId = materialGenealogy.ProductionRunId,
            RelationType = materialGenealogy.RelationType.ToString()
        };

        return Result.Success(dto);
    }
}