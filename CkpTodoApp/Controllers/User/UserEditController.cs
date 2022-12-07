using CkpTodoApp.Models;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace CkpTodoApp.Controllers
{
  [Route("api/user/edit/{id}")]
  [ApiController]
  public class UserEditController : ControllerBase
  {
    private readonly ILogger<UserEditController> _logger;

    public UserEditController(ILogger<UserEditController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    public RootResponse Post(UserEditRequest userEditRequest, int id)
    {
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      string? jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new RootResponse { Status = "auth-failed" };
      }

      ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      apiToken.Verify();

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

      ApiUserModel newUser = new ApiUserModel(
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
}
