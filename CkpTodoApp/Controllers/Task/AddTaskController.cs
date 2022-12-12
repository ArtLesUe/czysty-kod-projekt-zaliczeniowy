using CkpTodoApp.Models.ApiToken;
using CkpTodoApp.Models.Task;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers.Task;

[Route("api/task/add")]
[ApiController]
public class AddTaskController : ControllerBase
{
  [HttpPost]
  public RootResponse Post(TaskRequest taskRequest)
  {
    Request.Headers.TryGetValue("token", out StringValues headerValues);
    var jsonWebToken = headerValues.FirstOrDefault();

    if (string.IsNullOrEmpty(jsonWebToken))
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }

    var apiToken = new ApiTokenModel(0, 0, jsonWebToken);
    apiToken.Verify();

    if (apiToken.UserId == 0)
    {
      Response.StatusCode = 401;
      return new RootResponse { Status = "auth-failed" };
    }
        
    if (string.IsNullOrEmpty(taskRequest.Title))
    {
      Response.StatusCode = 422;
      return new RootResponse { Status = "wrong-data" };
    }
        
    var newTask = new TaskModel(
      taskRequest.Title ?? "", 
      taskRequest.Description ?? "");

    var taskManager = new TaskManager();
    taskManager.Add(newTask);
  
    Response.StatusCode = 201;
    return new RootResponse { Status = "OK" };
  }
}