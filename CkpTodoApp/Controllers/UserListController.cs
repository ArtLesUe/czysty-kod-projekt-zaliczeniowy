using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      string jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new RootResponse { Status = "HTTP 401" };
      }

      return new RootResponse
      {
        Status = jsonWebToken
      };
    }
  }
}
