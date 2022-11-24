using CkpTodoApp.Requests;
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
    public RootResponse Post(UserRegisterRequest userRegisterRequest)
    {
      if (!userRegisterRequest.Validate()) {
        Response.StatusCode = 422;
        return new RootResponse { Status = "wrong-data" };
      }

      return new RootResponse { Status = "OK" };
    }
  }
}
