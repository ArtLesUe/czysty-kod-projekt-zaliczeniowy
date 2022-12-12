using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/password/{id:int}")]
[ApiController]
public class UserPasswordController : ControllerBase
{
  [HttpPost]
  public RootResponse Post(UserPasswordRequest userPasswordRequest, int id)
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    apiToken.Verify();

    if (apiToken.UserId == 0)
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }

    if (string.IsNullOrEmpty(userPasswordRequest.Password))
    {
      Response.StatusCode = 406;
      return new RootResponse { Status = "empty-password-not-permitted" };
    }

    ApiUserModel user;

    try 
    {
      user = new ApiUserModel(id);
    }
    catch (Exception)
    {
      Response.StatusCode = 406;
      return new RootResponse { Status = "user-not-exists" };
    }

    user.PasswordChange(UserRegisterController.Md5(userPasswordRequest.Password));

    Response.StatusCode = 201;
    return new RootResponse { Status = "OK" };
  }
}