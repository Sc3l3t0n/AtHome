using AtHome.Shared.Models;
using Refit;

namespace AtHome.Shared.Services;

[Headers("Authorization: Bearer")]
public interface IItemApi
{
    
    [Get("/api/items/{id}")]
    public Task<Item> GetAsync(int id);
    
    [Get("/api/items")]
    public Task<List<Item>> GetAllAsync();
    
    [Get("/")]
    public Task Root();
}