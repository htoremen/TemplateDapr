using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TemplateDapr.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly DaprClient _daprClient;
    private readonly ILogger<UserController> logger;

    public UserController(DaprClient daprClient, ILogger<UserController> logger)
    {
        _daprClient = daprClient;
        this.logger = logger;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<GenericResponse<LoginResponseModel>> Login(LoginViewModel model)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<LoginResponseModel>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.Login);
        return response;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("create-user")]
    public async Task<GenericResponse<bool>> CreateUser(CreateUserModel model)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.CreateUser);

        return response;
    }


    [HttpPost]
    [AllowAnonymous]
    [Route("forgot-password")]
    public async Task<GenericResponse<bool>> ForgotPassword(ForgotPasswordModel model)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.ForgotPassword);

        return response;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("update-password")]
    public async Task<GenericResponse<bool>> UpdatePassword(ForgotUpdatePasswordModel model)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.UpdatedPassword);

        return response;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.RefreshToken);

        return Ok(response);
    }

}