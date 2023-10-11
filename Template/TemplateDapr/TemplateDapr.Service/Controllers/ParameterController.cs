using Core.Common;
using Core.Event.Events;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[Route("[controller]")]
[ApiController]
public class ParameterController : ControllerBase
{
    private readonly ILogger<ParameterController> logger;
    public ParameterController(ILogger<ParameterController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    [Route("get-parameter")]
    public async Task<GenericResponse<ParameterEvent>> GetParameter()
    {
        var guid= Guid.NewGuid().ToString();
        logger.LogInformation($"GetParameter : {guid} message from TemplateDaprAPI service");

        var response = new GenericResponse<ParameterEvent>
        {
            Data = new ParameterEvent { Name = guid },
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