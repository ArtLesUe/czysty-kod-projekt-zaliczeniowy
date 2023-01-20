using System.Text.Json;
using CkpTodoApp.Commons;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.ApiTokenService;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using CkpTodoApp.Services.AuthService;

namespace CkpTodoApp.Controllers.User;

[Route(GlobalConstants.BASE_URL_USER + "/details/{id:int}")]
[ApiController]
public class UserDetailsController : AuthService
{
  [HttpOptions]
  public void Options()
  {
    return;
  }

  [HttpGet]
  public List<ApiUserModel>? Get(int id)
  {
    var rootResponse = CheckAuth();

    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        return new List<ApiUserModel>();
    }

    using (var context = new DatabaseFrameworkService())
    {
      List<ApiUserModel> users = context.ApiUserModels.Where(f => f.Id == id).ToList();
      
      if (users.Count > 0)
        users[0].PasswordHashed = "";

      return users;
    }
  }
}