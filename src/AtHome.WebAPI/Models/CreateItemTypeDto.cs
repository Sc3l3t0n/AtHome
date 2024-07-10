using AtHome.WebApi.Interfaces;

namespace AtHome.WebApi.Models;

public class CreateItemTypeDto : ICreateDto<ItemType>
{
    public string Name { get; set; }

    public ErrorOr<ItemType> ToEntity()
    {
        // TODO: Outsource validation to a separate class
        if (string.IsNullOrWhiteSpace(Name))
            return Error.Validation("Name is required");

        return new ItemType()
        {
            Name = Name
        };
    }
}