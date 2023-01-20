using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiUserService;
using CkpTodoApp.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route(GlobalConstants.BASE_URL_USER + "/edit/{id:int}")]
[ApiController]
public class UserEditController : AuthService
{
  [HttpOptions]
  public void Options()
  {
    return;
  }

  [HttpPatch]
  public RootResponse Patch(UserEditRequest userEditRequest, int id)
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
      return rootResponse;
    }

    ApiUserModel oldUser;

    try 
    {
      oldUser = new ApiUserModel(id);
    } 
    catch(Exception)
    {
      Response.StatusCode = StatusCodes.Status406NotAcceptable;
      return new RootResponse { Status = StatusCodeEnum.UserDoesNotExist.ToString() };
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
    
    var apiUserService = new ApiUserService();
    apiUserService.Update(newUser);

    Response.StatusCode = StatusCodes.Status201Created;
    return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
  }
}
