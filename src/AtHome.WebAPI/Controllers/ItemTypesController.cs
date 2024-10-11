using AtHome.Shared;
using AtHome.Shared.Filters;
using AtHome.Shared.Interfaces;
using AtHome.Shared.Models;
using AtHome.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AtHome.WebApi.Controllers;

[ApiController]
[Route("api/item-types")]
public class ItemTypesController : BaseController<ItemType>
{
    public ItemTypesController(IItemTypeRepository repository, IItemTypeService service)
        : base(repository, service) { }

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

