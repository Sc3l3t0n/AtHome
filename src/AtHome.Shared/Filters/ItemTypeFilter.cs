using AtHome.Shared.Interfaces;
using AtHome.Shared.Models;

namespace AtHome.Shared.Filters;

public class ItemTypeFilter: IFilter<ItemType>
{
    public Func<ItemType, bool> ToPredicate()
    {
        return type => true;
    }
}