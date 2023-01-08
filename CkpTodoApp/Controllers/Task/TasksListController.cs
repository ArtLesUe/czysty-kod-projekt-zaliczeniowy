using System.Text.Json;
using CkpTodoApp.Commons;
using CkpTodoApp.Models.ApiUser;
using CkpTodoApp.Models.Task;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.DatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task
{
  [Route(GlobalConstants.BASE_URL_TASK + "/list")]
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

      using (var context = new DatabaseFrameworkService())
      {
        List<TaskModel> tasks = context.TaskModels.Where(f => f.Id > 0).ToList();
        return tasks;
      }
    }
  }
}
