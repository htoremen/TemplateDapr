using Core.Application.Common.Interfaces;

namespace Core.Infrastructure;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public MongoDbSettings MongoDbSettings { get; set; }

    public ServiceUrls ServiceUrls { get; set; }

    public Authenticate Authenticate { get; set; }
    public MailSettings MailSetting { get; set; }
    public Authentication Authentication { get; set; }
}

public class Authentication
{
    public Google Google { get; set; }
}

public class Google
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}

public class MailSettings
{
    public string? DisplayName { get; set; }
    public string? From { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Host { get; set; }
    public string? ImapHost { get; set; }
    public int Port { get; set; }
    public int ImapPort { get; set; }
    public bool UseSSL { get; set; }
    public bool UseStartTls { get; set; }
}

public class Authenticate
{
    public string Secret { get; set; }
    public string RefreshTokenTTL { get; set; }
}

public class ConnectionStrings
{
    public string ConnectionString { get; set; }
    public string Monitoring { get; set; }
}

public class ServiceUrls
{
    public string Saga { get; set; }
    public string Customer { get; set; }
    public string Agent { get; set; }
}

public class MongoDbSettings : IMongoDbSettings
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
}
