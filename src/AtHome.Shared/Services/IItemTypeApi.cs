using AtHome.Shared.Attributes;
using AtHome.Shared.Models;
using Refit;

namespace AtHome.Shared.Services;

[CustomRefitClient]
public interface IItemTypeApi
{
    [Get("/api/item-types/{id}")]
    public Task<ItemType> GetAsync(int id);
    
    [Get("/api/item-types")]
    public Task<List<ItemType>> GetAllAsync();
}