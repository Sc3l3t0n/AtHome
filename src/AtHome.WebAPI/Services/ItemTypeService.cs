using AtHome.Shared.Models;
using AtHome.WebApi.Database;
using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;

namespace AtHome.WebApi.Services;

public class ItemTypeService : IItemTypeService
{
    private readonly ApplicationDbContext _context;
    private readonly IItemTypeRepository _repository;

    public ItemTypeService(ApplicationDbContext context, IItemTypeRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<ErrorOr<ItemType>> CreateAsync(ICreateDto<ItemType> request)
    {
        return await request.ToEntity()
            .Then(itemType => _context.ItemTypes.Add(itemType).Entity)
            .ThenDoAsync(_ => _context.SaveChangesAsync());
    }

    public async Task<ErrorOr<ItemType>> UpdateAsync(IUpdateDto<ItemType> request, int id)
    {
        return await _repository.Get(id)
            .Then(request.UpdateEntity)
            .Then(itemType => _context.ItemTypes.Update(itemType).Entity)
            .ThenDoAsync(_ => _context.SaveChangesAsync());
    }

    public async Task<ErrorOr<ItemType>> DeleteAsync(int id)
    {
        return await _repository.Get(id)
            .Then(itemType => _context.ItemTypes.Remove(itemType).Entity)
            .ThenDoAsync(_ => _context.SaveChangesAsync());
    }
}