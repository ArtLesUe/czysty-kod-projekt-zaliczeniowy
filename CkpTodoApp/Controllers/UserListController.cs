using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers
{
  [Route("api/user/list")]
  [ApiController]
  public class UserListController : ControllerBase
  {
    private readonly ILogger<UserLoginTokenController> _logger;

    public UserListController(ILogger<UserLoginTokenController> logger)
    {
      _logger = logger;
    }

    [DisableCors]
    [HttpGet]
    public RootResponse Get()
    {
      return new RootResponse
      {
        Status = "ok"
      };
    }
  }
}
