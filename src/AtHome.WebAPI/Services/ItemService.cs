using AtHome.Shared.Models;
using AtHome.WebApi.Database;
using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;

namespace AtHome.WebApi.Services;

public class ItemService : IItemService
{
    private readonly ApplicationDbContext _context;
    private readonly IItemRepository _itemRepository;
    private readonly IPlaceRepository _placeRepository;
    private readonly IItemTypeRepository _itemTypeRepository;

    public ItemService(ApplicationDbContext context, IItemRepository itemRepository, IPlaceRepository placeRepository,
        IItemTypeRepository itemTypeRepository)
    {
        _context = context;
        _itemRepository = itemRepository;
        _placeRepository = placeRepository;
        _itemTypeRepository = itemTypeRepository;
    }

    public async Task<ErrorOr<Item>> CreateAsync(ICreateDto<Item> request)
    {
        if (request is not CreateItemDto itemRequest) return Error.Failure("Invalid request type");

        return await itemRequest
            .SetPlaceAndItemType(_itemTypeRepository, _placeRepository)
            .Then(dto => dto.ToEntity())
            .Then(entity => _context.Items.Add(entity).Entity)
            .ThenDoAsync(_ => _context.SaveChangesAsync());
    }

    public async Task<ErrorOr<Item>> UpdateAsync(IUpdateDto<Item> request, int id)
    {
        if (request is not UpdateItemDto itemRequest) return Error.Failure("Invalid request type");

        return await itemRequest
            .SetPlaceAndItemType(_itemTypeRepository, _placeRepository)
            .ThenAsync(dto => _itemRepository.Get(id)
                .Then(dto.UpdateEntity))
            .Then(entity => _context.Items.Update(entity).Entity)
            .ThenDoAsync(_ => _context.SaveChangesAsync());
    }

    public async Task<ErrorOr<Item>> DeleteAsync(int id)
    {
        return await _itemRepository.Get(id)
            .Then(item => _context.Items.Remove(item).Entity)
            .ThenDoAsync(_ => _context.SaveChangesAsync());
    }
}