﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TemplateDapr.API.Controllers;

[Route("[controller]")]
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

    [HttpPost]
    [Route("login")]
    public async Task<GenericResponse<LoginResponseModel>> Login(LoginViewModel model)
    {
        var request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Post,
            IdentityServiceValues.ServiceName,
            IdentityServiceValues.Login,
            model);

        var response = await _daprClient.InvokeMethodAsync<GenericResponse<LoginResponseModel>>(request);
        return response;
    }

    [HttpPost]
    [Route("create-user")]
    public async Task<GenericResponse<bool>> CreateUser(CreateUserModel model)
    {
        var request = _daprClient.CreateInvokeMethodRequest(HttpMethod.Post,
            IdentityServiceValues.ServiceName,
            IdentityServiceValues.CreateUser,
            model);    
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(request);

        return response;
    }


    [HttpPost]
    [Route("forgot-password")]
    public async Task<GenericResponse<bool>> ForgotPassword(ForgotPasswordModel model)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.ForgotPassword);

        return response;
    }

    [HttpPost]
    [Route("update-password")]
    public async Task<GenericResponse<bool>> UpdatePassword(ForgotUpdatePasswordModel model)
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.UpdatedPassword);

        return response;
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var response = await _daprClient.InvokeMethodAsync<GenericResponse<bool>>(HttpMethod.Post, IdentityServiceValues.ServiceName, IdentityServiceValues.RefreshToken);

        return Ok(response);
    }

}