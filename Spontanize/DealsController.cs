using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Commands.Deals;
using Service.Query.Deal;

namespace Spontanize;

[Route("/api/[controller]")]
[ApiController]
public class DealsController : Controller
{
    private IServiceHandler _handler;

    public DealsController(IServiceHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        GetAllQuery query = new GetAllQuery();

        return Ok(_handler.Submit(query));
    }

    [HttpGet]
    [Route("/api/[controller]/stats")]
    public IActionResult Statistics()
    {
        StatsDealRequest request = new StatsDealRequest();

        return Ok(_handler.Submit(request));
    }

    [HttpPost]
    public IActionResult Create([FromBody] Deal deal)
    {
        CreateDealCommand cmd = new CreateDealCommand() { Deal = deal };

        return Ok(_handler.Submit(cmd));
    }

    [HttpPut]
    public IActionResult Update([FromBody] Deal deal)
    {
        UpdateDealCommand cmd = new UpdateDealCommand() { Deal = deal };

        return Ok(_handler.Submit(cmd));
    }
}