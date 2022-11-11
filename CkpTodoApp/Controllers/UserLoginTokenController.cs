using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers
{
  [Route("api/user/login")]
  [ApiController]
  public class UserLoginTokenController : ControllerBase
  {
    private readonly ILogger<UserLoginTokenController> _logger;

    public UserLoginTokenController(ILogger<UserLoginTokenController> logger)
    {
      _logger = logger;
    }

    [DisableCors]
    [HttpPost]
    public UserLoginTokenResponse Post()
    {
      return new UserLoginTokenResponse
      {
        UserId = 1,
        Token = "TOKEN"
      };
    }
  }
}
