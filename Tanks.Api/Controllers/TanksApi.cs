using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tanks.Application.Commands;
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

    [HttpGet("tanks/{tank_id}")]
    public async Task<IActionResult> GetTankAsync([FromRoute(Name = "tank_id")] Guid id) 
    {
        GetTankQueryResult getTankQueryResult = await _mediator.Send(new GetTankQuery(id));

        return Ok(getTankQueryResult);
    }

    [HttpPost("tanks/add")]
    public async Task<IActionResult> AddTankAsync([FromBody] CreateTankCommand createTankCommand)
    {
        Guid id = await _mediator.Send(createTankCommand);

        return Ok(id);
    }

    [HttpGet("maps/{map_id}")]
    public async Task<IActionResult> GetMapAsync([FromRoute(Name = "map_id")] Guid id)
    {
        GetMapQueryResult getMapQueryResult = await _mediator.Send(new GetMapQuery(id));

        return Ok(getMapQueryResult);
    }

    [HttpPost("maps/add")]
    public async Task<IActionResult> AddMapAsync([FromBody] CreateRandomMapCommand createRandomMapCommand)
    {
        Guid id = await _mediator.Send(createRandomMapCommand);

        return Ok(id);
    }


}
