using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiUserService;
using CkpTodoApp.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route(GlobalConstants.BASE_URL_USER + "/password/{id:int}")]
[ApiController]
public class UserPasswordController : AuthService
{
  [HttpOptions]
  public void Options()
  {
    return;
  }

  [HttpPatch]
  public RootResponse Patch(UserPasswordRequest userPasswordRequest, int id)
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
      return rootResponse;
    }

    if (string.IsNullOrEmpty(userPasswordRequest.Password))
    {
      Response.StatusCode = StatusCodes.Status406NotAcceptable;
      return new RootResponse { Status = StatusCodeEnum.EmptyPasswordNotPermitted.ToString() };
    }

    ApiUserModel user;

    try 
    {
      user = new ApiUserModel(id);
    }
    catch (Exception)
    {
      Response.StatusCode = StatusCodes.Status406NotAcceptable;
      return new RootResponse { Status = StatusCodeEnum.UserDoesNotExist.ToString() };
    }

    var apiUserService = new ApiUserService();
    apiUserService.ChangePassword(user,  Md5HashGenerator.TextToMd5(userPasswordRequest.Password));

    Response.StatusCode = StatusCodes.Status201Created;
    return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
  }
}