using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiTokenService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/edit/{id:int}")]
[ApiController]
public class UserEditController : ControllerBase
{
  [HttpPost]
  public RootResponse Post(UserEditRequest userEditRequest, int id)
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    var apiTokenService = new ApiTokenService();
    apiTokenService.Verify(apiToken);
    
    if (apiToken.UserId == 0)
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }

    ApiUserModel oldUser;

    try 
    {
      oldUser = new ApiUserModel(id);
    } 
    catch(Exception)
    {
      Response.StatusCode = 406;
      return new RootResponse { Status = "user-dont-exists" };
    }

    var newUser = new ApiUserModel(
      oldUser.Id, 
      string.IsNullOrEmpty(userEditRequest.Name) ? oldUser.Name : userEditRequest.Name,
      string.IsNullOrEmpty(userEditRequest.Surname) ? oldUser.Surname : userEditRequest.Surname,
      oldUser.Email,
      oldUser.PasswordHashed,
      string.IsNullOrEmpty(userEditRequest.AboutMe) ? oldUser.AboutMe : userEditRequest.AboutMe,
      string.IsNullOrEmpty(userEditRequest.City) ? oldUser.City : userEditRequest.City,
      string.IsNullOrEmpty(userEditRequest.Country) ? oldUser.Country : userEditRequest.Country,
      string.IsNullOrEmpty(userEditRequest.University) ? oldUser.University : userEditRequest.University
    );
    newUser.Update();

    Response.StatusCode = 201;
    return new RootResponse { Status = "OK" };
  }
}
