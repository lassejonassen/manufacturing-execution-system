using SharedKernel;
using SharedKernel.Messaging;
using Traceability.Application.MaterialGenealogies.DTOs;
using Traceability.Domain.MaterialGenealogies.Entities;
using Traceability.Domain.MaterialGenealogies.Repositories;
using Traceability.Domain.ProductionRuns.Errors;
using Traceability.Domain.ProductionRuns.Repositories;

namespace Traceability.Application.MaterialGenealogies.Commands;

public sealed record CreateMaterialGenealogyCommand(CreateMaterialGenealogyDTO Dto) : IRequest<Result<Guid>>;

public sealed class CreateMaterialGenealogyCommandHandler(
    IMaterialGenealogyRepository materialGenealogyRepository,
    IProductionRunRepository productionRunRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<CreateMaterialGenealogyCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateMaterialGenealogyCommand request, CancellationToken cancellationToken)
    {
        var productionRun = await productionRunRepository.GetByIdAsync(request.Dto.ProductionRunId, cancellationToken);

        if (productionRun is null)
        {
            return Result.Failure<Guid>(ProductionRunErrors.NotFound);
        }

        var materialGenealogy = MaterialGenealogy.Create(
            request.Dto.InputMaterialId,
            request.Dto.OutputMaterialId,
            request.Dto.ProductionRunId,
            dateTimeProvider.UtcNow);

        if (materialGenealogy.IsFailure)
        {
            return Result.Failure<Guid>(materialGenealogy.Error);
        }

        materialGenealogyRepository.Add(materialGenealogy.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(materialGenealogy.Value.Id);
    }
}