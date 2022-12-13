using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.Task;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.ApiTokenService;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task;

[Route("api/task/add")]
[ApiController]
public class AddTaskService : AuthService
{
  [HttpPost]
  public RootResponse Post(TaskRequest taskRequest)
  {
    var rootResponse = CheckAuth();
    
    if (rootResponse.Status != "OK")
    {
      return rootResponse;
    }
    
    if (string.IsNullOrEmpty(taskRequest.Title))
    {
      Response.StatusCode = 422;
      return new RootResponse { Status = "wrong-data" };
    }
      
    var newTask = new TaskModel(
      taskRequest.Title ?? "", 
      taskRequest.Description ?? "");

    var taskManager = new TaskService();
    taskManager.Add(newTask);
    
    Response.StatusCode = 201;
    return new RootResponse { Status = "OK" };

  }
}