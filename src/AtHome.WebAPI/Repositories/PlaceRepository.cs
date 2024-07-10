using AtHome.WebApi.Database;
using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;

namespace AtHome.WebApi.Repositories;

public class PlaceRepository: IPlaceRepository
{
    private readonly ApplicationDbContext _context;

    public PlaceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<Place>>> GetAll()
    {
        return await _context.Places.ToListAsync();
    }

    public async Task<ErrorOr<Place>> Get(int id)
    {
        var place = await _context.Places.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (place == null) return Error.NotFound();
        return place;
    }
}