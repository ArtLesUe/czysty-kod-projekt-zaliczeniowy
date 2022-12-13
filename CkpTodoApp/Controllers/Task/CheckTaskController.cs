using CkpTodoApp.Constants;
using CkpTodoApp.Responses;
using CkpTodoApp.Services.AuthService;
using CkpTodoApp.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace CkpTodoApp.Controllers.Task;

[Route("api/task/check/{id:int}")]
[ApiController]
public class CheckTaskController : AuthService
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
            taskManager.CheckTaskById(id);
        } 
        catch (Exception)
        {
            Response.StatusCode = 406;
            return new RootResponse { Status = StatusCodeEnum.CheckingNotExistingForbidden.ToString() };
        }

        return new RootResponse { Status = StatusCodeEnum.Checked.ToString() };
    }
}