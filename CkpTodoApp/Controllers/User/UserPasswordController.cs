using CkpTodoApp.Constants;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiUserService;
using CkpTodoApp.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/password/{id:int}")]
[ApiController]
public class UserPasswordController : AuthService
{
  [HttpPost]
  public RootResponse Post(UserPasswordRequest userPasswordRequest, int id)
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
    apiUserService.ChangePassword(user, UserRegisterController.Md5(userPasswordRequest.Password));

    Response.StatusCode = StatusCodes.Status201Created;
    return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
  }
}