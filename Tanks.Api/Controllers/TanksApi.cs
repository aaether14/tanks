using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tanks.Application.Queries;

namespace Tanks.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class TanksApi : ControllerBase
{
    private readonly IMediator _mediator;

    public TanksApi(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("tanks/{id}")]
    public async Task<IActionResult> GetTankAsync(Guid id) 
    {
        GetTankQueryResult getTankQueryResult = await _mediator.Send(new GetTankQuery(id));
        return Ok(getTankQueryResult);
    }

}
