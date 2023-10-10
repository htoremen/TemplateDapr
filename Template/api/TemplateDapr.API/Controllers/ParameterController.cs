using Core.Common;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Core.Event.Events;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParameterController : ControllerBase
{
    private readonly DaprClient _daprClient;

    public ParameterController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [HttpGet("get-parameter")]
    public async Task<GenericResponse<ParameterEvent>> GetParameter(ParameterEvent parameter)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<ParameterEvent>>(HttpMethod.Get, "TemplateDapr", "get-parameter");
        return response;
    }
}