namespace Api.Models;

public class UserDatabaseSettings : IUserDatabaseSettings
{
    public string? UserCollectionName { get; set; }
    public string? UserConnectionString { get; set; }
    public string? UserDatabaseName { get; set; }
}

public interface IUserDatabaseSettings
{
    string? UserCollectionName { get; set; }
    string? UserConnectionString { get; set; }
    string? UserDatabaseName { get; set; }
}
