using Microsoft.AspNetCore.Mvc;

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
    public async Task<GenericResponse<List<CreateParameterModel>>> GetParameter()
    {
        try
        {
            var response = await _daprClient.InvokeMethodAsync<GenericResponse<List<CreateParameterModel>>>(HttpMethod.Get, TemplateDaprService.TemplateDaprServiceName, TemplateDaprService.GetParameter);
            logger.LogError("GetParameter Response : " + response.Data);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError("GetParameter : " + ex.Message);
        }
        return null;
    }


    [HttpPost("create-parameter")]
    public async Task<GenericResponse<bool>> CreateParameter(CreateParameterModel parameter)
    {
        try
        {
            for (int i = 0; i < 10000; i++)
            {
                await _daprClient.PublishEventAsync(PubsubNames.RabbitMQ, TopicNames.CreateParameter, parameter);
            }
            logger.LogError("pubsub.rabbitmq send : " + parameter.Name);
            return new GenericResponse<bool> { Data = true }; ;
        }
        catch (Exception ex)
        {
            logger.LogError("CreateParameter pubsub.rabbitmq : " + ex.Message);
        }
        return new GenericResponse<bool> { Data = false };
    }
}