using AtHome.WebApi.Interfaces;

namespace AtHome.WebApi.Models;

public class UpdateItemTypeDto: IUpdateDto<ItemType>
{
    public string? Name { get; set; }

    public ErrorOr<ItemType> UpdateEntity(ItemType entity)
    {
        if (!string.IsNullOrEmpty(Name)) entity.Name = Name;
        
        return entity;
    }
}