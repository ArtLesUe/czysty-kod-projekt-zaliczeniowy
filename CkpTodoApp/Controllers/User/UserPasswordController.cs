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
    
    if (rootResponse.Status != "OK")
    {
      return rootResponse;
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

    var apiUserService = new ApiUserService();
    apiUserService.ChangePassword(user, UserRegisterController.Md5(userPasswordRequest.Password));

    Response.StatusCode = 201;
    return new RootResponse { Status = "OK" };
  }
}