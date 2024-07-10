using System.Net.Http.Headers;
using AtHome.Shared.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace AtHome.Shared.Pages;

public partial class ItemList
{
    [Inject] private HttpClient _http { get; set; }

    private List<Item> _data = [];

    private bool _isFirstLoad = true;

    public ItemList()
    {
        _http = new HttpClient();
        _http.BaseAddress = new Uri("http://athome-webapi");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IkI2NVBPTTB0Q3Q3MjdKY3hmT3NyMyJ9.eyJpc3MiOiJodHRwczovL2Rldi1zY2VsZXRvbi5ldS5hdXRoMC5jb20vIiwic3ViIjoiQ0RKek83bDh3NGNET0dSdnRUTnVEWkVwaHZKU0Y4dWJAY2xpZW50cyIsImF1ZCI6ImF0LWhvbWUiLCJpYXQiOjE3MjA0NzM1NzcsImV4cCI6MTcyMDU1OTk3Nywic2NvcGUiOiJyZWFkIHdyaXRlIiwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwiYXpwIjoiQ0RKek83bDh3NGNET0dSdnRUTnVEWkVwaHZKU0Y4dWIifQ.ZQg8C2wnGVVQR8okwKLGuK5QAnAa9aN6kQnCgrRFXId0qMZjJfU9uzkUZSH_DRQX3tDzzODMNjNRaD7g9K1-mekdzqMRdUFWZRBvvTR9OJEC8XMJN6P4AD3M4Lh2wDJNEisYautxFcyQl5-VE9RA-3cf2GiKH8abbR9mKR6hCJCrrQbpV6ws4dH9aAOZeWETTyhq2edCVs-40HfcFN4vsHHyOaG_jY-dnkqjLT4rcxfz-PLgQncjP-czKF3ItInnKevVLaYRbZ3_8DAO33wKBxpjLhapArdj2QA9I9HnLa0zkf33HmxbvLES59pEum13uzaiyB_NbB06BqB7xuMAiA");
        _isFirstLoad = false;
    }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var response = await _http.GetAsync($"/api/items");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            _data = JsonConvert.DeserializeObject<List<Item>>(jsonString);
        }
        catch (Exception e)
        {
            _data = [];
            _http.CancelPendingRequests();
        }
    }
}