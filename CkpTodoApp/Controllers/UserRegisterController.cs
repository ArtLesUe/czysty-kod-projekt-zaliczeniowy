using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers
{
  [Route("api/user/register")]
  [ApiController]
  public class UserRegisterController : ControllerBase
  {
    private readonly ILogger<UserRegisterController> _logger;

    public UserRegisterController(ILogger<UserRegisterController> logger)
    {
      _logger = logger;
    }

    [DisableCors]
    [HttpPost]
    public RootResponse Post()
    {
      return new RootResponse { Status = "OK" };
    }
  }
}
