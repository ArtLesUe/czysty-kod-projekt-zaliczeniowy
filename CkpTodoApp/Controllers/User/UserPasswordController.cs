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
  [Route("api/user/password/{id}")]
  [ApiController]
  public class UserPasswordController : ControllerBase
  {
    private readonly ILogger<UserPasswordController> _logger;

    public UserPasswordController(ILogger<UserPasswordController> logger)
    {
      _logger = logger;
    }

    [DisableCors]
    [HttpPost]
    public RootResponse Post(UserPasswordRequest userPasswordRequest, int id)
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

      if (apiToken.UserId == id)
      {
        Response.StatusCode = 406;
        return new RootResponse { Status = "self-deletion-forbidden" };
      }

      if (string.IsNullOrEmpty(userPasswordRequest.Password))
      {
        Response.StatusCode = 406;
        return new RootResponse { Status = "empty-password-not-permitted" };
      }

      try 
      {
        ApiUserModel user = new ApiUserModel(id);
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
}