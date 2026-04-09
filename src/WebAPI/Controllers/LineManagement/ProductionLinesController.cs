using LineManagement.API.ProductionLines;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.LineManagement;

[Route("api/line-management/production-lines")]
public class ProductionLinesController(IProductionLineService productionLineService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await productionLineService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await productionLineService.GetByIdAsync(id, cancellationToken);
        return result.IsSuccess ? Ok(result) : HandleFailure(result.Error);
    }
}
