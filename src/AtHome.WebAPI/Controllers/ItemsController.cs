using AtHome.Shared.Models;
using AtHome.WebApi.Filter;
using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtHome.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : BaseController<Item>
{
    public ItemsController(IItemRepository repository, IItemService service) : base(repository, service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ItemFilter filter)
    {
        return await base.GetAll(filter);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemDto request)
    {
        return await base.Create(request);
    }

    [HttpPatch($"{{{nameof(id)}:int}}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateItemDto request)
    {
        return await base.Update(id, request);
    }
}