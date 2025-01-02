using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Commands.ActivatedDeals;
using Service.Query.ActivatedDeals.GetAll;
using Service.Query.ActivatedDeals.GetStatus;
using Spontanize.Utils;

namespace Spontanize.Controllers;

[Route("/api/Deals/activated")]
[ApiController]
public class ActivatedDealsControllers(IServiceHandler handler) : SpontanizeController
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllActivated()
    {
        var request = new GetAllActivatedDeals()
        {
            UserId = GetUserId()
        };
        
        return await HandleMediatr(handler, request);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Activate([FromBody] ActivateDealCommand cmd)
    {
        cmd.UserId = GetUserId();
        
        Console.WriteLine("DealId: " + cmd.DealId);
        
        return await HandleMediatr(handler, cmd);
    }

    [HttpGet("{status}")]
    [Authorize]
    public async Task<IActionResult> GetByStatus(String status)
    {
        GetByStatusQuery query = new GetByStatusQuery();
        query.UserId = GetUserId();
        query.Status = status;
        
        return await HandleMediatr(handler, query);
    }
}