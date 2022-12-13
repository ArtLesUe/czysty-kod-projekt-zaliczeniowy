using System.Text.Json;
using CkpTodoApp.Commons;
using CkpTodoApp.Models.Task;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task
{
  [Route("api/tasks/list")]
  [ApiController]
  public class TasksListController : AuthService
  {
    [HttpGet]
    public List<TaskModel>? Get()
    {
      var rootResponse = CheckAuth();
    
      if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
      {
        return new List<TaskModel>();
      }

      var databaseManagerController = new DatabaseService();
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
