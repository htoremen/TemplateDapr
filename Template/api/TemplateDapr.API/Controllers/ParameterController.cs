using Core.Common;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Core.Event.Events;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class ParameterController : ControllerBase
{
    private readonly DaprClient _daprClient;
    private readonly ILogger<ParameterController> logger;

    public ParameterController(DaprClient daprClient, ILogger<ParameterController> logger)
    {
        _daprClient = daprClient;
        this.logger = logger;
    }

    [HttpGet]
    [Route("get-parameter")]
    public async Task<GenericResponse<ParameterEvent>> GetParameter()
    {
        try
        {
            var response = await _daprClient.InvokeMethodAsync<GenericResponse<ParameterEvent>>(HttpMethod.Get, "TemplateDaprService", "/Parameter/get-parameter");
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("GetParameter : " + ex.Message);
        }
        return new GenericResponse<ParameterEvent>();
    }


    [HttpPost("create-parameter")]
    public async Task<GenericResponse<ParameterEvent>> CreateParameter(ParameterEvent parameter)
    {
        try
        {
            var response = new GenericResponse<ParameterEvent>
            {
                Data = parameter
            };

            await _daprClient.PublishEventAsync("rabbitmq-pubsub", "create.parameter", parameter);
            logger.LogError("pubsub.kafka send : " + parameter.Name);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("CreateParameter pubsub.kafka : " + ex.Message);
        }
        return new GenericResponse<ParameterEvent> { Data = parameter };
    }
}