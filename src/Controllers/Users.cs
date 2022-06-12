namespace Api.Controllers;

public class Users : Controller
{
    private readonly UserService c_userService;

    public Users(UserService userService)
    {
        c_userService = userService;
    }


    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var book = await c_userService.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }


    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<User>> Create(User user)
    {
        await c_userService.Create(user);
        ArgumentNullException.ThrowIfNull(user.Id);
        return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
    }
}
