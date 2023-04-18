using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace starter.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "GetAppliationGateway")]
    public async Task<ActionResult> Post([FromBody]string? tokenFromUser)
    {
        HttpClient client = new HttpClient();

        var credential = new Azure.Identity.DefaultAzureCredential();
        string token;
        if (tokenFromUser is null) {
            var tokenTask = await credential.GetTokenAsync(new Azure.Core.TokenRequestContext(new[] { "https://management.azure.com/.default" }));
            token = tokenTask.Token;
        } else {
            token = tokenFromUser;
        }
     
        var subscriptionId = "17a663b5-f43a-4bb8-aea0-5a37a2f0cb81";
        var resourceGroupName = "jump-rg-sg";
        var applicationGatewayName = "app-gw-jump-rg-sg";
        var armUri = new Uri($"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Network/applicationGateways/{applicationGatewayName}?api-version=2022-09-01");
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, armUri))
        {
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(requestMessage);
            
            return this.Ok(new {request = requestMessage, response = response});
        }
    }
}
