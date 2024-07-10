using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;

namespace AtHome.WebApi.Filter;

public class PlaceFilter: IFilter<Place>
{
    public Func<Place, bool> ToPredicate()
    {
        return place => true;
    }
}