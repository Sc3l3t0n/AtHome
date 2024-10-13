using AtHome.Shared.Errors;
using AtHome.Shared.Models;
using AtHome.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace AtHome.Shared.Pages;

public partial class PlaceList : ComponentBase
{
    [Inject] private IPlaceApi _placeApi { get; set; }

    private ErrorOr<List<Place>> _data = ApiErrors.NotLoaded();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _data = await _placeApi.GetAllAsync();
        }
        catch (Exception e)
        {
            _data = Error.Failure("Task.Failed", e.Message);
        }
    }
}