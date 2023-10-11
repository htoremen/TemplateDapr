using Core.Common;
using Core.Event.Events;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ParameterController : ControllerBase
{
    private readonly ILogger<ParameterController> logger;
    public ParameterController(ILogger<ParameterController> logger)
    {
        this.logger = logger;
    }

    [HttpGet("get-parameter")]
    public async Task<GenericResponse<ParameterEvent>> GetParameter(ParameterEvent parameter)
    {
        logger.LogInformation($"GetParameter : {parameter.Name} message from TemplateDaprAPI service");

        var response = new GenericResponse<ParameterEvent>
        {
            Data = parameter
        };

        return response;
    }

    [Topic("pubsub", "create-parameter")]
    [HttpPost("create-parameter")]
    public async Task<GenericResponse<ParameterEvent>> CreateParameter(ParameterEvent parameter)
    {
        logger.LogInformation($"CreateParameter : {parameter.Name} message from TemplateDaprAPI service");

        var response = new GenericResponse<ParameterEvent>
        {
            Data = parameter
        };

        return response;
    }
}