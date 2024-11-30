using Microsoft.AspNetCore.Mvc;
using Service.Commands.Organizations;
using Service;

namespace Spontanize;

[ApiController]
[Route("/api/[controller]")]
public class OrganizationController(IServiceHandler handler) : Controller
{
    [HttpPost]
    public IActionResult Create([FromBody] string name)
    {
        CreateOrganizationRequest request = new CreateOrganizationRequest() { Name = name };

        return Ok();
    }   
}