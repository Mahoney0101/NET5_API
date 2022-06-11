namespace Api.Controllers;

public class Users : Controller
{
    private readonly UserService _userService;

    public Users(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}", Name = "GetUser")]
    public ActionResult<User> Get(string id)
    {
        var book = _userService.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    [Route("login")]
    public ActionResult<User> Create(User user)
    {
        _userService.Create(user);
        ArgumentNullException.ThrowIfNull(user.Id);
        return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
    }
}
