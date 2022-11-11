using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
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
    public UserLoginTokenResponse Post(UserLoginRequest userLoginRequest)
    {
      if (!userLoginRequest.Validate()) {
        Response.StatusCode = 422;
        return new UserLoginTokenResponse();
      }

      if (userLoginRequest.Authenticate() == 0)
      {
        Response.StatusCode = 401;
        return new UserLoginTokenResponse();
      }
      
      return new UserLoginTokenResponse
      {
        UserId = 1,
        Token = "TOKEN: " + userLoginRequest.Login + " - " + userLoginRequest.Password
      };
    }
  }
}
