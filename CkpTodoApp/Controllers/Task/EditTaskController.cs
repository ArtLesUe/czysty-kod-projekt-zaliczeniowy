using CkpTodoApp.Constants;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task;

[Route("api/task/edit/{id:int}")]
[ApiController]
public class EditTaskController : AuthService
{
    [HttpPost]
    public RootResponse Post(TaskRequest taskRequest, int id)
    {   
        var rootResponse = CheckAuth();
    
        if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
        {
            return rootResponse;
        }

        var taskManager = new TaskService();
        taskManager.EditTaskById(id, taskRequest.Title, taskRequest.Description);
  
        Response.StatusCode = 201;
        return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
    }
}
