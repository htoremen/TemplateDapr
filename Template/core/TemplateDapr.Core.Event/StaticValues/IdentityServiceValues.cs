namespace Core.Event;

public static class IdentityServiceValues
{
    public static string ServiceName { get; set; } = "IdentityService";

    public static string Login { get; set;} = "/user/login";
    public static string CreateUser { get; set; } = "/user/create-user";
    public static string ForgotPassword { get; set; } = "/user/forgot-password";
    public static string UpdatedPassword { get; set; } = "/user/update-password";
    public static string RefreshToken { get; set; } = "/user/refresh-token";

    public static string UpdatePassword { get; set; } = "/user/update-password";
    public static string UpdateUserName { get; set; } = "/user/update-username";
    public static string SignOut { get; set; } = "/user/sign-out";
}
