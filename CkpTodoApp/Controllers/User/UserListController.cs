using System.Text.Json;
using CkpTodoApp.Constants;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/list")]
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
    
    var databaseManagerController = new DatabaseService();
    var resultSql = databaseManagerController.ExecuteSQLQuery(
      @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Name', Name,
            'Surname', Surname,
            'Email', Email
          )
        )
        FROM users
        ORDER BY id ASC;"
    );

    return JsonSerializer.Deserialize<List<ApiUserModel>>(resultSql);
  }
}