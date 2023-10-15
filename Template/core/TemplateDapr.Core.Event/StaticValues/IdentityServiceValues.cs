namespace Core.Event;

public static class IdentityServiceValues
{
    public static string ServiceName { get; set; } = "IdentityService";

    public static string Login { get; set;} = "login";
    public static string CreateUser { get; set; } = "create-user";
    public static string ForgotPassword { get; set; } = "forgot-password";
    public static string UpdatedPassword { get; set; } = "update-password";
    public static string RefreshToken { get; set; } = "refresh-token";

    public static string UpdatePassword { get; set; } = "update-password";
    public static string UpdateUserName { get; set; } = "update-username";
    public static string SignOut { get; set; } = "sign-out";
}
