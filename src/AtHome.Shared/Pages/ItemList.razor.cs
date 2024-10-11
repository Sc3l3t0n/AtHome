using AtHome.Shared.Errors;
using AtHome.Shared.Models;
using AtHome.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace AtHome.Shared.Pages;

public partial class ItemList
{
    [Inject] private IItemApi _itemApi { get; set; }

    private ErrorOr<List<Item>> _data = ApiErrors.NotLoaded();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _data = await _itemApi.GetAllAsync();
        }
        catch (Exception e)
        {
            _data = Error.Failure("Task.Failed", e.Message);
        }
    }
}