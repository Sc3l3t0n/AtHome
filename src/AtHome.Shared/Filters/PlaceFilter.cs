using AtHome.Shared.Interfaces;
using AtHome.Shared.Models;

namespace AtHome.Shared.Filters;

public class PlaceFilter: IFilter<Place>
{
    public Func<Place, bool> ToPredicate()
    {
        return place => true;
    }
}