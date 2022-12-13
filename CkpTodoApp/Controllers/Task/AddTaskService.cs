using CkpTodoApp.Constants;
using CkpTodoApp.Models.Task;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
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
    
    if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
    {
      return rootResponse;
    }
    
    if (string.IsNullOrEmpty(taskRequest.Title))
    {
      Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
      return new RootResponse { Status = StatusCodeEnum.WrongData.ToString() };
    }
      
    var newTask = new TaskModel(
      taskRequest.Title ?? "", 
      taskRequest.Description ?? "");

    var taskManager = new TaskService();
    taskManager.Add(newTask);
    
    Response.StatusCode = StatusCodes.Status201Created;
    return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };

  }
}