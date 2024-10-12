using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AtHome.Shared.Configuration;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace AtHome.Shared.Services;

public class ApiTokenService
{
    /// <summary>
    /// Httpclient of the service.
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Client configuration for token requesting.
    /// </summary>
    private readonly ClientConfiguration _client;

    /// <summary>
    /// Current jwt in as parsed object
    /// </summary>
    private JwtSecurityToken? _currentJwt;
    
    /// <summary>
    /// Current jwt in raw form
    /// </summary>
    private string? _currentRawJwt;

    /// <summary>
    /// Determines if the current token is still valid
    /// </summary>
    private bool TokenValid => _currentJwt is not null &&
                               _currentJwt.ValidTo - DateTime.Now > new TimeSpan(0, 2, 0);

    public ApiTokenService(HttpClient httpClient, IConfiguration configuration)
    {
        httpClient.BaseAddress =
            new Uri(configuration["Auth:TokenUrl"] ?? throw new ArgumentException("Auth:TokenUrl must be set"));
        _httpClient = httpClient;
        _client = configuration.GetSection("Auth:Client").Get<ClientConfiguration>();
    }

    /// <summary>
    /// Returns a jwt token.
    /// Only requests a new one if the old one is expired.
    /// </summary>
    public async Task<ErrorOr<JwtSecurityToken>> GetToken()
    {
        if (TokenValid) return _currentJwt!;

        var token = await RetrieveToken();

        if (token.IsError) return token.Errors;

        var jwt = ParseJwt(token.Value);

        if (jwt is null) return Error.Failure("Could not parse Token");

        SetCurrentToken(token.Value, jwt);

        return jwt;
    }

    /// <summary>
    /// Returns a raw jwt token.
    /// Only requests a new one if the old one is expired.
    /// </summary>
    public async Task<ErrorOr<string>> GetRawToken()
    {
        if (TokenValid) return _currentRawJwt!;

        var token = await RetrieveToken();

        if (token.IsError) return token.Errors;

        SetCurrentToken(token.Value);

        return token.Value;
    }

    /// <summary>
    /// Retrieve the token from the identity provider.
    /// </summary>
    private async Task<ErrorOr<string>> RetrieveToken()
    {
        var authToken = Encoding.ASCII.GetBytes($"{_client.ClientId}:{_client.ClientSecret}");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

        var requestData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials")
        });

        var response = await _httpClient.PostAsync("", requestData);

        if (!response.IsSuccessStatusCode) return Error.Failure("Could not retrieve a Token");
        var content = await response.Content.ReadAsStringAsync();

        var token = JsonDocument.Parse(content).RootElement.GetProperty("access_token").GetString();
        if (token is null) return Error.Failure("Could not retrieve a Token");

        return token;
    }

    /// <summary>
    /// Set current token for caching.
    /// </summary>
    /// <param name="jwt">Current raw token.</param>
    private void SetCurrentToken(string jwt)
    {
        SetCurrentToken(jwt, ParseJwt(jwt)!);
    }

    /// <summary>
    /// Set current token for caching.
    /// </summary>
    /// <param name="rawJwt">Current raw token.</param>
    /// <param name="jwt">Current parsed token.</param>
    private void SetCurrentToken(string rawJwt, JwtSecurityToken jwt)
    {
        _currentJwt = jwt;
        _currentRawJwt = rawJwt;
    }

    /// <summary>
    /// Parses a jwt to retrieve the information of it.
    /// </summary>
    /// <param name="jwt">Raw jwt token.</param>
    /// <returns>The token or null if it couldn't be parsed.</returns>
    private static JwtSecurityToken? ParseJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        return !handler.CanReadToken(jwt) ? null : handler.ReadJwtToken(jwt);
    }
}