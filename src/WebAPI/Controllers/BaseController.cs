using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace WebAPI.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult HandleFailure(Error error)
    {
        if (error.Type == ErrorType.Failure)
        {
            return Problem();
        }

        if (error.Type == ErrorType.Validation)
        {
            return Problem();
        }

        if (error.Type == ErrorType.NotFound)
        {
            return Problem();
        }

        if (error.Type == ErrorType.Conflict)
        {
            return Problem();
        }

        return Problem();
    }
}
