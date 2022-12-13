﻿using System.Security.Cryptography;
using System.Text;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Requests.User;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiTokenService;
using CkpTodoApp.Services.ApiUserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/register")]
[ApiController]
public class UserRegisterController : ControllerBase
{
  public static string Md5(string text)
  {
    var hasher = MD5.Create();
    var inputBytes = Encoding.ASCII.GetBytes(text);
    var hashBytes = hasher.ComputeHash(inputBytes);
    return Convert.ToHexString(hashBytes);
  }

  [HttpPost]
  public RootResponse Post(UserRegisterRequest userRegisterRequest)
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "wrong-auth" };
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    var apiTokenService = new ApiTokenService();
    apiTokenService.Verify(apiToken);
    
    if (apiToken.UserId == 0)
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "wrong-auth" };
    }

    if (!userRegisterRequest.Validate()) {
      Response.StatusCode = 422;
      return new RootResponse { Status = "wrong-data" };
    }

    var newUser = new ApiUserModel(
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
    
    var apiUserService = new ApiUserService();
    apiUserService.Save(newUser);

    Response.StatusCode = 201;
    return new RootResponse { Status = "OK" };
  }
}