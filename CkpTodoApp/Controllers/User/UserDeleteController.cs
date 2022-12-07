using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using CkpTodoApp.Models;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers
{
  [Route("api/user/delete/{id}")]
  [ApiController]
  public class UserDeleteController : ControllerBase
  {
    private readonly ILogger<UserDeleteController> _logger;

    public UserDeleteController(ILogger<UserDeleteController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public RootResponse Get(int id)
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

      try 
      { 
        ApiUserModel apiUserModel = new ApiUserModel(id);
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
}
