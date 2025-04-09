using Microsoft.AspNetCore.Mvc;

namespace HealthDashboardAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm",
        "Balmy", "Hot", "Sweltering", "Scorching"
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

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        string buildVersion = Environment.GetEnvironmentVariable("BUILD_VERSION") ?? "N/A";
        string gitCommit = Environment.GetEnvironmentVariable("GIT_COMMIT") ?? "N/A";
        string releaseId = Environment.GetEnvironmentVariable("RELEASE_ID") ?? "N/A";

        var healthInfo = new
        {
            status = "Healthy",
            buildVersion,
            gitCommit,
            releaseId,
            timestamp = DateTime.UtcNow.ToString("o") // ISO 8601 format
        };

        return Ok(healthInfo);
    }
}
