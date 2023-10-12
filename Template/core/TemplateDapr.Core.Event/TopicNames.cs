namespace TemplateDapr.Core;

public static class TopicNames
{
    public static string CreateParameter { get; set; } = "create.parameter";
    public static string UpdateParameter { get; set; } = "update.parameter";
}


public enum Topics
{
    CreateParameter = 1,
    UpdateParameter = 2
}