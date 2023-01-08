using System.Text.Json;
using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route(GlobalConstants.BASE_URL_USER + "/list")]
[ApiController]
public class UserListController : AuthService
{
  [HttpGet]
  public List<ApiUserModel>? Get()
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
      return new List<ApiUserModel>();
    }

    using (var context = new DatabaseFrameworkService())
    {
      List<ApiUserModel> users = context.ApiUserModels.Where(f => f.Id > 0).ToList();
      return users;
    }
  }
}