using System.Text.Json;
using CkpTodoApp.DatabaseControllers;
using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.Task;
using CkpTodoApp.Services.ApiTokenService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.Task
{
  [Route("api/tasks/list")]
  [ApiController]
  public class TasksListController : ControllerBase
  {
    [HttpGet]
    public List<TaskModel>? Get()
    {
      Request.Headers.TryGetValue("token", out StringValues headerValues);
      var jsonWebToken = headerValues.FirstOrDefault();

      if (string.IsNullOrEmpty(jsonWebToken))
      {
        Response.StatusCode = 401;
        return new List<TaskModel>();
      }

      var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
      var apiTokenService = new ApiTokenService();
      apiTokenService.Verify(apiToken);
      
      if (apiToken.UserId == 0)
      {
        Response.StatusCode = 401;
        return new List<TaskModel>();
      }

      var databaseManagerController = new DatabaseServiceController();
      var resultSql = databaseManagerController.ExecuteSQLQuery(
        @"SELECT json_group_array( 
          json_object(
            'Id', Id,
            'Title', Title,
            'Description', Description,
            'IsCheck', IsCheck
          )
        )
        FROM tasks
        ORDER BY id ASC;"
      );

      return JsonSerializer.Deserialize<List<TaskModel>>(resultSql);
    }
  }
}
