using AtHome.WebApi.Database;
using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;

namespace AtHome.WebApi.Services;

public class PlaceService : IPlaceService
{
    private readonly ApplicationDbContext _context;
    private readonly IPlaceRepository _repository;

    public PlaceService(ApplicationDbContext context, IPlaceRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<ErrorOr<Place>> CreateAsync(ICreateDto<Place> request)
    {
        return await request.ToEntity()
            .Then(place => _context.Places.Add(place).Entity)
            .ThenDoAsync(_ => _context.SaveChangesAsync());
    }

    public async Task<ErrorOr<Place>> UpdateAsync(IUpdateDto<Place> request, int id)
    {
        return await _repository.Get(id)
                    .Then(request.UpdateEntity)
                    .Then(place => _context.Places.Update(place).Entity)
                    .ThenDoAsync(_ => _context.SaveChangesAsync());
    }

    public async Task<ErrorOr<Place>> DeleteAsync(int id)
    {
         return await _repository.Get(id)
            .Then(place => _context.Places.Remove(place).Entity)
            .ThenDoAsync(_ => _context.SaveChangesAsync());
    }
}