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
  [Route("api/user/register")]
  [ApiController]
  public class UserRegisterController : ControllerBase
  {
    private readonly ILogger<UserRegisterController> _logger;

    public UserRegisterController(ILogger<UserRegisterController> logger)
    {
      _logger = logger;
    }

    public static string Md5(string text)
    {
      MD5 hasher = MD5.Create();
      byte[] inputBytes = Encoding.ASCII.GetBytes(text);
      byte[] hashBytes = hasher.ComputeHash(inputBytes);
      return Convert.ToHexString(hashBytes);
    }

    [HttpPost]
    public RootResponse Post(UserRegisterRequest userRegisterRequest)
    {
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      string? jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new RootResponse { Status = "wrong-auth" };
      }

      ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      apiToken.Verify();

      if (apiToken.UserId == 0)
      {
        Response.StatusCode = 401;
        return new RootResponse { Status = "wrong-auth" };
      }

      if (!userRegisterRequest.Validate()) {
        Response.StatusCode = 422;
        return new RootResponse { Status = "wrong-data" };
      }

      ApiUserModel newUser = new ApiUserModel(
        0, 
        string.IsNullOrEmpty(userRegisterRequest.Name) ? "" : userRegisterRequest.Name,
        string.IsNullOrEmpty(userRegisterRequest.Surname) ? "" : userRegisterRequest.Surname,
        string.IsNullOrEmpty(userRegisterRequest.Email) ? "" : userRegisterRequest.Email,
        string.IsNullOrEmpty(userRegisterRequest.Password) ? "" : Md5(userRegisterRequest.Password),
        string.IsNullOrEmpty(userRegisterRequest.AboutMe) ? "" : userRegisterRequest.AboutMe,
        string.IsNullOrEmpty(userRegisterRequest.City) ? "" : userRegisterRequest.City,
        string.IsNullOrEmpty(userRegisterRequest.Country) ? "" : userRegisterRequest.Country,
        string.IsNullOrEmpty(userRegisterRequest.University) ? "" : userRegisterRequest.University
      );
      newUser.Save();

      Response.StatusCode = 201;
      return new RootResponse { Status = "OK" };
    }
  }
}
