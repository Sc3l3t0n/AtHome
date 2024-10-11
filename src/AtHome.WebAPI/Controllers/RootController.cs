using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AtHome.WebApi.Controllers;

[ApiController]
[Route("/")]
public class RootController(ILogger<RootController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
            var token = HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                logger.LogInformation("JWT used in request: {Token}", token);
            }
            return Ok();
    }
}