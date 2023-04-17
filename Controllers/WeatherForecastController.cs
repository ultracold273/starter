using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace starter.Controllers;

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

    [HttpGet(Name = "GetAppliationGateway")]
    public async Task<ActionResult> Post()
    {
        var subscriptionId = "17a663b5-f43a-4bb8-aea0-5a37a2f0cb81";
        var resourceGroupName = "jump-rg-sg";
        var applicationGatewayName = "app-gw-jump-rg-sg";
        HttpClient client = new HttpClient();
        var response = await client.GetAsync($"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Network/applicationGateways/{applicationGatewayName}?api-version=2022-09-01");

        return this.Ok(response);
    }
}
