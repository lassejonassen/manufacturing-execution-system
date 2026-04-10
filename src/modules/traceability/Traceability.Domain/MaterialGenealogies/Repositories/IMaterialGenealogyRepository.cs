using SharedKernel;
using Traceability.Domain.MaterialGenealogies.Entities;

namespace Traceability.Domain.MaterialGenealogies.Repositories;

public interface IMaterialGenealogyRepository : IBaseRepository<MaterialGenealogy>
{
    Task<IReadOnlyList<MaterialGenealogy>> GetAllAsync(CancellationToken cancellationToken);
    Task<MaterialGenealogy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

}
