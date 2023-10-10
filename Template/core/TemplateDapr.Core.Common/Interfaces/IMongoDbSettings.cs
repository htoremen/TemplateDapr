namespace Core.Application.Common.Interfaces;

public interface IMongoDbSettings
{
    string DatabaseName { get; set; }
    string ConnectionString { get; set; }
}