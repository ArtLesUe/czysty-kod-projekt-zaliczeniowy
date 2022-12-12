using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.User;

[Route("api/user/list")]
[ApiController]
public class UserListController : ControllerBase
{
  [HttpGet]
  public List<ApiUserModel>? Get()
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = 401;
      return new List<ApiUserModel>();
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    apiToken.Verify();

    if (apiToken.UserId == 0)
    {
      Response.StatusCode = 401;
      return new List<ApiUserModel>();
    }

    var databaseManagerController = new DatabaseManagerController();
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