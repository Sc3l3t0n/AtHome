using AtHome.WebApi.Interfaces;
using AtHome.Shared.Models;

namespace AtHome.WebApi.Filter;

public class PlaceFilter: IFilter<Place>
{
    public Func<Place, bool> ToPredicate()
    {
        return place => true;
    }
}