using Identity.Application.Authenticates;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Controllers;

namespace Identity.API.Controllers
{
    public class UserController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("register-stage")]
        public async Task<GenericResponse<List<UserRegisterStageQueryResponse>>> RegisterStage()
        {
            var command = new UserRegisterStageQuery { ParameterTypeId = "c4140313-9344-4170-9b31-3bc3ce2bc200" };

            var response = await Mediator.Send(command);
            return response;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<GenericResponse<LoginResponse>> Login(LoginViewModel model)
        {
            var command = Mapper.Map<LoginCommand>(model);
            command.IpAddress = ipAddress();

            var response = await Mediator.Send(command);
            return response;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("signin-google")]
        public async Task GoogleLogin()
        {
            //var properties = new AuthenticationProperties { RedirectUri = "https://localhost:6021/GoogleResponse" };
            //return Challenge(properties, GoogleDefaults.AuthenticationScheme);

            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = "https://localhost:6010/GoogleResponse"
            });

            //var properties = new AuthenticationProperties { RedirectUri = Url.Action("googleresponse") };
            //return Challenge(properties, GoogleDefaults.AuthenticationScheme);

            //string returnUrl="";
            //string provider = "google";
            //string LoginCallback = "googleresponse";
            //string authenticationScheme = string.Empty;
            //authenticationScheme = GoogleDefaults.AuthenticationScheme;

            //var auth = new AuthenticationProperties
            //{
            //    RedirectUri = Url.Action(nameof(LoginCallback), new { provider, returnUrl })
            //};

            //return new ChallengeResult(authenticationScheme, auth);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("googleresponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return Ok(claims);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("google-logout")]
        public async Task<IActionResult> GoogleLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpPost]
        [Route("create-user")]
        public async Task<GenericResponse<bool>> CreateUser(CreateUserModel model)
        {
            var command = Mapper.Map<CreateUserCommand>(model);
            command.LoginType = Domain.Enums.LoginType.Web;
            var response = await Mediator.Send(command);
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("external-create-user")]
        public async Task<GenericResponse<bool>> ExternalCreateUser(ExternalCreateUserModel model)
        {
            var command = Mapper.Map<ExternalCreateUserCommand>(model);
            command.LoginType = Domain.Enums.LoginType.Google;
            var response = await Mediator.Send(command);
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<GenericResponse<bool>> ForgotPassword(ForgotPasswordModel model)
        {
            var command = Mapper.Map<ForgotPasswordCommand>(model);
            var response = await Mediator.Send(command);
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("update-password")]
        public async Task<GenericResponse<bool>> UpdatePassword(ForgotUpdatePasswordModel model)
        {
            var command = Mapper.Map<ForgotUpdatePasswordCommand>(model);
            var response = await Mediator.Send(command);
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await Mediator.Send(new UserRefreshTokenCommand
            {
                IpAddress = ipAddress(),
                Token = refreshToken
            });
            setTokenCookie(response.Data.RefreshToken);
            return Ok(response);
        }

        private void setTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}