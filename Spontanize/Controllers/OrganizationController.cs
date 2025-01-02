using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Commands.Organizations;
using Service;

namespace Spontanize;

[ApiController]
[Route("/api/[controller]")]
public class OrganizationController(IServiceHandler handler) : Controller
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request)
    {
        request.UserId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var response = await handler.Submit(request);
        
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await handler.Submit(new GetAllRequest());
        
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("/api/organization/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await handler.Submit(new GetRequest() {Id = id});
        
        return StatusCode(response.StatusCode, response);
    }
}