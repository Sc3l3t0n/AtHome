using AtHome.Shared.Interfaces;
using AtHome.Shared.Models;

namespace AtHome.Shared.Filters;

public class ItemFilter: IFilter<Item>
{
    public Func<Item, bool> ToPredicate()
    {
        return entity => true;
    }
}