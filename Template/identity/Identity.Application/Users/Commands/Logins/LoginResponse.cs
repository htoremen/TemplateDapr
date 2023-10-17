namespace Identity.Application.Users;

public class LoginResponse: LoginResponseModel
{
    public LoginResponse(User user, string jwtToken, string refreshToken)
    {
        UserId = user.UserId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Username = user.Username;
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }
}
