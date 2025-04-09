using Microsoft.AspNetCore.Mvc;

namespace HealthDashboardAPI.Controllers;

// WeatherForecast Controller
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

// Health Controller (corrected)
[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var version = Environment.GetEnvironmentVariable("BUILD_VERSION") ?? "N/A";
        var commit = Environment.GetEnvironmentVariable("GIT_COMMIT") ?? "N/A";
        var release = Environment.GetEnvironmentVariable("RELEASE_ID") ?? "N/A";

        return Ok(new
        {
            status = "Healthy",
            buildVersion = version,
            gitCommit = commit,
            releaseId = release
        });
    }
}
