using LineManagement.API.ProductionLines;
using LineManagement.Application.ProductionLines.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.LineManagement.ProductionLines;

namespace WebAPI.Controllers.LineManagement;

[Route("api/line-management/production-lines")]
public class ProductionLinesController(IProductionLineService productionLineService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await productionLineService.GetAllAsync(cancellationToken);

        var dtos = result.Select(x => new Contracts.LineManagement.ProductionLines.ProductionLineDTO
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
        });

        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await productionLineService.GetByIdAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        var dto = new Contracts.LineManagement.ProductionLines.ProductionLineDTO
        {
            Id = result.Value.Id,
            Name = result.Value.Name,
            Description = result.Value.Description,
        };

        return Ok(dto);

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductionLineRequestDTO request, CancellationToken cancellationToken)
    {
        var dto = new CreateProductionLineDTO
        {
            Name = request.Name,
            Description = request.Description
        };

        var result = await productionLineService.CreateAsync(dto, cancellationToken);

        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value) 
            : HandleFailure(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] UpdateProductionLineRequestDTO request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest("Provided Ids does not match");
        }

        var dto = new UpdateProductionLineDTO
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description
        };

        var result = await productionLineService.UpdateAsync(dto, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result.Error);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await productionLineService.DeleteAsync(id, cancellationToken);

        return result.IsSuccess
            ? NoContent()
            : HandleFailure(result.Error);
    }
}
