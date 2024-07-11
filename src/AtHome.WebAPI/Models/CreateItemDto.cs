using AtHome.Shared.Models;
using AtHome.WebApi.Interfaces;

namespace AtHome.WebApi.Models;

public class CreateItemDto : ICreateDto<Item>
{
    public string Name { get; set; }
    public int TypeId { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public int PlaceId { get; set; }
    public DateTime StoreDate { get; set; }
    public DateTime? ExpireDate { get; set; }

    private Place _place;
    private ItemType _itemType;

    public ErrorOr<Item> ToEntity()
    {
        return new Item()
        {
            Guid = Guid.NewGuid(),
            Name = Name,
            Description = Description,
            Amount = Amount,
            StoreDate = StoreDate,
            ExpireDate = ExpireDate,
            Place = _place,
            Type = _itemType
        };
    }

    public async Task<ErrorOr<CreateItemDto>> SetPlaceAndItemType(IItemTypeRepository itemTypeRepository,
        IPlaceRepository placeRepository)
    {
        var type = await itemTypeRepository.Get(TypeId);
        if (type.IsError) return Error.NotFound("ItemType.NotFound");
        _itemType = type.Value;

        var place = await placeRepository.Get(PlaceId);
        if (place.IsError) return Error.NotFound("Place.NotFound");
        _place = place.Value;

        return this;
    }
}