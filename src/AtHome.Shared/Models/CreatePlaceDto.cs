using AtHome.Shared.Interfaces;

namespace AtHome.Shared.Models;

public class CreatePlaceDto : ICreateDto<Place>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public ErrorOr<Place> ToEntity()
    {
        // TODO: Outsource validation to a separate class
        if (string.IsNullOrWhiteSpace(Name))
            return Error.Validation("Name is required");
        if (string.IsNullOrWhiteSpace(Description))
            return Error.Validation("Description is required");

        return new Place()
        {
            Name = Name,
            Description = Description
        };
    }
}