using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace CkpTodoApp.Controllers
{
  [Route("api/user/delete/{id}")]
  [ApiController]
  public class UserDeleteController : ControllerBase
  {
    private readonly ILogger<UserDeleteController> _logger;

    public UserDeleteController(ILogger<UserDeleteController> logger)
    {
      _logger = logger;
    }

    [DisableCors]
    [HttpGet]
    public RootResponse Get(int id)
    {
      return new RootResponse { Status = "deleted" };
    }
  }
}
