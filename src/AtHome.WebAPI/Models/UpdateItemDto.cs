using AtHome.WebApi.Interfaces;

namespace AtHome.WebApi.Models;

public class UpdateItemDto : IUpdateDto<Item>
{
    public string? Name { get; set; }
    public int? TypeId { get; set; }
    public string? Description { get; set; }
    public int? Amount { get; set; }
    public int? PlaceId { get; set; }
    public DateTime? StoreDate { get; set; }
    public DateTime? ExpireDate { get; set; }

    private Place? _place;
    private ItemType? _itemType;

    public ErrorOr<Item> UpdateEntity(Item entity)
    {
        if (Name != null) entity.Name = Name;
        if (TypeId != null && _itemType != null) entity.Type = _itemType;
        if (Description != null) entity.Description = Description;
        if (Amount != null) entity.Amount = Amount.Value;
        if (PlaceId != null && _place != null) entity.Place = _place;
        if (StoreDate != null) entity.StoreDate = StoreDate.Value;
        if (ExpireDate != null) entity.ExpireDate = ExpireDate.Value;
        
        return entity;
    }

    public async Task<ErrorOr<UpdateItemDto>> SetPlaceAndItemType(IItemTypeRepository itemTypeRepository, IPlaceRepository placeRepository)
    {
        if (TypeId != null)
        {
            var type = await itemTypeRepository.Get(TypeId.Value);
            if (type.IsError) return Error.NotFound("ItemType.NotFound");
            _itemType = type.Value;
        }

        if (PlaceId != null)
        {
            var place = await placeRepository.Get(PlaceId.Value);
            if (place.IsError) return Error.NotFound("Place.NotFound");
            _place = place.Value;
        }

        return this;
    }
}