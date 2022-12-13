using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiUserService;
using CkpTodoApp.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/edit/{id:int}")]
[ApiController]
public class UserEditController : AuthService
{
  [HttpPost]
  public RootResponse Post(UserEditRequest userEditRequest, int id)
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != "OK")
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
    
    var apiUserService = new ApiUserService();
    apiUserService.Update(newUser);

    Response.StatusCode = 201;
    return new RootResponse { Status = "OK" };
  }
}
