using AtHome.Shared.Errors;
using AtHome.Shared.Models;
using AtHome.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace AtHome.Shared.Pages;

public partial class ItemTypeList : ComponentBase
{
    [Inject] private IItemTypeApi _iItemTypeApi { get; set; }

    private ErrorOr<List<ItemType>> _data = ApiErrors.NotLoaded();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _data = await _iItemTypeApi.GetAllAsync();
        }
        catch (Exception e)
        {
            _data = Error.Failure("Task.Failed", e.Message);
        }
    }
}