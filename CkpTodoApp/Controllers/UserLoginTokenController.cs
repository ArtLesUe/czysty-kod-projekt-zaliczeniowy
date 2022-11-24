using CkpTodoApp.Models;
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
    public UserLoginTokenResponse Post([FromForm] UserLoginRequest userLoginRequest)
    {
      if (!userLoginRequest.Validate()) {
        Response.StatusCode = 422;
        return new UserLoginTokenResponse();
      }

      int userId = userLoginRequest.Authenticate();

      if (userId == 0)
      {
        Response.StatusCode = 401;
        return new UserLoginTokenResponse();
      }

      ApiTokenModel apiTokenModel = new ApiTokenModel(0, userId, Guid.NewGuid().ToString());
      apiTokenModel.Save();
      
      return new UserLoginTokenResponse
      {
        UserId = apiTokenModel.UserId,
        Token = apiTokenModel.Token
      };
    }
  }
}
