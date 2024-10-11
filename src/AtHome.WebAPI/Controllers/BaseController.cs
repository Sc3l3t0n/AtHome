using AtHome.Shared;
using AtHome.Shared.Interfaces;
using AtHome.WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtHome.WebApi.Controllers;

[Authorize]
public abstract class BaseController<T> : ControllerBase where T: class
{
    protected readonly IRepository<T> Repository;
    protected readonly IService<T> Service;

    protected BaseController(IRepository<T> repository, IService<T> service)
    {
        Repository = repository;
        Service = service;
    }

    protected async Task<IActionResult> GetAll([FromQuery] IFilter<T> filter)
    {
        var result = await Repository.GetAll();

        return result.Match<IActionResult>(
            items => Ok(items.Where(filter.ToPredicate())),
            BadRequest
        );
    }

    [HttpGet($"{{{nameof(id)}:int}}")]
    public virtual async Task<IActionResult> Get(int id)
    {
        var result = await Repository.Get(id);

        return result.Match<IActionResult>(
            Ok,
            error => error[0].Type switch
            {
                ErrorType.NotFound => NotFound(error[0]),
                _ => BadRequest(error[0])
            }
        );
    }

    protected async Task<IActionResult> Create([FromBody] ICreateDto<T> request)
    {
        var result = await Service.CreateAsync(request);

        return result.Match<IActionResult>(
            Ok,
            error => error[0].Type switch
            {
                ErrorType.NotFound => NotFound(error[0]),
                _ => BadRequest(error[0])
            }
        );
    }
    
    protected async Task<IActionResult> Update(int id, [FromBody] IUpdateDto<T> request)
    {
        var result = await Service.UpdateAsync(request, id);

        return result.Match<IActionResult>(
            Ok,
            error => error[0].Type switch
            {
                ErrorType.NotFound => NotFound(error[0]),
                _ => BadRequest(error[0])
            }
        );
    }

    [HttpDelete($"{{{nameof(id)}:int}}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var result = await Service.DeleteAsync(id);

        return result.Match<IActionResult>(
            Ok,
            error => error[0].Type switch
            {
                ErrorType.NotFound => NotFound(error[0]),
                _ => BadRequest(error[0])
            }
        );
    }
}