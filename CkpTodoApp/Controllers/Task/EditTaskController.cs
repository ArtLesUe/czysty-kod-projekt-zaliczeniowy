using CkpTodoApp.Commons;
using CkpTodoApp.Requests;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task;

[Route(GlobalConstants.BASE_URL_TASK + "/edit/{id:int}")]
[ApiController]
public class EditTaskController : AuthService
{
    [HttpOptions]
    public void Options()
    {
      return;
    }

    [HttpPatch]
    public RootResponse Patch(TaskRequest taskRequest, int id)
    {   
        var rootResponse = CheckAuth();
    
        if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
        {
            return rootResponse;
        }

        var taskManager = new TaskService();
        taskManager.EditTaskById(id, taskRequest.Title, taskRequest.Description);
  
        Response.StatusCode = StatusCodes.Status201Created;
        return new RootResponse { Status = StatusCodeEnum.Ok.ToString() };
    }
}
