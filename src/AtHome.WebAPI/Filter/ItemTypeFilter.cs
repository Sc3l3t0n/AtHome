using AtHome.WebApi.Interfaces;
using AtHome.WebApi.Models;

namespace AtHome.WebApi.Filter;

public class ItemTypeFilter: IFilter<ItemType>
{
    public Func<ItemType, bool> ToPredicate()
    {
        return type => true;
    }
}