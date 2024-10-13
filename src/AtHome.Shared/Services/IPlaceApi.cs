using AtHome.Shared.Attributes;
using AtHome.Shared.Models;
using Refit;

namespace AtHome.Shared.Services;

[CustomRefitClient]
public interface IPlaceApi
{
    [Get("/api/places/{id}")]
    public Task<Place> GetAsync(int id);
    
    [Get("/api/places")]
    public Task<List<Place>> GetAllAsync();
}