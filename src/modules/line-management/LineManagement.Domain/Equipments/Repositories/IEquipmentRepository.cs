using LineManagement.Domain.Equipments.Entities;
using SharedKernel;

namespace LineManagement.Domain.Equipments.Repositories;

public interface IEquipmentRepository : IBaseRepository<Equipment>
{
    Task<IReadOnlyList<Equipment>> GetAllAsync(CancellationToken cancellationToken);
    Task<Equipment?> GetByIdAsync(Guid id,  CancellationToken cancellationToken);
    Task<Equipment?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
