using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiTokenService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route(GlobalConstants.BASE_URL_USER + "/login")]
[ApiController]
public class UserLoginTokenController : ControllerBase
{
  [HttpPost]
  public UserLoginTokenResponse Post(UserLoginRequest userLoginRequest)
  {
    if (!userLoginRequest.Validate()) {
      Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
      return new UserLoginTokenResponse();
    }

    var userId = userLoginRequest.Authenticate();

    if (userId == 0)
    {
      Response.StatusCode = StatusCodes.Status401Unauthorized;
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