using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;

namespace AtHome.WebApi.Filter;

public class ItemFilter: IFilter<Item>
{
    public Func<Item, bool> ToPredicate()
    {
        return entity => true;
    }
}