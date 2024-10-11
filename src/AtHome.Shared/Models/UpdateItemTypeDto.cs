using AtHome.Shared.Interfaces;

namespace AtHome.Shared.Models;

public class UpdateItemTypeDto: IUpdateDto<ItemType>
{
    public string? Name { get; set; }

    public ErrorOr<ItemType> UpdateEntity(ItemType entity)
    {
        if (!string.IsNullOrEmpty(Name)) entity.Name = Name;
        
        return entity;
    }
}