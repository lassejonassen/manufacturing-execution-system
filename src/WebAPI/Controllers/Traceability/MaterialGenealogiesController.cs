using Microsoft.AspNetCore.Mvc;
using Traceability.API.MaterialGenealogies;
using Traceability.Application.MaterialGenealogies.DTOs;
using WebAPI.Contracts.Traceability.MaterialGenealogies;

namespace WebAPI.Controllers.Traceability;

[Tags("Traceability - Material Genealogies")]
[Route("api/traceability/material-genealogies")]
public class MaterialGenealogiesController(IMaterialGenealogyService materialGenealogyService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await materialGenealogyService.GetAllAsync(cancellationToken);

        var dtos = result.Select(x => new Contracts.Traceability.MaterialGenealogies.MaterialGenealogyDTO
        {
            Id = x.Id,
            InputMaterialId = x.InputMaterialId,
            OutputMaterialId = x.OutputMaterialId,
            ProductionRunId = x.ProductionRunId,
            RelationType = x.RelationType
        });

        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await materialGenealogyService.GetByIdAsync(id, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result.Error);
        }

        var dto = new Contracts.Traceability.MaterialGenealogies.MaterialGenealogyDTO
        {
            Id = result.Value.Id,
            InputMaterialId = result.Value.InputMaterialId,
            OutputMaterialId = result.Value.OutputMaterialId,
            RelationType = result.Value.RelationType,
            ProductionRunId = result.Value.ProductionRunId,
        };

        return Ok(dto);

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMaterialGenealogyRequestDTO request, CancellationToken cancellationToken)
    {
        var dto = new CreateMaterialGenealogyDTO
        {
            InputMaterialId = request.InputMaterialId,
            OutputMaterialId = request.OutputMaterialId,
            ProductionRunId = request.ProductionRunId,
            RelationType = request.RelationType,
        };

        var result = await materialGenealogyService.CreateAsync(dto, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value)
            : HandleFailure(result.Error);
    }
}
