using AtHome.Shared.Interfaces;
using AtHome.WebApi.Database;
using AtHome.WebApi.Interfaces;
using AtHome.Shared.Models;

namespace AtHome.WebApi.Repositories;

public class ItemRepository: IItemRepository
{
    private readonly ApplicationDbContext _context;
    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<Item>>> GetAll()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<ErrorOr<Item>> Get(int id)
    {
        var item = await _context.Items.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        if (item == null) return Error.NotFound("Item.NotFound");
        return item;
    }
}