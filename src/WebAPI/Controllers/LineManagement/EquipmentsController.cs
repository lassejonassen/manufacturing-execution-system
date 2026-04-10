using LineManagement.API.Equipments;
using LineManagement.Application.Equipments.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.LineManagement.Equipments;

namespace WebAPI.Controllers.LineManagement;

[Tags("Line Management - Equipments")]
[Route("api/line-management/equipments")]
public class EquipmentsController(IEquipmentService equipmentService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await equipmentService.GetAllAsync(cancellationToken);

        var dtos = result.Select(x => new Contracts.LineManagement.Equipments.EquipmentDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            ProductionLineId = x.ProductionLineId,
        });

        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await equipmentService.GetByIdAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        var dto = new Contracts.LineManagement.Equipments.EquipmentDTO
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Description = result.Value.Description,
            ProductionLineId = result.Value.ProductionLineId
        };

        return Ok(dto);

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEquipmentRequestDTO request, CancellationToken cancellationToken)
    {
        var dto = new CreateEquipmentDTO
        {
            Name = request.Name,
            Description = request.Description,
            ProductionLineId = request.ProductionLineId,
        };

        var result = await equipmentService.CreateAsync(dto, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value)
            : HandleFailure(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] UpdateEquipmentRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Provided Ids does not match");
        }

        var dto = new UpdateEquipmentDTO
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description
        };

        var result = await equipmentService.UpdateAsync(dto, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await equipmentService.DeleteAsync(id, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result.Error);
    }
}
