using Core.Common;
using Core.Event.Events;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ParameterController : ControllerBase
{
    [HttpGet("get-parameter")]
    public async Task<GenericResponse<ParameterEvent>> GetParameter(ParameterEvent parameter)
    {
        var response = new GenericResponse<ParameterEvent>
        {
            Data = parameter
        };

        return response;
    }
}