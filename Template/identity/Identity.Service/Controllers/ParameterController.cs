using AutoMapper;
using Identity.Application;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParameterController : ApiControllerBase
    {
        private readonly ILogger<ParameterController> logger;
        private readonly IMapper _mapper;
        public ParameterController(ILogger<ParameterController> logger, IMapper mapper)
        {
            this.logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-parameter")]
        public async Task<GenericResponse<List<CreateParameterModel>>> GetParameter()
        {
            var guid = Guid.NewGuid().ToString();
            logger.LogInformation($"GetParameter : {guid} message from TemplateDaprAPI service");

            var response = await Mediator.Send(new GetParameterQuery());
            return response;
        }

        [Topic("rabbitmq-pubsub", "create.parameter")]
        [HttpPost("create-parameter")]
        public async Task<GenericResponse<bool>> CreateParameter(CreateParameterModel model)
        {
            logger.LogInformation($"CreateParameter : {model.Name} message from TemplateDaprAPI service");

            var command = _mapper.Map<CreateParameterCommand>(model);
            var response = await Mediator.Send(command);
            return response;
        }
    }
}