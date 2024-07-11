using AtHome.WebApi.Interfaces;
using AtHome.Shared.Models;

namespace AtHome.WebApi.Filter;

public class ItemFilter: IFilter<Item>
{
    public Func<Item, bool> ToPredicate()
    {
        return entity => true;
    }
}