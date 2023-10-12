using Microsoft.AspNetCore.Mvc;
namespace Service.Controllers;

[Route("[controller]")]
[ApiController]
public class ParameterController : ApiControllerBase
{
    private readonly ILogger<ParameterController> logger;
    public ParameterController(ILogger<ParameterController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    [Route("get-parameter")]
    public async Task<GenericResponse<List<ParameterModel>>> GetParameter()
    {
        var guid= Guid.NewGuid().ToString();
        logger.LogInformation($"GetParameter : {guid} message from TemplateDaprAPI service");

        var response = await Mediator.Send(new GetParameterQuery());
        return response;
    }

    [Topic("rabbitmq-pubsub", "create.parameter")]
    [HttpPost("create-parameter")]
    public async Task<GenericResponse<ParameterModel>> CreateParameter(ParameterModel parameter)
    {
        logger.LogInformation($"CreateParameter : {parameter.Name} message from TemplateDaprAPI service");

        var response = new GenericResponse<ParameterModel>
        {
            Data = parameter
        };

        return response;
    }
}