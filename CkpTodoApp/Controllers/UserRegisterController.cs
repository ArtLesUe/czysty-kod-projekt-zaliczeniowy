using CkpTodoApp.Models;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      string? jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new RootResponse { Status = "wrong-auth" };
      }

      ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      apiToken.Verify();

      if (apiToken.UserId == 0)
      {
        Response.StatusCode = 401;
        return new RootResponse { Status = "wrong-auth" };
      }

      if (!userRegisterRequest.Validate()) {
        Response.StatusCode = 422;
        return new RootResponse { Status = "wrong-data" };
      }

      return new RootResponse { Status = "OK" };
    }
  }
}
