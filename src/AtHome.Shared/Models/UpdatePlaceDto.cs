﻿using AtHome.Shared.Interfaces;

namespace AtHome.Shared.Models;

public class UpdatePlaceDto: IUpdateDto<Place>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public ErrorOr<Place> UpdateEntity(Place place)
    {
        if (!string.IsNullOrWhiteSpace(Name)) place.Name = Name;
        if (!string.IsNullOrWhiteSpace(Description)) place.Description = Description;

        return place;
    }
}