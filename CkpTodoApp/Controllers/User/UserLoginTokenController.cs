using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiTokenService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/login")]
[ApiController]
public class UserLoginTokenController : ControllerBase
{
  [HttpPost]
  public UserLoginTokenResponse Post(UserLoginRequest userLoginRequest)
  {
    if (!userLoginRequest.Validate()) {
      Response.StatusCode = 422;
      return new UserLoginTokenResponse();
    }

    var userId = userLoginRequest.Authenticate();

    if (userId == 0)
    {
      Response.StatusCode = 401;
      return new UserLoginTokenResponse();
    }

    var apiToken = new ApiTokenModel(0, userId, Guid.NewGuid().ToString());
    var apiTokenService = new ApiTokenService();
    apiTokenService.Save(apiToken);   
    
    return new UserLoginTokenResponse
    {
      UserId = apiToken.UserId,
      Token = apiToken.Token
    };
  }
}