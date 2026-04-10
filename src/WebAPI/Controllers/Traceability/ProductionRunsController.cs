using Microsoft.AspNetCore.Mvc;
using Traceability.API.ProductionRuns;
using Traceability.Application.ProductionRuns.DTOs;
using WebAPI.Contracts.Traceability.ProductionRuns;

namespace WebAPI.Controllers.Traceability;

[Tags("Traceability - Production Runs")]
[Route("api/traceability/production-runs")]
public class ProductionRunsController(IProductionRunService productionRunService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await productionRunService.GetAllAsync(cancellationToken);

        var dtos = result.Select(x => new Contracts.Traceability.ProductionRuns.ProductionRunDTO
        {
            Id = x.Id,
            WorkOrderId = x.WorkOrderId,
            OperationId = x.OperationId,
            EquipmentId = x.EquipmentId,
            ProductionLineId = x.ProductionLineId,
            StartTimeUtc = x.StartTimeUtc,
            EndTimeUtc = x.EndTimeUtc,
            State = x.State,
        });

        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await productionRunService.GetByIdAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        var dto = new Contracts.Traceability.ProductionRuns.ProductionRunDTO
        {
            Id = result.Value.Id,
            WorkOrderId = result.Value.WorkOrderId,
            OperationId = result.Value.OperationId,
            EquipmentId = result.Value.EquipmentId,
            ProductionLineId = result.Value.ProductionLineId,
            StartTimeUtc = result.Value.StartTimeUtc,
            EndTimeUtc = result.Value.EndTimeUtc,
            State = result.Value.State,
        };

        return Ok(dto);

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductionRunRequestDTO request, CancellationToken cancellationToken)
    {
        var dto = new CreateProductionRunDTO
        {
            WorkOrderId = request.WorkOrderId,
            OperationId = request.OperationId,
            EquipmentId = request.EquipmentId,
            ProductionLineId = request.ProductionLineId,
            StartTimeUtc = request.StartTimeUtc
        };

        var result = await productionRunService.CreateAsync(dto, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value)
            : HandleFailure(result.Error);
    }

    [HttpPut("{id:guid}/complete")]
    public async Task<IActionResult> Complete([FromRoute] Guid id, [FromBody] CompleteProductionRunRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Provided Ids does not match");
        }

        var result = await productionRunService.CompleteAsync(id, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result.Error);
    }

    [HttpPut("{id:guid}/abort")]
    public async Task<IActionResult> Abort([FromRoute] Guid id, [FromBody] AbortProductionRunRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Provided Ids does not match");
        }

        var result = await productionRunService.AbortAsync(id, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result.Error);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await productionRunService.DeleteAsync(id, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result.Error);
    }
}
