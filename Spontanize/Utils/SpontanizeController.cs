using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Spontanize.Utils;

public class SpontanizeController : Controller
{
    protected async Task<IActionResult> HandleMediatr<T>(IServiceHandler handler, IRequest<T> request)
    {
        var response = await handler.Submit(request);
        
        return StatusCode(response.StatusCode, response);
    }

    protected string? GetUserId()
    {
        return HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    }
}