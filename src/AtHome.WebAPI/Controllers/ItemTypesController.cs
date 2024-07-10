using AtHome.WebApi.Filter;
using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtHome.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemTypesController : BaseController<ItemType>
{
    public ItemTypesController(IItemTypeRepository repository, IItemTypeService service) : base(repository, service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ItemTypeFilter filter)
    {
        return await base.GetAll(filter);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemTypeDto request)
    {
        return await base.Create(request);
    }

    [HttpPatch($"{{{nameof(id)}:int}}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateItemTypeDto request)
    {
        return await base.Update(id, request);
    }
}