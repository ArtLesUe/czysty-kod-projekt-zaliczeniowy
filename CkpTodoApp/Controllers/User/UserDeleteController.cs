using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiTokenService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/delete/{id:int}")]
[ApiController]
public class UserDeleteController : ControllerBase
{
  [HttpGet]
  public RootResponse Get(int id)
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    var apiTokenService = new ApiTokenService();
    apiTokenService.Verify(apiToken);
    
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

    try 
    { 
      var apiUserModel = new ApiUserModel(id);
      apiUserModel.Delete();
    } 
    catch (Exception)
    {
      Response.StatusCode = 406;
      return new RootResponse { Status = "deleting-not-existing-forbidden" };
    }

    return new RootResponse { Status = "deleted" };
  }
}