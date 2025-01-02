using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Commands.Deals;
using Service.Query.Deal;
using Spontanize.Utils;

namespace Spontanize.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class DealsController(IServiceHandler handler) : SpontanizeController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllQuery();

        return await HandleMediatr(handler, query);
    }

    [HttpGet]
    [Route("stats")]
    public async Task<IActionResult> Statistics()
    {
        var request = new StatsDealRequest();

        return await HandleMediatr(handler, request);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] Deal deal)
    {
        var userId = GetUserId();
        var cmd = new CreateDealCommand { Deal = deal, UserId = userId };

        return await HandleMediatr(handler, cmd);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Deal deal)
    {
        var cmd = new UpdateDealCommand { Deal = deal };

        return await HandleMediatr(handler, cmd);
    }

    [HttpGet("active/{id}")]
    public async Task<IActionResult> GetActive(int id)
    {
        var query = new GetActiveQuery { OrganizationId = id };

        return await HandleMediatr(handler, query);
    }
}