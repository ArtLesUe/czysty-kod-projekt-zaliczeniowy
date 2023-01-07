using System.Security.Cryptography;
using System.Text;
using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiUserService;
using CkpTodoApp.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route(GlobalConstants.BASE_URL_USER + "/register")]
[ApiController]
public class UserRegisterController : AuthService
{
  [HttpPost]
  public RootResponse Post(UserRegisterRequest userRegisterRequest)
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
      return rootResponse;
    }

    if (!userRegisterRequest.Validate()) {
      Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
      return new RootResponse { Status = StatusCodeEnum.WrongData.ToString() };
    }

    var newUser = new ApiUserModel(
      0, 
      string.IsNullOrEmpty(userRegisterRequest.Name) ? "" : userRegisterRequest.Name,
      string.IsNullOrEmpty(userRegisterRequest.Surname) ? "" : userRegisterRequest.Surname,
      string.IsNullOrEmpty(userRegisterRequest.Email) ? "" : userRegisterRequest.Email,
      string.IsNullOrEmpty(userRegisterRequest.Password) ? "" : Md5HashGenerator.TextToMd5(userRegisterRequest.Password),
      string.IsNullOrEmpty(userRegisterRequest.AboutMe) ? "" : userRegisterRequest.AboutMe,
      string.IsNullOrEmpty(userRegisterRequest.City) ? "" : userRegisterRequest.City,
      string.IsNullOrEmpty(userRegisterRequest.Country) ? "" : userRegisterRequest.Country,
      string.IsNullOrEmpty(userRegisterRequest.University) ? "" : userRegisterRequest.University
    );
    
    var apiUserService = new ApiUserService();
    apiUserService.Save(newUser);

    Response.StatusCode = StatusCodes.Status201Created;
    return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
  }
}