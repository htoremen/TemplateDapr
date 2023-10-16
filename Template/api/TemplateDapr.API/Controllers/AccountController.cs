using Infrastructure.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace TemplateDapr.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly DaprClient _daprClient;
    private readonly ILogger<AccountController> logger;

    public AccountController(DaprClient daprClient, ILogger<AccountController> logger)
    {
        _daprClient = daprClient;
        this.logger = logger;
    }

    [HttpPost]
    [Route("update-password")]
    public async Task<GenericResponse<bool>> UpdatePassword(UpdatePasswordModel model)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.UpdatedPassword);
        return response;
    }


    [HttpPost]
    [Route("update-username")]
    public async Task<GenericResponse<bool>> UpdateUserName(UpdateUserNameModel model)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.UpdateUserName);
        return response;
    }

    /// <summary>
    /// Metot tamamlanacak.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("sign-out")]
    public async Task<GenericResponse<bool>> SignOut()
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.SignOut);
        return GenericResponse<bool>.Success(true, 200);
    }
}
