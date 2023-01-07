using CkpTodoApp.Commons;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task;

[Route(GlobalConstants.BASE_URL_TASK + "/check/{id:int}")]
[ApiController]
public class CheckTaskController : AuthService
{
    [HttpPatch]
    public RootResponse Patch(int id)
    {
        var rootResponse = CheckAuth();
    
        if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
        {
            return rootResponse;
        }
        
        var taskManager = new TaskService();
        
        try
        {
            taskManager.CheckTaskById(id);
        } 
        catch (Exception)
        {
            Response.StatusCode = StatusCodes.Status406NotAcceptable;
            return new RootResponse { Status = StatusCodeEnum.CheckingNotExistingForbidden.ToString() };
        }

        return new RootResponse { Status = StatusCodeEnum.Checked.ToString() };
    }
}