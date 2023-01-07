using CkpTodoApp.Commons;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task;

[Route(GlobalConstants.BASE_URL_TASK + "/delete/{id}")]
[ApiController]
public class DeleteTaskController : AuthService
{
    [HttpGet]
    public RootResponse Get(int id)
    {
        var rootResponse = CheckAuth();
    
        if (rootResponse.Status != StatusCodeEnum.Ok.ToString())
        {
            return rootResponse;
        }

        var taskManager = new TaskService();
        
        try
        {
            taskManager.DeleteTaskById(id);
        } 
        catch (Exception)
        {
            Response.StatusCode = StatusCodes.Status406NotAcceptable;
            return new RootResponse { Status = StatusCodeEnum.DeletingNotExistingForbidden.ToString() };
        }

        return new RootResponse { Status = StatusCodeEnum.Deleted.ToString() };
    }
}