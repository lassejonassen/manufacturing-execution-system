using Microsoft.AspNetCore.Mvc;
using Traceability.API.ConsumedMaterials;
using Traceability.Application.ConsumedMaterials.DTOs;
using WebAPI.Contracts.Traceability.ConsumedMaterials;

namespace WebAPI.Controllers.Traceability;

[Tags("Traceability - Consumed Materials")]
[Route("api/traceability/consumed-materials")]
public class ConsumedMaterialsController(IConsumedMaterialService consumedMaterialService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await consumedMaterialService.GetAllAsync(cancellationToken);

        var dtos = result.Select(x => new Contracts.Traceability.ConsumedMaterials.ConsumedMaterialDTO
        {
            Id = x.Id,
            ConsumedAtUtc = x.ConsumedAtUtc,
            MaterialId = x.MaterialId,
            ProductionRunId = x.ProductionRunId,
            Quantity = x.Quantity,
            SourceReferenceId = x.SourceReferenceId,
            SourceType = x.SourceType,
            UnitOfMeasure = x.UnitOfMeasure
        });

        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await consumedMaterialService.GetByIdAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        var dto = new Contracts.Traceability.ConsumedMaterials.ConsumedMaterialDTO
        {
            Id = result.Value.Id,
            ConsumedAtUtc = result.Value.ConsumedAtUtc,
            MaterialId = result.Value.MaterialId,
            ProductionRunId = result.Value.ProductionRunId,
            Quantity = result.Value.Quantity,
            SourceReferenceId = result.Value.SourceReferenceId,
            SourceType = result.Value.SourceType,
            UnitOfMeasure = result.Value.UnitOfMeasure
        };

        return Ok(dto);

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateConsumedMaterialRequestDTO request, CancellationToken cancellationToken)
    {
        var dto = new CreateConsumedMaterialDTO
        {
            ConsumedAtUtc = request.ConsumedAtUtc,
            MaterialId = request.MaterialId,
            ProductionLineId = request.ProductionLineId,
            Quantity = request.Quantity,
            SourceReferenceId = request.SourceReferenceId,
            SourceType = request.SourceType,
            UnitOfMeasure = request.UnitOfMeasure
        };

        var result = await consumedMaterialService.CreateAsync(dto, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value)
            : HandleFailure(result.Error);
    }
}
