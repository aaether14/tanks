using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tanks.Api.Controllers;

[ApiController]
[Route("api/v1")]
public class TanksApi : ControllerBase
{
  
    [HttpGet("tanks/{id}")]
    public async Task<IActionResult> GetTankAsync(string id) 
    {
        await Task.CompletedTask;
        return Ok(id);
    }

}
