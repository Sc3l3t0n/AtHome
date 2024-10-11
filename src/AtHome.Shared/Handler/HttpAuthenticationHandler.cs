using System.Net.Http.Headers;
using AtHome.Shared.Services;
using Microsoft.Extensions.Logging;

namespace AtHome.Shared.Handler;

public class HttpAuthenticationHandler : DelegatingHandler
{

  private readonly ApiTokenService _tokenService;
  private readonly ILogger<HttpAuthenticationHandler> _logger;

  public HttpAuthenticationHandler(ApiTokenService tokenService, ILogger<HttpAuthenticationHandler> logger)
  {
    _tokenService = tokenService;
    _logger = logger;
  }

  protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
  {
    var token = _tokenService.GetToken();
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    _logger.LogDebug("Request: {Request}", request);
    return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
  }
}
