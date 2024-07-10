using AtHome.WebApi.Database;
using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;

namespace AtHome.WebApi.Repositories;

public class ItemTypeRepository : IItemTypeRepository
{
    readonly private ApplicationDbContext _context;

    public ItemTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<ItemType>>> GetAll()
    {
        return await _context.ItemTypes.ToListAsync();
    }

    public async Task<ErrorOr<ItemType>> Get(int id)
    {
        var itemType = await _context.ItemTypes.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (itemType == null) return Error.NotFound();
        return itemType;
    }
}