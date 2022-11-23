using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models;
using CkpTodoApp.Responses;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace CkpTodoApp.Controllers
{
  [Route("api/user/list")]
  [ApiController]
  public class UserListController : ControllerBase
  {
    private readonly ILogger<UserLoginTokenController> _logger;

    public UserListController(ILogger<UserLoginTokenController> logger)
    {
      _logger = logger;
    }

    [DisableCors]
    [HttpGet]
    public List<ApiUserModel>? Get()
    {
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      string? jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new List<ApiUserModel>();
      }

      ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      apiToken.Verify();

      if (apiToken.UserId == 0)
      {
        Response.StatusCode = 401;
        return new List<ApiUserModel>();
      }

      DatabaseManagerController databaseManagerController = new DatabaseManagerController();
      String resultSql = databaseManagerController.ExecuteSQLQuery(
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
}
