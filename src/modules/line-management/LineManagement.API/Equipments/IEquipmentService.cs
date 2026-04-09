using LineManagement.Application.Equipments.DTOs;
using SharedKernel;

namespace LineManagement.API.Equipments;

public interface IEquipmentService
{
    Task<IReadOnlyList<EquipmentDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<EquipmentDTO>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
