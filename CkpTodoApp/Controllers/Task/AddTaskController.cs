using CkpTodoApp.Models;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Task;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace CkpTodoApp.Controllers
{
    [Route("api/task/add")]
    [ApiController]
    public class AddTaskController : ControllerBase
    {
      [DisableCors]
      [HttpPost]
      public RootResponse Post(TaskRequest taskRequest)
      {
        Request.Headers.TryGetValue("token", out StringValues headerValues);
        string? jsonWebToken = headerValues.FirstOrDefault();

        if (string.IsNullOrEmpty(jsonWebToken))
        {
          Response.StatusCode = 401;
          return new RootResponse { Status = "wrong-auth" };
        }

        ApiTokenModel apiToken = new ApiTokenModel(0, 0, jsonWebToken);
        apiToken.Verify();

        if (apiToken.UserId == 0)
        {
          Response.StatusCode = 401;
          return new RootResponse { Status = "wrong-auth" };
        }
        
        var newTask = new TaskModel(
          taskRequest.Title ?? "", 
          taskRequest.Description ?? "", 
          false
          );

        var taskManager = new TaskManager();
        taskManager.Add(newTask);
  
        Response.StatusCode = 201;
        return new RootResponse { Status = "OK" };
      }
    }
}