using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tanks.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    public IActionResult Error()
    {
        if (HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error is Exception exception) 
        {
            return Problem(title: exception.GetType().Name,
                           detail: exception.Message);
        }

        return Problem();
    }

}