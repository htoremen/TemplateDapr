using Identity.Application.Accounts;
using Infrastructure.Models;
using Infrastructure.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Service.Controllers;

namespace Identity.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ApiControllerBase
{
    [HttpPost]
    [Route("update-password")]
    public async Task<GenericResponse<UpdatePasswordResponse>> UpdatePassword(UpdatePasswordModel model)
    {
        var command = Mapper.Map<UpdatePasswordCommand>(model);
        var userId = UserService.UserId;
        command.UserId = userId;
        var response = await Mediator.Send(command);
        return response;
    }


    [HttpPost]
    [Route("update-username")]
    public async Task<GenericResponse<UpdateUserNameResponse>> UpdateUserName(UpdateUserNameModel model)
    {
        var command = Mapper.Map<UpdateUserNameCommand>(model);

        command.UserId = UserService.UserId;
        var response = await Mediator.Send(command);
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
        var command = new SignOutCommand();
        command.UserId = UserService.UserId;
        var response = await Mediator.Send(command);

        return GenericResponse<bool>.Success(true, 200);
    }
}
