namespace Api.Services;

public class UserService
{
    private readonly IMongoCollection<User> c_users;

    public UserService(IUserDatabaseSettings settings)
    {
        var client = new MongoClient(settings.UserConnectionString);
        var database = client.GetDatabase(settings.UserDatabaseName);

        c_users = database.GetCollection<User>(settings.UserCollectionName);
    }


    public async Task<List<User>> Get()
    {
        var _userCollection = await c_users.FindAsync(user => true);
        return _userCollection.ToList();
    }


    public async Task<User> Get(string id)
    {
        var _user = await c_users.FindAsync(user => user.Id == id);
        return _user.FirstOrDefault();
    }


    public async Task<User> Create(User user)
    {
        await c_users.InsertOneAsync(user);
        return user;
    }


    public async Task Update(string id, User userIn)
    {
        await c_users.ReplaceOneAsync(user => user.Id == id, userIn);
    }


    public async Task Remove(User userIn)
    {
        await c_users.DeleteOneAsync(user => user.Id == userIn.Id);
    }


    public async Task Remove(string id)
    {
        await c_users.DeleteOneAsync(user => user.Id == id);
    }
}